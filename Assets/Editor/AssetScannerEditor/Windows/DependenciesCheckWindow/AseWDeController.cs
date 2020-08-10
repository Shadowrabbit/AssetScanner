// ********************************************************
// AssetTreeControllerDepCheck.cs
// 描述：被依赖检测控制器
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:56:04
// ********************************************************

using UnityEditor.IMGUI.Controls;

public class AseWDeController : AseBaseController<AseWDeModel, AseWBTAssetModel>
{
    private int id = 1;

    public AseWDeController(AseBaseTreeView<AseWDeModel, AseWBTAssetModel> assetTreeView, AseWDeModel assetTreeModel) :
        base(assetTreeView, assetTreeModel) {
    }

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
        BuildRefAssets(itemFolder, modelFolder);
        foreach (var submodelFolder in modelFolder.modelSubFolderList) {
            var subFolderItem = new AseWBTFolderItem(id++, -1, submodelFolder.name, submodelFolder);
            itemFolder.AddChild(subFolderItem);
            BuildFolders(subFolderItem, submodelFolder);
        }
    }

    /// <summary>
    /// 构建引用者资源
    /// </summary>
    /// <param name="itemFolder"></param>
    /// <param name="modelFolder"></param>
    private void BuildRefAssets(TreeViewItem itemFolder, AseWBTFolderModel modelFolder) {
        var modelAssetList = modelFolder.modelAssetList;
        //构建引用资源item并挂树
        foreach (var assetModel in modelAssetList) {
            var refAssetModel = new AseWDeTRefAssetModel(assetModel);
            var refAssetItem  = new AseWDeTRefAssetItem(id++, -1, refAssetModel.name, refAssetModel);
            itemFolder.AddChild(refAssetItem);
            //构建依赖资源Item
            BuildDepAssets(refAssetItem, refAssetModel);
        }
    }

    /// <summary>
    /// 构建依赖资源树
    /// </summary>
    /// <param name="itemAsset">被引用的资源item</param>
    /// <param name="modelAsset">被引用的资源模型</param>
    private void BuildDepAssets(TreeViewItem itemAsset, AseWBTAssetModel modelAsset) {
        //依赖资源模型
        var edpModelList = modelAsset.depModelList;
        foreach (var depAssetModel in edpModelList) {
            //构建引用者模型
            var depModel     = new AseWDeTDepAssetModel(depAssetModel);
            var depAssetItem = new AseWDeTDepAssetItem(id++, -1, depModel.name, depModel);
            itemAsset.AddChild(depAssetItem);
        }
    }
}