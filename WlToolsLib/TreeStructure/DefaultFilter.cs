using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 默认过滤器，都返回true，表示不过滤
    /// </summary>
    /// <typeparam name="TKey">键值泛型</typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class DefaultElementFilter<TKey, TLeaf, TNode> : IElementFilter<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 元素过滤器，叶子过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="TLeaf">要检测的叶子节点值</param>
        /// <returns></returns>
        public bool FilterLeaf(TLeaf leaf)
        {
            return true;
        }
        /// <summary>
        /// 元素过滤器，节点过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool FilterNode(TNode node)
        {
            return true;
        }
    }

    public class DefaultBuildFilter<TKey, TLeaf, TNode> : DefaultElementFilter<TKey, TLeaf, TNode>, IBuildFilter<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
    }

    public class DefaultShowFilter<TKey, TLeaf, TNode> : DefaultElementFilter<TKey, TLeaf, TNode>, IShowFilter<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
    }
}
