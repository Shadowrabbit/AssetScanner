// ********************************************************
// AssetTreeViewRefCheck.cs
// 描述：引用检测视图
// 作者：ShadowRabbit 
// 创建时间：2020年8月5日 16:59:48
// ********************************************************

using UnityEditor.IMGUI.Controls;

public class
    AseWRView : AseBaseTreeView<AseWRModel, AseWBTAssetModel>
{
    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="state">视图状态</param>
    /// <param name="multiColumnHeader">多列标题布局</param>
    public AseWRView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state,
        multiColumnHeader) {
    }
}