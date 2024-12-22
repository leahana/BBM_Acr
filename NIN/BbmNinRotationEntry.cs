using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using Anmi.Dragoon;
using BBM.Setting;
using BBM.Triggers;
using ImGuiNET;

namespace BBM;

public class BbmNinRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "NINBBM";

    private readonly List<SlotResolverData> _slotResolvers =
    [
        new(new SlotResolverGcdBase(), SlotMode.Gcd),

        // offGcd队列
        new(new SlotResolverOffGcdBase(), SlotMode.OffGcd)
    ];


    public Rotation? Build(string settingFolder)
    {
        // 初始化设置
        NinSettings.Build(settingFolder);

        // 初始化QT （依赖了设置的数据）
        BuildQt();
        var rot = new Rotation(_slotResolvers)
        {
            TargetJob = Jobs.Ninja,
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


    // 声明当前要使用的UI的实例 示例里使用QT
    public static JobViewWindow QT { get; private set; }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    private void BuildQt()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        QT = new JobViewWindow(NinSettings.Instance.JobViewSave, NinSettings.Instance.Save, "AE BRD [仅作为开发示范]");
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

    // 设置界面
    public void OnDrawSetting()
    {
        NinUiSettings.Instance.Draw();
    }

    public void OnUIUpdate()
    {
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


    public void Dispose()
    {
        // 释放资源
    }
}