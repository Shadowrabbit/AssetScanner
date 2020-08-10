// ********************************************************
// AseBaseSingleTon.cs
// 描述：单例基类
// 作者：ShadowRabbit 
// 创建时间：2020年8月4日 11:21:07
// ********************************************************

public class AseBaseSingleTon<T> where T : class, new()
{
    public static T Instance => Inner.InternalInstance;

    private static class Inner
    {
        internal static readonly T InternalInstance = new T();
    }
}