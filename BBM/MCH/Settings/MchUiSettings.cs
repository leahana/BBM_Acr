using AEAssist.GUI;
using ImGuiNET;

namespace BBM.MCH.Settings;

public class MchUiSettings
{
    public static MchUiSettings Instance = new();

    private MchSettings MchSettings => MchSettings.Instance;

    public void Draw()
    {
        ImGui.Checkbox("测试使用baseItem1", ref MchSettings.Instance.BaseBottom1Boolean);
        ImGuiHelper.LeftInputInt("电量多少用", ref MchSettings.BaseBottom2Value);

        if (ImGui.Button("Save"))
        {
            MchSettings.Instance.Save();
        }
    }
}