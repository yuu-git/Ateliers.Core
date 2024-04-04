
## ライセンス

このプロジェクトは [MIT ライセンス](LICENSE) の下で提供されています。  
詳細については、[LICENSE](LICENSE) ファイルをご覧ください。  
  
(This project is licensed under the terms of the [MIT License](LICENSE). See the [LICENSE](LICENSE) file for details. )

## リポジトリについて

C# による汎用機能ライブラリです。  
ateliers.dev のプロジェクト全てに適用する中核です。

## 動作確認環境（開発環境）

VisualStudio

## 設計手法

ドメイン駆動設計 (DDD) を元に C# で製造しています。  
基本的な設計はMicrosoft社のガイドラインを元にしています。  
https://learn.microsoft.com/ja-jp/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice

## プロジェクト構成

### メインプロジェクト

DDDの概念に基づき、以下の3つから構成されます。  
ライブラリであるため プレゼンテーションレイヤーは存在しません。

#### Ateliers.Core.ApplicationLayer プロジェクト:  

- ターゲット: .net standard 2.0 (2024/04/01 時点)  
  
DDDにおける『ユースケース』『アプリケーションサービス』『ファクトリ』を実装。  
DomainLayer および InfrastructureLayer を参照する。  
※ 汎用機能処理であるため、おそらく『ユースケース』は要件がない…かな。  

#### Ateliers.Core.DomainLayer プロジェクト:  
DDDにおける『ドメインサービス』『集約』『エンティティ』『値オブジェクト』を実装。  

#### Ateliers.Core.InfrastructureLayer プロジェクト:  
DDDにおける『インフラストラクチャサービス』『リポジトリ』を実装。   
DomainLayer を参照する。  

### サブプロジェクト

DDDには関連しないサポートプロジェクトです。  
生成AIの指示やテストサポートなどを提供します。


## サブモジュール化の手順

現在のプロジェクトディレクトリに submodules というディレクトリを作成して、その中に Ateliers.Core サブモジュールを追加したい場合：

git submodule add https://github.com/yuu-git/Ateliers.Core.git submodules/Ateliers.Core
