# WLToolsLib

utility c# tools

simple and easy use expand func

简单易用的扩展方法集合

like this

if(string.IsNullOrWhiteSpace(string)){
...
}

// expand func

public static bool NullEmpty(this string self) => string.IsNullOrWhiteSpace(self);

public static bool NotNullEmpty(this string self) => !self.NullEmpty();

// use easy

if(str.NullEmpty()) {...}

if(str.NotNullEmpty()) {...}

other like

obj.IsNull() = ReferenceEquals(self, null);

obj.NotNull() = !obj.IsNull()

IList.HasItem() = self.NotNull() && self.Any()

IList<T> AddRange<T>(this IList<T> self, IList<T> itemList)

int SortMulti<T>(T x, T y, Func<T, T, int>[] l, int i) //这是个多条件排序扩展|this is a multiple-conditions sort extension method

void Foreach<TData>(this IList<TData> self, Action<TData> action)//循环一个动作 | foreach with action

void RemoveOrHold<TSource>(this IList<TSource> self, bool isRemove, Func<TSource, bool> predicate) // 移除或保留 | 

bool RegexIsMatch(this string pattern, string content) // 正则匹配扩展

IEnumerable<Tuple<int, T>> ForeachIndex<T>(IEnumerable<T> source) // 循环返回index和数据 | foreach with index and data,like js.

string AESCBCEncryption(this string self, string key)

string AESCBCDecryption(this string self, string key)

.....

