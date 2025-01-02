using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.GUI.Tree;
using BBM.MCH.Utils;
using ImGuiNET;

namespace BBM.MCH.Triggers.Conditions;

public class MchTriggerCondHeat : ITriggerCond, ITriggerBase, ITriggerlineCheck
{
    public int Heat { get; set; } = 50;

    public string DisplayName { get; } = "BBM-Mch/条件/热量";

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text($"热量是否>={Heat}");
        return false;
    }

    private static int GetHeat() => MchSpellsHelper.GetHeat();

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return GetHeat() >= Heat;
    }

    public void Check(TreeCompBase parent, TreeNodeBase currNode, TriggerLine triggerLine, Env env,
        TriggerlineCheckResult checkResult)
    {
        if (Heat is >= 50 and <= 100)
            return;
        checkResult.AddError(currNode, "热量需要在50-100之间");
    }
}