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
    private const uint Drill = MchSpells.Drill;
    private const uint AirAnchor = MchSpells.AirAnchor;
    private const uint ChainSaw = MchSpells.ChainSaw;
    private const uint CleanShot = MchSpells.CleanShot;

    public int Check()
    {
        if (!this.IsReady(Drill))
            return -1;
        if (this.IsComboTimeWithin(3000) && MchSpellsHelper.GetLastComboSpellId() != CleanShot)
            return -3;
        if (this.IsCooldownWithin(AirAnchor, 1200.0))
            return -4;
        if (this.IsCooldownWithin(ChainSaw, 1200.0))
            return -5;
        if (!this.IsGcdReadySoon() && Drill.GetCharges() < 1.05)
            return -6;
        return CheckQt();
    }

    public void Build(Slot slot) => slot.Add(MchSpells.Drill.GetSpell());

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }
}