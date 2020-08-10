// ********************************************************
// ASEWDependenciesCheck.cs
// 描述：被依赖检测窗口
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:54:58
// ********************************************************

using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWDeWindow : EditorWindow
{
    private AseWDeController       _assetTreeControllerDepCheck; //控制器
    private SearchField            _searchField;                 //搜索框
    private AseMultiColumnHeader   _assetMultiColumnHeader;      //多列标题布局
    private MultiColumnHeaderState _multiColumnHeaderState;      //多列标题布局状态

    private void Awake() {
        Init();
    }

    private void OnGUI() {
        //工具栏

        #region ToolBar

        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        //展开
        if (GUILayout.Button(AseUIDef.Instance.guiContentExpandAll, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            _assetTreeControllerDepCheck.AssetTreeView.ExpandAll();
        }

        //折叠
        if (GUILayout.Button(AseUIDef.Instance.guiContentCollapseAll, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            _assetTreeControllerDepCheck.AssetTreeView.CollapseAll();
        }

        //搜索
        _assetTreeControllerDepCheck.AssetTreeView.searchString =
            _searchField.OnToolbarGUI(_assetTreeControllerDepCheck.AssetTreeView.searchString);
        EditorGUILayout.EndHorizontal();

        #endregion

        //绘制资源树
        _assetTreeControllerDepCheck.AssetTreeView.OnGUI(GUILayoutUtility.GetRect(0, maxSize.x, 0, maxSize.y));
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="refFolder">引用资源的文件目录</param>
    /// <param name="assetFolder">被引用的资源所在目录</param>
    /// <param name="publicAssetFolder">重复资源堆放公共目录</param>
    public static void Open(string refFolder, string assetFolder, string publicAssetFolder) {
        //数据
        var assetTreeModel = new AseWDeModel(assetFolder, refFolder);
        var window         = GetWindow<AseWDeWindow>();
        window.Focus();
        //视图
        var assetTreeView = new AseWDeView(new TreeViewState(), window._assetMultiColumnHeader);
        //控制器
        window._assetTreeControllerDepCheck = new AseWDeController(assetTreeView, assetTreeModel);
        //资源树重载
        window._assetTreeControllerDepCheck.AssetTreeView.Reload();
        //展开全部
        assetTreeView.ExpandAll();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init() {
        //标题
        titleContent = AseUIDef.Instance.guiContentDependencyCheck;
        //搜索框
        _searchField = new SearchField();
        //多列标题
        _multiColumnHeaderState = CreateMultiColumnHeader();
        _assetMultiColumnHeader = new AseMultiColumnHeader(_multiColumnHeaderState);
        //自适应
        _assetMultiColumnHeader.ResizeToFit();
    }

    /// <summary>
    /// 创建多列标题状态
    /// </summary>
    private MultiColumnHeaderState CreateMultiColumnHeader() {
        var columns = new[] {
            //名称
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentName,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                sortingArrowAlignment = TextAlignment.Right,
                width                 = AseUIDef.ButtonWidth1 * 5,
                minWidth              = AseUIDef.ButtonWidth1 * 5,
                autoResize            = false,
                allowToggleVisibility = false
            },
            //依赖次数
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentDepCount,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1,
                minWidth              = AseUIDef.ButtonWidth1,
                autoResize            = false,
                allowToggleVisibility = true
            },
            //路径
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentPath,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1 * 7,
                minWidth              = AseUIDef.ButtonWidth1 * 7,
                autoResize            = false,
                allowToggleVisibility = true
            },
        };

        return new MultiColumnHeaderState(columns);
    }
}