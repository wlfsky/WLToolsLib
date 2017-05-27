using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.Expand
{
    /// <summary>
    /// 其他扩展方法
    /// </summary>
    public static class OtherExpand
    {
        /// <summary>
        /// 并发随机数种子
        /// </summary>
        /// <returns></returns>
        public static int Seed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return System.Math.Abs(BitConverter.ToInt32(bytes, 0));
        }

        #region --实验性扩展方法--
        /// <summary>
        /// 下层的处理
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int n(int x)
        {
            return x + 10;
        }
        /// <summary>
        /// 创建一个委托 一个输入参数，一个输出参数
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="i"></param>
        /// <returns></returns>
        public delegate TO X<TI, TO>(TI i);

        /// <summary>
        /// 这个方法临时自定义 输入和输出转换
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <typeparam name="NTI"></typeparam>
        /// <typeparam name="NTO"></typeparam>
        /// <param name="self"></param>
        /// <param name="input"></param>
        /// <param name="CH"></param>
        /// <param name="CHO"></param>
        /// <returns></returns>
        public static NTO TX<TI, TO, NTI, NTO>(this X<TI, TO> self, NTI input, Func<NTI, TI> CH, Func<TO, NTO> CHO)
        {
            var ti = CH(input);
            var to = self(ti);
            var cho = CHO(to);
            return cho;
        }
        /// <summary>
        /// 这个方法和上面的一样，只是不是扩展方法
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <typeparam name="NTI"></typeparam>
        /// <typeparam name="NTO"></typeparam>
        /// <param name="self"></param>
        /// <param name="input"></param>
        /// <param name="CH"></param>
        /// <param name="CHO"></param>
        /// <returns></returns>
        public static NTO TX1<TI, TO, NTI, NTO>( X<TI, TO> self, NTI input, Func<NTI, TI> CH, Func<TO, NTO> CHO)
        {
            System.Console.WriteLine(input);
            var ti = CH(input);
            var to = self(ti);
            var cho = CHO(to);
            System.Console.WriteLine(cho);
            return cho;
        }
        /// <summary>
        /// 这个方法，固定了前后处理（打印加转换）减少了输入参数，借string扩展触发
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="self"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string TX2<TI, TO>(this string self, X<TI, TO> f)
        {
            System.Console.WriteLine(self);
            var ti = self.ToObj<TI>();
            var to = f(ti);
            var cho = to.ToJson();
            System.Console.WriteLine(cho);
            return cho;
        }
        /// <summary>
        /// 这个方法和上面那个一样，都固定了前后处理，但不是扩展方法
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="f"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TX2<TI, TO>(X<TI, TO> f, string input)
        {
            System.Console.WriteLine(input);
            var ti = input.ToObj<TI>();
            var to = f(ti);
            var cho = to.ToJson();
            System.Console.WriteLine(cho);
            return cho;
        }

        /// <summary>
        /// 这个是实验入口。 和调用方法范例
        /// </summary>
        public static void PFType()
        {
            X<int, int> x = n;
            System.Console.WriteLine(typeof(X<int, int>));
            // 自定义前后处理
            TX1<int, int, string, string>(n, "1", (i) => Convert.ToInt32(i), (o) => o.ToString());
            // 借指定的string触发处理，n就是那个具体处理函数
            System.Console.WriteLine("2".TX2<int, int>(n));
            // 直接调用函数，输入n 和参数
            System.Console.WriteLine(TX2<int, int>(n, "3"));
            // 直接调用函数，输入n 和参数
            System.Console.WriteLine(TX2<int, int>(new X1().XN, "3"));
        }
        #endregion
    }

    public class X1
    {
        public int XN(int x)
        {
            return x + 9;
        }
    }
}
