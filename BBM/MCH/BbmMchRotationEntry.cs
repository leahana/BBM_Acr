using System.Diagnostics;
using System.Numerics;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using BBM.MCH.Ability;
using BBM.MCH.Data;
using BBM.MCH.GCD;
using BBM.MCH.Opener;
using BBM.MCH.Settings;
using BBM.MCH.Triggers;
using BBM.MCH.Utils;
using BBM.NIN;
using ImGuiNET;

namespace BBM.MCH;

public class BbmMchRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "BBM";
    public static JobViewWindow Qt { get; private set; }

    public void Dispose()
    {
    }


    private readonly List<SlotResolverData> _slotResolvers =
    [
        new(new MchAbilityUseBattery(), SlotMode.OffGcd),
        new(new MchGcdBlazingShot(), SlotMode.Gcd),
        new(new MchGcdChainsaw(), SlotMode.Gcd),
        new(new MchGcdAirAnchor(), SlotMode.Gcd),
        new(new MchGcdDrill(), SlotMode.Gcd),
        new(new MchGcdExcavator([MchQtConstantsCn.UseExcavator]), SlotMode.Gcd),
        new(new MchGcdFullMetalField(), SlotMode.Gcd),
        new(new MchGcdAdvanced(), SlotMode.Gcd),
        new(new MchGcdBaseCombo(), SlotMode.Gcd),
        new(new MchAbilityBarrelStabilizer(), SlotMode.OffGcd),
        new(new MchAbilityHyperCharge(), SlotMode.OffGcd),
        new(new MchAbilityWildfire(), SlotMode.OffGcd),
        new(new MchAbilityReassemble(), SlotMode.OffGcd),
        new(new MchAbilityCheckMate(), SlotMode.OffGcd),
        new(new MchAbilityDoubleCheck(), SlotMode.OffGcd),
        new(new MchAbilitySecondWind(), SlotMode.OffGcd)
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
            AcrType = AcrType.HighEnd,
            MinLevel = 100,
            MaxLevel = 100,
            Description = "绝赞测试中",
        };

        rot.AddOpener(GetOpener);
        // 添加各种事件回调
        // rot.SetRotationEventHandler(new MchRotationEventHandler());

        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerActionQt());
        return rot;
    }

    private static IOpener? GetOpener(uint level)
    {
        switch (MchSettings.Instance.Opener)
        {
            case 0:
            case 1:
                return new MchCommonDrillOpener100();
            case 2:
                break;
        }

        return new MchCommonDrillOpener100();
    }


    private void BuildQt()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        Qt = new JobViewWindow(MchSettings.Instance.JobViewSave, MchSettings.Instance.Save, "bbm Mch jobView");


        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        // QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        AddQtTab();

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        AddQt();

        // 添加快捷按钮 (带技能图标)
        AddQtKey();

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

    private void AddQtTab()
    {
        Qt.AddTab("通用", DrawQtGeneral);
        Qt.AddTab("Dev", DrawQtDev);
    }

    private void AddQtKey()
    {
        Qt.AddHotkey("爆发药", new HotKeyResolver_Potion());
        Qt.AddHotkey("极限技", new HotKeyResolver_LB());
        Qt.AddHotkey("冲刺", new HotKeyResolver_疾跑());
        Qt.AddHotkey("冲刺", new HotKeyResolver_疾跑());
    }

    private void AddQt()
    {   

        Qt.AddQt(QtKey.UsePotion, false, MchQtConstantsCn.UsePotion);
        Qt.AddQt(QtKey.UseFullMetalField, true, MchQtConstantsCn.UseFullMetalField);
        Qt.AddQt(QtKey.ReserveCheckMate, false, MchQtConstantsCn.ReserveCheckMate);
        Qt.AddQt(QtKey.ReserveDoubleCheck, false, MchQtConstantsCn.ReserveDoubleCheck);
        Qt.AddQt(QtKey.UseChainSaw, true, MchQtConstantsCn.UseChainSaw);
        Qt.AddQt(QtKey.UseExcavator, false, MchQtConstantsCn.UseExcavator);
        Qt.AddQt(QtKey.UseAirAnchor, true, MchQtConstantsCn.UseAirAnchor);
        Qt.AddQt(QtKey.UseDrill, true, MchQtConstantsCn.UseDrill);
        Qt.AddQt(QtKey.UseOutbreak, false, MchQtConstantsCn.UseOutbreak);
        Qt.AddQt(QtKey.UseLastOutbreak, false, MchQtConstantsCn.UseLastOutbreak);
        
    }

    private static string UpdateLog = "2024.12.27 新增标准起手" +
                                      "\n第二行些什么我还没想好";

    private void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        if (ImGui.CollapsingHeader("   更新日志"))
        {
            ImGui.Text(UpdateLog);
        }

        ImGui.Separator();
        if (ImGui.CollapsingHeader("   重要说明"))
        {
            ImGui.Text("能力及插入相关：连续两个能力技插入间隔在620ms以下（可在FFLogs上查）");
            ImGui.Text("推荐使用NiGuangOwO佬的三插插件，我自己用的是最优双插模式550ms。开了更流畅，兄弟们开。");
            if (ImGui.Button("FuckAnimationLock"))
            {
                string url = "https://github.com/NiGuangOwO/DalamudPlugins";
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true // 在默认浏览器中打开
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"打开浏览器失败：{ex.Message}");
                }
            }

            ImGui.Separator();

            if (ImGui.Button("反馈问题"))
            {
                string url = "https://discord.com/channels/1191648233454313482/1191649639796064346";
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true // 在默认浏览器中打开
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"打开浏览器失败：{ex.Message}");
                }
            }
        }

        ImGui.Separator();
        if (ImGui.CollapsingHeader("   基础设置"))
        {
            ImGui.Text("当前模式：" + "高难模式");
            ImGui.Separator();
            var opener = MchSettings.Instance.Opener switch
            {
                0 => "100级 标准钻头起手",
                1 => "还没几把写好的起手",
                _ => "100级 标准钻头起手"
            };

            if (ImGui.BeginCombo("起手选择", opener))
            {
                if (ImGui.Selectable("100级 标准钻头起手"))
                    MchSettings.Instance.Opener = 0;
                if (ImGui.Selectable("还没几把写好的起手"))
                    MchSettings.Instance.Opener = 1;
                ImGui.EndCombo();
            }

            ImGui.Separator();

            ImGui.Text("设置抢开延迟:");
            // 拖动条
            if (ImGui.SliderInt("延迟 (ms)", ref MchSettings.Instance.GrabItLimit, 0, 1000))
            {
                // 拖动条调整后逻辑处理
            }

            ImGui.Text($"当前延迟: {MchSettings.Instance.GrabItLimit} ms");
            ImGui.Separator();
            ImGui.Text("设置电量阈值:");
            // 拖动条
            var instanceMinBattery = MchSettings.Instance.MinBattery;
            int step = 10; // 设置步长值
            if (ImGui.SliderInt("电量", ref instanceMinBattery, 0, 100))
            {
                // 拖动条调整后逻辑处理
                instanceMinBattery = (instanceMinBattery / step) * step;
                // 确保不超过范围
                instanceMinBattery = Math.Clamp(instanceMinBattery, 0, 100);
                // 拖动条调整后逻辑处理
                MchSettings.Instance.MinBattery = instanceMinBattery;
            }

            ImGui.Text($"当前电量: {MchSettings.Instance.MinBattery}");
            ImGui.Separator();
            if (!Qt.GetQt("爆发药"))
                ImGui.TextColored(new(0.866f, 0.609f, 0.278f, 1.000f), "如果你希望使用爆发药，请在QT面板中开启爆发药开关");
            ImGui.Checkbox("起手吃爆发药", ref MchSettings.Instance.UsePotionInOpener);
            ImGui.Separator();
            var noClipGcd3 = SettingMgr.GetSetting<GeneralSettings>().NoClipGCD3;
            if (!noClipGcd3)
                ImGui.TextColored(new Vector4(0.866f, 0.609f, 0.278f, 1.000f)
                    , "  未开启全局能力技能不卡GCD，可能导致本ACR产生能力技插入问题，建议开启"
                      + "\n  开启方法：AE首页→左侧ACR→设置→能力技→勾选 “全局能力技能不卡GCD”");
            ImGui.Checkbox("全局能力技能不卡GCD", ref noClipGcd3);
            ImGui.Separator();
            ImGui.Text("勾了也没用 还没写 哈哈：");
            ImGui.Checkbox("速行", ref MchSettings.Instance.UsePeloton);
            ImGui.SameLine();
            ImGui.Separator();
            if (ImGui.Button("保存设置"))
                MchSettings.Instance.Save();
        }

        ImGui.Separator();
        if (ImGui.CollapsingHeader("   技能队列"))
        {
            ImGui.Separator();
            if (ImGui.Button("清除队列"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
            }

            ImGui.SameLine();
            if (ImGui.Button("清除一个"))
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Dequeue();
                AI.Instance.BattleData.HighPrioritySlots_GCD.Dequeue();
            }

            if (AI.Instance.BattleData.HighPrioritySlots_GCD.Count > 0)
                foreach (var obj in AI.Instance.BattleData.HighPrioritySlots_GCD)
                    ImGui.Text(" ==" + obj);
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
                foreach (var obj in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                    ImGui.Text(" --" + obj);
            ImGui.Separator();
        }
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
        return Qt;
    }

    public void OnDrawSetting()
    {
        MchUiSettings.Instance.Draw();
    }
}