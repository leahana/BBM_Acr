using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
///  虹吸弹/双将 
/// </summary>
public class MchAbilityDoubleCheck : ISlotResolver
{
    public int Check()
    {
        if (!this.IsReady(MchSpells.双将))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // 过热状态 虹吸cd 小于 弹射
        if (MchSpellHelper.OverHeated() && MchSpells.双将.GetCharges() >= 1)
        {
            return MchSpells.双将.Cooldown() > MchSpells.将死.Cooldown() ? -3 : 3;
        }

        // 好了就打
        return MchSpells.双将.GetCharges() >= 1 && this.LastAbility() != MchSpells.双将 ? 0 : -1;
        
    }

    public void Build(Slot slot) => slot.Add(MchSpells.双将.GetSpell());
}