using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using BBM.MCH;
using ImGuiNET;

namespace BBM.MCH.Triggers;

// 时间轴qt
public class TriggerActionQt : ITriggerAction
{
    public string DisplayName { get; } = "Mch/qt";
    public string Remark { get; set; }

    public string Key = "";
    public bool Value;

    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;
    private string[] _qtArray;

    public TriggerActionQt()
    {
        _qtArray = BbmMchRotationEntry.Qt.GetQtArray();
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
        BbmMchRotationEntry.Qt.SetQt(Key, Value);
        return true;
    }
}