using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 回转飞锯
 */
public class MchGcdChainsaw(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.ChainSaw) || this.HasAura(MchBuffs.ExcavatorReady))
        {
            return -1;
        }
        // 调用通用方法进行 Qt 判断
        var validationResult = MchQtHelper.ValidateQtKeys(_qtKeys);
        return validationResult != 0 ? validationResult : 0;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.ChainSaw.GetSpell());
}