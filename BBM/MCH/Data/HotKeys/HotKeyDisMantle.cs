using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Extensions;
using ImGuiNET;
using static AEAssist.CombatRoutine.SpellTargetType;

namespace BBM.MCH.Data.HotKeys;

/// <summary>
/// 扳手HotKey
/// </summary>
public class HotkeyDismantle : IHotkeyResolver
{
    private const uint Dismantle = MchSpells.Dismantle;

    /*
     * 	1.	图标大小和位置:
       •	图标的大小是 size 的 80%。
       •	图标位置设置为 size 的 10% 偏移。
       2.	获取技能图标:
       •	调用了 MemApiIcon.GetActionTexture(Dismantle, out var textureWrap) 来获取技能的纹理对象。
       •	如果纹理不存在，直接退出。
       3.	绘制图标:
       •	如果纹理有效，使用 ImGui.Image 绘制技能图标。
     */
    public void Draw(Vector2 size)
    {
        var size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(Dismantle, out var textureWrap))
            return;
        if (textureWrap != null) ImGui.Image(textureWrap.ImGuiHandle, size1);
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(Dismantle.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (Dismantle.Cooldown() > 0.0)
        {
            LogHelper.Print("hotkey", "扳手 cd");
            return -1;
        }

        if (Core.Me.GetCurrTarget().HasAura(MchBuffs.BeDismantle, 0))
        {
            LogHelper.Print("hotkey", "目标已经有扳手DeBuff了");
            return -2;
        }

        return Dismantle.RecentlyUsed() ? -3 : 0;
    }

    public void Run()
    {
        if (Core.Me.InCombat())
        {
            var slot = new Slot();
            slot.Add(MchSpells.Dismantle.GetSpell(Target));
            AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(slot);
        }
        else
        {
            AI.Instance.BattleData.NextSlot ??= new Slot();
            AI.Instance.BattleData.NextSlot.Add(MchSpells.Dismantle.GetSpell(Target));
        }
    }
}