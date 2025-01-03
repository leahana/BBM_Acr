using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 * 野火
 */
public class MchAbilityWildfire(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    private const uint Wildfire = MchSpells.Wildfire;
    private const uint CheckMate = MchSpells.CheckMate;
    private const uint DoubleCheck = MchSpells.DoubleCheck;

    public int Check()
    {
        if (!this.IsReady(Wildfire))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        if (CheckMate.GetCharges().Equals(3.0) && DoubleCheck.GetCharges().Equals(3.0))
        {
            return -3;
        }

        if (!this.HasAura(MchBuffs.Overheated))
            return -4;

        // 爆发Qt关闭
        var validationResult = CheckQt();
        if (validationResult == -101) return -101;
        // 爆发Qt通过 返回野火Qt结果
        return validationResult;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Wildfire.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}