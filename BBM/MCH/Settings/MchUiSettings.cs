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
    }
    
}