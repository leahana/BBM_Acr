using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using BBM.MCH.Extensions;
using BBM.MCH.Utils;
using ImGuiNET;
namespace BBM.MCH.Data.HotKeys;

using static SpellTargetType;

public class HotKeyTactician : IHotkeyResolver
{
    private const uint Tactician = MchSpells.Tactician;

    public void Draw(Vector2 size)
    {
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        if (!Core.Resolve<MemApiIcon>()
                .GetActionTexture(Tactician, out var textureWrap))
            return;
        if (textureWrap != null) ImGui.Image(textureWrap.ImGuiHandle, size1);
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(Tactician.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (Tactician.Cooldown() > 0.0)
        {
            LogHelper.Print("hotkey", "策动CD");
            return -1;
        }

        if (CombatHelper.HasRangedMitigation())
        {
            LogHelper.Print("hotkey", "已经有远敏减伤Buff了");
            return -2;
        }

        return Tactician.GetSpell().RecentlyUsed() ? -3 : 0;
    }

    public void Run()
    {
        if (Core.Me.GetCurrTarget() != null && Core.Me.InCombat())
        {
            var slot = new Slot();
            slot.Add(Tactician.GetSpell(Self));
            AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(slot);
        }
        else
        {
            AI.Instance.BattleData.NextSlot ??= new Slot();
            AI.Instance.BattleData.NextSlot.Add(new Spell(Tactician, Self));
        }
    }
}