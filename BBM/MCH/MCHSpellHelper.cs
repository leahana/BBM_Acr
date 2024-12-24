using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace BBM.MCH;

public static class MchSpellHelper
{
    // 检查整备gcd
    public static bool CheckReassmableGcd(float timeleft, out uint SpellId)
    {
        if (Core.Me.Level < 76)
        {
            if (!SpellsDefine.HotShot.RecentlyUsed()
                && SpellsDefine.HotShot.IsUnlock() // 
                && SpellsDefine.HotShot.GetSpell().Cooldown.TotalMilliseconds < timeleft)
            {
                SpellId = SpellsDefine.HotShot;
                return true;
            }
        }
        else
        {
            if (!SpellsDefine.AirAnchor.RecentlyUsed() && SpellsDefine.AirAnchor.IsUnlock() &&
                SpellsDefine.AirAnchor.IsLevelEnough() &&
                SpellsDefine.AirAnchor.GetSpell().Cooldown.TotalMilliseconds < timeleft)
            {
                /*if (Core.Get<IMemApiMCH>().GetBattery()>=80)
                {
                    if (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds < timeleft)
                    {
                        SpellId = SpellsDefine.Drill;
                        return true;
                    }
                }*/
                SpellId = SpellsDefine.AirAnchor;
                return true;
            }
        }

        if (!SpellsDefine.ChainSaw.RecentlyUsed()
            && SpellsDefine.ChainSaw.IsLevelEnough()
            && SpellsDefine.ChainSaw.IsUnlock()
            && SpellsDefine.ChainSaw.GetSpell().Cooldown.TotalMilliseconds < timeleft)
        {
            // 野火
            if (SpellsDefine.Wildfire.GetSpell().IsReadyWithCanCast())
            {
                if (SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds < timeleft)
                {
                    // 钻头
                    SpellId = SpellsDefine.Drill;
                    return true;
                }
            }

            SpellId = SpellsDefine.ChainSaw;
            return true;
        }

        if (!SpellsDefine.Drill.RecentlyUsed() && SpellsDefine.Drill.IsUnlock() &&
            SpellsDefine.Drill.GetSpell().Cooldown.TotalMilliseconds < timeleft)
        {
            SpellId = SpellsDefine.Drill;
            return true;
        }

        SpellId = 0;
        return false;
    }
    public static bool 能力技封印() => ((IBattleChara) Core.Me).HasAura(1092U, 0);

    public static Spell? GetGaussRound()
    {
        Spell spellData = null;
        if (Core.Me.Level < 74)
        {
            if (SpellsDefine.GaussRound.GetSpell().Charges < 1f && SpellsDefine.Ricochet.GetSpell().Charges < 1f)
                return spellData;
            var gaussRound = SpellsDefine.GaussRound.GetSpell();
            var ricochet = SpellsDefine.Ricochet.GetSpell();
            LogHelper.Debug($"{gaussRound.Name}-{gaussRound.Charges} : {ricochet.Name} - {ricochet.Charges}");
            spellData = gaussRound.Charges >= ricochet.Charges ? gaussRound : ricochet;
        }
        else
        {
            if (!SpellsDefine.GaussRound.GetSpell().IsReadyWithCanCast() &&
                !SpellsDefine.Ricochet.GetSpell().IsReadyWithCanCast())
                return spellData;
            var gaussRound = SpellsDefine.GaussRound.GetSpell();
            var ricochet = SpellsDefine.Ricochet.GetSpell();
            LogHelper.Debug($"{gaussRound.Name}-{gaussRound.Charges} : {ricochet.Name} - {ricochet.Charges}");
            if (gaussRound.Charges >=
                ricochet.Charges)
                spellData = gaussRound;
            else
                spellData = ricochet;
        }

        return spellData;
    }
}