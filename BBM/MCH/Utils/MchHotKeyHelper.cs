using AEAssist;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Extension;
using AEAssist.Helper;
using BBM.MCH.Data;

namespace BBM.MCH.Utils;

public static class MchHotKeyHelper
{
    /// <summary>
    /// 武装解除Hotkey判断条件
    /// </summary>
    /// <returns></returns>
    public static int HotkeyCondDismantle()
    {
        if (!MchSpells.Dismantle.GetSpell().IsReadyWithCanCast())
        {
            LogHelper.Print("hotkey", "扳手 cd");
            return -1;
        }

        if (Core.Me.GetCurrTarget().HasAura(MchBuffs.BeDismantle, 0))
        {
            LogHelper.Print("hotkey", "目标已经有扳手DeBuff了");
            return -2;
        }

        return MchSpells.Dismantle.RecentlyUsed() ? -3 : 0;
    }

    /// <summary>
    /// 策动hotkey判断条件
    /// </summary>
    /// <returns></returns>
    public static int HotkeyCondTactician()
    {
        if (!MchSpells.Tactician.GetSpell().IsReadyWithCanCast())
        {
            LogHelper.Print("hotkey", "策动CD");
            return -1;
        }

        if (CombatHelper.HasRangedMitigation())
        {
            LogHelper.Print("hotkey", "已经有远敏减伤Buff了");
            return -2;
        }

        return MchSpells.Tactician.GetSpell().RecentlyUsed() ? -3 : 0;
    }


    /// <summary>
    /// 机器人结算hotkey判断条件
    /// </summary>
    /// <returns></returns>
    public static int HotKeyQueenOverdrive()
    {
        if (MchSpellsHelper.Robotactive()
            && MchSpells.QueenOverdrive.GetSpell().IsReadyWithCanCast())
        {
            return MchSpells.QueenOverdrive.GetSpell().RecentlyUsed() ? -3 : 0;
        }

        if (!MchSpellsHelper.Robotactive())
        {
            LogHelper.Print("hotkey", "没有机器人你点个几把");
        }

        LogHelper.Print("hotkey", "超档后式人偶不可用");
        return -1;
    }

    /// <summary>
    /// 野火结算判断
    /// </summary>
    /// <returns></returns>
    public static int HotKeyDetonator()
    {
        // 野火不可用 
        if (!MchSpells.Wildfire.RecentlyUsed() && MchSpells.Detonator.GetSpell().IsReadyWithCanCast())
        {
            return MchSpells.Detonator.GetSpell().RecentlyUsed() ? -3 : 0;
        }

        if (!Core.Me.GetCurrTarget().HasAura(AurasDefine.WildfireBuff, 0))
        {
            LogHelper.Print("hotkey", "目标没有野火buff");
            return -2;
        }

        LogHelper.Print("hotkey", "起爆不可用");
        return -1;
    }

    /// <summary>
    ///  喷火判断
    /// </summary>
    /// <returns></returns>
    public static int HotkeyFlamethrower()
    {
        
        // 野火不可用 
        if (!MchSpells.Flamethrower.RecentlyUsed() && MchSpells.Flamethrower.GetSpell().IsReadyWithCanCast())
        {
            return MchSpells.Flamethrower.GetSpell().RecentlyUsed() ? -3 : 0;
        }

        LogHelper.Print("hotkey", "火焰喷射器不可用");
        return -1;
    }

    /// <summary>
    /// 超荷Hotkey
    /// </summary>
    /// <returns></returns>
    public static int HotkeyHyperCharge()
    {
        // 野火不可用 
        if (!MchSpells.HyperCharge.RecentlyUsed()
            && MchSpells.HyperCharge.GetSpell().IsReadyWithCanCast()
            && MchSpellsHelper.GetHeat() >= 50
           )
        {
            return MchSpells.HyperCharge.GetSpell().RecentlyUsed() ? -3 : 0;
        }

        return -1;
    }
}