using AEAssist.CombatRoutine.Module.Opener;
using BBM.MCH.Settings;
using BBM.MCH.SlotSequences.Opener;

namespace BBM.MCH.Managers;

/// <summary>
/// 机工士/起手管理
/// </summary>
public class MchOpenerManager
{
    public static readonly MchOpenerManager Instance = new();

    public IOpener? GetOpener(uint level)
    {
        switch (MchSettings.Instance.Opener)
        {
            case 0:
                return new MchAirAnchorOpener100();
            case 1:
                return new MchCommonDrillOpener100();
            case 2:
                break;
        }

        return new MchCommonDrillOpener100();
    }
}