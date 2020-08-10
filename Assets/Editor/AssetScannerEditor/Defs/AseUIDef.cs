// ********************************************************
// AseUIDef.cs
// 描述：UI相关定义
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 11:16:56
// ********************************************************

using UnityEngine;

public class AseUIDef : AseBaseSingleTon<AseUIDef>
{
    public const string SettingFolder = "Assets/Editor/AssetScannerEditor/Assets/"; //设置资源储存目录

    public const string SettingPath = "Assets/Editor/AssetScannerEditor/Assets/AssetScannerSetting.asset"; //设置文件目录

    public readonly string[] pubFilterFolders = {
        "Assets/Bundles/UI/UIAtlas/Common_1",
        "Assets/Bundles/UI/UIAtlas/Background",
        "Assets/Bundles/UI/UIAtlas/Tachie",
        "Assets/Bundles/UI/UIAtlas/Card",
        "Assets/Bundles/UI/UIAtlas/SkillIcon",
        "Assets/Bundles/UI/UIAtlas/Goods",
        "Assets/Bundles/UI/UIAtlas/FurnitureIcons",
    };

    #region Size

    public const int   VerticalPadding       = 2;   //拖动item的内部垂直间距
    public const float ButtonWidth1          = 75f; //按钮宽度
    public const float ButtonWidth2          = 40f;
    public const float ReorderableItemHeight = 55f; //可拖动item高度
    public const float IconSize              = 16f; //图标尺寸

    #endregion

    #region GUIContent

    public readonly GUIContent guiContentTitle             = new GUIContent("标题");
    public readonly GUIContent guiContentCheckList         = new GUIContent("检查列表");
    public readonly GUIContent guiContentAssetFolder       = new GUIContent("依赖资源目录", "被依赖的资源所在目录");
    public readonly GUIContent guiContentReferenceFolder   = new GUIContent("引用资源目录", "引用其他资源的资源所在目录");
    public readonly GUIContent guiContentPublicAssetFolder = new GUIContent("公共资源目录", "用来存放被多模块引用的资源的路径");
    public readonly GUIContent guiContentDuplicateCheck    = new GUIContent("重复检测");
    public readonly GUIContent guiContentDependencyCheck   = new GUIContent("依赖检测");
    public readonly GUIContent guiContentReferenceCheck    = new GUIContent("引用检测");
    public readonly GUIContent guiContentName              = new GUIContent("名称");
    public readonly GUIContent guiContentPath              = new GUIContent("路径");
    public readonly GUIContent guiContentFileSize          = new GUIContent("文件大小");
    public readonly GUIContent guiContentCreateTime        = new GUIContent("创建时间");
    public readonly GUIContent guiContentMD5               = new GUIContent("MD5值");
    public readonly GUIContent guiContentExpandAll         = new GUIContent("展开");
    public readonly GUIContent guiContentCollapseAll       = new GUIContent("折叠");
    public readonly GUIContent guiContentSortOut           = new GUIContent("一键整理", "将被多模块引用的资源移交至公共区");
    public readonly GUIContent guiContentDataNotFound      = new GUIContent("没有找到数据");
    public readonly GUIContent guiContentRefCount          = new GUIContent("引用次数");
    public readonly GUIContent guiContentDepCount          = new GUIContent("依赖数量");
    public readonly GUIContent guiContentFolder            = new GUIContent("所在目录");

    #endregion

    #region Str

    public const string StrSelect            = "选择";
    public const string StrTitleAssetScanner = "资源扫描器";
    public const string StrFinding           = "正在查询";
    public const string FileContFormat       = "文件数:{0}";
    public const string StrMoving            = "正在移动文件";

    #endregion
}