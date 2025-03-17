using AEAssist.CombatRoutine;
using AEAssist.Helper;
using BBM.MCH.Managers;
using BBM.MCH.Settings;

namespace BBM.MCH;

public class MchRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "BBM";

    // Qt管理
    private static readonly MchQtManager MchQtManger = MchQtManager.Instance;

    // 决策管理
    private static readonly MchSlotResolverManager MchSlotResolverManager = MchSlotResolverManager.Instance;

    // 起手管理
    private static readonly MchOpenerManager MchOpenerManager = MchOpenerManager.Instance;


    public Rotation? Build(string settingFolder)
    {
        // Ui设置初始化放入Try catch。
        try
        {
            // 初始化设置
            MchSettings.Build(settingFolder);
            // 初始化QT （依赖了设置的数据）
            MchQtManger.BuildQt();
        }
        catch (Exception e)
        {
            LogHelper.Error("BBM-Mch:Failed to build Qt. message:" + e.Message);
            throw;
        }


        // 设置Rotation基本信息和技能决策
        var rot = new Rotation(MchSlotResolverManager.GetSlotResolvers())
        {
            TargetJob = Jobs.Machinist,
            AcrType = AcrType.HighEnd,
            MinLevel = 100,
            MaxLevel = 100,
            Description = "目前仅支持100级高难，其他没写" +
                          "\n 沉淀",
        };

        // 添加起手设置
        rot.AddOpener(MchOpenerManager.GetOpener);

        // 添加各种事件回调
        rot.SetRotationEventHandler(new MchRotationEventHandler());

        // rot.AddHotkeyEventHandlers();
        // 添加时间轴触发器
        rot.AddTriggers();
        return rot;
    }

    public IRotationUI GetRotationUI()
    {
        return MchQtManager.Qt;
    }

    public void OnDrawSetting()
    {
        MchUiSettings.Instance.Draw();
    }

    public void Dispose()
    {
    }
}