using AEAssist;
using AEAssist.Extension;
using BBM.MCH.Data;
using Dalamud.Game.ClientState.Objects.Types;

namespace BBM.MCH.Utils;

/**
 * 一般战斗条件判断工具
 */
public abstract class CombatHelper
{
    /// <summary>
    /// 自身获取当前血量
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static bool GetHpPercent(float limit) => Core.Me.CurrentHpPercent() > limit;

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

    public static bool IsOffGcdLocked => Core.Me.HasAura(1092U, 0);
    public static bool IsGcdLocked() => Core.Me.HasAura(620U, 0);
    
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