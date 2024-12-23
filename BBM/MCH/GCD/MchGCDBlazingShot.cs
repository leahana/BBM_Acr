using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

namespace BBM.MCH.GCD;

public class MchGcdBlazingShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        // 有过热才打
        if (Core.Me.HasAura(AurasDefine.Overheated))
            return 1;

        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.HeatBlast.GetSpell());
    }
}