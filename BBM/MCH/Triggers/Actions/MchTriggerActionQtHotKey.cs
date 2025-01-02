using AEAssist.CombatRoutine.Trigger;
using AEAssist.CombatRoutine.Trigger.Node;
using AEAssist.GUI;
using AEAssist.GUI.Tree;
using BBM.MCH.Managers;
using ImGuiNET;

namespace BBM.MCH.Triggers.Actions;

/// <summary>
/// 时间轴行为：QtHotkey
/// </summary>
public class MchTriggerActionQtHotKey : ITriggerAction, ITriggerBase
{
    public string DisplayName { get; } = "BBM-Mch/行为/QTHotKey";
    public string Remark { get; set; }

    public string Key = "";
    public bool Value;

    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;
    private string[] _qtHotKeyArray;

    public MchTriggerActionQtHotKey()
    {
        _qtHotKeyArray = MchQtManager.Qt.GetHotkeyArray();
    }

    public bool Draw()
    {
        _selectIndex = Array.IndexOf(_qtHotKeyArray, Key);
        if (_selectIndex == -1)
        {
            _selectIndex = 0;
            _selectIndex = 0;
        }

        ImGuiHelper.LeftCombo("使用QtHotkey", ref _selectIndex, _qtHotKeyArray);
        Key = _qtHotKeyArray[_selectIndex];
        return true;
    }

    public bool Handle()
    {
        // 激活单个快捷键
        MchQtManager.Qt.SetHotkey(Key);
        return true;
    }
}