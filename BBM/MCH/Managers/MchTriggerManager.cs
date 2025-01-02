using AEAssist.CombatRoutine;
using BBM.MCH.Triggers.Actions;
using BBM.MCH.Triggers.Conditions;

namespace BBM.MCH.Managers;

public static class MchTriggerManager
{
    public static void AddTriggers(this Rotation rotation)
    {
        AddTriggerConditions(rotation);

        AddTriggerActions(rotation);
    }

    // 时间轴条件
    private static void AddTriggerConditions(Rotation rot)
    {
        // 电量条件
        rot.AddTriggerCondition(new MchTriggerConditionBattery());
        // 热量条件
        rot.AddTriggerCondition(new MchTriggerCondHeat());
        // 战斗时间条件
        rot.AddTriggerCondition(new MchCondAfterBattleStart());
    }

    // 时间轴行为
    private static void AddTriggerActions(Rotation rot)
    {
        // 电量行为
        rot.AddTriggerAction(new MchTriggerActionBattery());
        // Qt行为
        rot.AddTriggerAction(new MchTriggerActionQt());
        // QtHotkey行为
        rot.AddTriggerAction(new MchTriggerActionQtHotKey());
    }
}