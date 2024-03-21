*****************************************************
  Test Creation Guidelines - 01
*****************************************************

# GitHub Copilot テスト作成ガイドライン

このファイルは Github Copilot による単体テストを作成するためのガイドラインです。

## 1. テストの環境について

・使用言語は `C#` です。
・テストプロジェクトは `xUnit` を使用しています。

## 2. テストの作成方法

テストの作成方法については以下の通りです。

### 2.1 自動生成テストクラスであることを明記

・自動生成されたことを明示的にするため、1行目にはと2行目には、以下のコメントを記載して下さい。
　`テスト対象のクラス名` は後記の命名規則に従って記載して下さい。

`// テスト対象のクラス名.DayInfoTest.CopilotGen.cs - このテストは GitHub Copilot によって自動生成されました。`
`// 手動によるテストの追加が必要な場合は テスト対象のクラス名.cs を作成し、partialクラスでテストを追加してください。`

・3行目は空白とし、4行目から using ディレクティブを記載して下さい。

### 2.2 テストクラスのネームスペースについて

・テストクラスのネームスペースは `テスト対象のクラスが格納されているネームスペース名称.UnitTests` として下さい。
　サンプルは以下の通りです。

例：SampleClass のテストクラスのネームスペースは `SampleNamespace` とします。
```csharp
namespace SampleNamespace.UnitTests
{
	// テストクラス
}
```

### 2.3 テストクラスの命名規則

・クラスの名称は `テスト対象のクラス名Test` として下さい。
・自動生成されたテストは手動生成のクラスと区別したいため partial クラスとして作成して下さい。
・partial クラスのため、テスト対象のクラス名のファイル名には `.CopilotGen.cs` を付けて下さい。
・クラス名の末尾に `Test` がついていた場合は `Test` を重複させないように注意して下さい。

サンプルは以下の通りです。

例1： テスト対象のクラス名は `SampleClass` とします。
```csharp
namespace SampleNamespace.UnitTests
{
	public partial class SampleClassTest
	{
		// テストメソッド
	}
}
```
例2： テスト対象のクラス名は `MyClassTest` とします。 末尾に `Test` がついている例です。
```csharp
namespace SampleNamespace.UnitTests
{
	public partial class MyClassTest
	{
		// テストメソッド
	}
}
```


### 2.4 テストメソッドの作成規則

リファクタリングに対応させるため、少し複雑です。

・最初に `const` でテスト内容の名称を定義します。
　・`const` で定義する定数の名称は `TESTNAME_` + `大番号` + `_` + `小番号` として下さい。
　　大番号はテスト対象のクラス名の連番、小番号はテスト対象のメソッド名の連番です。
    大番号は3桁で定義し、小番号は5桁で定義して下さい。
    ・大番号は1ずつカウントアップし、小番号は100ずつカウントアップして下さい。

　・`const` に定義する文字列は `nameof(テスト対象のクラス名)` + `.` + `nameof(テスト対象のクラス名.テスト対象のメソッド名)` + `_` + `テスト内容` として下さい。
    クラス名やメソッド名のリファクタリング時に、テスト名称も変わるように、テスト対象のクラス名とテスト対象のメソッド名は `nameof` を使用して動的に取得して下さい。

  ・テスト内容は、期待する動作を簡潔に記載して下さい。
  　どのパターンでどのような動作を期待しているのかは、別途、記載します。

  ・最終的に const の定義は、以下のサンプルのようになります。
    テスト対象のクラス名が `StringService` で、テスト対象のメソッド名が `TestTargetMethod1` および `TestTargetMethod2` の場合のサンプルです。
    
    ```csharp
    public const string TESTNAME_001_00100 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作1";
    public const string TESTNAME_001_00200 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作2";
    public const string TESTNAME_001_00300 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作3";
    public const string TESTNAME_002_00100 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod2) + "_" + "テストに期待する動作1";
    public const string TESTNAME_002_00200 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod2) + "_" + "テストに期待する動作2";
    ```

・テストメソッドの名称は `TEST_` + `大番号` + `_` + `小番号` として下さい。
・fact や Theory の DisplayName には const で定義したテスト内容の名称を指定して下さい。
・大番号と小番号は const で定義したものと同じものを指定して下さい。

・ここまでのテストメソッドの作成規則を全てまとめると、以下のサンプルのようになります。

```csharp
namespace SampleNamespace.UnitTests
{
	public partial class SampleClassTest
	{        
        public const string TESTNAME_001_00100 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作1";
        public const string TESTNAME_001_00200 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作2";
        public const string TESTNAME_001_00300 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod1) + "_" + "テストに期待する動作3";
        public const string TESTNAME_002_00100 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod2) + "_" + "テストに期待する動作1";
        public const string TESTNAME_002_00200 = nameof(StringService) + "." + nameof(StringService.TestTargetMethod2) + "_" + "テストに期待する動作2";

        [Fact(DisplayName = TESTNAME_001_00100)]
        public void TEST_001_00100()
        {
            // テストコード
        }

        [Fact(DisplayName = TESTNAME_001_00200)]
        public void TEST_001_00200()
        {
            // テストコード
        }

        // 他のテストメソッドも同様に作成
	}
}
```

## 3. テスト作成の観点

テスト作成の観点については以下の通りです。

### 3.1 対象範囲

・public および internal のメソッドに対してテストを作成してください。テストプロジェクト側で internal を参照できるように記載をしておきます。
・protected や private のメソッドに対してはテストを作成しないでください。
・可能な限り、カバレッジを100%にするようにテストを作成してください。
・protected や private はカバレッジの対象外とします。（カバーできない範囲は設計および実装のミスです。）

### 3.2 テストの内容

・引数のエラーチェックによる Exception の発生は全て確認してください。
・数値の範囲チェックや null チェックなど、引数のチェックについては、全てのパターンを確認してください。
・戻り値のチェックについては、全てのパターンを確認してください。
・戻り値が void の場合は、可能であれば、メソッドの実行後の状態を確認してください。
　・戻り値が void の場合で、メソッドの実行後の状態を確認できない場合は、正常終了のみを確認してください。

### 3.2.1 文字列のチェックについて

・文字列のチェックについては、全てのパターンを確認してください。
・文字列の長さが 0 の場合、1 の場合、最大長の場合、最大長 + 1 の場合など、全てのパターンを確認してください。
・文字列の内容が null の場合、空文字の場合、半角スペースのみの場合、全角スペースのみの場合、最大長の場合、最大長 + 1 の場合など、全てのパターンを確認してください。

### 3.2.2 数値のチェックについて

・数値のチェックについては、全てのパターンを確認してください。
・数値の範囲が 0 の場合、1 の場合、最大値の場合、最大値 + 1 の場合など、全てのパターンを確認してください。
・数値の範囲が 0 から 1 の場合、0 から 1 未満の場合、0 から 1 以上の場合、0 から 1 以下の場合など、全てのパターンを確認してください。
・可能であれば、オーバーフローのチェックも行ってください。 int や long などで不可能な場合は、対象外とします。

