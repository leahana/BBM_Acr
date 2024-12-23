using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace BBM
{
    using AEAssist.CombatRoutine.Module;
    using BBM.GCD;


    public class SlotResolverGcdBase : ISlotResolver
    {
        public int Check()
        {
            return 0;
        }

        public void Build(Slot slot)
        {
            foreach (var u in NinSpellsHelper.NinAbilityAsGcdSet)
            {
                slot.Add(u.GetSpell());
            }
            var lastComboSpellId = Core.Resolve<MemApiSpell>().GetLastComboSpellId();
            
        }
    }
}