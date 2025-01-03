using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/// <summary>
/// 掘地飞轮
/// </summary>
/// <param name="qtKeys"></param>
public class MchGcdExcavator(params string[] qtKeys) : ISlotResolver, IQtChecker

{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.Excavator)) return -1;
        if (!this.HasAura(MchBuffs.ExcavatorReady)) return -3;
        // 空气矛小于1000ms 不打
        if (MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1000.0) return -4;
        // 调用通用方法进行 Qt 判断
        return CheckQt();
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Excavator.GetSpell());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}