// ********************************************************
// ASEATVDItemGroup.cs
// 描述：重复检测窗口的资源组
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 10:32:20
// ********************************************************

using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class AseWDuTGroupItem : AseWBTBaseItem<AseWDuTGroupModel>
{
    public AseWDuTGroupItem(int id, int depth, string displayName, AseWDuTGroupModel model) :
        base(id, depth, displayName, model) {
    }

    /// <summary>
    /// 列绘制
    /// </summary>
    /// <param name="cellRect"></param>
    /// <param name="column"></param>
    /// <param name="selected"></param>
    /// <param name="focused"></param>
    public override void ColGUI(Rect cellRect, int column, bool selected, bool focused) {
        if (column != 0) return;
        //首列绘制图标
        DrawIcon(ref cellRect);
        TreeView.DefaultGUI.Label(cellRect, "文件数:" + model.count, selected, focused);
    }
}