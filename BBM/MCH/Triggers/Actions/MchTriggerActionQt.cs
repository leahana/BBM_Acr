using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using ImGuiNET;

namespace BBM.MCH.Triggers.Actions;

// 时间轴qt
public class MchTriggerActionQt : ITriggerAction
{
    public string DisplayName { get; } = "BBM-Mch/行为/QT";
    public string Remark { get; set; }

    public string Key = "";
    public bool Value;

    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;
    private string[] _qtArray;

    public MchTriggerActionQt()
    {
        _qtArray = MchRotationEntry.Qt.GetQtArray();
    }

    public bool Draw()
    {
        _selectIndex = Array.IndexOf(_qtArray, Key);
        if (_selectIndex == -1)
        {
            _selectIndex = 0;
            _selectIndex = 0;
        }

        ImGuiHelper.LeftCombo("选择QT", ref this._selectIndex, this._qtArray);
        this.Key = this._qtArray[this._selectIndex];
        ImGui.Text("勾选为开启QT，不勾选则关闭");
        ImGui.Text("开/关  ");
        ImGui.SameLine();
        using (new GroupWrapper())
            ImGui.Checkbox("", ref this.Value);
        return true;
    }

    public bool Handle()
    {
        MchRotationEntry.Qt.SetQt(Key, Value);
        return true;
    }
}