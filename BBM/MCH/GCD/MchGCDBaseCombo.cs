using AEAssist.CombatRoutine.Module;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 基础gcd 123
 */
public class MchGcdBaseCombo : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        // 整备||过热 不打123
        if (CombatHelper.IsReassembled() || CombatHelper.IsOverheated())
        {
            return -1;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = MchSpellHelper.GetGcdBaseCombo();
        slot.Add(spell);
    }
}