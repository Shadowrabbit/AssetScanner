// ********************************************************
// AseWDuView.cs
// 描述：AssetTreeViewItem组成AssetTreeView
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 19:51:40
// ********************************************************

using UnityEditor.IMGUI.Controls;

public class
    AseWDuView : AseBaseTreeView<AseWDuModel, AseWDuTDepAssetModel>
{
    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="state"></param>
    /// <param name="multiColumnHeader"></param>
    public AseWDuView(TreeViewState state, MultiColumnHeader multiColumnHeader) :
        base(state, multiColumnHeader) {
    }
}