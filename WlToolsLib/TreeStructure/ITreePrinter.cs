using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树打印器接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public interface ITreePrinter<TKey, TLeaf, TNode> : IBaseTreeWorker<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 深度字符委托
        /// </summary>
        Func<int, string, string> PreString { get; set; }
        /// <summary>
        /// 打印树结构
        /// </summary>
        /// <returns>树结构字符串</returns>
        string Print();
    }
}
