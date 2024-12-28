using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/// <summary>
///  虹吸弹/双将 
/// </summary>
public class MchAbilityDoubleCheck(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.双将))
            return -1;
        if (!this.CanInsertAbility())
            return -2;
        // 过热状态 虹吸cd 小于 弹射
        if (MchSpellHelper.OverHeated() && MchSpells.双将.GetCharges() >= 1)
        {
            return MchSpells.双将.GetCharges() > MchSpells.将死.GetCharges() ? 3 : -3;
        }

        // 检查Qt配置
        var validationResult = MchQtHelper.ValidateQtKeys(_qtKeys);
        if (validationResult < 0)
        {
            return validationResult;
        }

        // 好了就打
        return MchSpells.双将.GetCharges() >= 1 && this.LastAbility() != MchSpells.双将 ? 0 : -1;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.双将.GetSpell());
}