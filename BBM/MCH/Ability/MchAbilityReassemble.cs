using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;

namespace BBM.MCH.Ability;

/**
 * off gcd 整备
 */
public class MchAbilityReassemble : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        var reassembleSpell = SpellsDefine.Reassemble.GetSpell();
        var wildfireSpell = SpellsDefine.Wildfire.GetSpell();
        var airAnchorSpell = SpellsDefine.AirAnchor.GetSpell();
        var drillSpell = SpellsDefine.Drill.GetSpell();

        // 如果整备技能还没有准备好
        if (!reassembleSpell.IsReadyWithCanCast()) return -1;

        // 判断 GCD 冷却时间是否足够
        if (GCDHelper.GetGCDCooldown() <= 600) return -2;

        // 检查是否已经有过热状态
        if (Core.Me.HasAura(AurasDefine.Reassembled)) return -3;

        // 检查整备是否在最近 1200 毫秒内已经使用过
        if (reassembleSpell.RecentlyUsed(1200)) return -1;

        // 检查是否能进行强威力 GCD 的释放
        if (!MchSpellHelper.CheckReassmableGcd(GCDHelper.GetGCDCooldown(), out var strongGcd)) return -4;

        // 如果当前强威力 GCD 是 HotShot，则不使用整备
        if (strongGcd == SpellsDefine.HotShot) return -5;

        // 检查是否处于过热状态
        if (Core.Resolve<JobApi_Machinist>().OverHeated) return -1;

        // 判断是否进入野火阶段，且在 GCD 内没有强威力技能可以使用
        if (wildfireSpell.IsReadyWithCanCast())
        {
            if (strongGcd == SpellsDefine.ChainSaw)
            {
                // 如果回转飞锯被选中，且空气矛和钻头都未准备好，则不使用整备
                if (!airAnchorSpell.IsReadyWithCanCast()
                    && !drillSpell.IsReadyWithCanCast())
                {
                    return -4;
                }
            }
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        // 添加整备技能到槽位
        slot.Add(SpellsDefine.Reassemble.GetSpell());
    }
}