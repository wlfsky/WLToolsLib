using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WlToolsLib.Future.User
{
    public abstract class BaseRecord
    {
        public DateTime AddTime { get; set; } = DateTime.Now;
        public string Creator { get; set; } = string.Empty;
        public int IsDel { get; set; } = 0;
    }
}
