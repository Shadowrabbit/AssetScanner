// ********************************************************
// AseWRTDepAssetModel.cs
// 描述：依赖资源模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 15:18:41
// ********************************************************

public class AseWRTDepAssetModel : AseWBTAssetModel
{
    public AseWRTDepAssetModel() {
    }

    public AseWRTDepAssetModel(AseWBTAssetModel assetModel) : base(assetModel) {
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
                    //被引用次数
                    return refModelList.Count.ToString();
                case 3:
                    //所在目录
                    return fileRelativePath;
            }

            return string.Empty;
        }
    }
}