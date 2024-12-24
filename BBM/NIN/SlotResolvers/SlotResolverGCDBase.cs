using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.NIN.GCD;

namespace BBM.NIN.SlotResolvers
{
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