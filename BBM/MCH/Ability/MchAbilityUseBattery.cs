using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Settings;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

public class MchAbilityUseBattery : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        // 1. 检查自动机女王技能是否准备好
        if (!this.IsReady(MchSpells.AutomationQueen)) return -1;

        // 2. GCD剩余时间小于 600 毫秒）
        if (!this.CanInsertAbility()) return -2;

        // 3. 检查是否已经有机器人
        if (MchSpellHelper.Robotactive()) return -3;

        // 4. 检查是否处于过热状态
        if (MchSpellHelper.OverHeated()) return -4;

        // 5. 检查超负荷技能是否最近1200ms使用过
        if (MchSpells.Hypercharge.RecentlyUsed()) return -5;

        // 6. 检查召唤技能的剩余时间
        if (MchSpellHelper.SummonRemain() > TimeSpan.Zero.Ticks) return -6;

        // 7. 检查蓄电量是否足够
        if (MchSpellHelper.GetBattery() < 50) return -7;

        var heat = MchSpellHelper.GetHeat();

        if (heat == 60 && this.IsCooldownWithin(MchSpells.ChainSaw, 2100.0))
        {
            return -8;
        }

        if (heat == 80 && this.HasAura(MchBuffs.ExcavatorReady))
        {
            return -9;
        }

        return MchSpellHelper.GetBattery() >= MchSettings.Instance.MinBattery ? 2 : -14;
    }


    public void Build(Slot slot)
    {
        slot.Add(MchSpells.AutomationQueen.GetSpell());
    }
}