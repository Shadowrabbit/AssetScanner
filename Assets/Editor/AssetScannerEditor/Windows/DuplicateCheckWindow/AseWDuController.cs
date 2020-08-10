// ********************************************************
// AssetTreeController.cs
// 描述：资源树控制器
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 10:51:07
// ********************************************************

using System.Linq;
using UnityEditor.IMGUI.Controls;

public class AseWDuController : AseBaseController<AseWDuModel, AseWDuTDepAssetModel>
{
    public AseWDuController(AseWDuView assetTreeView,
        AseWDuModel                    assetTreeModel) : base(
        assetTreeView,
        assetTreeModel) {
    }

    /// <summary>
    /// 构建资源树根节点
    /// </summary>
    /// <param name="root"></param>
    public override void BuildRoot(ref TreeViewItem root) {
        var id = 1;
        //资源树数据不存在
        if (AssetTreeModel?.AssetTreeGroups == null) {
            return;
        }

        //构建组
        var groups = AssetTreeModel.AssetTreeGroups;
        foreach (var group in groups) {
            var groupModel = new AseWDuTGroupModel(group.Count());
            var groupItem = new AseWDuTGroupItem(id++, -1,
                string.Format(AseUIDef.FileContFormat, group.Count()), groupModel);
            root.AddChild(groupItem);
            //构建每一组内的资源
            foreach (var assetModel in group) {
                var depAssetModel = new AseWDuTDepAssetModel(assetModel);
                var assetItem     = new AseWDuTDepAssetItem(id++, -1, assetModel.name, depAssetModel);
                groupItem.AddChild(assetItem);
            }
        }
    }
}