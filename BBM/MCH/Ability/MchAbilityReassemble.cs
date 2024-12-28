using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;

namespace BBM.MCH.Ability;

/**
 * 整备
 */
public class MchAbilityReassemble(params string[] qtKeys) : ISlotResolver
{
    private readonly List<string> _qtKeys = qtKeys.ToList(); // 支持多种 Qt 的判断逻辑

    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        // 如果整备技能还没有准备好
        if (!this.IsReady(MchSpells.Reassemble))
            return -1;

        // 判断 能力技能是否可插
        if (!this.CanInsertAbility())
            return -2;

        // 检查是否已经有整备状态
        if (this.HasAura(MchBuffs.Reassembled)) return -3;

        // 检查整备是否在最近 1200 毫秒内已经使用过
        if (MchSpellHelper.ReassembledUsed(1200)) return -4;

        // 检查是否能进行强威力 GCD 的释放
        if (!MchSpellHelper.CheckReassembleGcd(GCDHelper.GetGCDCooldown(), out var strongGcd)) return -5;

        // 如果当前强威力 GCD 是 HotShot，则不使用整备
        if (strongGcd == MchSpells.HotShot) return -6;

        // 检查是否处于过热状态
        if (this.HasAura(MchBuffs.Overheated)) return -7;

        var qtResult = 101;
        switch (strongGcd)
        {
            case MchSpells.AirAnchor:
                qtResult = MchQtHelper.ValidateQtKeys(new List<string>() { MchQtConstantsCn.UseAirAnchor });
                break;
            case MchSpells.Drill:
                qtResult = MchQtHelper.ValidateQtKeys(new List<string>() { MchQtConstantsCn.UseDrill });
                break;
            case MchSpells.ChainSaw:
                qtResult = MchQtHelper.ValidateQtKeys(new List<string>() { MchQtConstantsCn.UseChainSaw });
                break;
        }

        // 有qt限制不准放技能 那就整备也不放
        if (qtResult == -101)
        {
            return qtResult;
        }


        if (!this.IsReady(MchSpells.Wildfire)) return 0; // 野火未准备好

        if (strongGcd != MchSpells.ChainSaw) return 0; // 当前 GCD 不是回转飞锯

        // 如果回转飞锯被选中 检查空气矛和钻头是否准备好
        if (!this.IsReady(MchSpells.AirAnchor) && !this.IsReady(MchSpells.Drill))
            return -8;
        // 如果回转飞锯被选中 检查空气矛和钻头是否准备好
        if (MchSpellHelper.GetLastComboSpellId() == MchSpells.Wildfire)
            return -9;

        return 1;
    }

    public void Build(Slot slot)
    {
        slot.Add(MchSpells.Reassemble.GetSpell());
    }
}