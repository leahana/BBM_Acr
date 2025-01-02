using System.Diagnostics;
using System.Numerics;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using BBM.MCH.Data;
using BBM.MCH.Data.HotKeys;
using BBM.MCH.Settings;
using BBM.MCH.Utils;
using ImGuiNET;

namespace BBM.MCH.Managers;

/// <summary>
/// 机工士/ Qt管理
/// </summary>
public class MchQtManager
{
    private static readonly string UpdateLog = "2024.12.27 新增标准钻头起手" +
                                               "\n2024.12.28 第二行些什么我还没想好" +
                                               "\n2024.12.30 增加很多qt控制 暂不支持Aoe" +
                                               "\n2025.1.2" +
                                               "重构了一下入口类，提取相关qt时间轴设置";

    public static readonly MchQtManager Instance;

    public static readonly JobViewWindow Qt;

    private static readonly MchSettings MchSettings;

    // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
    private static readonly JobViewSave QtViewSave = new()
    {
        QtLineCount = 3,
        MainColor = new Vector4(0.336f, 0.278f, 0.866f, 0.700f),
        QtWindowBgAlpha = 0.0f,
        QtHotkeySize = new Vector2(60, 60)
    }; // QT设置存档

    static MchQtManager()
    
    {
        Qt = new JobViewWindow(QtViewSave, MchSettings.Instance.Save, "bbm Mch jobView");
        Instance = new MchQtManager();
        MchSettings = MchSettings.Instance;
    }

    public void BuildQt()
    {
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        // QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        AddQtTab();

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        AddQt();

        // 添加快捷按钮 (带技能图标)
        AddQtHotKey();
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
        Qt.AddTab("更新日志", DrawQtUpdateLog);
    }

    private void DrawQtUpdateLog(JobViewWindow jobViewWindow)
    {
        ImGui.Separator();
        if (ImGui.CollapsingHeader("   更新日志"))
        {
            ImGui.Text(UpdateLog);
        }

        ImGui.Separator();
    }

    private void AddQt()
    {
        Qt.AddQt(MchQtKeys.UsePotion, false, MchQtKeys.UsePotion);
        Qt.AddQt(MchQtKeys.UseOutbreak, true, MchQtKeys.UseOutbreak);
        Qt.AddQt(MchQtKeys.UseBaseComboFirst, false, MchQtKeys.UseBaseComboFirst);
        Qt.AddQt(MchQtKeys.UseChainSaw, true, MchQtKeys.UseChainSaw);
        Qt.AddQt(MchQtKeys.ReserveCheckMate, false, MchQtKeys.ReserveCheckMate);
        Qt.AddQt(MchQtKeys.ReserveDoubleCheck, false, MchQtKeys.ReserveDoubleCheck);
        Qt.AddQt(MchQtKeys.UseExcavator, true, MchQtKeys.UseExcavator);
        Qt.AddQt(MchQtKeys.UseFullMetalField, true, MchQtKeys.UseFullMetalField);
        Qt.AddQt(MchQtKeys.UseDrill, true, MchQtKeys.UseDrill);
        Qt.AddQt(MchQtKeys.UseAirAnchor, true, MchQtKeys.UseAirAnchor);
        Qt.AddQt(MchQtKeys.Aoe, false, MchQtKeys.Aoe);
        Qt.AddQt(MchQtKeys.UseReassemble, true, MchQtKeys.UseBaseComboFirst);
        Qt.AddQt(MchQtKeys.UseHyperCharge, true, MchQtKeys.UseHyperCharge);
    }

