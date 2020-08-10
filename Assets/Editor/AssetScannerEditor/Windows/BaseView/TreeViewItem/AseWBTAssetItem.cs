// ********************************************************
// AseWBTAssetItem.cs
// 描述：资源项
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 13:20:40
// ********************************************************

using UnityEditor;
using UnityEngine;

public class AseWBTAssetItem : AseWBTBaseItem<AseWBTAssetModel>
{
    protected AseWBTAssetItem(int id, int depth, string displayName, AseWBTAssetModel model) : base(id, depth,
        displayName, model) {
    }

    public override void ColGUI(Rect cellRect, int column, bool selected, bool focused) {
        //首列绘制图标
        if (column == 0) {
            DrawIcon(ref cellRect);
        }
    }

    /// <summary>
    /// 绘制图标
    /// </summary>
    /// <param name="cellRect"></param>
    protected override void DrawIcon(ref Rect cellRect) {
        if (icon != null) {
            base.DrawIcon(ref cellRect);
            return;
        }

        //节点的图标不存在 创建
        icon = AssetDatabase.GetCachedIcon(model.fileRelativePath) as Texture2D;
        //创建失败
        if (icon == null) return;
        //图标矩形
        var rectTex = new Rect(cellRect)
            {width = AseUIDef.IconSize, height = AseUIDef.IconSize, y = cellRect.y + AseUIDef.VerticalPadding};

        GUI.DrawTexture(rectTex, icon, ScaleMode.ScaleToFit);
        cellRect.xMin += rectTex.width;
    }
}