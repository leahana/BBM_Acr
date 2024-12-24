using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace BBM.NIN.SlotResolvers;

public class SlotResolverOffGcdBase : ISlotResolver
{
    public int Check()
    {
        // 如果没学习 充能数<1 等等情况 就返回 否则就使用
        if (!SpellsDefine.Bhavacakra.IsMaxChargeReady(0.5f))
        {
            // 建议check方法内每个返回的负数都不一样 方便定位问题
            return -1;
        }

        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.Bhavacakra.GetSpell());
    }
}