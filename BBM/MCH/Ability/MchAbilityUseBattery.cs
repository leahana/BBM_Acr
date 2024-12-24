using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;

namespace BBM.MCH.Ability;

public class MchAbilityUseBattery : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        var jobApi = Core.Resolve<JobApi_Machinist>();

        // 1. 检查自动机女王技能是否准备好
        if (!SpellsDefine.AutomationQueen.GetSpell().IsReadyWithCanCast()) return -3;

        // 2. 复唱GCD后段（剩余时间小于 800 毫秒）
        if (GCDHelper.GetGCDCooldown() <= 800) return -12;

        // 3. 检查是否已经有机器人
        if (jobApi.Robotactive) return -1;

        // 4. 检查是否处于过热状态
        if (jobApi.OverHeated) return -4;

        // 5. 检查超负荷技能是否最近使用过
        if (SpellsDefine.Hypercharge.RecentlyUsed()) return -5;

        // 6. 检查召唤技能的剩余时间
        if (jobApi.SummonRemain > TimeSpan.Zero.Ticks) return -6;

        // 7. 检查蓄电量是否足够（>=50）
        if (jobApi.GetBattery >= 50) return 1;

        // 8. 检查回转飞锯使用条件
        if (SpellsDefine.ChainSaw.IsUnlock() && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= 2500)
            return 2;

        // 9. 默认返回
        return -10;
    }

    public void Build(Slot slot)
    {
        // 如果角色等级小于 80，则释放 RookAutoturret（炮塔）。
        slot.Add(Core.Me.Level < 80 ? SpellsDefine.RookAutoturret.GetSpell() : SpellsDefine.AutomationQueen.GetSpell());
    }
}