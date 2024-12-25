using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Data;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 回转飞锯
 */
public class MchGcdChainsaw : ISlotResolver
{
    public int Check()
    {
        // if (!MCHRotationEntry.QT.GetQt("回转飞锯"))
        // return -2;
        if (CombatHelper.HasBuff(MchBuffs.掘地飞轮预备))
            return -3;
        if (Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalMilliseconds <= 3000.0 &&
            CombatHelper.GetLastComboSpellId() != MchSpells.CleanShot)
            return -2;
        return CombatHelper.IsCooldownWithin(500f) &&
               MchSpells.ChainSaw.GetSpell().Cooldown.TotalMilliseconds <= 1500.0
            ? 1
            : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.ChainSaw.GetSpell());
}