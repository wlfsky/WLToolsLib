using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.CryptoHelper;
using Gma.QrCodeNet.Encoding;
using Pinyin4net;
using Pinyin4net.Format;

namespace WlToolsLib.Expand
{
    public static class StringOutExpand
    {
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
    }
}
