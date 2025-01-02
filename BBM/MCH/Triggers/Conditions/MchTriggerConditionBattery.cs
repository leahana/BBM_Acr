using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.GUI.Tree;
using BBM.MCH.Utils;
using ImGuiNET;

namespace BBM.MCH.Triggers.Conditions;

public class MchTriggerConditionBattery : ITriggerCond, ITriggerBase, ITriggerlineCheck
{
    public int Battery { get; set; } = 50;

    public string DisplayName { get; } = "BBM-Mch/条件/电量设置";

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text("数值应为0-100之间");
        ImGui.Text($"电量>={Battery}时使用机器人");
        return false;
    }

    private static int GetBattery() => MchSpellsHelper.GetBattery();

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return GetBattery() >= Battery;
    }

    public void Check(TreeCompBase parent, TreeNodeBase currNode, TriggerLine triggerLine, Env env,
        TriggerlineCheckResult checkResult)
    {
        if (Battery is >= 0 and <= 100)
            return;
        checkResult.AddError(currNode, "电量需要在0-100之间");
    }
}