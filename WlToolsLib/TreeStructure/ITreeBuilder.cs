using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树组装器接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public interface ITreeBuilder<TKey, TLeaf, TNode> : IBaseTreeWorker<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        //List<TLeaf> SourceLeafList { get; set; }
        //List<TNode> SourceNodeList { get; set; }
        //TNode TreeRoot { get; set; }

        /// <summary>
        /// 构建树结构
        /// </summary>
        void Build();
    }
}
