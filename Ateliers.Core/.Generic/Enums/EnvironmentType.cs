using System;

namespace Ateliers
{
    /// <summary> 
    /// 環境型フラグ
    /// </summary>
    /// <remarks>
    /// <para> 概要: ビルドの状態および稼働環境を示す。『デバッグ時のみ処理する』『本番環境でのみ処理する』などの判定に使う。 </para>
    /// <para> 使用例としては『デバッグ-開発環境 = 8 + 1024 = 1032』『正式リリース-検証環境 = 64 + 4096 = 4160』となる。 </para>
    /// </remarks>
    [Flags]
    public enum EnvironmentType 
    {
        /// <summary> 不明（未設定） </summary>
        Non = 1,

        /// <summary> （開発）デバッグ </summary>
        Debug = 8,
        /// <summary> アルファリリース </summary>
        Alpha = 16,
        /// <summary> ベータリリース </summary>
        Beta = 32,
        /// <summary> 正式リリース </summary>
        Official = 64,

        /// <summary> テスト </summary>
        Test = 128,

        /// <summary> 開発環境 </summary>
        Develop = 1024,
        /// <summary> 結合環境 </summary>
        Integration = 2048,
        /// <summary> 検証環境 </summary>
        Staging = 4096,
        /// <summary> 本番環境 </summary>
        Product = 8192,
    }
}
