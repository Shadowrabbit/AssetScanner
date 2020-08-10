// ********************************************************
// AseWBTFolderItem.cs
// 描述：目录项
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 15:18:31
// ********************************************************

using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWBTFolderItem : AseWBTBaseItem<AseWBTFolderModel>
{
    [NotNull] public override Texture2D icon => EditorGUIUtility.FindTexture("Folder Icon");

    public AseWBTFolderItem(int id, int depth, string displayName, AseWBTFolderModel model) : base(id, depth,
        displayName, model) {
    }

    public override void ColGUI(Rect cellRect, int column, bool selected, bool focused) {
        if (column != 0) return;
        //首列绘制图标
        DrawIcon(ref cellRect);
        TreeView.DefaultGUI.Label(cellRect, model.name, selected, focused);
    }
}