    private void AddQtHotKey()
    {
        Qt.AddHotkey("爆发药", new HotKeyResolver_Potion());
        Qt.AddHotkey("极限技", new HotKeyResolver_LB());
        Qt.AddHotkey("冲刺", new HotKeyResolver_疾跑());
        Qt.AddHotkey("防击退", new NormalSpellHotKeyResolver(SpellsDefine.ArmsLength, SpellTargetType.Target, func: null));
        Qt.AddHotkey("内丹", new NormalSpellHotKeyResolver(SpellsDefine.SecondWind, SpellTargetType.Target, func: null));
        Qt.AddHotkey("超荷",
            new NormalSpellHotKeyResolver(MchSpells.HyperCharge, SpellTargetType.Target,
                func: MchHotKeyHelper.HotkeyHyperCharge));
        Qt.AddHotkey("策动",
            new NormalSpellHotKeyResolver(SpellsDefine.Tactician, SpellTargetType.Target,
                MchHotKeyHelper.HotkeyCondTactician));
        Qt.AddHotkey("武装解除",
            new NormalSpellHotKeyResolver(MchSpells.Dismantle, SpellTargetType.Target,
                MchHotKeyHelper.HotkeyCondDismantle));
        Qt.AddHotkey("火焰喷射器",
            new NormalSpellHotKeyResolver(MchSpells.Flamethrower, SpellTargetType.Target,
                MchHotKeyHelper.HotkeyFlamethrower));
        Qt.AddHotkey("机器人结算",
            new NormalSpellHotKeyResolver(MchSpells.QueenOverdrive, SpellTargetType.Target,
                MchHotKeyHelper.HotKeyQueenOverdrive));
        Qt.AddHotkey("野火结算",
            new NormalSpellHotKeyResolver(MchSpells.Detonator, SpellTargetType.Target,
                MchHotKeyHelper.HotKeyDetonator));
    }

    // 画通用Tab页设置
    private void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
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
            // 目前仅支持100级高难
            ImGui.Text("当前模式：" + (MchSettings.IsHighEnd
                ? "高难模式"
                : "日常模式"));

            ImGui.Separator();
            var opener = MchSettings.Opener switch
            {
                0 => "100级 空气锚起手",
                1 => "100级 标准钻头起手",
                _ => "100级 空气锚起手"
            };

            if (ImGui.BeginCombo("起手选择", opener))
            {
                if (ImGui.Selectable("100级 空气锚起手"))
                    MchSettings.Opener = 0;
                if (ImGui.Selectable("100级 标准钻头起手"))
                    MchSettings.Opener = 1;
                ImGui.EndCombo();
            }

            ImGui.Separator();

            ImGui.Text("设置抢开延迟:");
            // 拖动条
            if (ImGui.SliderInt("延迟 (ms)", ref MchSettings.GrabItLimit, 0, 1000))
            {
                // 拖动条调整后逻辑处理
            }

            ImGui.Text($"当前延迟: {MchSettings.GrabItLimit} ms");
            ImGui.Separator();
            ImGui.Text("设置电量阈值:");

            // 拖动条
            var instanceMinBattery = MchSettings.MinBattery;
            int step = 10; // 设置步长值
            if (ImGui.SliderInt("电量", ref instanceMinBattery, 0, 100))
            {
                // 拖动条调整后逻辑处理
                instanceMinBattery = (instanceMinBattery / step) * step;
                // 确保不超过范围
                instanceMinBattery = Math.Clamp(instanceMinBattery, 0, 100);
                // 拖动条调整后逻辑处理
                MchSettings.MinBattery = instanceMinBattery;
            }

            ImGui.Text($"当前电量: {MchSettings.MinBattery}");
            ImGui.Separator();
            if (!Qt.GetQt(MchQtKeys.UsePotion))
                ImGui.TextColored(new(0.866f, 0.609f, 0.278f, 0.950f), "如果你希望使用爆发药，请在QT面板中开启爆发药开关");
            ImGui.Checkbox("起手吃爆发药", ref MchSettings.UsePotionInOpener);
            ImGui.Separator();
            var noClipGcd3 = SettingMgr.GetSetting<GeneralSettings>().NoClipGCD3;
            if (!noClipGcd3)
                ImGui.TextColored(new Vector4(0.866f, 0.609f, 0.278f, 0.950f)
                    , "  未开启全局能力技能不卡GCD，可能导致本ACR产生能力技插入问题，建议开启"
                      + "\n  开启方法：AE首页→左侧ACR→设置→能力技→勾选 “全局能力技能不卡GCD”");
            ImGui.Checkbox("全局能力技能不卡GCD", ref noClipGcd3);
            ImGui.Separator();
            ImGui.Text("勾了也没用 还没写 哈哈：");
            ImGui.Checkbox("速行", ref MchSettings.UsePeloton);
            ImGui.SameLine();
            ImGui.Separator();
            if (ImGui.Button("保存设置"))
                MchSettings.Save();
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
        }

        ImGui.Separator();
    }


    // 画dev页设置
    private void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("Dev相关信息");
        foreach (var v in jobViewWindow.GetQtArray())
        {
            ImGui.Text($"Qt按钮: {v}");
        }

        foreach (var v in jobViewWindow.GetHotkeyArray())
        {
            ImGui.Text($"Hotkey按钮: {v}");
        }
    }
}