using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using BBM.MCH.Settings;

namespace BBM.MCH.Extensions;

public static class CommonExtensions
{
    /// <summary>
    /// 通用的 IsReady 检查逻辑
    /// </summary>
    /// <param name="resolver">当前的 ISlotResolver 实例</param>
    /// <param name="spellId">技能 ID</param>
    /// <returns>是否准备好</returns>
    public static bool IsReady(this ISlotResolver resolver, uint spellId)
    {
        return spellId.GetSpell().IsReadyWithCanCast();
    }


    /// <summary>
    /// 通用的 IsReady 检查逻辑
    /// </summary>
    /// <param name="resolver">当前的 ISlotResolver 实例</param>
    /// <returns>是否准备好</returns>
    public static bool CanInsertAbility(this ISlotResolver resolver)
    {
        return GCDHelper.GetGCDCooldown() >= MchSettings.Instance.GcdCooldownLimit;
    }

    /// <summary>
    /// 通用的 IsReady 检查逻辑
    /// </summary>
    /// <param name="resolver">当前的 ISlotResolver 实例</param>
    /// <param name="auraId"></param>
    /// <returns>是否准备好</returns>
    public static bool HasAura(this ISlotResolver resolver, uint auraId)
    {
        return Core.Me.HasAura(auraId: auraId);
    }

    public static bool IsComboTimeWithin(this ISlotResolver resolver, double comboTime)
    {
        return Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds <= comboTime;
    }

    public static bool IsComboTimeWithOut(this ISlotResolver resolver, double comboTime)
    {
        return Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds > comboTime;
    }

    public static bool IsCooldownWithin(this ISlotResolver resolver, uint spellId, double coolDownTime)
    {
        return spellId.GetSpell().Cooldown.TotalMilliseconds <= coolDownTime;
    }

    public static bool IsGcdReadySoon(this ISlotResolver resolver, double cooldownTime = 500)
    {
        return GCDHelper.GetGCDCooldown() <= cooldownTime;
    }

    public static double GetCharges(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetCharges(spellId);
    }

    public static int GetHeat(this ISlotResolver resolver)
    {
        return Core.Resolve<JobApi_Machinist>().GetHeat;
    }
}