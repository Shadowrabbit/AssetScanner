// ********************************************************
// AseWDeTRefAssetItem.cs
// 描述：依赖者资源
// 作者：ShadowRabbit 
// 创建时间：2020年8月8日 18:33:17
// ********************************************************

using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWDeTRefAssetItem : AseWBTAssetItem
{
    public AseWDeTRefAssetItem(int id, int depth, string displayName, AseWBTAssetModel model) : base(id, depth,
        displayName, model) {
    }

    /// <summary>
    /// 列绘制
    /// </summary>
    /// <param name="cellRect"></param>
    /// <param name="column"></param>
    /// <param name="selected"></param>
    /// <param name="focused"></param>
    public override void ColGUI(Rect cellRect, int column, bool selected, bool focused) {
        //首列绘制图标
        if (column == 0) {
            DrawIcon(ref cellRect);
        }

        if (column > 1) {
            return;
        }

        TreeView.DefaultGUI.Label(cellRect, model[column], selected, focused);
    }
}