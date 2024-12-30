using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using BBM.MCH.Utils;
using ImGuiNET;

namespace BBM.MCH.Triggers.Conditions;

public class MchTriggerConditionBattery : ITriggerCond, ITriggerBase
{
    [LabelName("机工量谱_电量")] public int Battery { get; set; }

    public string DisplayName { get; } = "Mch/电量设置";

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text($"电量>={Battery}时使用机器人");
        return false;
    }

    private static int GetBattery() => MchSpellHelper.GetBattery();

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return GetBattery() >= Battery;
    }
}