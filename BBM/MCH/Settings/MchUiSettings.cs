namespace BBM.MCH.Settings;

/// <summary>
/// 机工士 Ui设置
/// </summary>
public class MchUiSettings
{
    public static readonly MchUiSettings Instance = new();

    private static MchSettings MchSettings => MchSettings.Instance;

    public void Draw()
    {
    }
}