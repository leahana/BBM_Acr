using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 回转飞锯
 */
public class MchGcdChainsaw : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.ChainSaw) || this.HasAura(MchBuffs.ExcavatorReady))
        {
            return -1;
        }

        return 0;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.ChainSaw.GetSpell());
}