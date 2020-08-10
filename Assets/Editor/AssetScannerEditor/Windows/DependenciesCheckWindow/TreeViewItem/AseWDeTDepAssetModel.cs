// ********************************************************
// AseWDeTDepAssetModel.cs
// 描述：被依赖的资源
// 作者：ShadowRabbit 
// 创建时间：2020年8月8日 17:36:50
// ********************************************************

public class AseWDeTDepAssetModel : AseWBTAssetModel
{
    public AseWDeTDepAssetModel(AseWBTAssetModel assetModel) : base(assetModel) {
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
                case 2:
                    //文件路径
                    return fileRelativePath;
            }

            return string.Empty;
        }
    }
}