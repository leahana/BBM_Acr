using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using ImGuiNET;

namespace BBM.Triggers;

// 时间轴
public class TriggerActionQt : ITriggerAction
{
    public string DisplayName { get; } = "Nin/qt";
    public string Remark { get; set; }

    public string Key = "";
    public bool Value;

    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;
    private string[] _qtArray;

    public TriggerActionQt()
    {
        _qtArray = BbmNinRotationEntry.QT.GetQtArray();
    }

    public bool Draw()
    {
        _selectIndex = Array.IndexOf(_qtArray, Key);
        if (_selectIndex == -1)
        {
            _selectIndex = 0;
        }

        ImGuiHelper.LeftCombo("选择Key", ref _selectIndex, _qtArray);
        Key = _qtArray[_selectIndex];
        ImGui.SameLine();
        using (new GroupWrapper())
        {
            ImGui.Checkbox("", ref Value);
        }

        return true;
    }

    public bool Handle()
    {
        BbmNinRotationEntry.QT.SetQt(Key, Value);
        return true;
    }
}