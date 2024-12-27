using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 空气矛
 */
public class MchGcdAirAnchor : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.AirAnchor))
        {
            return -1;
        }
        // 防止短连击
        if (this.IsComboTimeWithin(3000.0) && MchSpellHelper.GetLastComboSpellId() != MchSpells.CleanShot)
        {
            return -3;
        }

        return this.IsGcdReadySoon() && this.IsCooldownWithin(MchSpells.AirAnchor, 1200)
            ? 1
            : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.AirAnchor.GetSpell());
}