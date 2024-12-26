using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

public class MchGcdExcavator : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.Excavator)) return -1;
        if (!this.HasAura(MchBuffs.ExcavatorReady)) return -2;
        // 空气矛小于1000ms 不打
        if (MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1000.0) return -3;
        return !MchQtHelper.QtExcavator() ? -10 : 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Excavator.GetSpell());
    }
}