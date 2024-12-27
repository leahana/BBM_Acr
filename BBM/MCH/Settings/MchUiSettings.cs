using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.GUI;
using ImGuiNET;

namespace BBM.MCH.Settings;

public class MchUiSettings
{
    public static readonly MchUiSettings Instance = new();

    private static MchSettings MchSettings => MchSettings.Instance;

    public void Draw()
    {
        ImGui.Checkbox("速行", ref MchSettings.Instance.UsePeloton);
        ImGuiHelper.LeftInputInt("电量多少使用机器人", ref MchSettings.MinBattery);
        ImGuiHelper.LeftInputInt("热量多少使用超荷", ref MchSettings.MaxHeat);
        if (ImGui.Button("Save"))
        {
            MchSettings.Instance.Save();
        }
    }

    public readonly JobViewSave JobViewSave = new()
    {
        MainColor = new Vector4(217f, 0.158f, 171f, 1f), //樱桃粉红
        QtLineCount = 2,
        QtWindowBgAlpha = 0.1f,
    }; // QT设置存档
}