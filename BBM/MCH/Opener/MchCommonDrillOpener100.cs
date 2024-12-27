using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;

namespace BBM.MCH.Opener;

/// <summary>
/// 标准钻头起手
/// </summary>
public class MchCommonDrillOpener100 : IOpener, ISlotSequence
{
    public int StartCheck()
    {
        var battleChara = (Core.Me).GetCurrTarget();
        if (AI.Instance.BattleData.CurrBattleTimeInMs > 3000L)
            return -1;
        if (PartyHelper.Party.Count <= 4
            && battleChara != null
            && battleChara.IsDummy()
            && !battleChara.IsBoss())
        {
            return -2;
        }

        // 旋转飞锯没好
        if (!MchSpells.ChainSaw.GetSpell().IsReadyWithCanCast())
            return -3;
        return 0;
    }

    public int StopCheck() => -1;

    public List<Action<Slot>> Sequence { get; } =
    [
        Step1Gcd,
        Step2Gcd,
        Step3Gcd,
        Step4Gcd,
        Step5Gcd,
        Step6_,
        Step7
    ];

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        // 倒计时4.8s 整备
        countDownHandler.AddAction(4800, MchSpells.Reassemble);
        // 2s吃爆发药
        countDownHandler.AddPotionAction(2000);
    }

    // 1g 钻头+双蛋 曹飞惊喜蛋 
    private static void Step1Gcd(Slot slot)
    {
        slot.Add(MchSpells.Drill.GetSpell());
        slot.Add(MchSpells.将死.GetSpell());
        slot.Add(MchSpells.双将.GetSpell());
    }

    // 2g 空气矛+枪管加热 
    private static void Step2Gcd(Slot slot)
    {
        slot.Add(MchSpells.AirAnchor.GetSpell());
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
        slot.Add(MchSpells.将死.GetSpell());
        slot.Add(MchSpells.Wildfire.GetSpell());
    }

    // 6g 全金属爆发 双插 双将+超荷
    private static void Step6Gcd(Slot slot)
    {
        slot.Add(MchSpells.FullMetalField.GetSpell());
        slot.Add(MchSpells.双将.GetSpell());
        slot.Add(MchSpells.Hypercharge.GetSpell());
    }

    // 热冲击x5 7.5s

    private static void Step6_(Slot slot)
    {
        for (var i = 1; i <= 5; i++)
        {
            slot.Add(MchSpells.BlazingShot.GetSpell());
            slot.Add(!(MchSpells.将死.GetCharges() > MchSpells.双将.GetCharges())
                ? MchSpells.将死.GetSpell()
                : MchSpells.双将.GetSpell()
            );
        }
    }

    // 钻头 双插 双蛋
    private static void Step7(Slot slot)
    {
        slot.Add(MchSpells.Drill.GetSpell());
        if (MchSpells.将死.GetCharges() > 0) slot.Add(MchSpells.将死.GetSpell());
        if (MchSpells.双将.GetCharges() > 0) slot.Add(MchSpells.双将.GetSpell());
    }
    // 后面就是123了
}