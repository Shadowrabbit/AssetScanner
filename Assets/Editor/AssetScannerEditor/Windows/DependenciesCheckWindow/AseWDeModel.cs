// ********************************************************
// AseWDeModel.cs
// 描述：被依赖资源树数据模型
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:56:51
// ********************************************************

using UnityEngine;

public class AseWDeModel : AseBaseAssetTreeModel
{
    public AseWDeModel(string assetFolder, string refFolder) : base(assetFolder, refFolder) {
        modelFolderRoot = new AseWBTFolderModel(refFolder); //目录根节点
        modelFolderMap.Add(refFolder, modelFolderRoot);
        BuildFolderTree(modelFolderRoot); //构建目录树
        foreach (var assetModel in refAssetModelList) {
            //关联路径与资源
            var currentFolder = assetModel.currentFolder;
            if (!modelFolderMap.ContainsKey(currentFolder)) {
                Debug.LogError("找不到目录:" + currentFolder);
                return;
            }

            //将引用资源挂载到目录下
            modelFolderMap[currentFolder].modelAssetList.Add(assetModel);
        }
    }
}