// ********************************************************
// AseBaseTreeView.cs
// 描述：视图基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 17:16:56
// ********************************************************

using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public abstract class AseBaseTreeView<T, T1> : TreeView
    where T : AseBaseModel<T1>
    where T1 : AseWBTAssetModel, new()
{
    public AseBaseController<T, T1> assetTreeController; //控制器

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="state"></param>
    /// <param name="multiColumnHeader"></param>
    protected AseBaseTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) :
        base(state, multiColumnHeader) {
        rowHeight                     = 20f;
        showAlternatingRowBackgrounds = true;
        showBorder                    = true;
    }

    /// <summary>
    /// 注册控制器
    /// </summary>
    /// <param name="controller"></param>
    public void RegisterController(AseBaseController<T, T1> controller) {
        assetTreeController = controller;
    }

    /// <summary>
    /// 构建根节点
    /// </summary>
    /// <returns></returns>
    protected override TreeViewItem BuildRoot() {
        var root = new TreeViewItem {id = 0, depth = -1, displayName = "Root"};
        assetTreeController.BuildRoot(ref root);
        SetupDepthsFromParentsAndChildren(root);
        return root;
    }

    /// <summary>
    /// 是否可以多选
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override bool CanMultiSelect(TreeViewItem item) {
        return false;
    }

    /// <summary>
    /// 双击回调
    /// </summary>
    /// <param name="id"></param>
    protected override void DoubleClickedItem(int id) {
        //获取view
        if (!(FindItem(id, rootItem) is AseWBTAssetItem item)) {
            return;
        }

        var model = item.model;
        if (!(model is AseWBTAssetModel assetModel)) {
            return;
        }

        //找到物体资源
        var obj = AssetDatabase.LoadAssetAtPath<Object>(assetModel.fileRelativePath);
        if (!obj) return;
        //选中
        Selection.activeObject = obj;
        EditorGUIUtility.PingObject(obj);
    }

    /// <summary>
    /// 点击回调
    /// </summary>
    /// <param name="id"></param>
    protected override void ContextClickedItem(int id) {
        //todo 打开菜单？
    }

    /// <summary>
    /// 行绘制 TreeView内部调用
    /// </summary>
    /// <param name="args">args.item是我们的AssetTreeViewItem</param>
    protected override void RowGUI(RowGUIArgs args) {
        var treeViewItem = args.item;
        //拿到TreeViewItem    
        if (!(treeViewItem is IColGUI iColGUI)) {
            base.RowGUI(args);
            return;
        }

        //对可见的Item进行绘制
        for (var i = 0; i < args.GetNumVisibleColumns(); ++i) {
            //内容渲染位置偏移 在折叠式箭头后面
            var contentIndent = GetContentIndent(treeViewItem);
            var rect          = args.GetCellRect(i);
            //第一列追加偏移量
            if (i == 0) {
                rect.xMin += contentIndent;
            }

            //列绘制
            iColGUI.ColGUI(rect, args.GetColumn(i), args.selected, args.focused);
        }
    }
}