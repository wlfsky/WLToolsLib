using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.KV
{
    public static class ExpandKVEntity
    {
        public static KVEntity<TK, TV> K2E<TK, TV>(this List<KVEntity<TK, TV>> _self, TK k)
        {
            return _self.FirstOrDefault((s) => { return s.ID.Equals(k); });
        }

        public static KVEntity<TK, TV> V2E<TK, TV>(this List<KVEntity<TK, TV>> _self, TV v)
        {
            return _self.FirstOrDefault((s) => { return s.Name.Equals(v); });
        }
    }
}
