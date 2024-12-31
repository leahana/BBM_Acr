using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 热冲击
 */
public class MchGcdBlazingShot(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.Gcd;
    
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
        slot.Add(MchSpells.BlazingShot.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}