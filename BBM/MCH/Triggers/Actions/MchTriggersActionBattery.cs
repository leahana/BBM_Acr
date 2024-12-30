using AEAssist.CombatRoutine.Trigger;
using BBM.MCH.Settings;
using ImGuiNET;

namespace BBM.MCH.Triggers.Actions;

/// <summary>
/// 时间轴电量控制行为
/// </summary>
public class MchTriggersActionBattery : ITriggerAction, ITriggerBase
{
    public string DisplayName { get; } = "Mch/电量设置";

    private int Battery { get; set; } = 0;

    public void Check()
    {
    }

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text($"电量>={Battery}时放机器人");
        return false;
    }

    public bool Handle()
    {
        MchSettings.Instance.MinBattery = Battery;
        return true;
    }
}