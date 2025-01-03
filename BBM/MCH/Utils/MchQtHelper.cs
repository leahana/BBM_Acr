using AEAssist.Helper;
using BBM.MCH.Data;
using BBM.MCH.Managers;

namespace BBM.MCH.Utils;

/// <summary>
/// 机工士/ qt条件判断工具
/// </summary>
public static class MchQtHelper
{
    private const int QtYes = 101;
    private const int QtNo = -101;

    // 一般qt结果返回
    private static int _qtResult(bool result) => result ? QtYes : QtNo;


    // 添加一个字典来存储不同 Qt 对应的逻辑
    private static readonly Dictionary<string, Func<int>> QtResolvers = new()
    {
        { MchQtKeys.UseExcavator, QtExcavator },
        { MchQtKeys.UseFullMetalField, QtFullMetalField },
        { MchQtKeys.UseChainSaw, QtUseChainSaw },
        { MchQtKeys.UseAirAnchor, QtUseAirAnchor },
        { MchQtKeys.UseDrill, QtUseDrill },
        { MchQtKeys.ReserveCheckMate, QtReserveCheckMate },
        { MchQtKeys.ReserveDoubleCheck, QtReserveDoubleCheck },
        { MchQtKeys.UseOutbreak, QtUseOutbreak },
        { MchQtKeys.Aoe, QtAoe },
        { MchQtKeys.UseReassemble, QtUseReassemble },
        { MchQtKeys.UseBaseComboFirst, QtUseBaseComboFirst },
        { MchQtKeys.UseHyperCharge, QtUseHyperCharge },
        { MchQtKeys.UseWildfire, QtUseWildfire },
        { MchQtKeys.UseBarrelStabilizer, QtUseBarrelStabilizer }
    };

    /// <summary>
    /// 超荷Qt
    /// </summary>
    /// <returns></returns>
    private static int QtUseHyperCharge()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseHyperCharge);
        return qt ? 111 : -111;
    }

    /// <summary>
    /// 强制只打123
    /// </summary>
    /// <returns></returns>
    private static int QtUseBaseComboFirst()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseBaseComboFirst);
        return !qt ? 123 : -123;
    }

    /// <summary>
    /// 整备qt
    /// </summary>
    /// <returns></returns>
    private static int QtUseReassemble()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseReassemble);
        return qt ? 112 : -112;
    }

    /// <summary>
    /// 野火Qt
    /// </summary>
    /// <returns></returns>
    private static int QtUseWildfire()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseWildfire);
        return qt ? 113 : -113;
    }

    /// <summary>
    /// 枪管加热Qt
    /// </summary>
    /// <returns></returns>
    private static int QtUseBarrelStabilizer()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseBarrelStabilizer);
        return qt ? 114 : -114;
    }

    /// <summary>
    /// AOEqt 暂时无用
    /// </summary>
    /// <returns></returns>
    private static int QtAoe()
    {
        return _qtResult(MchQtManager.Qt.GetQt(MchQtKeys.Aoe));
    }

    /// <summary>
    /// 全金属爆发Qt
    /// </summary>
    /// <returns></returns>
    private static int QtFullMetalField()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseFullMetalField);
        return qt ? 161 : -161;
    }


    // 空气锚QT
    private static int QtUseAirAnchor()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseAirAnchor);
        return qt ? 151 : -151;
    }

    // 钻头QT
    private static int QtUseDrill()
    {
        var result = MchQtManager.Qt.GetQt(MchQtKeys.UseDrill);
        return result ? 153 : -153;
    }

    /// <summary>
    /// 飞锯QT
    /// </summary>
    private static int QtUseChainSaw()
    {
        var result = MchQtManager.Qt.GetQt(MchQtKeys.UseChainSaw);
        return result ? 152 : -152;
    }

    // 飞轮QT
    private static int QtExcavator()
    {
        var result = MchQtManager.Qt.GetQt(MchQtKeys.UseExcavator);
        return result ? 163 : -163;
    }

    // 将死Qt
    private static int QtReserveCheckMate()
    {
        //  开启 说明 保留不用 大于两层返回复数 交给Check
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.ReserveCheckMate);
        return !qt ? 145 : -145;
    }

    // 双将Qt
    private static int QtReserveDoubleCheck()
    {
        //  开启 说明 保留不用 大于两层返回复数
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.ReserveDoubleCheck);
        return !qt ? 146 : -146;
    }

    // 爆发Qt
    private static int QtUseOutbreak()
    {
        var qt = MchQtManager.Qt.GetQt(MchQtKeys.UseOutbreak);
        return qt ? 101 : -101;
    }


    public static int ValidateQtKeys(IEnumerable<string> qtKeys)
    {
        foreach (var qtKey in qtKeys)
        {
            if (!QtResolvers.TryGetValue(qtKey, out var qtFunc))
            {
                LogHelper.Debug($"Invalid QtKey: {qtKey}");
                return -199; // Qt 未配置
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