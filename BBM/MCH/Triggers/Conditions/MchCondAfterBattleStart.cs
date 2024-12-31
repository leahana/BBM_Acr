using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.Extension;
using AEAssist.GUI.Tree;

namespace BBM.MCH.Triggers.Conditions;

public class MchCondAfterBattleStart : ITriggerCond, ITriggerlineCheck
{
    public string DisplayName => "BBM-Mch/条件/战斗开始后多少秒";
    public string Remark { get; set; }
    public int Delay { get; set; }

    public bool Draw()
    {
        return false;
    }


    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        if (!Core.Me.IsDummy())
        {
            return false;
        }

        long battleTime = AI.Instance.BattleData.CurrBattleTimeInMs;
        return battleTime >= this.Delay * 1000;
    }

    public void Check(TreeCompBase parent, TreeNodeBase currNode, TriggerLine triggerLine, Env env,
        TriggerlineCheckResult checkResult)
    {
        if (Delay <= 0)
        {
            checkResult.AddError(currNode, "战斗时间应该大于0");
        }
    }
}