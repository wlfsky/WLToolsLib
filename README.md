# WLToolsLib
utility c# tools

simple and easy use expand func

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
