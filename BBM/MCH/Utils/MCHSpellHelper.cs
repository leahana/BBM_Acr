using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Data;

namespace BBM.MCH.Utils;

public static class MchSpellHelper
{
    // 检查整备gcd
    static MchSpellHelper()
    {
    }

    public static bool CheckReassmableGcd(float timeleft, out uint SpellId)
    {
        if (Core.Me.Level < 76)
        {
            if (!SpellsDefine.HotShot.RecentlyUsed()
                && SpellsDefine.HotShot.IsUnlock()
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

    public static bool 能力技封印() => Core.Me.HasAura(1092U, 0);
    public static bool 战技封印() => Core.Me.HasAura(620U, 0);

    public static bool 敌人无敌或自身受限制()
    {
        var battleChara = Core.Me.GetCurrTarget();
        return
            battleChara != null
            && (battleChara.HasAnyAura(Buff.敌人无敌BUFF) || battleChara.HasAnyAura(Buff.远程物理攻击无效化)
                                                      || Core.Me.HasAnyAura(Buff.无法造成伤害)
                                                      || (!Core.Me.HasAnyAura(Buff.加速度炸弹, 1500) &&
                                                          Core.Me.HasAnyAura(Buff.加速度炸弹))
                                                      || Core.Me.IsCasting
                                                      || Core.Me.HasAura(1175U)
                                                      && Core.Me.CurrentJob() == (Jobs)19
                                                      || Core.Me.CurrentJob() == (Jobs)31);
    }

    public static bool 选择目标()
    {
        var battleChara = Core.Me.GetCurrTarget();
        return battleChara == null;
    }


    /// <summary>
    /// 获取基础123连击
    /// </summary>
    /// <returns>下一个连击Spell</returns>
    public static Spell GetGcdBaseCombo()
    {
        var memApiSpell = Core.Resolve<MemApiSpell>(); // 提取重复调用
        var lastComboSpellId = memApiSpell.GetLastComboSpellId();

        // 根据 lastComboSpellId 返回对应的 Spell
        return lastComboSpellId switch
        {
            MchSpells.SplitShot => memApiSpell.CheckActionChange(MchSpells.SlugShot).GetSpell(),
            MchSpells.SlugShot => memApiSpell.CheckActionChange(MchSpells.CleanShot).GetSpell(),
            _ => memApiSpell.CheckActionChange(MchSpells.SplitShot).GetSpell()
        };
    }
        public static Spell GetBaseGCDCombo()
    {
      if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 2866U)
        return SpellHelper.GetSpell(Core.Resolve<MemApiSpell>().CheckActionChange(2868U));
      return Core.Resolve<MemApiSpell>().GetLastComboSpellId() == 2868U ? SpellHelper.GetSpell(Core.Resolve<MemApiSpell>().CheckActionChange(2873U)) : SpellHelper.GetSpell(Core.Resolve<MemApiSpell>().CheckActionChange(2866U));
    }

    public static bool CheckReassmableGCD(float timeleft, out uint SpellId)
    {
      TimeSpan cooldown;
      int num1;
      if (SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(16498U)))
      {
        cooldown = SpellHelper.GetSpell(16498U).Cooldown;
        if (cooldown.TotalMilliseconds < (double) timeleft)
        {
          num1 = BbmMchRotationEntry.Qt.GetQt("钻头") ? 1 : 0;
          goto label_4;
        }
      }
      num1 = 0;
label_4:
      if (num1 != 0)
      {
        SpellId = 16498U;
        return true;
      }
      int num2;
      if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(16498U)))
      {
        cooldown = SpellHelper.GetSpell(16498U).Cooldown;
        if (cooldown.TotalMilliseconds >= (double) timeleft + 20000.0)
        {
          num2 = 0;
          goto label_10;
        }
      }
      num2 = BbmMchRotationEntry.Qt.GetQt("钻头") ? 1 : 0;
label_10:
      if (num2 != 0)
      {
        SpellId = 16498U;
        return true;
      }
      int num3;
      if (!SpellExtension.IsReadyWithCanCast(SpellHelper.GetSpell(16500U)))
      {
        cooldown = SpellHelper.GetSpell(16500U).Cooldown;
        if (cooldown.TotalMilliseconds < (double) timeleft)
        {
          num3 = BbmMchRotationEntry.Qt.GetQt("空气锚") ? 1 : 0;
          goto label_16;
        }
      }
      num3 = 0;
label_16:
      if (num3 != 0)
      {
        SpellId = 16500U;
        return true;
      }
      int num4;
      if (!SpellExtension.RecentlyUsed(25788U, 1200) && SpellExtension.IsUnlock(25788U))
      {
        cooldown = SpellHelper.GetSpell(25788U).Cooldown;
        if (cooldown.TotalMilliseconds < (double) timeleft)
        {
          num4 = BbmMchRotationEntry.Qt.GetQt("回转飞锯") ? 1 : 0;
          goto label_22;
        }
      }
      num4 = 0;
label_22:
      if (num4 != 0)
      {
        SpellId = 25788U;
        return true;
      }
      SpellId = 0U;
      return false;
    }
}