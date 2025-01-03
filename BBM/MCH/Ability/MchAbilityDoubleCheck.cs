using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
///  虹吸弹/双将 
/// </summary>
public class MchAbilityDoubleCheck(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑
    private const uint DoubleCheck = MchSpells.DoubleCheck;
    private const uint CheckMate = MchSpells.CheckMate;

    public int Check()
    {
        if (!this.IsReady(DoubleCheck))
            return -1;
        if (!this.CanInsertAbility())
            return -2;

        // 检查Qt配置
        var validationResult = CheckQt();

        // 处理爆发 Qt 关闭
        if (validationResult == -101)
        {
            return validationResult;
        }

        // 保留双将 Qt 开启 
        if (validationResult == -146)
        {
            var charges = MchSpells.DoubleCheck.GetSpell().Charges;

            // 保留两层防止溢出
            if (charges < 2.9)
            {
                // 大于2.3层且 （在过热 或 热量大于45）
                if (charges >= 2.3 && (MchSpellsHelper.OverHeated() || MchSpellsHelper.GetHeat() >= 45))
                {
                    return 146;
                }

                return -146;
            }
        }

        // 默认模式 爆发Qt开启&&保留将关闭 
        var doubleCheckCharges = DoubleCheck.GetCharges();
        var checkMateCharges = CheckMate.GetCharges();
        var isOverHeated = MchSpellsHelper.OverHeated();

        // 双将>将死 防止出现 热冲击1+双将，热冲击2+双将 的情况，看着难受
        if (isOverHeated && doubleCheckCharges >= 1)
        {
            return doubleCheckCharges > checkMateCharges ? 3 : -3;
        }

        return doubleCheckCharges >= 1 && this.LastAbility() != DoubleCheck ? 4 : -4;
    }


    public void Build(Slot slot) => slot.Add(DoubleCheck.GetSpell());

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}