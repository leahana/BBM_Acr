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
        if (!MchSpellHelper.CheckReassembleGcd(
                (SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs + 700),
                out var _))
            return -1;
        // 过热不打高伤害技能
        return MchSpellHelper.OverHeated() ? -2 : 0;
    }

    public void Build(Slot slot)
    {
        MchSpellHelper.CheckReassembleGcd(SettingMgr.GetSetting<GeneralSettings>().ActionQueueInMs + 700,
            out var spellId);
        slot.Add(spellId.GetSpell());
    }
}