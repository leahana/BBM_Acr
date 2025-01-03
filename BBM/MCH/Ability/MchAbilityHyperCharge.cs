using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 *  超 荷
 */
public class MchAbilityHyperCharge(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!this.IsReady(MchSpells.HyperCharge))
            return -1;

        if (!this.CanInsertAbility())
            return -2;

        // 连击在0-9.5s不打超荷防止断连击
        if (this.IsComboTimeWithin(9500.0) && this.IsComboTimeWithOut(0.0))
            return -3;

        // 热量小于50 且没有超荷预备buff
        if (MchSpellsHelper.IsHeatBelow(50) && !this.HasAura(MchBuffs.HyperChargeReady))
            return -4;

        // 空气锚/飞锯/钻头>  第二层充能  -8s=1.6层
        if (this.IsCooldownWithin(MchSpells.AirAnchor, 8000.0)
            || this.IsCooldownWithin(MchSpells.ChainSaw, 8000.0)
            || MchSpells.Drill.GetCharges() > 1.6)
            return -5;

        // 120快好了 不打
        if (MchSpellsHelper.GetHeat() < 100 && MchSpells.BarrelStabilizer.Cooldown() <= 35000.0)
        {
            return -6;
        }

        // 全金属爆发或者飞轮预备
        if (this.HasAura(MchBuffs.ExcavatorReady) || this.HasAura(MchBuffs.FullMetalFieldReady))
            return -7;


        var validationResult = CheckQt();

        if (validationResult < 0)
        {
            // -101 爆发关了 -111 超荷关了
            return validationResult;
        }


        // 120 超荷+野火
        if (this.HasAura(MchBuffs.HyperChargeReady) && this.IsReady(MchSpells.Wildfire)) return 2;
        
        return MchSpellsHelper.GetHeat() >= 50 ? 1 : -4;
    }


    public void Build(Slot slot)
    {
        slot.Add(MchSpells.HyperCharge.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}