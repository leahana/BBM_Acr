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
        if (!this.IsReady(MchSpells.Hypercharge))
            return -1;

        if (!this.CanInsertAbility())
            return -2;

        // 热量小于50 且没有超荷预备buff
        if (MchSpellHelper.IsHeatBelow(50) && !this.HasAura(MchBuffs.超荷预备))
            return -3;

        // 连击在0-9.5s不打超荷防止断连击
        if (this.IsComboTimeWithin(9500.0) && this.IsComboTimeWithOut(0.0))
            return -5;

        // 空气锚/飞锯/钻头>  第二层充能  -8s=1.6层
        if (this.IsCooldownWithin(MchSpells.AirAnchor, 8000.0)
            || this.IsCooldownWithin(MchSpells.ChainSaw, 8000.0)
            || MchSpells.Drill.GetCharges() > 1.6)
            return -6;

        // 能量小于100 且枪管加热cd35s内。存资源 
        // if (MchSpellHelper.GetHeat() < 100
        //     && this.IsCooldownWithin(MchSpells.BarrelStabilizer, 35000.0))
        //     return -7;

        if (this.HasAura(MchBuffs.掘地飞轮预备) || this.HasAura(MchBuffs.全金属爆发预备))
            return -8;
        // 120快好了 不打
        if (MchSpellHelper.GetHeat() < 100 &&
            MchSpells.BarrelStabilizer.Cooldown() <= 35000.0)
        {
            return -9;
        }

        var checkQt = CheckQt();
        if (checkQt != 0)
        {
            return checkQt;
        }

        // 120 超荷+野火
        if (this.HasAura(MchBuffs.超荷预备) && this.IsReady(MchSpells.Wildfire)) return 2;
        ;

        return MchSpellHelper.GetHeat() >= 50 ? 2 : -10;
    }


    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Hypercharge.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}