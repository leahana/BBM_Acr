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
        var result = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseExcavator);
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
        { MchQtConstantsCn.ReserveCheckMate, QtReserveCheckMate },
        { MchQtConstantsCn.ReserveDoubleCheck, QtReserveDoubleCheck },
        { MchQtConstantsCn.UseOutbreak, QtUseOutbreak },
        { MchQtConstantsCn.UseAoe, QtAoe },
        { MchQtConstantsCn.UseReassemble, QtUseReassemble },
        { MchQtConstantsCn.UseBaseComboFirst, QtUseBaseComboFirst }
    };

    /// <summary>
    /// 只打123
    /// </summary>
    /// <returns></returns>
    private static int QtUseBaseComboFirst()
    {
        return _qtResult(!MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseBaseComboFirst));
    }

    /// <summary>
    /// 整备qt
    /// </summary>
    /// <returns></returns>
    private static int QtUseReassemble()
    {
        return _qtResult(MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseReassemble));
    }

    /// <summary>
    /// AOEqt 暂时无用
    /// </summary>
    /// <returns></returns>
    private static int QtAoe()
    {
        return _qtResult(MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseAoe));
    }

    // 全金属爆发Qt
    private static int QtFullMetalField()
    {
        return _qtResult(MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseFullMetalField));
    }


// 飞锯QT
    private static int QtUseChainSaw()
    {
        var result = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseChainSaw);
        return _qtResult(result);
    }

// 空气锚QT
    private static int QtUseAirAnchor()
    {
        var result = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseAirAnchor);
        return _qtResult(result);
    }

// 钻头QT
    private static int QtUseDrill()
    {
        var result = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseDrill);
        return _qtResult(result);
    }

// 将死Qt
    private static int QtReserveCheckMate()
    {
        //  开启 说明 保留不用 大于两层返回复数
        var qt = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveCheckMate);
        if (qt && MchSpells.CheckMate.GetSpell().Charges < 2.9)
        {
            return MchSpells.CheckMate.GetSpell().Charges >= 2.0 && MchSpellHelper.GetHeat() >= 45 ? 1 : -45;
        }

        return MchSpells.CheckMate.GetSpell().Charges >= 2.5 ? 99 : _qtResult(!qt);
    }

// 双将Qt
    private static int QtReserveDoubleCheck()
    {
        //  开启 说明 保留不用 大于两层返回复数
        var qt = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.ReserveDoubleCheck);
        if (qt && MchSpells.DoubleCheck.GetSpell().Charges < 2.9)
        {
            return MchSpells.DoubleCheck.GetSpell().Charges >= 2.0 && MchSpellHelper.GetHeat() >= 45 ? 1 : -45;
        }

        return MchSpells.DoubleCheck.GetSpell().Charges >= 2.5 ? 99 : _qtResult(!qt);
    }

// 爆发Qt
    private static int QtUseOutbreak()
    {
        var qt = MchRotationEntry.Qt.GetQt(MchQtConstantsCn.UseOutbreak);
        return _qtResult(qt);
    }


    public static int ValidateQtKeys(IEnumerable<string> qtKeys)
    {
        foreach (var qtKey in qtKeys)
        {
            if (!QtResolvers.TryGetValue(qtKey, out var qtFunc))
            {
                LogHelper.Debug($"Invalid QtKey: {qtKey}");
                return -99; // Qt 未配置
            }

            var value = qtFunc();
            LogHelper.Debug($"Qt {qtKey} 判断 qtFunc={qtFunc()},qtValue={value}");
            if (value < 0)
            {
                return value;
            }
        }

        return 0; // 所有 Qt 判断通过
    }
}