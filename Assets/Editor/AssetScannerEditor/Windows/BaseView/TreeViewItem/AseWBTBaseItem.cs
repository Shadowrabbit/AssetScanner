// ********************************************************
// ASEBaseAssetTreeViewItem.cs
// 描述：资源树列表项基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 10:16:57
// ********************************************************

using UnityEditor.IMGUI.Controls;
using UnityEngine;

public abstract class AseWBTBaseItem<T> : TreeViewItem, IColGUI
{
    public readonly T model; //列表项模型

    protected AseWBTBaseItem(int id, int depth, string displayName, T model) : base(id, depth,
        displayName) {
        this.model = model;
    }


    /// <summary>
    /// 绘制图标
    /// </summary>
    /// <param name="cellRect"></param>
    protected virtual void DrawIcon(ref Rect cellRect) {
        //节点的图标不存在 
        if (icon == null) {
            return;
        }

        //图标矩形
        var rectTex = new Rect(cellRect)
            {width = AseUIDef.IconSize, height = AseUIDef.IconSize, y = cellRect.y + AseUIDef.VerticalPadding};

        //图标存在 绘制
        GUI.DrawTexture(rectTex, icon, ScaleMode.ScaleToFit);
        cellRect.xMin += rectTex.width;
    }

    /// <summary>
    /// 列绘制
    /// </summary>
    /// <param name="cellRect">item的矩形</param>
    /// <param name="column">从1开始</param>
    /// <param name="selected"></param>
    /// <param name="focused"></param>    
    public abstract void ColGUI(Rect cellRect, int column, bool selected, bool focused);
}