using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 空气矛
 */
public class MchGcdAirAnchor(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.AirAnchor))
        {
            return -1;
        }

        // 防止断连击
        if (this.IsComboTimeWithin(3000.0) && MchSpellHelper.GetLastComboSpellId() != MchSpells.CleanShot)
        {
            return -3;
        }

        // ！ 空气锚1.2s转好且可打Gcd
        if (!this.IsGcdReadySoon() && !this.IsCooldownWithin(MchSpells.AirAnchor, 1200))
        {
            return -4;
        }

        // 检查Qt配置
        var validationResult = MchQtHelper.ValidateQtKeys(_qtKeys);
        return validationResult != 0 ? validationResult : 0;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.AirAnchor.GetSpell());
}