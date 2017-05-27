using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基类叶子
    /// </summary>
    public abstract class BaseLeaf<TKey> : IDisplay, IEquatable<BaseLeaf<TKey>>
    {
        public TKey PID { get; set; }
        public TKey ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 空初始化
        /// </summary>
        public BaseLeaf()
        {
            
        }
        /// <summary>
        /// 显示方法
        /// </summary>
        /// <returns></returns>
        public string Display()
        {
            return string.Format("ID:{0}  PID:{1}  Name:{2} \r\n", ID, PID, Name);
        }
        /// <summary>
        /// 是否相等，对比id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BaseLeaf<TKey> other)
        {
            if (other.ID.Equals(this.ID) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
