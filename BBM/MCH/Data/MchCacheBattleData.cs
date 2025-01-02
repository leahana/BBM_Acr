namespace BBM.MCH.Data;

/// <summary>
/// 战斗缓存数据
/// </summary>
public class MchCacheBattleData
{
    public static readonly MchCacheBattleData Instance = new();

    // 热键使用高优先级
    public bool HotkeyUseHighPrioritySlot = false;
}