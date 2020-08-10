// ********************************************************
// ASEStringExtension.cs
// 描述：string扩展
// 作者：ShadowRabbit 
// 创建时间：2020年8月7日 16:14:52
// ********************************************************

using System;
using System.Linq;

public static class AseStringExtension
{
    /// <summary>
    /// 路径转数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] PathToArray(this string str) {
        str = str.FixPath();
        return str.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// 路径修复
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string FixPath(this string str) {
        return str.Replace('\\', '/');
    }

    /// <summary>
    /// 获取所在路径
    /// </summary>
    /// <param name="str"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFolder(this string str, string fileName) {
        return str.Substring(0, str.Length - 1 - fileName.Length);
    }

    /// <summary>
    /// 是否在公共目录下 作为一键整理的过滤条件
    /// </summary>    
    /// <param name="folder">相对目录</param>
    /// <returns></returns>
    public static bool IsInPubFolder(this string folder) {
        return AseUIDef.Instance.pubFilterFolders.Any(filterFolder => filterFolder.Equals(folder));
    }
}