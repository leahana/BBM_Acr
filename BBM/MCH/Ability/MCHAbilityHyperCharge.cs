using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Settings;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 *  超 荷
 */
public class MchAbilityHyperCharge : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (CombatHelper.IsCooldownWithin(MchSettings.Instance.GcdCooldownLimit) 
            || MchSpells.Hypercharge.GetSpell().IsReadyWithCanCast())
        {
            return -1;
        }

        if (CombatHelper.HasBuff(MchBuffs.超荷预备))
        {
            return 1;
        }

        if (CombatHelper.IsHeatBelow(MchSettings.Instance.MaxHeat))
            return -3;

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Hypercharge.GetSpell());
    }
}