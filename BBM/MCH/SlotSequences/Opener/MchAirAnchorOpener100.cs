using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Managers;
using BBM.MCH.Settings;
using BBM.MCH.Utils;

namespace BBM.MCH.SlotSequences.Opener;

/// <summary>
/// 100级空气矛起手
/// </summary>
public class MchAirAnchorOpener100 : IOpener, ISlotSequence
{
    public List<Action<Slot>> Sequence { get; } =
    [
        Step1Gcd,
        Step2Gcd,
        Step3Gcd,
        Step4Gcd,
        Step5Gcd,
        Step6Gcd,
    ];


    public int StartCheck()
    {
        // 检查爆发技能
        if (MchSpellsHelper.CheckOpenerOutbreakSpells())
        {
            return -1;
        }

        // 检查战斗时间
        if (AI.Instance.BattleData.CurrBattleTimeInMs > 3000L)
            return -2;

        // 检查目标
        var battleChara = (Core.Me).GetCurrTarget();
        if (PartyHelper.Party.Count <= 4
            && battleChara != null
            && battleChara.IsDummy()
            && !battleChara.IsBoss())
        {
            return -3;
        }

        return 0;
    }

    public int StopCheck(int index)
    {
        // 自身等级非100 且 超荷未被使用 且 在过热状态
        if (Core.Me.Level != 100
            && !MchSpells.HyperCharge.RecentlyUsed()
            && MchSpellsHelper.OverheatRemain() <= 100)
        {
            return 0;
        }

        return -1;
    }

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        LogHelper.Print("BBM-Mch_100空气锚起手");
        // 倒计时4.8s 整备
        countDownHandler.AddAction(4800, MchSpells.Reassemble);
        // 2s吃爆发药
        if (MchSettings.Instance.UsePotionInOpener && MchQtManager.Qt.GetQt(MchQtKeys.UsePotion))
        {
            countDownHandler.AddPotionAction(2000);
        }

        // 倒计时300ms 抢开
        countDownHandler.AddAction(
            MchSettings.Instance.GrabItLimit,
            MchSpells.AirAnchor,
            SpellTargetType.Target);
    }


    // 1g 空气锚+双蛋 
    private static void Step1Gcd(Slot slot)
    {
        slot.Add(MchSpells.CheckMate.GetSpell());
        slot.Add(MchSpells.DoubleCheck.GetSpell());
    }

    // 2g 钻头+枪管加热 
    private static void Step2Gcd(Slot slot)
    {
        slot.Add(MchSpells.Drill.GetSpell());
        slot.Add(MchSpells.BarrelStabilizer.GetSpell());
    }

    // 3g  飞锯  
    private static void Step3Gcd(Slot slot)
    {
        slot.Add(MchSpells.ChainSaw.GetSpell());
    }

    // 4g 飞轮 双插 机器人+整备
    private static void Step4Gcd(Slot slot)
    {
        slot.Add(MchSpells.Excavator.GetSpell());
        slot.Add(MchSpells.AutomationQueen.GetSpell());
        slot.Add(MchSpells.Reassemble.GetSpell());
    }


    //5g 钻头 双插 将死+野火
    private static void Step5Gcd(Slot slot)
    {
        slot.Add(MchSpells.Drill.GetSpell());
        slot.Add(MchSpells.CheckMate.GetSpell());
        slot.Add(MchSpells.Wildfire.GetSpell());
    }

    // 6g 全金属爆发 双插 双将+超荷
    private static void Step6Gcd(Slot slot)
    {
        slot.Add(MchSpells.FullMetalField.GetSpell());
        slot.Add(MchSpells.DoubleCheck.GetSpell());
        slot.Add(MchSpells.HyperCharge.GetSpell());
    }
    // 热冲击x5 7.5s
    // 后面就是123了
}