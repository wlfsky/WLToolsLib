using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树组装器 实现
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public class TreeBuilder<TKey, TLeaf, TNode> : ITreeBuilder<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 叶子源
        /// </summary>
        public List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点源
        /// </summary>
        public List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树根
        /// </summary>
        public TNode TreeRoot { get; set; }
        /// <summary>
        /// 建造过滤器
        /// </summary>
        public IBuildFilter<TKey, TLeaf, TNode> BuildFilter { get; set; }


        public TreeBuilder()
        {
            if (BuildFilter == null)
            {
                BuildFilter = new DefaultBuildFilter<TKey, TLeaf, TNode>();
            }
        }

        /// <summary>
        /// 将列表构建成树结构
        /// </summary>
        public void Build()
        {
            BindNodeLeaf(TreeRoot, SourceNodeList, SourceLeafList);
        }
        /// <summary>
        /// 迭代指定节点下的节点
        /// </summary>
        /// <param name="parentNode">指定节点</param>
        /// <param name="sourceNodeList">数据源</param>
        /// <returns>迭代返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode, List<TNode> sourceNodeList)
        {
            foreach(TNode n in sourceNodeList)
            {
                if (n.PID.Equals(parentNode.ID) == true)
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// 迭代指定节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定节点</param>
        /// <param name="sourceLeafList">数据源</param>
        /// <returns>迭代返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode, List<TLeaf> sourceLeafList)
        {
            foreach (TLeaf l in sourceLeafList)
            {
                if (l.PID.Equals(parentNode.ID) == true)
                {
                    yield return l;
                }
            }
        }
        /// <summary>
        /// 将指定叶子或者节点加入到树结构中
        /// </summary>
        /// <param name="parent">指定节点</param>
        /// <param name="sourceNode">节点源</param>
        /// <param name="sourceLeaf">叶子源</param>
        private void BindNodeLeaf(TNode parent, List<TNode> sourceNode, List<TLeaf> sourceLeaf)
        {
            //取得Node子项队列
            foreach (TNode node in ChildNode(parent, sourceNode))
            {
                if (BuildFilter.FilterNode(node) == false)
                {
                    continue;
                }
                parent.Add(node);
                //sourceNode.Remove(node);
                //递归
                BindNodeLeaf(node, sourceNode, sourceLeaf);
            }
            //取得Leaf子项队列
            foreach (TLeaf leaf in ChildLeaf(parent, sourceLeaf))
            {
                if (BuildFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                parent.Add(leaf);
                //sourceLeaf.Remove(leaf);
            }
        }
    }
}
