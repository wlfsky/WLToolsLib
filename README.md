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

int SortMulti<T>(T x, T y, Func<T, T, int>[] l, int i)

void Foreach<TData>(this IList<TData> self, Action<TData> action)

void RemoveOrHold<TSource>(this IList<TSource> self, bool isRemove, Func<TSource, bool> predicate)

bool RegexIsMatch(this string pattern, string content)

IEnumerable<Tuple<int, T>> ForeachIndex<T>(IEnumerable<T> source)

string AESCBCEncryption(this string self, string key)

string AESCBCDecryption(this string self, string key)

.....

