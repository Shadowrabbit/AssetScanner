// ********************************************************
// AseWRTRefAssetModel.cs
// 描述：引用资源模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 15:19:05
// ********************************************************

public class AseWRTRefAssetModel : AseWBTAssetModel
{
    public AseWRTRefAssetModel() {
    }

    public AseWRTRefAssetModel(AseWBTAssetModel assetModel) : base(assetModel) {
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
                    //所在路径
                    return currentFolder;
            }

            return string.Empty;
        }
    }
}