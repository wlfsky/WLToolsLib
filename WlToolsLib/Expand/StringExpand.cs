using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.CryptoHelper;
using Pinyin4net;
using Gma.QrCodeNet.Encoding;
using System.Drawing;
using Pinyin4net.Format;

namespace WlToolsLib.Expand
{
    public static class StringExpand
    {
        #region --简化验证--

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NullEmpty(this string self) => string.IsNullOrWhiteSpace(self);

        /// <summary>
        /// string.IsNullOrWiteSpace的简化版
        /// 支持检测null的字符类型（好tm强大）
        /// </summary>
        /// <param name="self">扩展字符串</param>
        /// <returns></returns>
        public static bool NotNullEmpty(this string self) => !self.NullEmpty();

        /// <summary>
        /// null或者空字符串返回true，空格和有值false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NullStr(this string self) => string.IsNullOrEmpty(self);

        /// <summary>
        /// 不为null或者空字符串返回true，null或者空字符串返回false
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNullStr(this string self) => !self.NullStr();

        #endregion --简化验证--

        /// <summary>
        /// 返回两个字符串 之间的字符串，去两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="leftStr"></param>
        /// <param name="rightStr"></param>
        /// <returns></returns>
        public static string SinceStr(this string self, string leftStr, string rightStr)
        {
            if (self.NullEmpty()) return string.Empty;
            if (leftStr.NullStr() && rightStr.NullStr()) return self;
            var s = self.IndexOf(leftStr);
            var e = self.IndexOf(rightStr);
            if (e <= s) return string.Empty;
            if (s < 0 && e < 0) return self;
            if (s < 0) s = 0;
            if (e < 0) e = self.Length;
            var leftLen = leftStr.Length;
            var l = e - s - leftLen;
            if (l <= 0) return string.Empty;
            var sinceStr = self.Substring(s + leftLen, l);
            if (sinceStr.NullEmpty()) return string.Empty;
            return sinceStr.Trim();
        }

        /// <summary>
        /// 仿python切片,string版本
        /// </summary>
        /// <param name="self"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Slice(this string self, int? s = null, int? e = null, int? step = null)
        {
            /*两边都没有返回原值*/
            if (self.IsNull()) return self;
            if (!s.HasValue && !e.HasValue) return self;
            if (!s.HasValue && e.HasValue) { return self.Substring(0, e.Value); }
            if (s.HasValue && !e.HasValue) { return self.Substring(s.Value, self.Length - s.Value); }
            return self;
        }
        #region --去除两端空格--

        /// <summary>
        /// 去两端空格，如果无值或者null原样返回，不会报异常
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string XTrim(this string self)
        {
            if (self.NotNullEmpty())
            {
                self = self.Trim();
                return self;
            }
            return string.Empty;
        }

        /// <summary>
        /// 实体的常用字符类型字段整合处理，空或空字符串用默认值替代再去掉两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static string TrimOrDef(this string self, string defVal)
        {
            if (self.NullEmpty())
            {
                self = defVal;
            }
            return self.XTrim();

        }

        /// <summary>
        /// 实体的常用字符类型字段整合处理
        /// 空或空字符串用默认值(string.Empty)替代再去掉两端空格
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string TrimOrDef(this string self)
        {
            return self.TrimOrDef(string.Empty);
        }

        /// <summary>
        /// 去两端空格，对象为单位，还无法递归
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void ObjStrTrim<T>(this T self, bool filterSwitch = true, List<string> filterList = null) where T : class
        {
            if (self.NotNull())
            {
                var strType = typeof(string);
                var objType = self.GetType();
                foreach (var properItem in objType.GetProperties())
                {
                    if (properItem.PropertyType == strType)
                    {
                        var v = Convert.ToString(properItem.GetValue(self));
                        if (v.NotNullEmpty())
                        {
                            v = v.Trim();
                        }
                        properItem.SetValue(self, v);
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 根据路径获取txt文件字符串，目前只能utf-8编码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetTextFile(this string path)
        {
            if (path.NotNullEmpty())
            {
                using (var fs = System.IO.File.OpenText(path))
                {
                    var jsonStr = fs.ReadToEnd();
                    return jsonStr;
                }
            }
            return null;
        }

        /// <summary>
        /// 汉字转换拼音
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToPinYin(this string self)
        {
            if (self.NullEmpty())
            {
                return string.Empty;
            }
            HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();
            format.ToneType = HanyuPinyinToneType.WITHOUT_TONE;
            StringBuilder sb = new StringBuilder();
            foreach (var item in self)
            {
                string[] pinyinStr = PinyinHelper.ToHanyuPinyinStringArray(item, format);
                sb.Append(pinyinStr.JoinBy(" "));
            }

            //return NPinyin.Pinyin.GetPinyin(self, Encoding.UTF8);

            return sb.ToString();
        }

        /// <summary>
        /// 将字符串转换二维码图片
        /// </summary>
        /// <param name="self"></param>
        /// <param name="errLevel"></param>
        /// <returns></returns>
        public static QrCode ToQrCode(this string self, ErrorCorrectionLevel errLevel = ErrorCorrectionLevel.M)
        {
            if (self.NullEmpty())
            {
                return null;
            }
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            //QrCode qrCode = qrEncoder.Encode(self);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(self, out qrCode);
            return qrCode;
            //Renderer renderer = new Renderer(5, Brushes.Black, Brushes.White);
            //renderer.CreateImageFile(qrCode.Matrix, @"c:\temp\HelloWorld.png", ImageFormat.Png);
        }

        #region --两边起点查找，提取特定字符串左右两侧的字符串--
        /// <summary>
        /// 从右侧开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastIndexOfRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.LastIndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            position += 1;
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }

        /// <summary>
        /// 从右侧开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastIndexOfLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.LastIndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string IndexOfLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.IndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string IndexOfRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.IndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            position += 1;
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }
        #endregion

        #region --组合字符串--
        /// <summary>
        /// string.join的简化写法
        /// </summary>
        /// <param name="self"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinBy(this IEnumerable<string> self, string separator = ",")
        {
            if (self.IsNull())
            {
                return null;
            }
            if (self.HasItem().IsFalse())
            {
                return "";
            }
            return string.Join(separator, self);
        }
        /// <summary>
        /// 用指定的字符串拼接由制定转换器转换出来的字符串组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="processor"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinBy<T>(this IEnumerable<T> self, Func<T, string> converter, string separator = ",")
        {
            if (converter.IsNull() || self.NoItem())
            {
                return string.Empty;
            }
            List<string> t = new List<string>();
            foreach (var i in self)
            {
                t.Add(converter(i));
            }
            string r = string.Join(separator, t);
            return r;
        }
        #endregion

        #region --编辑字符串--
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string HeadUpper(this string self)
        {
            if (self.NullEmpty())
            {
                return self;
            }
            return self[0].ToString().ToUpper() + self.Substring(1);
        }
        #endregion

        #region --去掉字符串的文件名非法字符--

        /// <summary>
        /// 去掉字符串的文件名非法字符
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string RemoveFileNameIllegalChar(this string self)
        {
            // \ / : * ? "< > |
            return self.Replace("\\", "")
                .Replace("/", "")
                .Replace(":", "")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace("\'", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");

        }
        #endregion
    }
}
