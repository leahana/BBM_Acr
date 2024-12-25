using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using BBM.MCH.Data;

namespace BBM.MCH.Utils;

/**
 * 战斗条件判断工具类
 */
public abstract class CombatHelper
{
    /// <summary>
    /// 判断技能是否可以使用
    /// </summary>
    /// <param name="spellId">技能 ID</param>
    /// <returns>是否可以使用</returns>
    public static bool IsSpellReady(uint spellId)
    {
        // 使用 Spell的拓展方法
        return spellId.GetSpell().IsReadyWithCanCast();
    }

    /// <summary>
    /// 判断技能是否处于指定冷却时间内
    /// </summary>
    /// <param name="spellId">技能 ID</param>
    /// <param name="cooldownThresholdMs">冷却时间阈值（毫秒）</param>
    /// <returns>是否在冷却时间内</returns>
    public static bool IsCooldownWithin(double cooldownThresholdMs)
    {
        return GCDHelper.GetGCDCooldown() <= cooldownThresholdMs;
    }


    /// <summary>
    /// 判断玩家是否存在特定 Buff
    /// </summary>
    /// <param name="buffId">Buff ID</param>
    /// <returns>是否存在 Buff</returns>
    public static bool HasBuff(uint buffId)
    {
        return Core.Me.HasAura(auraId: buffId);
    }


    /// <summary>
    /// 判断当前热量是否低于指定的阈值。
    /// </summary>
    /// <param name="threshold">指定的热量阈值（整数值）。</param>
    /// <returns>
    /// 如果当前热量小于指定的阈值，则返回 true；否则返回 false。
    /// </returns>
    /// <remarks>
    /// 该方法依赖于 JobApi_Machinist 类，获取当前角色的热量值进行比较。
    /// 可用于执行技能前的条件判断，例如判断是否需要过热技能或进入特殊状态。
    /// </remarks>
    public static bool IsHeatBelow(int threshold)
    {
        return Core.Resolve<JobApi_Machinist>().GetHeat < threshold;
    }

    /// <summary>
    /// 检查角色是否处于整备状态。
    /// </summary>
    /// <returns>如果角色处于整备状态，则返回 true；否则返回 false。</returns>
    public static bool IsReassembled()
    {
        return Core.Me.HasAura(MchBuffs.Reassembled);
    }

    /// <summary>
    /// 检查角色是否处于过热状态。
    /// </summary>
    /// <returns>如果角色处于过热状态，则返回 true；否则返回 false。</returns>
    public static bool IsOverheated()
    {
        return Core.Resolve<MemApiBuff>().BuffStackCount(Core.Me, MchBuffs.Overheated) > 0;
    }

    public static bool FullMetalFieldReady()
    {
        return Core.Me.HasAura(MchBuffs.FullMetalFieldReady);
    }

    public static bool ReassembledUsed(int threshold)
    {
        return MchSpells.Reassemble.RecentlyUsed(threshold);
    }

    public static bool QtFullMetalField()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchSpellsCnConstants.FullMetalField);
    }

    public static uint GetLastComboSpellId() => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
}