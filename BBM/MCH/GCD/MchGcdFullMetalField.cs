using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 全金属爆发好了就用
 */
public class MchGcdFullMetalField : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.FullMetalField))
            return -1;
        if (!this.HasAura(MchBuffs.FullMetalFieldReady))
            return -2;
        return !CombatHelper.QtFullMetalField() ? -3 : 0;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.FullMetalField.GetSpell());
}