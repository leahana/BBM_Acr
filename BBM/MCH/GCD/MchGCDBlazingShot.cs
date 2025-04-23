using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 热冲击
 */
public class MchGcdBlazingShot(params string[] qtKeys) : ISlotResolver, IQtChecker, IAoeChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    private const uint BlazingShot = MchSpells.BlazingShot;
    private const uint AutoCrossbow = MchSpells.AutoCrossbow;

    public int Check()
    {
        // 有过热buff 
        if (this.HasAura(MchBuffs.Overheated))
        {
            return CheckQt();
        }

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(BlazingShot.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }


    public Spell CheckAoe()
    {
        bool qtAoeFlag = MchQtHelper.ValidateQtKey(MchQtKeys.Aoe);
        // Aoe qt关闭 直接返回热冲击
        if (!qtAoeFlag)
        {
            return BlazingShot.GetSpell();
        }
        var battleChara = Core.Me.GetCurrTarget();
        // 有一个5个目标打aoe才赚的职业
        if (battleChara != null)
        {
            var nearbyEnemyCount = TargetHelper.GetNearbyEnemyCount(battleChara, 12, 6);
            if (qtAoeFlag && nearbyEnemyCount >= 5)
            {
                return AutoCrossbow.GetSpell();
            }
        }
        return BlazingShot.GetSpell();
    }
}