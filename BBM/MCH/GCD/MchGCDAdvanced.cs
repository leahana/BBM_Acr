using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using BBM.MCH.Utils;

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
        // 判断当前gcd是否可以使用高威力技能 （ActionQueueInMs 这个参数表示动作队列的时间（单位：毫秒））
        MchSpellHelper.CheckReassmableGcd(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs, out var strongGcd);
        
        // 电量大于80 当前gcd是空气矛使用机器人
        if (Core.Resolve<JobApi_Machinist>().GetBattery >= 80 && strongGcd == SpellsDefine.AirAnchor)
        {
            //  判断蓄电量是否足够，以及当前 GCD 技能是否为空气矛
            var spell = SpellsDefine.AutomationQueen.GetSpell();
            if (spell.IsReadyWithCanCast())
            {
                slot.Add(spell);
            }
        }

        slot.Add(strongGcd.GetSpell());
    }
}