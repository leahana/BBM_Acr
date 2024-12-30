namespace BBM.MCH.Data;

public class MchBattleData
{
    public static readonly MchBattleData Instance = new();


    // 热键使用高优先级
    public bool HotkeyUseHighPrioritySlot = false;
}