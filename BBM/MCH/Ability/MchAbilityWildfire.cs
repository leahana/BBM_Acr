using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Ability;

/**
 * 野火
 */
public class MchAbilityWildfire : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.Wildfire))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        if (!this.HasAura(MchBuffs.过热))
            return -3;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Wildfire.GetSpell());
    }
}