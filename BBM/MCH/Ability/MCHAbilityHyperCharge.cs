using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using BBM.MCH.Data;
using BBM.MCH.Settings;
using Dalamud.Game.ClientState.Objects.Types;

namespace BBM.MCH.Ability;

/**
 *  超 荷
 */
public class MchAbilityHyperCharge : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 600)
        {
            return -1;
        }

        if (!SpellsDefine.Hypercharge.GetSpell().IsReadyWithCanCast())
            return -1;

        if (Core.Me.HasAura(auraId: MchBuffs.超荷预备))
        {
            return 1;
        }
        
        if (Core.Resolve<JobApi_Machinist>().GetHeat < 50)
            return -3;

       
        /*if (!AI.Instance.Is2ndAbilityTime())
            return -103;*/

        // if (MchSettings.Instance.WildfireFirst && SpellsDefine.Wildfire.CoolDownInGCDs(3))
        //     if (!SpellsDefine.Wildfire.CoolDownInGCDs(1))
        //         return -1;
        // if (!MchSettings.Instance.WildfireFirst)
        //     if (MchSpellHelper.CheckReassmableGcd(MchSettings.Instance.StrongGCDCheckTime, out var strongGcd))
        //         return -5;
        // if (!SpellsDefine.Wildfire.GetSpell().IsReadyWithCanCast())
        //     if (MchSpellHelper.CheckReassmableGcd(MchSettings.Instance.StrongGCDCheckTime, out var strongGcd))
        //         return -5;
        // if (SpellsDefine.Wildfire.GetSpell().IsReadyWithCanCast())
        // {
        //     MchSpellHelper.CheckReassmableGcd(MchSettings.Instance.StrongGCDCheckTime, out var strongGcd);
        //     if (strongGcd == SpellsDefine.AirAnchor || strongGcd == SpellsDefine.Drill)
        //     {
        //         return -4;
        //     }
        // }
        // if (SpellsDefine.Reassemble.RecentlyUsed() || Core.Me.HasAura(AurasDefine.Reassembled))
        // {
        //     return -2;
        // }
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Hypercharge.GetSpell());
    }
}