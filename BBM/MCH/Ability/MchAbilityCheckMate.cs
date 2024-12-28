using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
/// 弹射/将死 
/// </summary>
public class MchAbilityCheckMate(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.将死))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // 过热状态 将死>双将
        if (MchSpellHelper.OverHeated() && MchSpells.将死.GetCharges() >= 1)
            return MchSpells.将死.GetCharges() > MchSpells.双将.GetCharges() ? 3 : -3;
        // 检查Qt配置
        var validationResult = MchQtHelper.ValidateQtKeys(_qtKeys);
        if (validationResult < 0)
        {
            return validationResult;
        }

        return MchSpells.将死.GetCharges() >= 1 && this.LastAbility() != MchSpells.将死 ? 0 : -3;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.将死.GetSpell());
}