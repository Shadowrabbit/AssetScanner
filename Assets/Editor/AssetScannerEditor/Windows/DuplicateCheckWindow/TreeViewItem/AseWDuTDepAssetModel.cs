// ********************************************************
// AseWDuTDepAssetModel.cs
// 描述：依赖资源模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 10:34:57
// ********************************************************

public class AseWDuTDepAssetModel : AseWBTAssetModel
{
    public AseWDuTDepAssetModel() {
    }

    public AseWDuTDepAssetModel(AseWBTAssetModel assetModel) : base(assetModel) {
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
                    return fileRelativePath;
                case 2:
                    return fileSize;
                case 3:
                    return createTime;
                case 4:
                    return md5;
            }

            return string.Empty;
        }
    }
}