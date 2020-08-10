// ********************************************************
// AseWBTAssetModel.cs
// 描述：资源树列表项 资源模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 18:14:10
// ********************************************************

using System.Collections.Generic;
using System.Linq;

public class AseWBTAssetModel
{
    public string guid;             //唯一标识
    public string fileRelativePath; //文件所在相对路径
    public string name;             //文件名称
    public string createTime;       //文件创建时间
    public string fileSize;         //文件大小    
    public string md5;              //md5值

    public List<AseWBTAssetModel> refModelList = new List<AseWBTAssetModel>(); //该资源引用的资源的数据列表
    public List<AseWBTAssetModel> depModelList = new List<AseWBTAssetModel>(); //该资源依赖的资源的数据列表
    public virtual string this[int index] => string.Empty;

    public AseWBTAssetModel() {
    }

    protected AseWBTAssetModel(AseWBTAssetModel assetModel) {
        guid             = assetModel.guid;
        fileRelativePath = assetModel.fileRelativePath;
        name             = assetModel.name;
        createTime       = assetModel.createTime;
        fileSize         = assetModel.fileSize;
        md5              = assetModel.md5;
        refModelList     = assetModel.refModelList;
        depModelList     = assetModel.depModelList;
    }

    /// <summary>
    ///是否被不同模块引用
    /// </summary>
    public bool IsRefByDiffModule() {
        if (refModelList.Count == 0) {
            return false;
        }

        var anyRef = refModelList[0]; //其中一个引用
        //遍历引用 是否存在某个引用所属模块与AnyRef不同
        return refModelList.Any(modelRefAsset =>
            !modelRefAsset.currentFolder.Equals(anyRef.currentFolder));
    }

    /// <summary>
    /// 所在目录
    /// </summary>
    public string currentFolder => fileRelativePath.GetFolder(name);
}