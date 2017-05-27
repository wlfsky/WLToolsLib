
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 请用单元测试测试
    /// </summary>
    public class TreeTest
    {
        // 使用方法和测试移动到 Business 项目中 在 Department更好中
        public string test()
        {
            List<N> NL = new List<N>();
            N root = new N { ID = "1", PID = "0", Name = "root" };
            N r1 = new N { ID = "2", PID = "1", Name = "r1-N" };
            N r2 = new N { ID = "3", PID = "1", Name = "r2-N" };
            N r11 = new N { ID = "4", PID = "2", Name = "r11-N" };
            N r12 = new N { ID = "5", PID = "2", Name = "r12-N" };
            N r21 = new N { ID = "6", PID = "3", Name = "r21-N" };
            NL.Add(root);
            NL.Add(r1);
            NL.Add(r2);
            NL.Add(r11);
            NL.Add(r12);
            NL.Add(r21);

            List<L> LL = new List<L>();
            L rl = new L { ID = "7", PID = "1", Name = "RL-L" };
            L rl1 = new L { ID = "8", PID = "2", Name = "R1L-L" };
            L rl11 = new L { ID = "9", PID = "4", Name = "R11L1-L" };
            L rl12 = new L { ID = "10", PID = "4", Name = "R11L2-L" };
            L rl21 = new L { ID = "11", PID = "6", Name = "R21L1-L" };
            LL.Add(rl);
            LL.Add(rl1);
            LL.Add(rl11);
            LL.Add(rl12);
            LL.Add(rl21);

            //LL.Clear();

            TreeBuilder<string, L, N> b = new TreeBuilder<string, L, N> { SourceLeafList = LL, SourceNodeList = NL, TreeRoot = root };
            b.BuildFilter = new testbFilter();
            b.Build();
            TreePrinter<string, L, N> p = new TreePrinter<string, L, N> { TreeRoot = root, PreString = DeepString };

            return p.Print();
        }
        private static string DeepString(int deep, string deepChar)
        {
            string temp = string.Empty;
            if (deep > 0)
            {
                for (int i = 0; i < deep; i++)
                {
                    temp += deepChar;
                }
            }
            return temp;
        }

        public class testbFilter : IBuildFilter<string, L, N>
        {
            public bool FilterLeaf(L leaf)
            {
                //if (leaf.ID == "10") return false;
                //else return true;
                return true;
            }
            public bool FilterNode(N node)
            {
                //if (node.ID == "6") return false;
                //else return true;
                return true;
            }
        }

        public class testpFilter : IBuildFilter<string, L, N>
        {
            public bool FilterLeaf(L leaf)
            {
                if (leaf.ID == "10") return false;
                else return true;
            }
            public bool FilterNode(N node)
            {
                if (node.ID == "6") return false;
                else return true;
            }
        }

        public class N : BaseNode<string>
        {
            //
        }

        public class L : BaseLeaf<string>
        {
            //
        }
    }
}
