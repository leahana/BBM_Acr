using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Settings;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 *  内丹
 */
public class MchAbilitySecondWind(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑
    
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;
    
    private const uint SecondWind = SpellsDefine.SecondWind;

    private readonly MchSettings _mchSettings = MchSettings.Instance;

    public int Check()
    {
        if (!this.IsReady(SecondWind))
            return -1;

        if (!this.CanInsertAbility())
        {
            return -2;
        }

        // 自身血量 百分比大于 40%
        if (CombatHelper.GetHpPercent(_mchSettings.SecondWindThreshold))
        {
            return -3;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SecondWind.GetSpell());
    }
}