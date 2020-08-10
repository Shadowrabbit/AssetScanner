// ********************************************************
// ReferenceCheckWindow.cs
// 描述：引用检测窗口
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:58:42
// ********************************************************

using System;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWRWindow : EditorWindow
{
    private AseWRController           _assetTreeControllerRefCheck; //资源树视图控制器
    private SearchField            _searchField;                 //搜索框
    private AseMultiColumnHeader   _assetMultiColumnHeader;      //多列标题布局
    private MultiColumnHeaderState _multiColumnHeaderState;      //多列标题布局状态
    private string                 _publicAssetFolder;           //公共存放目录

    /// <summary>
    /// 打开窗口            
    /// </summary>
    /// <param name="refFolder">引用资源的文件目录</param>
    /// <param name="assetFolder">被引用的资源所在目录</param>
    /// <param name="publicAssetFolder">重复资源堆放公共目录</param>
    public static void Open(string refFolder, string assetFolder, string publicAssetFolder) {
        //数据
        var assetTreeModel = new AseWRModel(assetFolder, refFolder);
        var window         = GetWindow<AseWRWindow>();
        window._publicAssetFolder = publicAssetFolder;
        window.Focus();
        //视图
        var assetTreeView = new AseWRView(new TreeViewState(), window._assetMultiColumnHeader);
        //控制器
        window._assetTreeControllerRefCheck = new AseWRController(assetTreeView, assetTreeModel);
        //资源树重载
        window._assetTreeControllerRefCheck.AssetTreeView.Reload();
        //展开全部
        assetTreeView.ExpandAll();
    }

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
            _assetTreeControllerRefCheck.AssetTreeView.ExpandAll();
        }

        //折叠
        if (GUILayout.Button(AseUIDef.Instance.guiContentCollapseAll, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            _assetTreeControllerRefCheck.AssetTreeView.CollapseAll();
        }

        //一键整理
        if (GUILayout.Button(AseUIDef.Instance.guiContentSortOut, EditorStyles.toolbarButton,
            GUILayout.Width(AseUIDef.ButtonWidth1))) {
            SortOutAsset();
        }

        //搜索
        _assetTreeControllerRefCheck.AssetTreeView.searchString =
            _searchField.OnToolbarGUI(_assetTreeControllerRefCheck.AssetTreeView.searchString);
        EditorGUILayout.EndHorizontal();

        #endregion

        //绘制资源树
        _assetTreeControllerRefCheck.AssetTreeView.OnGUI(GUILayoutUtility.GetRect(0, maxSize.x, 0, maxSize.y));
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init() {
        //标题
        titleContent = AseUIDef.Instance.guiContentReferenceCheck;
        //搜索框
        _searchField = new SearchField();
        //多列标题
        _multiColumnHeaderState = CreateMultiColumnHeader();
        _assetMultiColumnHeader = new AseMultiColumnHeader(_multiColumnHeaderState);
        //自适应
        _assetMultiColumnHeader.ResizeToFit();
    }

    /// <summary>
    /// 整理资源
    /// </summary>    
    private void SortOutAsset() {
        //过滤出需要移动的资源文件
        var assetsNeedToMove = _assetTreeControllerRefCheck.AssetTreeModel.GetDiffRefModelAssetList()
            .Where(x => !x.currentFolder.IsInPubFolder());
        try {
            var assetToMoveList = assetsNeedToMove.ToList();
            for (var i = 0; i < assetToMoveList.Count; i++) {
                EditorUtility.DisplayProgressBar(AseUIDef.StrMoving, string.Empty, (float) i / assetToMoveList.Count);
                var newPath = _publicAssetFolder.FixPath() + "/" + assetToMoveList[i].name; //移动后的新路径
                var strError =
                    AssetDatabase.MoveAsset(assetToMoveList[i].fileRelativePath, newPath);
                //报错
                if (!string.IsNullOrEmpty(strError)) {
                    Debug.LogError(strError);
                }

                Debug.Log(assetToMoveList[i].name + "移动至公共目录:" + _publicAssetFolder);
            }
        }
        catch (Exception e) {
            Debug.LogError(e);
        }
        finally {
            EditorUtility.ClearProgressBar();
        }
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
            //引用次数
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentRefCount,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1,
                minWidth              = AseUIDef.ButtonWidth1,
                autoResize            = false,
                allowToggleVisibility = true
            },
            //所在目录
            new MultiColumnHeaderState.Column {
                headerContent =
                    AseUIDef.Instance.guiContentFolder,
                headerTextAlignment   = TextAlignment.Left,
                canSort               = false,
                width                 = AseUIDef.ButtonWidth1 * 5,
                minWidth              = AseUIDef.ButtonWidth1 * 5,
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