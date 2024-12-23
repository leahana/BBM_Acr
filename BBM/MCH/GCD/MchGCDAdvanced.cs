using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;

namespace BBM.MCH.GCD;

public class MchGcdAdvanced:ISlotResolver
{
    public int Check()
    {
        if (!MchSpellHelper.CheckReassmableGCD(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs,
                out var strongGcd))
            return -2;
        if (Core.Resolve<JobApi_Machinist>().OverHeated)
            return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        MchSpellHelper.CheckReassmableGCD(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs,
            out var strongGcd);
        //if (SpellsDefine.Reassemble.IsReady()) slot.Add(SpellsDefine.Reassemble.GetSpell());
        if (Core.Resolve<JobApi_Machinist>().GetBattery > 80 && strongGcd == SpellsDefine.AirAnchor)
        {
            if (SpellsDefine.AutomationQueen.IsReady())
            {
                slot.Add(SpellsDefine.AutomationQueen.GetSpell());
            }
        }
        slot.Add(strongGcd.GetSpell());
    }
}