using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

public class MchGcdDrill : ISlotResolver
{
    public int Check()
    {
        if (this.IsComboTimeWithin(3000) && MchSpellHelper.GetLastComboSpellId() != MchSpells.CleanShot)
            return -2;
        if (this.IsCooldownWithin(MchSpells.AirAnchor, 1200.0))
            return -3;
        if (this.IsCooldownWithin(MchSpells.ChainSaw, 1200.0))
            return -4;
        return this.IsGcdReadySoon() && this.IsCooldownWithin(MchSpells.Drill, 21000.0)
            ? 1
            : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.Drill.GetSpell());
}