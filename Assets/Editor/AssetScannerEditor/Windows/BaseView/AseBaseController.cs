// ********************************************************
// AseBaseController.cs
// 描述：控制器基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 17:16:09
// ********************************************************

using UnityEditor.IMGUI.Controls;

public abstract class AseBaseController<T, T1>
    where T : AseBaseModel<T1>
    where T1 : AseWBTAssetModel, new()
{
    //视图
    public AseBaseTreeView<T, T1> AssetTreeView { get; }

    //数据模型
    public T AssetTreeModel { get; }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="assetTreeView"></param>
    /// <param name="assetTreeModel"></param>
    protected AseBaseController(AseBaseTreeView<T, T1> assetTreeView,
        T                                             assetTreeModel) {
        AssetTreeView  = assetTreeView;
        AssetTreeModel = assetTreeModel;
        assetTreeView.RegisterController(this);
    }

    /// <summary>
    /// 构建资源树根节点
    /// </summary>
    /// <param name="root"></param>
    public abstract void BuildRoot(ref TreeViewItem root);
}