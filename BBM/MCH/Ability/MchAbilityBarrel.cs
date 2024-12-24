using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace BBM.MCH.Ability;

/**
 * 枪管加热
 */
public class MchAbilityBarrelStabilizer : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        // 高于50热量
        // if (Core.Resolve<JobApi_Machinist>().GetHeat > 50)
        //     return -1;
        if (!SpellsDefine.BarrelStabilizer.GetSpell().IsReadyWithCanCast())
            return -1;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.BarrelStabilizer.GetSpell());
    }
}