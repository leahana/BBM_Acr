using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using BBM.MCH.Utils;
using ImGuiNET;

namespace BBM.MCH.Triggers.Conditions;

public class MchTriggerCondHeat : ITriggerCond, ITriggerBase
{
    [LabelName("机工量谱_热量")] public int Heat { get; set; }

    public string DisplayName { get; } = "Mch/热量设置";

    public string Remark { get; set; }

    public bool Draw()
    {
        ImGui.Text($"热量>={Heat}时使用超荷");
        return false;
    }

    private static int GetHeat() => MchSpellHelper.GetHeat();

    public bool Handle(ITriggerCondParams triggerCondParams)
    {
        return GetHeat() >= Heat;
    }
}