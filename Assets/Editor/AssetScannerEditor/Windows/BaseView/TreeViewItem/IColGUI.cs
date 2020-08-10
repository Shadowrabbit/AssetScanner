// ********************************************************
// IColGUI.cs
// 描述：列绘制接口
// 作者：ShadowRabbit 
// 创建时间：2020年8月10日 14:06:54
// ********************************************************
using UnityEngine;

public interface IColGUI
{
    /// <summary>
    /// 列绘制
    /// </summary>
    /// <param name="cellRect">item的矩形</param>
    /// <param name="column">从1开始</param>
    /// <param name="selected"></param>
    /// <param name="focused"></param>    
    void ColGUI(Rect cellRect, int column, bool selected, bool focused);
}