
using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.GUI.Tree;

namespace BBM.MCH.Triggers;

public class MchCondAfterBattleStart:ITriggerCond,ITriggerlineCheck
{
    public bool Draw()
    {
        throw new NotImplementedException();
    }

    public string DisplayName { get; }
    public string Remark { get; set; }
    public bool Handle(ITriggerCondParams condParams = null)
    {
        throw new NotImplementedException();
    }

    public void Check(TreeCompBase parent, TreeNodeBase currNode, TriggerLine triggerLine, Env env,
        TriggerlineCheckResult checkResult)
    {
        throw new NotImplementedException();
    }
}