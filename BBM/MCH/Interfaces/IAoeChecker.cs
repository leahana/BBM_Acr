using AEAssist.CombatRoutine;

namespace BBM.MCH.Interfaces;

/// <summary>
/// AOE检查接口 
/// </summary>
public interface IAoeChecker
{
    /**
     * 判断当前技能是否可以替换为AOE版本
     */
    Spell CheckAoe();
}