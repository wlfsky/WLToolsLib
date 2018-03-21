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
        
        /// <summary>
        /// 是否在某个队列中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">值</param>
        /// <param name="tList">队列</param>
        /// <returns></returns>
        public static bool In<T>(this T self, IEnumerable<T> tList)
        {
            return tList.Contains(self);
        }

        /// <summary>
        /// 是否不在某个队列中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">值</param>
        /// <param name="tList">队列</param>
        /// <returns></returns>
        public static bool NotIn<T>(this T self, IEnumerable<T> tList)
        {
            return !tList.Contains(self);
        }


        /// <summary>
        /// GPS两点距离
        /// </summary>
        /// <param name="gps1"></param>
        /// <param name="gps2"></param>
        /// <returns></returns>
        public static double GpsDistance(this GpsCoordinate gps1, GpsCoordinate gps2)
        {
            double a = 0.0, b = 0.0, R, d, sa2=0.0, sb2=0.0;;
            R = 6378137; //地球半径
            Parallel.Invoke(()=> {
                gps1.Lat = gps1.Lat * Math.PI / 180.0;
            },()=> {
                gps2.Lat = gps2.Lat * Math.PI / 180.0;
            },()=> {
                b = (gps1.Lon - gps2.Lon) * Math.PI / 180.0;
            });
            Parallel.Invoke(()=> {
                a = gps1.Lat - gps2.Lat;
                sa2 = Math.Sin(a / 2.0);
            },()=> {
                sb2 = Math.Sin(b / 2.0);
            });
            double x = 0.0, y = 0.0;
            Parallel.Invoke(() =>
            {
                x = Math.Cos(gps1.Lat);
            }, () =>
            {
                y = Math.Cos(gps2.Lat);
            });
            d = 2 * R * Math.Asin(Math.Sqrt(sa2 * sa2 + x * y * sb2 * sb2));
            return d;
        }

        /// <summary>
        /// 布尔值取反
        /// 因为 叹号 取反 常被 错看，所以就加了这么一个看起来累赘的方法。
        /// 比叹号取反要清晰些。双刃剑！
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool Contrary(this bool self)
        {
            return !self;
        }

        #region --bool类型明确的检查--
        /// <summary>
        /// 是否true，此方法只是为了让代码更明确清晰
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsTrue(this bool self)
        {
            return self == true;
        }

        /// <summary>
        /// 是否false，此方法只是为了让代码更明确清晰
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsFalse(this bool self)
        {
            return self == false;
        }
        #endregion

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
        /// <typeparam name="TIN"></typeparam>
        /// <typeparam name="TOUT"></typeparam>
        /// <param name="i"></param>
        /// <returns></returns>
        public delegate TOUT X<in TIN, out TOUT>(TIN i);

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

    /// <summary>
    /// 实验类型
    /// </summary>
    public class X1
    {
        public int XN(int x)
        {
            return x + 9;
        }
    }

    /// <summary>
    /// GPS坐标点
    /// </summary>
    public class GpsCoordinate
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}
