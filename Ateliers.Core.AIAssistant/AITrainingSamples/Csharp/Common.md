*****************************************************
  AITrainingSamples - C# - 共通編
*****************************************************

このサンプルは GitHub Copilot が C# のコードを記載する際の学習用サンプルを示しています。


## 1. null の確認は `is null` を使う

```csharp

// bad
if (list == null)

// good
if (list is null)

```