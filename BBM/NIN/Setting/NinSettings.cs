using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.IO;

namespace BBM.NIN.Setting;

/// <summary>
/// 配置文件适合放一些一般不会在战斗中随时调整的开关数据
/// 如果一些开关需要在战斗中调整 或者提供给时间轴操作 那就用QT
/// 非开关类型的配置都放配置里 比如诗人绝峰能量配置
/// </summary>
public class NinSettings
{
    public static NinSettings Instance;
    
    #region 标准模板代码 可以直接复制后改掉类名即可
    private static string path;
    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath,nameof(NinSettings) + ".json");
        if (!File.Exists(path))
        {
            Instance = new NinSettings();
            Instance.Save();
            return;
        }
        try
        {
            Instance = JsonHelper.FromJson<NinSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }
    #endregion
    
        
    public bool BaseBottom1Boolean = true; // 使用速行 默认开启
    public int BaseBottom2Value = 100; // 非爆发期绝峰多少能量再用

    public JobViewSave JobViewSave = new(); // QT设置存档
}