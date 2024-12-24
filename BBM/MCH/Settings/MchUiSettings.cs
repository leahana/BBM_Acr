using AEAssist.GUI;
using ImGuiNET;

namespace BBM.MCH.Settings;

public class MchUiSettings
{
    public static readonly MchUiSettings Instance = new();

    private MchSettings MchSettings => MchSettings.Instance;

    public void Draw()
    {
        ImGui.Checkbox("速行", ref MchSettings.Instance.UsePeloton);
        ImGuiHelper.LeftInputInt("电量多少使用机器人", ref MchSettings.BatteryGaugeValue);
        if (ImGui.Button("Save"))
        {
            MchSettings.Instance.Save();
        }
    }
}