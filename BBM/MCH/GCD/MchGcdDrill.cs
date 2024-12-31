using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

public class MchGcdDrill(params string[] qtKeys) : ISlotResolver, IQtChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public int Check()
    {
        if (!this.IsReady(MchSpells.Drill))
            return -1;
        if (this.IsComboTimeWithin(3000) && MchSpellHelper.GetLastComboSpellId() != MchSpells.CleanShot)
            return -2;
        if (this.IsCooldownWithin(MchSpells.AirAnchor, 1200.0))
            return -3;
        if (this.IsCooldownWithin(MchSpells.ChainSaw, 1200.0))
            return -4;
        if (!this.IsGcdReadySoon() && !this.IsCooldownWithin(MchSpells.Drill, 20500.0))
            return -5;
        return CheckQt();
    }

    public void Build(Slot slot) => slot.Add(MchSpells.Drill.GetSpell());

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}