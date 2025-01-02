using AEAssist.CombatRoutine.Module.Opener;
using BBM.MCH.Opener;
using BBM.MCH.Settings;

namespace BBM.MCH.Managers;

/// <summary>
/// 机工士/起手管理
/// </summary>
public class MchOpenerManager
{
    public static readonly MchOpenerManager Instance;

    static MchOpenerManager()
    {
        Instance = new MchOpenerManager();
    }

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