using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 *  内丹
 */
public class MchAbilitySecondWind (params string[] qtKeys): ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!this.IsReady(SpellsDefine.SecondWind))
            return -1;

        if (!this.CanInsertAbility())
        {
            return -2;
        }

        // 自身血量 百分比大于 40%
        if (CombatHelper.GetHpPercent(0.4f))
        {
            return -3;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.SecondWind.GetSpell());
    }
}