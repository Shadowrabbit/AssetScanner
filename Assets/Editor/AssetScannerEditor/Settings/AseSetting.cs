// ********************************************************
// ASESetting.cs
// 描述：资源扫描器配置
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 10:37:34
// ********************************************************

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetScannerSetting", menuName = "Create AssetScannerSetting", order = 1)]
public class AseSetting : ScriptableObject
{
    [Serializable]
    public class AssetReferenceInfo
    {
        public string title;           //标题
        public string assetFolder;     //资源所在文件夹
        public string referenceFolder; //引用资源的文件所在文件夹
        public string publicFolder;    //公共资源存放目录
    }

    [SerializeField]
    private List<AssetReferenceInfo> assetReferenceInfoList = new List<AssetReferenceInfo>(); //资源引用信息列表

    public List<AssetReferenceInfo> AssetReferenceInfoList => assetReferenceInfoList; //资源引用信息列表访问器
}