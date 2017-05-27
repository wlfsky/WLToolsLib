using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    public interface IBaseTreeWorker<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 叶子源列表
        /// </summary>
        List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点源列表
        /// </summary>
        List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树根
        /// </summary>
        TNode TreeRoot { get; set; }
    }
}
