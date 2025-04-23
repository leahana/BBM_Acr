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
 * 基础123
 */
public class MchGcdBaseCombo(params string[] qtKeys) : ISlotResolver, IAoeChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    private const uint Scattergun = MchSpells.Scattergun;


    public int Check()
    {
        // 整备||过热 不打123
        if (this.HasAura(MchBuffs.Overheated) || this.HasAura(MchBuffs.Reassembled))
        {
            return -1;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(CheckAoe());
    }

    public Spell CheckAoe()
    {
        bool qtAoeFlag = MchQtHelper.ValidateQtKey(MchQtKeys.Aoe);
        if (!qtAoeFlag)
        {
            return MchSpellsHelper.GetGcdBaseCombo();
        }

        var battleChara = Core.Me.GetCurrTarget();
        if (battleChara != null)
        {
            var nearbyEnemyCount = TargetHelper.GetNearbyEnemyCount(battleChara, 12, 6);
            if (qtAoeFlag && nearbyEnemyCount >= 3)
            {
                return Scattergun.GetSpell();
            }
        }

        return MchSpellsHelper.GetGcdBaseCombo();
    }
}