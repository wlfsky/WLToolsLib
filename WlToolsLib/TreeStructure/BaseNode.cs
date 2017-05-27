using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基类节点
    /// </summary>
    public abstract class BaseNode<TKey> : BaseLeaf<TKey>, IDisplay, IEdit<TKey>
    {
        List<BaseLeaf<TKey>> childrenNode = new List<BaseLeaf<TKey>>();
        /// <summary>
        /// 子节点
        /// </summary>
        public List<BaseLeaf<TKey>> ChildrenNode
        {
            get { return childrenNode; }
            set { childrenNode = value; }
        }
        /// <summary>
        /// 空初始化
        /// </summary>
        public BaseNode()
        {

        }
        /// <summary>
        /// 添加叶子
        /// </summary>
        /// <param name="item"></param>
        public void Add(BaseLeaf<TKey> item)
        {
            childrenNode.Add(item);
        }
        /// <summary>
        /// 移除指定叶子
        /// </summary>
        /// <param name="item"></param>
        public void Remove(BaseLeaf<TKey> item)
        {
            childrenNode.Remove(item);
        }
        /// <summary>
        /// 检查给定节点是否其子节点
        /// </summary>
        /// <param name="child">给定节点</param>
        /// <returns></returns>
        public bool IsChildNode(BaseNode<TKey> child)
        {
            if (this.ID.Equals(child.PID) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 在给定源节点队列中检查本节点有多少子节点
        /// </summary>
        /// <param name="sourceList">给定源节点队列</param>
        /// <returns></returns>
        public int HasChildNode(List<BaseNode<TKey>> sourceList)
        {
            int temp = 0;
            foreach (var n in sourceList)
            {
                if (this.ID.Equals(n.PID))
                {
                    temp += 1;
                }
            }
            return temp;
        }
        /// <summary>
        /// 检查给定叶子节点是否是其子叶子节点
        /// </summary>
        /// <param name="child">给定的叶子</param>
        /// <returns></returns>
        public bool IsChildLeaf(BaseLeaf<TKey> child)
        {
            if (this.ID.Equals(child.PID) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 显示或 打印
        /// </summary>
        /// <returns></returns>
        public new string Display()
        {
            return base.Display();
        }
    }
}
