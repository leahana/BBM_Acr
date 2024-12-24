using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace BBM.MCH.GCD;

/**
 * 基础gcd 123
 */
public class MchGcdBaseCombo : ISlotResolver
{
    private Spell GetSpell()
    {
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == SpellsDefine.SlugShot)
            return SpellsDefine.CleanShot.GetSpell();
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == SpellsDefine.SplitShot)
            return SpellsDefine.SlugShot.GetSpell();
        return SpellsDefine.SplitShot.GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        // todo: 需要判断是否可以使用技能。
        // 整备状态
        if (Core.Me.HasAura(AurasDefine.Reassembled))
        {
            return -2;
        }
        /*if (MCHSpellHelper.CheckReassmableGCD(500,
                out var strongGcd))
            return -2;*/

        // 过热
        if (Core.Resolve<MemApiBuff>().BuffStackCount(Core.Me, AurasDefine.Overheated) > 0)
            return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(GetSpell().Id).GetSpell();
        slot.Add(spell);
    }
}