using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
/// 弹射/将死 
/// </summary>
public class MchAbilityCheckMate(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑
    private const uint CheckMate = MchSpells.CheckMate;
    private const uint DoubleCheck = MchSpells.DoubleCheck;

    public int Check()
    {
        if (!this.IsReady(CheckMate))
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

        // 保留将死 Qt 开启 
        if (validationResult == -145)
        {
            var charges = MchSpells.CheckMate.GetSpell().Charges;

            // 保留两层防止溢出
            if (charges < 2.9)
            {
                // 大于2.3层且 （在过热 或 热量大于45）
                if (charges >= 2.3 && (MchSpellsHelper.OverHeated() || MchSpellsHelper.GetHeat() >= 45))
                {
                    return 145;
                }

                return -145;
            }
        }

        // 默认模式 爆发Qt开启&&保留将死关闭
        var checkMateCharges = CheckMate.GetCharges();
        var doubleCheckCharges = DoubleCheck.GetCharges();
        var isOverHeated = MchSpellsHelper.OverHeated();

        // 将死>双将 防止出现 热冲击1+将死，热冲击2+将死的情况，看着难受
        if (isOverHeated && checkMateCharges >= 1)
        {
            return checkMateCharges > doubleCheckCharges ? 3 : -3;
        }

        // 检查默认逻辑/防止gcd双插两次将死
        return checkMateCharges >= 1 && this.LastAbility() != CheckMate ? 4 : -4;
    }

    public void Build(Slot slot) => slot.Add(CheckMate.GetSpell());


    public int CheckQt()
    {
        // 检查Qt配置
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}