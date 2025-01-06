using AEAssist.CombatRoutine.Module;
using BBM.MCH.Ability;
using BBM.MCH.Data;
using BBM.MCH.GCD;

namespace BBM.MCH.Managers;

/// <summary>
/// 机工士/技能决策管理
/// </summary>
public class MchSlotResolverManager
{
    public static readonly MchSlotResolverManager Instance;
    private static readonly List<SlotResolverData> SlotResolvers;

    static MchSlotResolverManager()
    {
        Instance = new MchSlotResolverManager();

        // Qt判断 以QtKey 从左到右优先级
        SlotResolvers =
        [
            // 机器人电量slotResolver, qt:  爆发 
            new SlotResolverData(new MchAbilityUseBattery(MchQtKeys.UseOutbreak),
                SlotMode.OffGcd),
            // 热冲击slotResolver, qt: 优先使用123
            new SlotResolverData(new MchGcdBlazingShot(MchQtKeys.UseBaseComboFirst
            ), SlotMode.Gcd),
            // 空气矛slotResolver, qt:  爆发 空气矛 优先使用123
            new SlotResolverData(
                new MchGcdAirAnchor(MchQtKeys.UseOutbreak, MchQtKeys.UseBaseComboFirst, MchQtKeys.UseAirAnchor),
                SlotMode.Gcd),
            // 回转飞锯slotResolver, qt:  爆发 回转飞锯 优先使用123
            new SlotResolverData(
                new MchGcdChainsaw(MchQtKeys.UseOutbreak, MchQtKeys.UseBaseComboFirst, MchQtKeys.UseChainSaw),
                SlotMode.Gcd),
            // 掘地飞轮slotResolver, qt： 爆发 掘地飞轮 优先使用123
            new SlotResolverData(
                new MchGcdExcavator(MchQtKeys.UseOutbreak, MchQtKeys.UseBaseComboFirst, MchQtKeys.UseExcavator),
                SlotMode.Gcd),
            // 钻头slotResolver, qt:  爆发 优先使用123 钻头  
            new SlotResolverData(
                new MchGcdDrill(MchQtKeys.UseOutbreak, MchQtKeys.UseBaseComboFirst, MchQtKeys.UseDrill), SlotMode.Gcd),

            // 全金属爆发slotResolver, qt： 爆发 全金属爆发 优先使用123
            new SlotResolverData(
                new MchGcdFullMetalField(MchQtKeys.UseOutbreak, MchQtKeys.UseFullMetalField,
                    MchQtKeys.UseBaseComboFirst),
                SlotMode.Gcd),

            // new(new MchGcdAdvanced(), SlotMode.Gcd),
            // 基础123Gcd slotResolver  qt: 无
            new SlotResolverData(new MchGcdBaseCombo(), SlotMode.Gcd),

            // 枪管加热slotResolver qt:  爆发 枪管加热
            new SlotResolverData(new MchAbilityBarrelStabilizer(MchQtKeys.UseOutbreak, 
                    MchQtKeys.UseBarrelStabilizer),
                SlotMode.OffGcd),

            // 超荷slotResolver qt:  爆发 超荷
            new SlotResolverData(new MchAbilityHyperCharge(MchQtKeys.UseOutbreak, MchQtKeys.UseHyperCharge),
                SlotMode.OffGcd),

            // 野火slotResolver qt：  爆发 野火
            new SlotResolverData(new MchAbilityWildfire(MchQtKeys.UseOutbreak,
                MchQtKeys.UseWildfire), SlotMode.OffGcd),

            // 整备slotResolver qt:  爆发 使用整备
            new SlotResolverData(new MchAbilityReassemble(MchQtKeys.UseOutbreak, MchQtKeys.UseReassemble),
                SlotMode.OffGcd),

            // 将死slotResolver  qt:  爆发 (保留)2层双将
            new SlotResolverData(new MchAbilityCheckMate(MchQtKeys.UseOutbreak, MchQtKeys.ReserveCheckMate),
                SlotMode.OffGcd),

            // 双将slotResolver  qt:  爆发 (保留)2层双将
            new SlotResolverData(new MchAbilityDoubleCheck(MchQtKeys.UseOutbreak, MchQtKeys.ReserveDoubleCheck),
                SlotMode.OffGcd),

            // new SlotResolverData(new MchAbilitySecondWind(), SlotMode.OffGcd)
        ];
    }

    public List<SlotResolverData> GetSlotResolvers()
    {
        return SlotResolvers;
    }
}