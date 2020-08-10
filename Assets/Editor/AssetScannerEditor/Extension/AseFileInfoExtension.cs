// ********************************************************
// AseFileInfoExtension.cs
// 描述：文件信息扩展
// 作者：ShadowRabbit 
// 创建时间：2020年8月6日 14:51:59
// ********************************************************

using System.IO;

public static class AseFileInfoExtension
{
    /// <summary>
    /// 文件是否为unity的资源文件
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <returns></returns>
    public static bool IsAssetFile(this FileInfo fileInfo) {
        if (fileInfo.Extension.Equals(".meta")) {
            return false;
        }

        if (fileInfo.Extension.Equals(".lua")) {
            return false;
        }

        return !fileInfo.Extension.Equals(".gitkeep");
    }
}