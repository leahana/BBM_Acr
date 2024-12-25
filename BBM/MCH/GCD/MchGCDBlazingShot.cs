using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.GCD;

/**
 * 热冲击
 */
public class MchGcdBlazingShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        // 有过热buff 
        return this.HasAura(MchBuffs.Overheated) ? 1 : -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.BlazingShot.GetSpell());
    }
}