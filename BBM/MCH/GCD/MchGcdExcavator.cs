using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.GCD;

public class MchGcdExcavator : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.Excavator)) return -1;
        if (!this.HasAura(MchBuffs.ExcavatorReady)) return -2;
        // if (MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1000.0) return -3;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Excavator.GetSpell());
    }
}