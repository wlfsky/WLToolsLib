using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class RegexExpand
    {
        /// <summary>
        /// 正则是否匹配，匹配返回true
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool RegexIsMatch(this string pattern, string content)
        {
            if (pattern.NotNullEmpty())
            {
                var r = new Regex(pattern);
                if (r.IsMatch(content))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 不匹配，返回true
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool RegexUnMatch(this string pattern, string content)
        {
            return !pattern.RegexIsMatch(content);
        }

        /// <summary>
        /// 正则匹配所有项
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<Match> RegexMatch(this string pattern, string content)
        {
            List<Match> matchList = null;
            if (pattern.NotNullEmpty())
            {
                var r = new Regex(pattern);
                var matchs = r.Matches(content);
                matchList = new List<Match>();
                if (matchs.Count > 0)
                {
                    for (int i = 0; i < matchs.Count; i++)
                    {
                        matchList.Add(matchs[i]);
                    }
                }

            }
            return null;
        }
    }
}
