using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
/// 弹射/将死 
/// </summary>
public class MchAbilityCheckMate : ISlotResolver
{
    public int Check()
    {
        
        if (!this.IsReady(MchSpells.将死))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // 过热状态 冷却cd 将死>双将
        if (MchSpellHelper.OverHeated() && MchSpells.将死.GetCharges() >= 1)
            return MchSpells.将死.Cooldown() > MchSpells.双将.Cooldown() ? -3 : 3;
        
        // 
        return MchSpells.将死.GetCharges() >= 1 && this.LastAbility() != MchSpells.将死 ? 0 : -3;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.将死.GetSpell());
}