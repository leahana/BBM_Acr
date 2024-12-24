using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;

namespace BBM.MCH.GCD;

/**
 * 空气矛 钻头 飞锯
 */
public class MchGcdAdvanced : ISlotResolver
{
    public int Check()
    {
        // 虹吸
        if (!MchSpellHelper.CheckReassmableGcd(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs,
                out var strongGcd))
            return -2;
        if (Core.Resolve<JobApi_Machinist>().OverHeated)
            return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        MchSpellHelper.CheckReassmableGcd(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs,
            out var strongGcd);
        // if (SpellsDefine.Reassemble.IsReady()) slot.Add(SpellsDefine.Reassemble.GetSpell());
        // 电量大于80 当前gcd是空气矛使用机器人
        if (Core.Resolve<JobApi_Machinist>().GetBattery > 80 && strongGcd == SpellsDefine.AirAnchor)
        {
            // 
            var spell = SpellsDefine.AutomationQueen.GetSpell();
            if (spell.IsReadyWithCanCast())
            {
                slot.Add(spell);
            }
        }

        slot.Add(strongGcd.GetSpell());
    }
}