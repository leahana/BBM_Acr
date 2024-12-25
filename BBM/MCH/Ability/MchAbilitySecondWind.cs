using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Settings;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 *  内丹
 */
public class MchAbilitySecondWind : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.SecondWind.GetSpell().IsReadyWithCanCast())
            return -1;

        if (!CombatHelper.CanInsertAbility(MchSettings.Instance.GcdCooldownLimit))
        {
            return -2;
        }

        if (CombatHelper.GetHpPercent(0.4f))
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