using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    public interface ITreeStructure
    {

    }

    /// <summary>
    /// 显示接口
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        /// 显示接口方法
        /// </summary>
        /// <returns></returns>
        string Display();
    }

    /// <summary>
    /// 编辑接口
    /// </summary>
    public interface IEdit<TKey>
    {
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="item"></param>
        void Add(BaseLeaf<TKey> item);
        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="item"></param>
        void Remove(BaseLeaf<TKey> item);
    }

    /// <summary>
    /// 父亲接口
    /// </summary>
    /// <typeparam name="TLeaf">叶子泛型，必须继承自 BaseLeaf</typeparam>
    /// <typeparam name="TNode">节点泛型，必须继承自 BaseNode</typeparam>
    public interface IFathers<TKey, TLeaf, TNode>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        /// <summary>
        /// 获得父节点队列的方法
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        List<TNode> Fathers(TLeaf child);
    }

    /// <summary>
    /// 孩子接口
    /// </summary>
    /// <typeparam name="TLeaf">叶子泛型，必须继承自 BaseLeaf</typeparam>
    /// <typeparam name="TNode">节点泛型，必须继承自 BaseNode</typeparam>
    public interface IChildren<TKey, TLeaf, TNode>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        TNode ChildrenTreeNode(TNode father);
        List<TNode> ChildrenNode(TNode father);
        List<TLeaf> ChildrenLeaf(TNode father);
    }
}
