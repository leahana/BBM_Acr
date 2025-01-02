using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace BBM.MCH;

public class MchRotationEventHandler : IRotationEventHandler
{
    /// <summary>
    /// 非战斗情况下的回调 例如远敏可以考虑此时唱跑步歌 T可以考虑切姿态
    /// </summary>
    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }


    /// <summary>
    /// 在战斗重置(一般时团灭重来,脱战等)时触发
    /// </summary>
    public void OnResetBattle()
    {
    }

    /// <summary>
    /// ACR默认再没目标时是不工作的 为了兼容没目标时的处理 比如舞者在转阶段可能要提前跳舞
    /// </summary>
    public Task OnNoTarget()
    {
        return Task.CompletedTask;
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
        LogHelper.Print("已切换至acr：BBM-Mch");
    }

    /// <summary>
    /// 从当前ACR退出
    /// </summary>
    public void OnExitRotation()
    {
        LogHelper.Print("退出ACR");
    }

    /// <summary>
    /// 地图切换时触发
    /// </summary>
    public void OnTerritoryChanged()
    {
        LogHelper.Print("地图已切换");
    }
}