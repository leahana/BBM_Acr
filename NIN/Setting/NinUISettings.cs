using AEAssist.GUI;
using ImGuiNET;

namespace BBM.Setting;

/**
 * ui设置
 */
public class NinUiSettings
{
    public static NinUiSettings Instance = new();
    
    public NinSettings NinSettings => NinSettings.Instance;

    public void Draw()
    {
        ImGui.Checkbox("测试使用baseItem1", ref NinSettings.Instance.BaseBottom1Boolean);
        ImGuiHelper.LeftInputInt("非爆发期Apex值达到多少时才使用", ref NinSettings.BaseBottom2Value);

        if (ImGui.Button("Save"))
        {
            NinSettings.Instance.Save();
        }
    }
}