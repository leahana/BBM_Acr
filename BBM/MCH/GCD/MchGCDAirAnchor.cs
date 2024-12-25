using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Data;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 空气矛
 */
public class MchGcdAirAnchor : ISlotResolver
{
    public int Check()
    {
        // 上一个连击是狙击弹 不打空气矛
        if (Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds <= 3000.0 &&
            CombatHelper.GetLastComboSpellId() != MchSpells.CleanShot)
        {
            return -2;
        }

        return CombatHelper.IsCooldownWithin(500f)
               && MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1500.0
            ? 1
            : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.AirAnchor.GetSpell());
}