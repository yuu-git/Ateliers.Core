## ライセンス

このプロジェクトは [MIT ライセンス](LICENSE) の下で提供されています。  
詳細については、[LICENSE](LICENSE) ファイルをご覧ください。  
  
(This project is licensed under the terms of the [MIT License](LICENSE). See the [LICENSE](LICENSE) file for details. )

## リポジトリの概要

C# による汎用機能ライブラリです。  
ateliers.dev のプロジェクト全てに適用する中核です。

## 設計手法

ドメイン駆動設計 (DDD) を元に C# で製造しています。  
基本的な設計はMicrosoft社のガイドラインを元にしています。  
https://learn.microsoft.com/ja-jp/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice

## 動作環境 (開発者作業環境)

### 1. ソフトウェア
+ OS: Windows 11
+ IDE: Visual Studio Community 2022 - Version 17.9.3 
+ IDE: Visual Studio Code - Version 1.87.2
  
### 2. IDE インストール済み拡張機能
+ [VS] GitHub Copilot - 1.156.0.0
+ [VS Code] GitHub Copilot - 1.175.0
 
## プロジェクト構成

基幹ライブラリであるため プレゼンテーションレイヤーは存在しません。

### 1. Ateliers.Core.ApplicationLayer プロジェクト:  

Framework: .Net Standard 2.0 (2024/03 ～ 現在)  
  
DDDにおける『ユースケース』『アプリケーションサービス』『ファクトリ』を実装。  
DomainLayer および InfrastructureLayer を参照する。  
※ 汎用機能処理であるため、おそらく『ユースケース』は要件がない…かな。  

### 2. Ateliers.Core.DomainLayer プロジェクト:  

Framework: .Net Standard 2.0 (2024/03 ～ 現在)  
  
DDDにおける『ドメインサービス』『集約』『エンティティ』『値オブジェクト』を実装。  

### 3. Ateliers.Core.InfrastructureLayer プロジェクト:  

Framework: .Net Standard 2.0 (2024/03 ～ 現在)  
  
DDDにおける『インフラストラクチャサービス』『リポジトリ』を実装。   
DomainLayer を参照する。  

### 4. Ateliers.Core.AIAssistant プロジェクト：

Framework: .Net Standard 2.0 (2024/03 ～ 現在)  
  
主に GitHub Copilot の利用時に、学習コードや生成方法を格納したプロジェクト。  
指示や規約がメインとなり、コードベースではなくテキストベース (`*.md`) となっている。  
そのため DDD に対する参照は無し。

### 5. Ateliers.Core.TestHelper プロジェクト：

Framework: .Net Standard 2.0 (2024/03 ～ 現在)  
  
テストコードを作成時に利用するライブラリで、テスト用の汎用機能を提供する。  
汎用的な `Mock` やテストフォルダーの作成や削除の機能を提供する。

## サブモジュール化の手順

### 1. コマンド

現在のプロジェクトディレクトリに submodules というディレクトリを作成して、その中に Ateliers.Core サブモジュールを追加したい場合：
```
git submodule add https://github.com/yuu-git/Ateliers.Core.git submodules/Ateliers.Core
```

### 2. ブランチについて

基本的に master の使用を推奨。  
開発中機能の使用は Develop ブランチを使用し、試験的機能を試す場合は、新しくブランチを作る。  
`checkout` および `pull` の手順は以下の通り  
```
サブモジュールディレクトリに移動後：
git checkout master
git pull origin master
```
または
```
サブモジュールディレクトリに移動後：
git checkout Develop
git pull origin Develop
```
