using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 整合 树结构各种接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public interface ITreeWorker<TKey, TLeaf, TNode> : ITreePrinter<TKey, TLeaf, TNode> , ITreeBuilder<TKey, TLeaf, TNode>, IFathers<TKey, TLeaf, TNode>, IChildren<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        ///// <summary>
        ///// 叶子源列表
        ///// </summary>
        //List<TLeaf> SourceLeafList { get; set; }
        ///// <summary>
        ///// 节点源列表
        ///// </summary>
        //List<TNode> SourceNodeList { get; set; }
        ///// <summary>
        ///// 树根
        ///// </summary>
        //TNode TreeRoot { get; set; }
        ///// <summary>
        ///// 深度字符委托
        ///// </summary>
        //Func<int, string, string> PreString { get; set; }
        ///// <summary>
        ///// 构建树结构
        ///// </summary>
        //void Build();
        ///// <summary>
        ///// 打印树结构
        ///// </summary>
        ///// <returns>树结构字符串</returns>
        //string Print();
    }
}
