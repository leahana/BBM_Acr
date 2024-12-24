using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

namespace BBM.MCH.Ability;

/**
 *  内丹
 */
public class MchAbilitySecondWind : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 600)
        {
            return -1;
        }

        if (!SpellsDefine.SecondWind.GetSpell().IsReadyWithCanCast())
            return -1;
        if (Core.Me.CurrentHpPercent() > 0.4)
        {
            return -2;
        }

        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.SecondWind.GetSpell());
    }
}