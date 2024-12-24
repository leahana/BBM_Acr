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
        //如果整备还没准备好,不打整备,充能技能至少有1层,IsReady才返回True
        if (!SpellsDefine.Reassemble.GetSpell().IsReadyWithCanCast())
            return -1;
        if (GCDHelper.GetGCDCooldown() <= 600)
            return -2;

        //有过热不开整备
        if (Core.Me.HasAura(AurasDefine.Reassembled))
        {
            return -3;
        }

        if (SpellsDefine.Reassemble.RecentlyUsed(1200))
        {
            return -1;
        }

        if (!MchSpellHelper.CheckReassmableGcd(GCDHelper.GetGCDCooldown(), out var strongGcd))
            return -4;
        if (strongGcd == SpellsDefine.HotShot)
        {
            return -5;
        }

        // 判断一个gcd的70%时间 (一般是2500*0.7 = 1850ms左右)内,是否至少有一个强威力gcd冷却完毕
        /*var time = Core.Resolve<MemApiSpell>().GetGCDDuration(false) * 0.7f;
        if (!MCHSpellHelper.CheckReassmableGCD((int)time, out var strongGcd))
            return -3;*/
        if (Core.Resolve<JobApi_Machinist>().OverHeated)
            return -1;
        /*if (AI.Instance.BattleData.LimitAbility)
        {
            return -2;
        }*/

        // 野火好了
        if (SpellsDefine.Wildfire.GetSpell().IsReadyWithCanCast())
        {
            // gcd是回转飞锯
            if (strongGcd == SpellsDefine.ChainSaw)
            {
                // 空气矛和钻头也没好
                if (!SpellsDefine.AirAnchor.GetSpell().IsReadyWithCanCast()
                    && !SpellsDefine.Drill.GetSpell().IsReadyWithCanCast())
                {
                    return -4;
                }
            }
        }


        return 0;
    }

    public void Build(Slot slot)
    {
        //var time = Core.Resolve<MemApiSpell>().GetGCDDuration(false) * 0.7f;
        //MCHSpellHelper.CheckReassmableGCD((int)time, out var strongGcd);
        // 找到对应的强威力gcd,接下来的技能使用就是整备+对应gcd
        slot.Add(SpellsDefine.Reassemble.GetSpell());
        //slot.Add(strongGcd.GetSpell());
    }
}