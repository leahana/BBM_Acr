using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
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
        if (!this.IsReady(MchSpells.Hypercharge))
            return -1;

        if (!this.CanInsertAbility())
            return -2;

        if (CombatHelper.IsHeatBelow(50) && !this.HasAura(MchBuffs.超荷预备))
            return -3;

        if (this.IsComboTimeWithin(9500.0) && this.IsComboTimeWithOut(0.0))
            return -5;

        if (this.IsCooldownWithin(MchSpells.AirAnchor, 8000.0)
            || this.IsCooldownWithin(MchSpells.ChainSaw, 8000.0)
            || MchSpells.Drill.GetCharges() > 1.6)
            return -6;
        
        if (this.GetHeat() < 100 && this.IsCooldownWithin(MchSpells.BarrelStabilizer, 35000.0))
            return -7;
     
        if (this.HasAura(MchBuffs.掘地飞轮预备) || this.HasAura(MchBuffs.全金属爆发预备))
            return -8;
        
        if (this.HasAura(MchBuffs.超荷预备))
            return this.IsCooldownWithin(MchSpells.Wildfire, 500.0) ? 1 : -9;
        
        return this.GetHeat() >= 50 ? 2 : -10;
    }
    

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Hypercharge.GetSpell());
    }
}