using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Ability;

/**
 * 枪管加热
 */
public class MchAbilityBarrelStabilizer : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!this.IsReady(MchSpells.BarrelStabilizer))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.BarrelStabilizer.GetSpell());
    }
}