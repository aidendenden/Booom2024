using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor.PackageManager.UI;
 
// 在Unity编辑器加载时执行的自动保存和播放模式检测类
[InitializeOnLoad]
public class AutoSaveAndPlayMode : EditorWindow
{
    private  static float saveTime=300; // 默认的自动保存时间间隔，单位为秒
    private static float nextSave = 0; // 下一次自动保存的时间戳
    private string userInput = "300"; // 用户输入的时间间隔，默认为 300 秒
 
    // 静态构造函数，在类被加载时注册播放模式状态改变事件
    static AutoSaveAndPlayMode()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
 
    // 菜单项：个人插件 -> 自动保存
    [MenuItem("个人插件/自动保存")]
    static void Init()
    {
        
        // 创建 AutoSaveAndPlayMode 窗口实例
        AutoSaveAndPlayMode window = (AutoSaveAndPlayMode)EditorWindow.GetWindowWithRect(typeof(AutoSaveAndPlayMode), new Rect(0, 0, 300, 80));
        window.Show();
    }
 
    // 在窗口进行绘制
    void OnGUI()
    { 
        // 从EditorPrefs中读取保存的值，如果没有则使用默认值
        saveTime = EditorPrefs.GetFloat("AutoSave_saveTime",300);
        
        // 若下一次保存的时间戳为 0，则初始化为当前时间加上自动保存时间间隔
        if (nextSave == 0)
        {
            nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
        }
        // 显示自动保存时间间隔和下一次保存时间
        float timeToSave = nextSave - (float)EditorApplication.timeSinceStartup;
        EditorGUILayout.LabelField("下一次保存:", timeToSave.ToString() + " 秒");
 
        // 用户输入框及秒单位
        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("自动保存间隔:");
        userInput = EditorGUILayout.TextField(userInput, GUILayout.Width(50));
        GUILayout.Label("秒", GUILayout.Width(30));
        GUILayout.EndHorizontal();
 
        // 当用户按下回车键时执行保存
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return )
        {
            if (float.TryParse(userInput, out float newSaveTime))
            {
                saveTime = newSaveTime;
                nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
                // 保存用户输入的值到EditorPrefs
                EditorPrefs.SetFloat("AutoSave_saveTime", saveTime);
                SaveScene();
            }
            else
            {
                Debug.LogError("无效的时间间隔，请输入有效的数字。");
            }
        }
        else
        {
            SaveScene();
        }
 
        // 实时更新窗口
        Repaint();
    }
 
    // 手动保存当前场景的方法
    void SaveScene()
    {
        // 若不处于播放模式，且当前时间超过下一次保存的时间戳
        if (!EditorApplication.isPlaying && EditorApplication.timeSinceStartup > nextSave)
        {
            // 直接保存当前所有打开的场景
            bool saveOK = EditorSceneManager.SaveOpenScenes();
            // 在控制台中输出保存成功或失败的消息
            Debug.Log("保存场景 " + (saveOK ? "成功" : "失败！"));
            // 更新下一次保存的时间戳
            nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
        }
    }
 
    // 播放模式状态改变时的回调方法
    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // 在即将切换到播放模式时执行的代码
            bool shouldSaveScene = EditorUtility.DisplayDialog("保存 场景", "是否要在进入播放模式之前保存场景?", "是", "否");
 
            if (shouldSaveScene)
            {
                EditorSceneManager.SaveOpenScenes();
            }
        }
    }
}