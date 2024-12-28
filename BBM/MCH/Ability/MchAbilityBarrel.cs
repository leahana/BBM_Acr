using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Ability;

/**
 * 枪管加热
 */
public class MchAbilityBarrelStabilizer(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!this.IsReady(MchSpells.BarrelStabilizer))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.BarrelStabilizer.GetSpell());
    }
}