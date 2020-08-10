// ********************************************************
// AseWDeTRefAssetModel.cs
// 描述：依赖者资源 数据模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月8日 17:29:08
// ********************************************************

public class AseWDeTRefAssetModel : AseWBTAssetModel
{
    public AseWDeTRefAssetModel(AseWBTAssetModel assetModel) : base(assetModel) {
    }

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    public override string this[int index] {
        get {
            switch (index) {
                case 0:
                    return name;
                case 1:
                    //依赖数量
                    return depModelList.Count.ToString();
            }

            return string.Empty;
        }
    }
}