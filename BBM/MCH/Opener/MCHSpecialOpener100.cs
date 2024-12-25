using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace BBM.MCH.Opener;

public class MchSpecialOpener100 : IOpener, ISlotSequence
{
    public int StartCheck()
    {
        if (AI.Instance.BattleData.CurrBattleTimeInMs > 3000L)
            return -9;
        if (PartyHelper.Party.Count <= 4 &&
            !GameObjectExtension.IsDummy(GameObjectExtension.GetCurrTarget((IBattleChara)Core.Me)) &&
            !GameObjectExtension.IsBoss(GameObjectExtension.GetCurrTarget((IBattleChara)Core.Me)))
            return -1;
        if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(25788U)))
            return -4;
        // return MCHSettings.Instance.m4s != 1 ? -5 : 0;
        return 0;
    }

    public int StopCheck() => -1;

    public List<Action<Slot>> Sequence { get; } = new List<Action<Slot>>()
    {
        Step0,
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
        Step6,
        Step7,
        Step8
    };

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(4800, 2876U, (SpellTargetType)1);
        if (Core.Resolve<MemApiMap>().GetCurrTerrId() != (ushort)1232)
            // || MCHSettings.Instance.m4s != 1 || !MCHSettings.Instance.enablePotion)
            return;
        countDownHandler.AddPotionAction(2000);
    }

    private static void Step0(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(16498U));
        slot.Add(SpellHelper.GetSpell(36979U));
        slot.Add(SpellHelper.GetSpell(36980U));
    }

    private static void Step1(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(25788U));
        slot.Add(SpellHelper.GetSpell(7414U));
        if (Core.Resolve<MemApiMap>().GetCurrTerrId() != (ushort)1232)
            return;
        slot.Add(SpellHelper.GetSpell(16889U));
    }

    private static void Step2(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(16500U));
        slot.Add(SpellHelper.GetSpell(36980U));
    }

    private static void Step3(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(36981U));
        slot.Add(SpellHelper.GetSpell(16501U));
        slot.Add(SpellHelper.GetSpell(36979U));
    }

    private static void Step4(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(36982U));
        slot.Add(SpellHelper.GetSpell(17209U));
        slot.Add(SpellHelper.GetSpell(2878U));
    }

    private static void Step5(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(36978U));
        if (
            Core.Resolve<MemApiSpell>().GetCharges(36979U)
            >
            Core.Resolve<MemApiSpell>().GetCharges(36980U)
        )
            slot.Add(SpellHelper.GetSpell(36979U));
        else
            slot.Add(SpellHelper.GetSpell(36980U));
    }

    private static void Step6(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(36978U));
        slot.Add(SpellHelper.GetSpell(2876U));
    }

    private static void Step7(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(16498U));
        slot.Add(SpellHelper.GetSpell(36979U));
        slot.Add(SpellHelper.GetSpell(36980U));
    }

    private static void Step8(Slot slot)
    {
        slot.Add(SpellHelper.GetSpell(16498U));
        if (SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(36979U)))
            slot.Add(SpellHelper.GetSpell(36979U));
        if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(36980U)))
            return;
        slot.Add(SpellHelper.GetSpell(36980U));
    }
}