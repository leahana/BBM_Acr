using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.GUI.Tree;
using BBM.MCH.Settings;
using ImGuiNET;

namespace BBM.MCH.Triggers.Actions;

/// <summary>
/// 时间轴电量控制行为
/// </summary>
public class MchTriggerActionBattery : ITriggerAction, ITriggerBase, ITriggerlineCheck
{
    public string DisplayName => "BBM-Mch/行为/电量控制-设置电量阈值";

    public int Battery { get; set; } = 0;

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text($"电量>={Battery}时放机器人");
        ImGui.Text($"电量应为整数");
        return false;
    }

    public bool Handle()
    {
        MchSettings.Instance.MinBattery = Battery;
        return true;
    }

    public void Check(TreeCompBase parent, TreeNodeBase currNode, TriggerLine triggerLine, Env env,
        TriggerlineCheckResult checkResult)
    {
        if (Battery <= 0)
        {
            checkResult.AddError(currNode, "战斗时间应该大于0");
        }
    }
}