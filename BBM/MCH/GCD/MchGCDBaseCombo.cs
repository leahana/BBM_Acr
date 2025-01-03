using AEAssist.CombatRoutine.Module;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 基础123
 */
public class MchGcdBaseCombo(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

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
        slot.Add(MchSpellsHelper.GetGcdBaseCombo());
    }
}