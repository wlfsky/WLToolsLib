using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 完整树工作器，组合了组装和打印
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public class TreeWorker<TKey, TLeaf, TNode> : ITreeWorker<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 叶子源列表
        /// </summary>
        public List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点源列表
        /// </summary>
        public List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树根
        /// </summary>
        public TNode TreeRoot { get; set; }
        /// <summary>
        /// 深度字符委托
        /// </summary>
        public Func<int, string, string> PreString { get; set; }
        /// <summary>
        /// 输出字符串
        /// </summary>
        private StringBuilder outPutStr = new StringBuilder();
        /// <summary>
        /// 建造过滤器
        /// </summary>
        protected IBuildFilter<TKey, TLeaf, TNode> buildFilter { get; set; }
        /// <summary>
        /// 显示过滤器
        /// </summary>
        protected IShowFilter<TKey, TLeaf, TNode> showFilter { get; set; }
        //

        public TreeWorker()
        {
            if (buildFilter == null)
            {
                buildFilter = new DefaultBuildFilter<TKey, TLeaf, TNode>();
            }
            if (showFilter == null)
            {
                showFilter = new DefaultShowFilter<TKey, TLeaf, TNode>();
            }
        }

        #region -- 构建 --
        /// <summary>
        /// 构建树结构
        /// </summary>
        public void Build()
        {
            this.BindNodeLeaf(TreeRoot, SourceNodeList, SourceLeafList);
        }
        /// <summary>
        /// 枚举指定节点的子节点
        /// </summary>
        /// <param name="parentNode">指定父节点</param>
        /// <param name="sourceNodeList">节点源</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode, List<TNode> sourceNodeList)
        {
            foreach (TNode n in sourceNodeList)
            {
                if (n.PID.Equals(parentNode.ID) == true)
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// 枚举指定父节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定父节点</param>
        /// <param name="sourceLeafList">叶子源</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode, List<TLeaf> sourceLeafList)
        {
            foreach (TLeaf l in sourceLeafList)
            {
                if (l.PID.Equals(parentNode.ID))
                {
                    yield return l;
                }
            }
        }

        private void BindNodeLeaf(TNode parent, List<TNode> sourceNode, List<TLeaf> sourceLeaf)
        {
            //取得Node子项队列
            foreach (TNode node in ChildNode(parent, sourceNode))
            {
                if (buildFilter.FilterNode(node) == false)
                {
                    continue;
                }
                parent.Add(node);
                //sourceNode.Remove(node);
                BindNodeLeaf(node, sourceNode, sourceLeaf);//递归
            }
            //取得Leaf子项队列
            foreach (TLeaf leaf in ChildLeaf(parent, sourceLeaf))
            {
                if (buildFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                parent.Add(leaf);
                //sourceLeaf.Remove(leaf);
            }
        }
        #endregion

        #region -- 打印 --
        /// <summary>
        /// 打印树结构
        /// </summary>
        /// <returns>树结构字符串</returns>
        public string Print()
        {
            PrintNode(TreeRoot, 0);
            return outPutStr.ToString();
        }
        /// <summary>
        /// 枚举指定父节点下的子节点
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode)
        {
            foreach (var n in parentNode.ChildrenNode)
            {
                if (n is TNode)
                {
                    yield return n as TNode;
                }
            }
        }
        /// <summary>
        /// 枚举指定父节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode)
        {
            foreach (var l in parentNode.ChildrenNode)
            {
                if (l is TLeaf)
                {
                    yield return l as TLeaf;
                }
            }
        }
        /// <summary>
        /// 打印当前父亲节点
        /// </summary>
        /// <param name="parent">当前父亲节点</param>
        /// <param name="deep">深度值</param>
        private void PrintNode(TNode parent, int deep)
        {
            outPutStr.Append(PreString(deep, "-") + parent.Display());
            foreach (TNode node in ChildNode(parent))
            {
                if (showFilter.FilterNode(node) == false)
                {
                    continue;
                }
                //outPutStr.Append(DeepString(deep, "-") + n.Display());
                PrintNode(node, deep + 1);
            }
            foreach (TLeaf leaf in ChildLeaf(parent))
            {
                if (showFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                outPutStr.Append(PreString(deep + 1, "-") + leaf.Display());
            }
        }
        #endregion

        #region -- 任意节点父级队列（不包含兄弟和父级兄弟） --
        /// <summary>
        /// 获取所有的父级队列
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public List<TNode> Fathers(TLeaf child)
        {
            List<TNode> fatherList = new List<TNode>();
            GetFather(child, fatherList);
            return fatherList;
        }
        /// <summary>
        /// 数据源队列中寻找父节点
        /// </summary>
        /// <param name="childNode"></param>
        /// <returns></returns>
        private TNode Father(TLeaf childNode)
        {
            TNode temp = null;
            foreach (TNode n in SourceNodeList)
            {
                if (n.ID.Equals(childNode.PID))
                {
                    temp = n;
                }
                else
                {
                    temp = null;
                }
            }
            return temp;
        }
        /// <summary>
        /// 获取父级 递归
        /// </summary>
        /// <param name="child"></param>
        /// <param name="fathers"></param>
        private void GetFather(TLeaf child, List<TNode> fathers)
        {
            TNode temp = Father(child);
            if (temp != null)
            {
                fathers.Add(temp);
                GetFather(temp as TLeaf, fathers);
            }
        }
        #endregion

        #region -- 任意节点子集队列（） --
        /// <summary>
        /// 从列表结构中获取（并非从构建好的树形结构中获取）
        /// </summary>
        /// <param name="father"></param>
        /// <returns></returns>
        public TNode ChildrenTreeNode(TNode father)
        {
            return FindNode(father, TreeRoot);
        }
        /// <summary>
        /// 以给定的父节点为准寻找以下给定的子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fatherNode"></param>
        /// <returns></returns>
        private TNode FindNode(TNode node, TNode fatherNode)
        {
            TNode temp = null;
            foreach (TLeaf l in fatherNode.ChildrenNode)
            {
                if (l is TNode)
                {
                    if (l.Equals(node))
                    {
                        temp = l as TNode;
                    }
                    else
                    {
                        temp = FindNode(node, l as TNode);
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// 以给定节点返回其所有子节点列表
        /// </summary>
        /// <param name="father">给定的父节点</param>
        /// <returns>返回的所有子节点列表</returns>
        public List<TNode> ChildrenNode(TNode father)
        {
            TNode temp = FindNode(father, TreeRoot);
            List<TNode> allNode = new List<TNode>();
            GetChildrenNode(temp, allNode);
            return allNode;
        }
        /// <summary>
        /// 将给定的父节点的子节点写入给定的列表
        /// </summary>
        /// <param name="father"></param>
        /// <param name="nodeList"></param>
        private void GetChildrenNode(TNode father, List<TNode> nodeList)
        {
            foreach (TLeaf l in father.ChildrenNode)
            {
                if (l is TNode)
                {
                    nodeList.Add(l as TNode);
                    GetChildrenNode(l as TNode, nodeList);
                }
            }
        }
        /// <summary>
        /// 获取给定父节点的所有子叶子
        /// </summary>
        /// <param name="father">给定的父节点</param>
        /// <returns>返回叶子列表</returns>
        public List<TLeaf> ChildrenLeaf(TNode father)
        {
            TNode temp = FindNode(father, TreeRoot);
            List<TLeaf> allLeaf = new List<TLeaf>();
            GetChildrenLeaf(temp, allLeaf);
            return allLeaf;
        }
        /// <summary>
        /// 将给定父节点的叶子保存进给定的列表
        /// </summary>
        /// <param name="father">给定父节点</param>
        /// <param name="leafList">保存叶子的列表</param>
        private void GetChildrenLeaf(TNode father, List<TLeaf> leafList)
        {
            foreach(TLeaf l in father.ChildrenNode)
            {
                if (l is TNode)
                {

                    GetChildrenLeaf(l as TNode, leafList);
                }
                else
                {
                    leafList.Add(l);
                }
            }
        }

        #endregion
    }
}
