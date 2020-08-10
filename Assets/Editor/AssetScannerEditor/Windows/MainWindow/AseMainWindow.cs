// ********************************************************
// AseMainWindow.cs
// 描述：资源扫描器编辑器窗口
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 11:41:49
// ********************************************************

using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class AseMainWindow : EditorWindow
{
    private AseSetting      _assetsScannerSetting; //设置文件实例
    private Vector2         _scrollViewVector2;    //滑动窗口尺寸
    private ReorderableList _reorderableList;      //可拖动重排序列表

    [MenuItem("Tools/AssetScanner/Open MainWindow &[")]
    private static void ShowWindow() {
        GetWindow<AseMainWindow>();
    }

    #region Life cycle

    private void Awake() {
        Init();
    }

    private void OnGUI() {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        //绘制列表项
        if (_reorderableList == null) return;
        _scrollViewVector2 = GUILayout.BeginScrollView(_scrollViewVector2);
        _reorderableList.DoLayoutList();
        GUILayout.EndScrollView();
    }

    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init() {
        titleContent.text = AseUIDef.StrTitleAssetScanner;
        //设置文件检测
        CheckSettingFile();
        _reorderableList =
            new ReorderableList(_assetsScannerSetting.AssetReferenceInfoList, null, true,
                true, true, true) {
                drawHeaderCallback
                    = OnDrawHeaderCallback,
                drawElementCallback
                    = OnDrawElementCallback
            };

        _reorderableList.elementHeight += AseUIDef.ReorderableItemHeight;
    }

    /// <summary>
    /// item绘制回调
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="index">从0开始</param>
    /// <param name="isActive"></param>
    /// <param name="isFocused"></param>
    private void OnDrawElementCallback(Rect rect, int index, bool isActive, bool isFocused) {
        //没有数据
        if (_assetsScannerSetting == null || _assetsScannerSetting.AssetReferenceInfoList.Count <= index) {
            return;
        }

        //当前引用信息
        var assetReferenceInfo = _assetsScannerSetting.AssetReferenceInfoList[index];

        //标题

        #region Title

        var rect11 = new Rect(rect) {
            y      = rect.y + AseUIDef.VerticalPadding, width = AseUIDef.ButtonWidth1,
            height = EditorGUIUtility.singleLineHeight
        };
        EditorGUI.LabelField(rect11, AseUIDef.Instance.guiContentTitle);
        var rect12 = new Rect(rect11) {x = rect11.x + rect11.width, width = rect.width - rect11.width};
        assetReferenceInfo.title = EditorGUI.TextField(rect12, assetReferenceInfo.title);

        #endregion

        //资源目录

        #region AssetFolder

        var rect21 = new Rect(rect11.x, rect12.y + rect12.height + AseUIDef.VerticalPadding, AseUIDef.ButtonWidth1,
            rect12.height);
        EditorGUI.LabelField(rect21, AseUIDef.Instance.guiContentAssetFolder);
        var rect22 = new Rect(rect21) {x = rect21.x + rect21.width, width = AseUIDef.ButtonWidth2};
        //目录选择
        if (GUI.Button(rect22, AseUIDef.StrSelect)) {
            var folder = EditorUtility.SaveFolderPanel(AseUIDef.StrSelect, assetReferenceInfo.assetFolder, "");
            assetReferenceInfo.assetFolder = folder.Replace(Application.dataPath, "Assets");
        }

        var rect23 = new Rect(rect22.x             + rect22.width, rect22.y,
            rect.width - AseUIDef.ButtonWidth1 * 3 - AseUIDef.ButtonWidth2,
            rect21.height);
        EditorGUI.LabelField(rect23, assetReferenceInfo.assetFolder);
        //重复检测
        var rect24 = new Rect(rect23.x + rect23.width, rect23.y, AseUIDef.ButtonWidth1, rect23.height);
        if (GUI.Button(rect24, AseUIDef.Instance.guiContentDuplicateCheck)) {
            AseWDuWindow.Open(assetReferenceInfo.referenceFolder, assetReferenceInfo.assetFolder,
                assetReferenceInfo.publicFolder);
        }

        //引用检测
        var rect25 = new Rect(rect24) {x = rect24.x + rect24.width};
        if (GUI.Button(rect25, AseUIDef.Instance.guiContentReferenceCheck)) {
            AseWRWindow.Open(assetReferenceInfo.referenceFolder, assetReferenceInfo.assetFolder,
                assetReferenceInfo.publicFolder);
        }

        #endregion

        //引用目录

        #region RefFolder

        var rect31 = new Rect(rect.x, rect24.y + rect24.height + AseUIDef.VerticalPadding, AseUIDef.ButtonWidth1,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(rect31, AseUIDef.Instance.guiContentReferenceFolder);
        var rect32 = new Rect(rect31) {x = rect31.x + rect31.width, width = AseUIDef.ButtonWidth2};
        //目录选择
        if (GUI.Button(rect32, AseUIDef.StrSelect)) {
            var folder =
                EditorUtility.SaveFolderPanel(AseUIDef.StrSelect, assetReferenceInfo.referenceFolder, "");
            assetReferenceInfo.referenceFolder = folder.Replace(Application.dataPath, "Assets");
        }

        var rect33 = new Rect(rect23) {y = rect32.y};
        EditorGUI.LabelField(rect33, assetReferenceInfo.referenceFolder);
        //依赖检测
        var rect34 = new Rect(rect25) {y = rect32.y};
        if (GUI.Button(rect34, AseUIDef.Instance.guiContentDependencyCheck)) {
            AseWDeWindow.Open(assetReferenceInfo.referenceFolder, assetReferenceInfo.assetFolder,
                assetReferenceInfo.publicFolder);
        }

        #endregion

        //公共目录

        #region PubFolder

        var rect41 = new Rect(rect.x, rect34.y + rect34.height + AseUIDef.VerticalPadding, AseUIDef.ButtonWidth1,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(rect41, AseUIDef.Instance.guiContentPublicAssetFolder);
        var rect42 = new Rect(rect41) {x = rect41.x + rect41.width, width = AseUIDef.ButtonWidth2};
        //目录选择
        if (GUI.Button(rect42, AseUIDef.StrSelect)) {
            var folder =
                EditorUtility.SaveFolderPanel(AseUIDef.StrSelect, assetReferenceInfo.publicFolder, "");
            assetReferenceInfo.publicFolder = folder.Replace(Application.dataPath, "Assets");
        }

        //目录
        var rect43 = new Rect(rect42.x + rect42.width, rect42.y, rect.width - rect41.width - rect42.width,
            rect42.height);
        EditorGUI.LabelField(rect43, assetReferenceInfo.publicFolder);

        #endregion

        //保存资源
        if (!GUI.changed) return;
        EditorUtility.SetDirty(_assetsScannerSetting);
        AssetDatabase.SaveAssets();
        Repaint();
    }

    /// <summary>
    /// 标题绘制回调
    /// </summary>
    /// <param name="rect"></param>
    private void OnDrawHeaderCallback(Rect rect) {
        EditorGUI.LabelField(rect, AseUIDef.Instance.guiContentCheckList);
    }

    /// <summary>
    /// 检测设置资源文件
    /// </summary>
    private void CheckSettingFile() {
        _assetsScannerSetting = AssetDatabase.LoadAssetAtPath<AseSetting>(AseUIDef.SettingPath);
        //设置文件不存在
        if (_assetsScannerSetting != null) return;
        Debug.LogWarning("资源扫描器配置文件不存在");
        //检查保存路径
        if (!Directory.Exists(AseUIDef.SettingFolder))
            Directory.CreateDirectory(AseUIDef.SettingFolder);
        //创建资源实例
        var assetScannerSetting = CreateInstance<AseSetting>();
        //创建资源
        AssetDatabase.CreateAsset(assetScannerSetting, AseUIDef.SettingPath);
        AssetDatabase.Refresh();
        Debug.Log("资源扫描器配置文件已创建");
    }
}