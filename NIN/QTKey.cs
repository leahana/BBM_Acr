using Anmi.Dragoon;
using BBM;

namespace Anmi.Dragoon;

// 直接定义好 方便编码
public static class QTKey
{
    public const string Base_01 = "Base_01111111";
    public const string AOE = "AOE";

}
public static class QT
{
    public static bool QTGET(string qtName) => BbmNinRotationEntry.QT.GetQt(qtName);
    public static bool QTSET(string qtName, bool qtValue) => BbmNinRotationEntry.QT.SetQt(qtName, qtValue);
}