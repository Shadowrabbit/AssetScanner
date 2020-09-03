// ********************************************************
// AssetTreeModelRefCheck.cs
// 描述：引用检测模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:59:33
// ********************************************************

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AseWRModel : AseBaseAssetTreeModel
{
    public AseWRModel(string depFolder, string refFolder) : base(depFolder, refFolder) {
        modelFolderRoot = new AseWBTFolderModel(depFolder); //目录根节点
        modelFolderMap.Add(depFolder, modelFolderRoot);
        BuildFolderTree(modelFolderRoot); //构建目录树
        foreach (var assetModel in depAssetModelList) {
            //关联路径与资源
            var currentFolder = assetModel.CurrentFolder;
            if (!modelFolderMap.ContainsKey(currentFolder)) {
                Debug.LogError("找不到目录:" + currentFolder);
                return;
            }

            //将依赖资源挂载到目录下
            modelFolderMap[currentFolder].modelAssetList.Add(assetModel);
        }
    }

    /// <summary>
    /// 获取被不同模块引用的资源模型列表
    /// </summary>
    /// <returns></returns>
    public IEnumerable<AseWBTAssetModel> GetDiffRefModelAssetList() {
        return depAssetModelMap.Values.Where(modelAsset => modelAsset.IsRefByDiffModule()).ToList();
    }
}