using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Settings;

namespace BBM.MCH.Extensions;

/// <summary>
/// 机工技能/buff 自定义拓展
/// </summary>
public static class MchSpellsExtension
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
        return GCDHelper.GetGCDCooldown() >= MchSettings.GcdCooldownLimit;
    }

    /// <summary>
    /// 检查技能是否可用
    /// </summary>
    /// <param name="spellId"></param>
    /// <returns></returns>
    public static bool CheckActionCanUse(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().CheckActionCanUse(spellId);
    }

    /// <summary>
    /// 上一个能力技
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    public static uint LastAbility(this ISlotResolver resolver)
    {
        return Core.Resolve<MemApiSpellCastSuccess>().LastAbility;
    }

    /// <summary>
    /// 判断自身是否有buff
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="auraId"></param>
    /// <returns></returns>
    public static bool HasAura(this ISlotResolver resolver, uint auraId)
    {
        return Core.Me.HasAura(auraId: auraId);
    }

    /// <summary>
    /// 连击时间小于等于
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="comboTime"></param>
    /// <returns></returns>
    public static bool IsComboTimeWithin(this ISlotResolver resolver, double comboTime)
    {
        return Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds <= comboTime;
    }


    /// <summary>
    /// 连击时间大于
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="comboTime"></param>
    /// <returns></returns>
    public static bool IsComboTimeWithOut(this ISlotResolver resolver, double comboTime)
    {
        return Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds > comboTime;
    }

    /// <summary>
    /// 冷却时间小于等于
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="spellId"></param>
    /// <param name="coolDownTime"></param>
    /// <returns></returns>
    public static bool IsCooldownWithin(this ISlotResolver resolver, uint spellId, double coolDownTime)
    {
        return spellId.GetSpell().Cooldown.TotalMilliseconds <= coolDownTime;
    }

    /// <summary>
    /// 当前gcd还有 500ms转好
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="cooldownTime"></param>
    /// <returns></returns>
    public static bool IsGcdReadySoon(this ISlotResolver resolver, double cooldownTime = 500)
    {
        return GCDHelper.GetGCDCooldown() <= cooldownTime;
    }


    /// <summary>
    /// 技能充能层数
    /// </summary>
    /// <param name="spellId"></param>
    /// <returns></returns>
    public static double GetCharges(this uint spellId)
    {
        return Core.Resolve<MemApiSpell>().GetCharges(spellId);
    }


    /// <summary>
    /// 技能冷却时间
    /// </summary>
    /// <param name="spellId"></param>
    /// <returns></returns>
    public static double Cooldown(this uint spellId)
    {
        return spellId.GetSpell().Cooldown.TotalMilliseconds;
    }
}