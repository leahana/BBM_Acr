using System.Numerics;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace BBM.MCH.Settings
{
    /// <summary>
    /// 配置文件适合放一些一般不会在战斗中随时调整的开关数据
    /// 如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
    /// 非开关类型的配置都放配置里 比如诗人绝峰能量配置
    /// </summary>
    public class MchSettings
    {
        public static MchSettings Instance;

        #region 标准模板代码 可以直接复制后改掉类名即可

        private static string path;

        public static void Build(string settingPath)
        {
            path = Path.Combine(settingPath, nameof(MchSettings) + ".json");
            if (!File.Exists(path))
            {
                Instance = new MchSettings();
                Instance.Save();
                return;
            }

            try
            {
                Instance = JsonHelper.FromJson<MchSettings>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Instance = new MchSettings();
                LogHelper.Error(e.ToString());
            }
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            File.WriteAllText(path, JsonHelper.ToJson(this));
        }

        #endregion

        public int Opener = 0; // 起手选择
        public bool UsePeloton = true; // 使用速行 默认开启
        public bool WildfireFirst = true; // 提前野火
        public bool UsePotionInOpener = false; // 是否在开场使用药水
        public readonly double GcdCooldownLimit = 600.0;
        public int MinBattery = 50; // 最小电量
        public int MinHeat = 50; // 最大热量
        public int GrabItLimit = 300;

        public static readonly JobViewSave JobViewSave = new()
        {
            QtLineCount = 3,
            MainColor = new Vector4(0.336f, 0.278f, 0.866f, 0.700f), //
            // MainColor = new Vector4(0.030f, 0.286f, 0.955f, 0.700f), //
            QtWindowBgAlpha = 0.0f,
            QtHotkeySize = new Vector2(60, 60)
        }; // QT设置存档
    }
}