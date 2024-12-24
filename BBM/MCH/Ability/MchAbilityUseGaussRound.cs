using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace BBM.MCH.Ability;

public class MchAbilityUseGaussRound : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        // 检查是否是热冲击技能，并且是最后使用的 GCD 技能
        if (!Core.Resolve<JobApi_Machinist>().OverHeated &&
            Core.Resolve<MemApiSpellCastSuccess>().LastGcd == SpellsDefine.HeatBlast)
        {
            // 如果是热冲击最后的 GCD 技能，返回 -8，表示不触发后续技能（防止双叉）
            return -8;
        }

        if (Core.Me.Level < 74)
        {
            // 如果 GCD 冷却时间小于 600ms，不能释放技能，返回 -2
            if (GCDHelper.GetGCDCooldown() <= 600)
                return -2;

            // 如果 GaussRound 或 Ricochet 的充能数大于 1，表示技能可以使用
            if (SpellsDefine.GaussRound.GetSpell().Charges > 1f || SpellsDefine.Ricochet.GetSpell().Charges > 1f)
            {
                return 1;
            }
        }

        // 如果 GaussRound 和 Ricochet 都不可用，返回 -1
        if (!SpellsDefine.GaussRound.GetSpell().IsReadyWithCanCast() &&
            !SpellsDefine.Ricochet.GetSpell().IsReadyWithCanCast())
            return -1;

        // 如果 GCD 冷却时间小于等于 600ms，不能释放技能，返回 -2
        if (GCDHelper.GetGCDCooldown() < 600)
            return -2;

        // 如果 GaussRound 或 Ricochet 达到最大充能，表示可以释放技能
        if (SpellsDefine.GaussRound.IsMaxChargeReady() || SpellsDefine.Ricochet.IsMaxChargeReady())
            return 1;
        
        // 如果 Wildfire 技能准备好，返回 -2，表示暂时不释放其他技能
        if (SpellsDefine.Wildfire.GetSpell().IsReadyWithCanCast())
        {
            return -2;
        }

        // 检查是否可以释放强威力 GCD 技能
        if (MchSpellHelper.CheckReassmableGcd(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs,
                out var strongGcd))
        {
            // 如果 Overheated 状态没有堆叠，且 Reassemble 技能准备好，则返回 -3
            if (Core.Resolve<MemApiBuff>().GetStack(Core.Me, AurasDefine.Overheated) < 1)
            {
                if (SpellsDefine.Reassemble.GetSpell().IsReadyWithCanCast())
                {
                    return -3;
                }
            }
        }

        // 如果机工士处于过热状态，返回 1，表示可以使用技能

        if (Core.Resolve<JobApi_Machinist>().OverHeated)
            return 1;

        // 如果 GaussRound 或 Ricochet 充能数大于 1，表示技能仍在冷却，返回 0
        if (SpellsDefine.GaussRound.GetSpell().Charges > 1 || SpellsDefine.Ricochet.GetSpell().Charges > 1)
        {
            return 0;
        }

        // 默认返回 0，表示当前没有释放技能的条件
        return 0;
    }

    public void Build(Slot slot)
    {
        var spellData = MchSpellHelper.GetGaussRound();
        if (spellData == null)
            return;
        slot.Add(spellData);
    }
}