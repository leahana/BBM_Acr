using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using ImGuiNET;

namespace BBM.MCH.Data.HotKeys;

/// <summary>
/// 停手HotKey
/// </summary>
public class HotKeyStopMove : IHotkeyResolver
{
    private const uint DiamondBack = SpellsDefine.DiamondBack;

    public void Draw(Vector2 size)
    {
        Vector2 size1 = size * 0.8f;
        ImGui.SetCursorPos(size * 0.1f);
        if (!Core.Resolve<MemApiIcon>().GetActionTexture(DiamondBack, out var textureWrap)) return;
        if (textureWrap != null) ImGui.Image(textureWrap.ImGuiHandle, size1);
    }

    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(DiamondBack.GetSpell(), size, isActive);
    }

    public int Check() => 0;

    public void Run()
    {
        Core.Resolve<MemApiMove>().CancelMove();
    }
}