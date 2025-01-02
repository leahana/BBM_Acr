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
        if (validationResult < 0)
        {
            return validationResult;
        }

        // 过热状态 将死>双将 防止出现 热冲击1+将死，热冲击2+将死的情况，看着难受！
        if (MchSpellsHelper.OverHeated() && CheckMate.GetCharges() >= 1)
            return CheckMate.GetCharges() > DoubleCheck.GetCharges() ? 3 : -3;

        return CheckMate.GetCharges() >= 1 && this.LastAbility() != CheckMate ? 0 : -3;
    }

    public void Build(Slot slot) => slot.Add(CheckMate.GetSpell());


    public int CheckQt()
    {
        // 检查Qt配置
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}