using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 元素过滤器接口，是 构建过滤器 和 显示过滤器 的基础
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    public interface IElementFilter<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 元素过滤器，叶子过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="TLeaf">要检测的叶子节点值</param>
        /// <returns></returns>
        bool FilterLeaf(TLeaf leaf);
        /// <summary>
        /// 元素过滤器，节点过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool FilterNode(TNode node);
    }
}
