using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Managers;
using BBM.MCH.Settings;

namespace BBM.MCH;

public class MchRotationEventHandler : IRotationEventHandler
{
    /// <summary>
    /// 非战斗情况下的回调 例如远敏可以考虑此时唱跑步歌 T可以考虑切姿态
    /// </summary>
    public async Task OnPreCombat()
    {
        await Task.CompletedTask;
        //
        // // 如果开启了远敏技能 Peloton 的使用选项
        // if (MchSettings.Instance.UsePeloton && !Core.Me.HasAura(AurasDefine.Peloton))
        // {
        //     // 尝试释放 Peloton 技能
        //     await SpellsDefine.Peloton.GetSpell().Cast();
        // }
    }


    /// <summary>
    /// 在战斗重置(一般时团灭重来,脱战等)时触发
    /// </summary>
    public void OnResetBattle()
    {
        if (!SettingMgr.GetSetting<GeneralSettings>().NoClipGCD3)
            LogHelper.Print("请开启: 全局能力技不卡GCD");
        if (SettingMgr.GetSetting<GeneralSettings>().MaxAbilityTimesInGcd != 2)
            LogHelper.Print("请设置: GCD内最大能力技数量 = 2");
        if (!MchSettings.Instance.AutoResetBattleData)
            return;
        // 重制Qt设置
        MchQtManager.Instance.ResetQt();
        // 重制电量热量
        MchSettings.Instance.MinBattery = 50;
        MchSettings.Instance.MinHeat = 50;
        // 战斗数据重制
        MchCacheBattleData.Instance.Reset();
    }


    /// <summary>
    /// ACR默认再没目标时是不工作的 为了兼容没目标时的处理 比如舞者在转阶段可能要提前跳舞
    /// </summary>
    public async Task OnNoTarget()
    {
        await Task.CompletedTask;
    }


    /// <summary>
    /// 读条技能读条判定成功 (读条快结束 可以滑步的时间点)
    /// </summary>
    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
    }


    /// <summary>
    /// 某个技能使用之后的处理,比如诗人在刷Dot之后记录这次是否是强化buff的Dot
    /// 如果是读条技能，则是服务器判定它释放成功的时间点，比上面的要慢一点
    /// </summary>
    /// <param name="slot">这个技能归属的Slot</param>
    /// <param name="spell">某个使用完的技能</param>
    public void AfterSpell(Slot slot, Spell spell)
    {
    }


    /// <summary>
    /// 战斗中每帧都会触发的逻辑
    /// </summary>
    /// <param name="currTimeInMs">从战斗开始到现在的时间,单位毫秒(ms)</param>
    public void OnBattleUpdate(int currTimeInMs)
    {
    }

    /// <summary>
    /// 切到当前ACR
    /// </summary>
    public void OnEnterRotation()
    {
        LogHelper.Print("已切换ACR：BBM-Mch");
    }

    /// <summary>
    /// 从当前ACR退出
    /// </summary>
    public void OnExitRotation()
    {
        LogHelper.Print("退出ACR: BBM-Mch");
    }

    /// <summary>
    /// 地图切换时触发
    /// </summary>
    public void OnTerritoryChanged()
    {
        LogHelper.Print("地图已切换");
    }
}