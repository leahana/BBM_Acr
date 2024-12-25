namespace BBM.MCH;

// 直接定义好 方便编码
public static class QTKey
{
    public const string UseBaseGcd = "基础123";
    public const string AOE = "AOE";
    public const string Test1 = "Test1";
    public const string Test2= "Test2";
    public const string UsePotion = "爆发药";
    public const string FullMetalField = "全金属爆发";

}

public static class Qt
{
    public static bool Qtget(string qtName) => BbmMchRotationEntry.Qt.GetQt(qtName);
    public static bool Qtset(string qtName, bool qtValue) => BbmMchRotationEntry.Qt.SetQt(qtName, qtValue);
}