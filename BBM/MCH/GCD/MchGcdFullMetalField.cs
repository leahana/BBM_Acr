using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.GCD;

/**
 * 全金属爆发好了就用
 */
public class MchGcdFullMetalField : ISlotResolver
{
    public int Check()
    {
        // 全金属爆发状态就绪
        if (!this.IsReady(MchSpells.FullMetalField))
            return -1;
        // 有全金属爆发状态预备状态
        if (!this.HasAura(MchBuffs.FullMetalFieldReady))
            return -2;
        // 空气锚冷却小于1000ms
        if (MchSpells.AirAnchor.GetSpell().Cooldown.TotalMilliseconds <= 1000.0) return -3;
        // 全金属爆发qt开启状态
        return !MchQtHelper.QtFullMetalField() ? -10 : 0;
    }

    public void Build(Slot slot) => slot.Add(MchSpells.FullMetalField.GetSpell());
}