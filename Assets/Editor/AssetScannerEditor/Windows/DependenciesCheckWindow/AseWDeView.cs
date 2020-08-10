// ********************************************************
 // AssetTreeViewDepCheck.cs
 // 描述：被依赖检测视图
 // 作者：ShadowRabbit 
 // 创建时间：2020年8月5日 16:56:27
 // ********************************************************
 
 using UnityEditor.IMGUI.Controls;
 
 public class AseWDeView : AseBaseTreeView<AseWDeModel, AseWBTAssetModel>
 {
     public AseWDeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader) {
     }
 }