using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using BBM.MCH.GCD;
using BBM.MCH.Settings;
using ImGuiNET;
using MCH.Triggers;

namespace MCH;

public class BbmMchRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "BBM";

    public void Dispose()
    {
    }

    private readonly List<SlotResolverData> _slotResolvers =
    [
        new(new MchGcdBlazingShot(), SlotMode.Gcd),
        new(new MchGcdAdvanced(), SlotMode.Gcd),
        new(new MchGcdBaseCombo(), SlotMode.Gcd),
        // // offGcd队列
        // new(new SlotResolverOffGcdBase(), SlotMode.OffGcd)
    ];


    public Rotation? Build(string settingFolder)
    {
        // 初始化设置
        MchSettings.Build(settingFolder);

        // 初始化QT （依赖了设置的数据）
        BuildQt();
        var rot = new Rotation(_slotResolvers)
        {
            TargetJob = Jobs.Machinist,
            AcrType = AcrType.Normal,
            MinLevel = 0,
            MaxLevel = 100,
            Description = "测试用1111111",
        };

        // 添加各种事件回调
        // rot.SetRotationEventHandler(new BardRotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerActionQt());
        return rot;
    }

    public static JobViewWindow QT { get; private set; }


    private void BuildQt()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        QT = new JobViewWindow(MchSettings.Instance.JobViewSave, MchSettings.Instance.Save, "MCH测试 jobView");
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        // QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("Dev", DrawQtDev);

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        QT.AddQt(QTKey.Base_01, true, "da1");
        QT.AddQt(QTKey.AOE, false);

        // 添加快捷按钮 (带技能图标)
        QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
        QT.AddHotkey("极限技", new HotKeyResolver_LB());

        /*
    // 这是一个自定义的快捷按钮 一般用不到
    // 图片路径是相对路径 基于AEAssist(C|E)NVersion/AEAssist
    // 如果想用AE自带的图片资源 路径示例: Resources/AE2Logo.png
    QT.AddHotkey("极限技", new HotkeyResolver_General("#自定义图片路径", () =>
    {
        // 点击这个图片会触发什么行为
        LogHelper.Print("你好");
    }));
    */
    }

    private void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画通用信息");
    }

    private void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画Dev信息");
        foreach (var v in jobViewWindow.GetQtArray())
        {
            ImGui.Text($"Qt按钮: {v}");
        }

        foreach (var v in jobViewWindow.GetHotkeyArray())
        {
            ImGui.Text($"Hotkey按钮: {v}");
        }
    }

    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    public void OnDrawSetting()
    {
        MchUiSettings.Instance.Draw();
    }
}