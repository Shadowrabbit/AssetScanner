// ********************************************************
// ASERTVIAsset.cs
// 描述：依赖资源项
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 15:18:06
// ********************************************************

using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWRTDepAssetItem : AseWBTAssetItem
{
    public AseWRTDepAssetItem(int id, int depth, string displayName, AseWBTAssetModel model) : base(id, depth,
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

        if (column == 2 || column > 3) {
            return;
        }

        TreeView.DefaultGUI.Label(cellRect, model[column], selected, focused);
    }
}