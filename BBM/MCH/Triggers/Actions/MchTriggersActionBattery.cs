using AEAssist.CombatRoutine.Trigger;
using BBM.MCH.Settings;
using ImGuiNET;

namespace BBM.MCH.Triggers.Actions;

/// <summary>
/// 时间轴电量控制行为
/// </summary>
public class MchTriggersActionBattery : ITriggerAction, ITriggerBase
{
    public string DisplayName => "BBM-Mch/行为/电量控制";

    private int Battery { get; set; } = 0;
    
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