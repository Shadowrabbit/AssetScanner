// ********************************************************
// AseWRController.cs
// 描述：引用检测控制器
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:59:15
// ********************************************************

using UnityEditor.IMGUI.Controls;

public class AseWRController : AseBaseController<AseWRModel, AseWBTAssetModel>
{
    private int id = 1;

    public AseWRController(
        AseBaseTreeView<AseWRModel, AseWBTAssetModel> assetTreeView, AseWRModel assetTreeModel) : base(assetTreeView,
        assetTreeModel) {
    }

    /// <summary>
    /// 构建根节点
    /// </summary>
    /// <param name="root"></param>
    public override void BuildRoot(ref TreeViewItem root) {
        var rootModelFolder = AssetTreeModel.modelFolderRoot;                                        //根目录数据
        var rootItemFolder  = new AseWBTFolderItem(id++, -1, rootModelFolder.name, rootModelFolder); //根目录视图
        BuildFolders(rootItemFolder, rootModelFolder);                                               //构建根目录
        root.AddChild(rootItemFolder);
    }

    /// <summary>
    /// 构建目录树
    /// </summary>
    /// <param name="itemFolder"></param>
    /// <param name="modelFolder"></param>
    private void BuildFolders(TreeViewItem itemFolder, AseWBTFolderModel modelFolder) {
        BuildDepAssets(itemFolder, modelFolder);
        foreach (var submodelFolder in modelFolder.modelSubFolderList) {
            var subFolderItem = new AseWBTFolderItem(id++, -1, submodelFolder.name, submodelFolder);
            itemFolder.AddChild(subFolderItem);
            BuildFolders(subFolderItem, submodelFolder);
        }
    }

    /// <summary>
    /// 构建依赖资源树
    /// </summary>
    /// <param name="itemFolder"></param>
    /// <param name="modelFolder"></param>
    private void BuildDepAssets(TreeViewItem itemFolder, AseWBTFolderModel modelFolder) {
        var modelAssetList = modelFolder.modelAssetList;
        //构建资源item并挂树
        foreach (var modelAsset in modelAssetList) {
            var modelDep  = new AseWRTDepAssetModel(modelAsset);
            var itemAsset = new AseWRTDepAssetItem(id++, -1, modelAsset.name, modelDep);
            itemFolder.AddChild(itemAsset);
            //构建引用者item
            BuildRefAssets(itemAsset, modelDep);
        }
    }

    /// <summary>
    /// 构建引用者资源
    /// </summary>
    /// <param name="itemAsset">被引用的资源item</param>
    /// <param name="modelAsset">被引用的资源模型</param>
    private void BuildRefAssets(TreeViewItem itemAsset, AseWBTAssetModel modelAsset) {
        //被引用的资源模型
        var modelAssetRefList = modelAsset.refModelList;
        foreach (var modelAssetRef in modelAssetRefList) {
            //构建引用者模型
            var modelRef = new AseWRTRefAssetModel(modelAssetRef);
            var itemRef  = new AseWRTRefAssetItem(id++, -1, modelRef.name, modelRef);
            itemAsset.AddChild(itemRef);
        }
    }
}