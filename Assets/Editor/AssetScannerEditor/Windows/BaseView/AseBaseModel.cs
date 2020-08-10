// ********************************************************
// AseBaseModel.cs
// 描述：资源树数据模型基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月6日 15:00:06
// ********************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public abstract class AseBaseModel<T> where T : AseWBTAssetModel, new()
{
    private const string Format = "yyyy-MM-dd HH:mm:ss";

    //资源组
    public IEnumerable<IGrouping<string, T>> AssetTreeGroups { get; protected set; }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="assetFolder">资源存放目录</param>
    protected AseBaseModel(string assetFolder) {
    }

    /// <summary>
    /// 创建节点模型列表
    /// </summary>
    /// <param name="assetFolder"></param>
    /// <returns></returns>
    protected IEnumerable<T> CreateAssetModelList(string assetFolder) {
        var assetTreeNodeModelList = new List<T>(); //节点列表
        try {
            EditorUtility.DisplayProgressBar(AseUIDef.StrFinding, string.Empty, 0f);
            var assetFileNames = Directory.GetFiles(assetFolder, "*", SearchOption.AllDirectories); //目标目录全部资源文件
            //过滤掉meta文件
            var fileInfos = (from assetFileName in assetFileNames
                select new FileInfo(assetFileName)
                into fileInfo
                where fileInfo.IsAssetFile()
                select fileInfo).ToArray();
            for (var i = 0; i < fileInfos.Length; i++) {
                var fileInfo = fileInfos[i];
                EditorUtility.DisplayProgressBar(AseUIDef.StrFinding, fileInfo.Name, i * 1f / fileInfos.Length);
                //设置数据
                var assetModel = CreateAssetModel(fileInfo);
                assetTreeNodeModelList.Add(assetModel);
            }
        }
        catch (Exception e) {
            Debug.LogError(e);
        }
        finally {
            EditorUtility.ClearProgressBar();
        }

        return assetTreeNodeModelList;
    }

    /// <summary>
    /// 创建资源数据
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <returns></returns>
    private T CreateAssetModel(FileInfo fileInfo) {
        //设置数据
        var assetModel = new T();
        //md5
        using (var md5 = MD5.Create()) {
            using (var stream = File.OpenRead(fileInfo.FullName)) {
                assetModel.md5 = BitConverter.ToString(md5.ComputeHash(stream)).ToLower();
            }
        }

        assetModel.name       = fileInfo.Name;
        assetModel.createTime = fileInfo.CreationTime.ToString(Format);
        //相对路径
        var filePath    = fileInfo.FullName;
        var fixFilePath = filePath.Replace("\\", "/");
        var relativePath =
            fixFilePath.Substring(fixFilePath.IndexOf("Assets", StringComparison.Ordinal));
        assetModel.fileRelativePath = relativePath;
        //id
        assetModel.guid = AssetDatabase.AssetPathToGUID(relativePath);
        //文件大小
        var fileSize = fileInfo.Length;
        assetModel.fileSize = fileSize >= 1 << 20
            ? $"{fileSize / 1024f / 1024f:F} Mb"
            : $"{fileSize         / 1024f:F} Kb";
        return assetModel;
    }
}