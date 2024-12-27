using AEAssist.Helper;
using BBM.MCH.Data;

namespace BBM.MCH.Utils;

public static class MchQtHelper
{
    // 飞轮QT
    public static int QtExcavator()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseExcavator) ? 101 : -101;
    }

    // 添加一个字典来存储不同 Qt 对应的逻辑
    public static readonly Dictionary<string, Func<int>> QtResolvers = new()
    {
        { MchQtConstantsCn.UseExcavator, QtExcavator },
        // { MchQtConstantsCn.UseFullMetalField, QtFullMetalField }
    };

    // 全金属爆发
    public static bool QtFullMetalField()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseFullMetalField);
    }


    // 飞锯QT
    public static bool QtUseChainSaw()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseChainSaw);
    }

    // 空气锚QT
    public static bool QtUseAirAnchor()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseAirAnchor);
    }

    // 钻头QT
    public static bool QtUseDrill()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseDrill);
    }

    // 将死Qt
    public static bool QtReserveCheckMate()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveCheckMate);
    }

    // 双将Qt
    public static bool QtReserveDoubleCheck()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveDoubleCheck);
    }

    // 爆发Qt
    public static bool QtUseOutbreak()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseOutbreak);
    }

    // 最终爆发Qt
    public static bool QtUseOutUseLastOutbreak()
    {
        return BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseLastOutbreak);
    }


    public static int ValidateQtKeys(IEnumerable<string> qtKeys)
    {
        foreach (var qtKey in qtKeys)
        {
            if (!QtResolvers.TryGetValue(qtKey, out var qtFunc))
            {
                return -99; // Qt 未配置
            }

            {
                LogHelper.Debug($"Qt {qtKey} 判断未通过.");
                return qtFunc();
            }
        }

        return 0; // 所有 Qt 判断通过
    }
}