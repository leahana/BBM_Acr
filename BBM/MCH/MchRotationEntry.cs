using AEAssist.CombatRoutine;
using BBM.MCH.Managers;
using BBM.MCH.Settings;

namespace BBM.MCH;

public class MchRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "BBM";
    private static readonly MchSettings MchSettings = MchSettings.Instance;
    private static readonly MchQtManager MchQtManger = MchQtManager.Instance;
    private static readonly MchSlotResolverManager MchSlotResolverManager = MchSlotResolverManager.Instance;
    private static readonly MchOpenerManager MchOpenerManager = MchOpenerManager.Instance;

    public Rotation? Build(string settingFolder)
    {
        // 初始化设置
        MchSettings.Build(settingFolder);

        // 初始化QT （依赖了设置的数据）
        MchQtManger.BuildQt();

        // 设置Rotation基本信息和技能决策
        var rot = new Rotation(MchSlotResolverManager.GetSlotResolvers())
        {
            TargetJob = Jobs.Machinist,
            AcrType = AcrType.HighEnd,
            MinLevel = 100,
            MaxLevel = 100,
            Description = "目前仅支持100级高难，其他没写" +
                          "/n 沉淀",
        };

        // 添加起手设置
        rot.AddOpener(MchOpenerManager.GetOpener);

        // 添加各种事件回调
        // rot.SetRotationEventHandler(new MchRotationEventHandler());

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