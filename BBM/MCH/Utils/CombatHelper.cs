using AEAssist;
using AEAssist.Extension;
using BBM.MCH.Data;
using Dalamud.Game.ClientState.Objects.Types;

namespace BBM.MCH.Utils;

/**
 * 一般战斗条件判断工具
 */
public static class CombatHelper
{
    /// <summary>
    /// 自身获取当前血量
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static bool GetHpPercent(float limit) => Core.Me.CurrentHpPercent() > limit / 100.0f;

    /// <summary>
    /// 是否有远敏减
    /// </summary>
    /// <returns></returns>
    public static bool HasRangedMitigation()
    {
        return Core.Me.HasAura(MchBuffs.Tactician)
               || Core.Me.HasAura(AurasDefine.ShieldSamba)
               || Core.Me.HasAura(AurasDefine.Troubadour);
    }

    /// <summary>
    /// 能力技锁定判断
    /// </summary>
    public static bool IsOffGcdLocked => Core.Me.HasAura(1092U, 0);

    /// <summary>
    /// GCD锁定判断
    /// </summary>
    /// <returns></returns>
    public static bool IsGcdLocked() => Core.Me.HasAura(620U, 0);

    /// <summary>
    ///  目标无敌状态判断
    /// </summary>
    /// <returns></returns>
    public static bool IsTargetImmune()
    {
        var battleChara = Core.Me.GetCurrTarget();
        return
            battleChara != null
            && (battleChara.HasAnyAura(Buff.敌人无敌BUFF)
                || battleChara.HasAnyAura(Buff.远程物理攻击无效化)
                || Core.Me.HasAnyAura(Buff.无法造成伤害));
    }
}