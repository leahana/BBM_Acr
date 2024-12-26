using BBM.MCH.Data;

namespace BBM.MCH.Utils;

public static class MchQtHelper
{
    // 全金属爆发
    public static bool QtFullMetalField()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.FullMetalField);
    }

    // 飞轮QT
    public static bool QtExcavator()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.Excavator);
    }
}