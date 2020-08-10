// ********************************************************
// AseWDuWindow.cs
// 描述：资源重复检测窗口
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 20:33:21
// ********************************************************

using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWDuWindow : EditorWindow
{
    private AseWDuController _assetTreeControllerDupCheck; //资源树视图控制器
    private SearchField                 _searchField;                 //搜索框
    private AseMultiColumnHeader      _assetMultiColumnHeader;      //多列标题布局
    private MultiColumnHeaderState      _multiColumnHeaderState;      //多列标题布局状态

    private void Awake() {
        Init();
    }

    private void OnGUI() {
        var assetTreeNodeList = _assetTreeControllerDupCheck.AssetTreeModel.AssetTreeGroups.ToList();
        if (assetTreeNodeList.Count == 0) {
            return;
        }

        //数据
        var assetTreeModel = _assetTreeControllerDupCheck.AssetTreeModel;
        if (assetTreeModel == null) {
            return;
        }

        //工具栏

        #region ToolBar

        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        //展开
        if (GUILayout.Button(AseUIDef.Instance.guiContentExpandAll, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            _assetTreeControllerDupCheck.AssetTreeView.ExpandAll();
        }

        //折叠
        if (GUILayout.Button(AseUIDef.Instance.guiContentCollapseAll, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            _assetTreeControllerDupCheck.AssetTreeView.CollapseAll();
        }

        //搜索
        _assetTreeControllerDupCheck.AssetTreeView.searchString =
            _searchField.OnToolbarGUI(_assetTreeControllerDupCheck.AssetTreeView.searchString);
        EditorGUILayout.EndHorizontal();

        #endregion

        //绘制资源树
        _assetTreeControllerDupCheck.AssetTreeView.OnGUI(GUILayoutUtility.GetRect(0, maxSize.x, 0, maxSize.y));
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="refFolder">引用资源的文件目录</param>
    /// <param name="assetFolder">被引用的资源所在目录</param>
    /// <param name="publicAssetFolder">重复资源堆放公共目录</param>
    public static void Open(string refFolder, string assetFolder, string publicAssetFolder) {
        //数据
        var assetTreeModel = new AseWDuModel(assetFolder);
        var window         = GetWindow<AseWDuWindow>();
        window.Focus();
        //视图
        var assetTreeView = new AseWDuView(new TreeViewState(), window._assetMultiColumnHeader);
        //控制器
        window._assetTreeControllerDupCheck = new AseWDuController(assetTreeView, assetTreeModel);
        //没有数据
        var assetTreeNodeList = window._assetTreeControllerDupCheck.AssetTreeModel.AssetTreeGroups.ToList();
        if (assetTreeNodeList.Count == 0) {
            window.ShowNotification(AseUIDef.Instance.guiContentDataNotFound);
            return;
        }

        //资源树重载
        window._assetTreeControllerDupCheck.AssetTreeView.Reload();
        //展开全部
        assetTreeView.ExpandAll();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init() {
        //标题
        titleContent = AseUIDef.Instance.guiContentDuplicateCheck;
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
                width                 = AseUIDef.ButtonWidth1 * 3,
                minWidth              = AseUIDef.ButtonWidth1 * 3,
                autoResize            = false,
                allowToggleVisibility = false
            },
            //相对路径
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentPath,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1 * 5,
                minWidth              = AseUIDef.ButtonWidth1 * 2,
                autoResize            = false,
                allowToggleVisibility = true
            },
            //文件大小
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentFileSize,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1,
                minWidth              = AseUIDef.ButtonWidth1,
                autoResize            = false,
                allowToggleVisibility = true
            },
            //创建时间
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentCreateTime,
                headerTextAlignment = TextAlignment.Left,
                canSort             = false,
                width               = AseUIDef.ButtonWidth1 * 2,
                minWidth            = AseUIDef.ButtonWidth1,
                autoResize          = true
            },
            //md5值
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentMD5,
                headerTextAlignment = TextAlignment.Left,
                canSort             = false,
                width               = AseUIDef.ButtonWidth1 * 3,
                minWidth            = AseUIDef.ButtonWidth1,
                autoResize          = true
            }
        };

        return new MultiColumnHeaderState(columns);
    }
}