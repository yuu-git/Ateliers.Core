*****************************************************
  AITrainingSamples - C# - Linq編
*****************************************************

このサンプルは GitHub Copilot が C# のコードを記載する際の学習用サンプルを示しています。


## 1. 空のリストを作成して返すのではなく `Enumerable.Empty<T>()` を使う

```csharp

// bad
return new List<string>();

// good
return Enumerable.Empty<string>();

```

## 2. リストの中身は Any() で確認する

```csharp

// bad
if (list.Count > 0)

// good
if (list.Any())

```
