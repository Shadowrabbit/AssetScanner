// ********************************************************
// AseBaseAssetTreeModel.cs
// 描述：资源树模型基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月10日 10:44:48
// ********************************************************

using System.Collections.Generic;
using System.IO;
using UnityEditor;

public abstract class AseBaseAssetTreeModel : AseBaseModel<AseWBTAssetModel>
{
    //依赖资源映射 guid->ASERTVIModelAsset    
    protected readonly Dictionary<string, AseWBTAssetModel> depAssetModelMap =
        new Dictionary<string, AseWBTAssetModel>();

    //目录映射 folder->ASETVIModelFolder
    protected readonly Dictionary<string, AseWBTFolderModel> modelFolderMap =
        new Dictionary<string, AseWBTFolderModel>();

    public             AseWBTFolderModel             modelFolderRoot;   //根节点    
    protected readonly IEnumerable<AseWBTAssetModel> depAssetModelList; //依赖资源列表
    protected readonly IEnumerable<AseWBTAssetModel> refAssetModelList; //引用资源列表

    /// <summary>    
    /// 构造器
    /// </summary>
    /// <param name="assetFolder">依赖资源的目录</param>
    /// <param name="refFolder">引用者的目录</param>
    protected AseBaseAssetTreeModel(string assetFolder, string refFolder) : base(assetFolder) {
        depAssetModelList = CreateAssetModelList(assetFolder); //依赖资源节点模型列表
        refAssetModelList = CreateAssetModelList(refFolder);   //引用资源节点模型列表
        //生成依赖映射
        foreach (var depAssetModel in depAssetModelList) {
            //依赖资源添加到字典
            if (!depAssetModelMap.ContainsKey(depAssetModel.guid)) {
                depAssetModelMap.Add(depAssetModel.guid, depAssetModel);
            }
        }

        foreach (var refAssetModel in refAssetModelList) {
            //设置引用依赖关系
            BindRefAndDep(refAssetModel);
        }
    }

    /// <summary>
    /// 构建目录树
    /// </summary>
    /// <param name="modelFolder"></param>
    protected void BuildFolderTree(AseWBTFolderModel modelFolder) {
        //搜索资源目录构建
        var assetFolders = Directory.GetDirectories(modelFolder.folder, "*", SearchOption.TopDirectoryOnly);
        foreach (var subFolder in assetFolders) {
            var subModelFolder = new AseWBTFolderModel(subFolder);   //子目录
            modelFolderMap.Add(subFolder.FixPath(), subModelFolder); //添加到字典
            modelFolder.modelSubFolderList.Add(subModelFolder);      //添加层级关系
            BuildFolderTree(subModelFolder);                         //递归层级构建
        }
    }

    /// <summary>
    /// 绑定引用依赖关系
    /// </summary>
    /// <param name="refAssetModel">节点数据</param>
    private void BindRefAndDep(AseWBTAssetModel refAssetModel) {
        //依赖资源的路径数组
        var depAssetPaths = AssetDatabase.GetDependencies(refAssetModel.fileRelativePath);
        foreach (var path in depAssetPaths) {
            //id
            var guid = AssetDatabase.AssetPathToGUID(path);
            //自身引用自身不算
            if (guid == refAssetModel.guid) {
                continue;
            }

            if (!depAssetModelMap.ContainsKey(guid)) {
                //Debug.LogWarning("依赖资源:" + path + "不在依赖搜索范围内");
                continue;
            }

            //依赖引用信息
            var depAssetModel = depAssetModelMap[guid];
            depAssetModel.refModelList.Add(refAssetModel);
            refAssetModel.depModelList.Add(depAssetModel);
        }
    }
}