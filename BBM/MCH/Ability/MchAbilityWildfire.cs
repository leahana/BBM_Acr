using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Ability;

/**
 * 野火
 */
public class MchAbilityWildfire (params string[] qtKeys): ISlotResolver
{
    
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.Wildfire))
            return -1;
        if (!this.CanInsertAbility())
            return -2;

        if (MchSpells.将死.GetCharges().Equals(3.0) && MchSpells.双将.GetCharges().Equals(3.0))
        {
            return -3;
        }
        if (!this.HasAura(MchBuffs.过热))
            return -4;

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Wildfire.GetSpell());
    }
}