using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Data;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

public class MchGcdDrill : ISlotResolver
{
    public int Check()
    {
        // if (!MCHRotationEntry.QT.GetQt("钻头"))
        //     return -2;
        if (Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds <= 3000.0 &&
            CombatHelper.GetLastComboSpellId() != 2873U &&
            Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds != 0.0)
            return -3;
        if (MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1500.0)
            //&& MCHRotationEntry.QT.GetQt("空气锚"))
            return -31;
        if (MchSpells.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= 1500.0)
            //&& MCHRotationEntry.QT.GetQt("回转飞锯"))
            return -10;
        return CombatHelper.IsCooldownWithin(500f) &&
               MchSpells.Drill.GetSpell().Cooldown.TotalMilliseconds <= 21000.0
            ? 1
            : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.Drill.GetSpell());
}