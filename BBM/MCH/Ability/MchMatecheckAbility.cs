using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Ability;

public class MchMatecheckAbility : ISlotResolver
{
    public int Check()
    {
        if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(36980U)))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // if (!MCHRotationEntry.QT.GetQt("爆发"))
        // return -3;
        // if (!MCHRotationEntry.QT.GetQt("双将将死"))
        // return -4;
        // if (MCHRotationEntry.QT.GetQt("保留两层双将将死") && (double) 36980U.充能层数() < 2.9)
        // return (double) 36980U.充能层数() >= 2.0 && Core.Resolve<JobApi_Machinist>().GetHeat >= 45 ? 1 : -5;

        if (Core.Resolve<JobApi_Machinist>().OverHeated)
            return MchSpells.将死.GetSpell().Cooldown.TotalMilliseconds <
                   MchSpells.双将.GetSpell().Cooldown.TotalMilliseconds
                ? 66
                : -66;
        // if (MCHRotationEntry.QT.GetQt("倾斜爆发"))
        // return 2;
        if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(36979U)) ||
            !SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(36980U)))
            return 20;
        double totalMilliseconds1 = SpellHelper.GetSpell(36980U).Cooldown.TotalMilliseconds;
        TimeSpan cooldown = SpellHelper.GetSpell(36979U).Cooldown;
        double totalMilliseconds2 = cooldown.TotalMilliseconds;
        if (totalMilliseconds1 < totalMilliseconds2)
            return 67;
        cooldown = SpellHelper.GetSpell(36980U).Cooldown;
        double totalMilliseconds3 = cooldown.TotalMilliseconds;
        cooldown = SpellHelper.GetSpell(36979U).Cooldown;
        double totalMilliseconds4 = cooldown.TotalMilliseconds;
        return totalMilliseconds3 == totalMilliseconds4 ? 76 : -67;
    }

    public void Build(Slot slot) => slot.Add(SpellHelper.GetSpell(36980U));
}