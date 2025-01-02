using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Utils;

/**
 * 技工技能工具类。 用于复杂的判断
 */
public static class MchSpellsHelper
{
    /// <summary>
    /// 获取最后一次连击的id
    /// </summary>
    /// <returns></returns>
    public static uint GetLastComboSpellId() => Core.Resolve<MemApiSpell>().GetLastComboSpellId();


    /// <summary>
    /// 获取基础123连击
    /// </summary>
    /// <returns>下一个连击Spell</returns>
    public static Spell GetGcdBaseCombo()
    {
        var memApiSpell = Core.Resolve<MemApiSpell>(); // 提取重复调用
        var lastComboSpellId = memApiSpell.GetLastComboSpellId();

        // 根据 lastComboSpellId 返回对应的 Spell
        return lastComboSpellId switch
        {
            MchSpells.SplitShot => memApiSpell.CheckActionChange(MchSpells.SlugShot).GetSpell(),
            MchSpells.SlugShot => memApiSpell.CheckActionChange(MchSpells.CleanShot).GetSpell(),
            _ => memApiSpell.CheckActionChange(MchSpells.SplitShot).GetSpell()
        };
    }

    /// <summary>
    /// 获取可以整备的3大件cd
    /// </summary>
    /// <param name="timeleft">时间阈值（毫秒）</param>
    /// <param name="spellId">返回的技能 ID</param>
    /// <returns>是否存在优先技能</returns>
    public static bool CheckReassembleGcd(float timeleft, out uint spellId)
    {
        // 检查空气矛（优先级最高）
        var airAnchor = MchSpells.AirAnchor.GetSpell();
        if (airAnchor.Cooldown.TotalMilliseconds < timeleft || airAnchor.IsReadyWithCanCast())
        {
            spellId = MchSpells.AirAnchor;
            return true;
        }

        // 检查钻头
        var drill = MchSpells.Drill.GetSpell();
        if (drill.Cooldown.TotalMilliseconds < timeleft || drill.IsReadyWithCanCast())
        {
            spellId = MchSpells.Drill;
            return true;
        }

        // 检查回转飞锯
        var chainSaw = MchSpells.ChainSaw.GetSpell();
        if (chainSaw.Cooldown.TotalMilliseconds < timeleft || chainSaw.IsReadyWithCanCast())
        {
            // 额外判断野火和钻头的情况
            var wildfire = MchSpells.Wildfire.GetSpell();
            if (wildfire.IsReadyWithCanCast() && drill.Cooldown.TotalMilliseconds < timeleft)
            {
                spellId = MchSpells.Drill;
                return true;
            }

            // 默认返回回转飞锯
            spellId = MchSpells.ChainSaw;
            return true;
        }

        // 无可用技能
        spellId = 0;
        return false;
    }



    /// <summary>
    /// 是否有机器人
    /// </summary>
    /// <returns></returns>
    public static bool Robotactive() => Core.Resolve<JobApi_Machinist>().Robotactive;

    /// <summary>
    /// 是否过热状态
    /// </summary>
    /// <returns></returns>
    public static bool OverHeated() => Core.Resolve<JobApi_Machinist>().OverHeated;

    /// <summary>
    /// 判断当前热量是否低于指定的阈值。
    /// </summary>
    /// <param name="threshold">指定的热量阈值（整数值）</param>
    /// <returns>
    /// 如果当前热量小于指定的阈值，则返回 true；否则返回 false。
    /// </returns>
    public static bool IsHeatBelow(int threshold)
    {
        return Core.Resolve<JobApi_Machinist>().GetHeat < threshold;
    }

    /// <summary>
    /// 是否使用过整备
    /// </summary>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool ReassembledUsed(int threshold)
    {
        return MchSpells.Reassemble.RecentlyUsed(threshold);
    }

    public static bool WildFireRecentlyUsed(int threshold)
    {
        return MchSpells.Wildfire.RecentlyUsed(threshold);
    }


    /// <summary>
    /// 获取热量
    /// </summary>
    /// <returns></returns>
    public static int GetHeat()
    {
        return Core.Resolve<JobApi_Machinist>().GetHeat;
    }

    /// <summary>
    /// 获取电量
    /// </summary>
    /// <returns></returns>
    public static int GetBattery()
    {
        return Core.Resolve<JobApi_Machinist>().GetBattery;
    }

    public static long OverheatRemain()
    {
        return Core.Resolve<JobApi_Machinist>().OverheatRemain;
    }


    /// <summary>
    /// 机器人存在剩余时间
    /// </summary>
    /// <returns></returns>
    public static long SummonRemain()
    {
        return Core.Resolve<JobApi_Machinist>().SummonRemain;
    }

    /// <summary>
    /// 检查起手爆发技能 飞锯/空气矛/钻头/枪管加热/野火/
    /// </summary>
    public static bool CheckOpenerOutbreakSpells()
    {
        return MchSpells.ChainSaw.GetSpell().IsReadyWithCanCast()
               && MchSpells.AirAnchor.GetSpell().IsReadyWithCanCast()
               && MchSpells.Drill.IsMaxChargeReady()
               && MchSpells.BarrelStabilizer.GetSpell().IsReadyWithCanCast()
               && MchSpells.Wildfire.GetSpell().IsReadyWithCanCast();
    }
}