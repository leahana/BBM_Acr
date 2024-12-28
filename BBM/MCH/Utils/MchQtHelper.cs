using AEAssist.Helper;
using BBM.MCH.Data;

namespace BBM.MCH.Utils;

public static class MchQtHelper
{
    private const int QtYes = 101;
    private const int QtNo = -101;

    private static int _qtResult(bool result) => result ? QtYes : QtNo;

    // 飞轮QT
    private static int QtExcavator()
    {
        var result = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseExcavator);
        return _qtResult(result);
    }

// 添加一个字典来存储不同 Qt 对应的逻辑
    private static readonly Dictionary<string, Func<int>> QtResolvers = new()
    {
        { MchQtConstantsCn.UseExcavator, QtExcavator },
        { MchQtConstantsCn.UseFullMetalField, QtFullMetalField },
        { MchQtConstantsCn.UseChainSaw, QtUseChainSaw },
        { MchQtConstantsCn.UseAirAnchor, QtUseAirAnchor },
        { MchQtConstantsCn.UseDrill, QtUseDrill },
        { MchQtConstantsCn.ReserveDoubleCheck, QtReserveDoubleCheck },
        { MchQtConstantsCn.UseOutbreak, QtUseOutbreak },
        { MchQtConstantsCn.UseLastOutbreak, QtUseLastOutbreak }
    };

// 全金属爆发Qt
    private static int QtFullMetalField()
    {
        var result = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseFullMetalField);
        return _qtResult(BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseFullMetalField));
    }


// 飞锯QT
    private static int QtUseChainSaw()
    {
        var result = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseChainSaw);
        return _qtResult(result);
    }

// 空气锚QT
    private static int QtUseAirAnchor()
    {
        var result = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseAirAnchor);
        return _qtResult(result);
    }

// 钻头QT
    private static int QtUseDrill()
    {
        var result = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseDrill);
        return _qtResult(result);
    }

// 将死Qt
    private static int QtReserveCheckMate()
    {
        var chargeFlag = true;
        var qt = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveCheckMate);
        // qt开启是保留
        if (qt)
        {
            chargeFlag = MchSpells.将死.GetSpell().Charges >= 2;
        }

        return _qtResult(chargeFlag);
    }

// 双将Qt
    private static int QtReserveDoubleCheck()
    {
        var chargeFlag = true;
        var qt = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveDoubleCheck);
        if (qt)
        {
            chargeFlag = MchSpells.双将.GetSpell().Charges >= 2;
        }

        return _qtResult(chargeFlag);
    }

// 爆发Qt
    private static int QtUseOutbreak()
    {
        var qt = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseOutbreak);
        return _qtResult(qt);
    }

// 最终爆发Qt
    private static int QtUseLastOutbreak()
    {
        var qt = BbmMchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseLastOutbreak);
        return _qtResult(qt);
    }


    public static int ValidateQtKeys(IEnumerable<string> qtKeys)
    {
        foreach (var qtKey in qtKeys)
        {
            if (!QtResolvers.TryGetValue(qtKey, out var qtFunc))
            {
                return -99; // Qt 未配置
            }

            LogHelper.Debug($"Qt {qtKey} 判断未通过.");
            var value = qtFunc();
            if (value == -101)
            {
                return value;
            }
        }

        return 0; // 所有 Qt 判断通过
    }
}