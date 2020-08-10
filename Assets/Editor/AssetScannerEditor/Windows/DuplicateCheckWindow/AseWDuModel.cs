// ********************************************************
// AssetTreeModel.cs
// 描述：资源树模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 18:56:18
// ********************************************************

using System.Linq;

public class AseWDuModel : AseBaseModel<AseWDuTDepAssetModel>
{
    public AseWDuModel(string assetFolder) : base(assetFolder) {
        //资源列表列表
        var assetModelList = CreateAssetModelList(assetFolder);
        //资源列表组
        AssetTreeGroups = assetModelList.GroupBy(info => info.md5)
            .Where(group => group.Count() > 1);
    }
}