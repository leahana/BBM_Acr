using System.Globalization;
using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ImGuiNET;

namespace BBM.MCH.Data.HotKeys;

/// <summary>
/// 技能HotKeyResolver （通用）
/// </summary>
/// <param name="spellId">技能Id</param>
/// <param name="targetType">目标类型</param>
/// <param name="func">技能条件判断</param>
public class NormalSpellHotKeyResolver(uint spellId, SpellTargetType targetType, Func<int>? func) : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {
        uint id = Core.Resolve<MemApiSpell>().CheckActionChange(spellId);
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(id, out var textureWrap))
            return;
        if (textureWrap != null) ImGui.Image(textureWrap.ImGuiHandle, size1);
        // Check if skill is on cooldown and apply grey overlay if true
        // 技能不在cd且可用
        if (!Core.Resolve<MemApiSpell>().CheckActionChange(spellId).GetSpell().IsReadyWithCanCast())
        {
            // Use ImGui.GetItemRectMin() and ImGui.GetItemRectMax() for exact icon bounds
            Vector2 overlayMin = ImGui.GetItemRectMin();
            Vector2 overlayMax = ImGui.GetItemRectMax();
            // Draw a grey overlay over the icon
            ImGui.GetWindowDrawList().AddRectFilled(
                overlayMin,
                overlayMax,
                ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.5f))); // 50% transparent grey
        }

        var cooldownRemaining = spellId.GetSpell().Cooldown.TotalMilliseconds / 1000;
        if (cooldownRemaining > 0)
        {
            // Convert cooldown to seconds and format as string 秒转换成String方便展示
            string cooldownText = Math.Ceiling(cooldownRemaining).ToString(CultureInfo.CurrentCulture);

            // 计算文本位置，向左下角偏移
            Vector2 textPos = ImGui.GetItemRectMin();
            textPos.X -= 1; // 向左移动一点
            textPos.Y += size1.Y - ImGui.CalcTextSize(cooldownText).Y + 5; // 向下移动一点

            // 绘制冷却时间文本
            ImGui.GetWindowDrawList()
                .AddText(textPos, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 1)), cooldownText);
        }
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(Core.Resolve<MemApiSpell>().CheckActionChange(spellId).GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (func != null)
        {
            return func();
        }

        return spellId.GetSpell().IsReadyWithCanCast() ? 0 : -1;
    }


    public new void Run()
    {
        Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(spellId).GetSpell(targetType);
        if (!MchCacheBattleData.Instance.HotkeyUseHighPrioritySlot)
        {
            AI.Instance.BattleData.NextSlot ??= new Slot();
            AI.Instance.BattleData.NextSlot.Add(spell);
        }
        else
        {
            Slot slot = new Slot();
            slot.Add(spell);
            if (spell.IsAbility())
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(slot);
            else
                AI.Instance.BattleData.HighPrioritySlots_GCD.Enqueue(slot);
        }
    }
}