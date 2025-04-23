using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Interfaces;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

public class MchGcdDrill(params string[] qtKeys) : ISlotResolver, IQtChecker, IAoeChecker
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑
    private const uint Drill = MchSpells.Drill;
    private const uint AirAnchor = MchSpells.AirAnchor;
    private const uint ChainSaw = MchSpells.ChainSaw;
    private const uint CleanShot = MchSpells.CleanShot;
    private const uint Bioblaster = MchSpells.Bioblaster;

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

    public void Build(Slot slot)
    {
        slot.Add(CheckAoe());
    }

    public int CheckQt()
    {
        return MchQtHelper.ValidateQtKeys(_qtKeys);
    }

    public Spell CheckAoe()
    {
        bool qtAoeFlag = MchQtHelper.ValidateQtKey(MchQtKeys.Aoe);
        if (!qtAoeFlag)
        {
            return Drill.GetSpell();
        }

        var battleChara = Core.Me.GetCurrTarget();

        if (battleChara != null)
        {
            // 三目标跳12秒才赚四目标9秒
            var nearbyEnemyCount = TargetHelper.GetNearbyEnemyCount(battleChara, 12, 6);
            LogHelper.Info(
                "Bioblaster AOE毒菌判断，TargetHelper.GetNearbyEnemyCount(battleChara, 12, 6)=" + nearbyEnemyCount +
                "TTKHelper.IsTargetTTK(battleChara, 9500, false) =" + TTKHelper.IsTargetTTK(battleChara, 9500, false));
            // 3目标毒跳12s以上
            if (nearbyEnemyCount >= 4 && TTKHelper.IsTargetTTK(battleChara, 9500, false)
                // 4目标毒跳9s以上
                || nearbyEnemyCount >= 3 && TTKHelper.IsTargetTTK(battleChara, 12500, false))
            {
                return Bioblaster.GetSpell();
            }
        }

        return Drill.GetSpell();
    }
}