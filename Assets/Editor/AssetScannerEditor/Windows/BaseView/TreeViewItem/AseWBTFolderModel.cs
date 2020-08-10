// ********************************************************
// AseWBTFolderModel.cs
// 描述：目录项数据模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 15:18:54
// ********************************************************

using System.Collections.Generic;

public class AseWBTFolderModel
{
    public readonly string                 name;                                          //名称
    public readonly string                 folder;                                        //路径
    public readonly List<AseWBTAssetModel> modelAssetList = new List<AseWBTAssetModel>(); //挂载在该目录下的资源列表

    public readonly List<AseWBTFolderModel> modelSubFolderList = new List<AseWBTFolderModel>(); //子目录

    public AseWBTFolderModel(string folder) {
        this.folder = folder.FixPath();
        var names = this.folder.PathToArray();
        name = names[names.Length - 1];
    }
}