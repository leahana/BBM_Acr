using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
///  虹吸弹/双将 
/// </summary>
public class MchAbilityDoubleCheck : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.双将))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // 过热状态  虹吸cd 小于 弹射
        if (MchSpellHelper.OverHeated() && MchSpells.双将.GetCharges() >= 1)
        {
            return MchSpells.双将.Cooldown() > MchSpells.将死.Cooldown() ? -3 : 3;
        }

        // 好了就打
        return MchSpells.双将.GetCharges() >= 1 && this.LastAbility() != MchSpells.双将 ? 0 : -1;

        // if (!MCHRotationEntry.QT.GetQt("爆发"))
        // return -3;
        // if (!MCHRotationEntry.QT.GetQt("双将将死"))
        // return -4;
        // if (MCHRotationEntry.QT.GetQt("保留两层双将将死") && (double) 36979U.充能层数() < 2.9)
        // return (double) 36979U.充能层数() >= 2.0 && Core.Resolve<JobApi_Machinist>().GetHeat >= 45 ? 1 : -5;

        // if (MCHRotationEntry.QT.GetQt("倾斜爆发"))
        // return 2;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.双将.GetSpell());
}