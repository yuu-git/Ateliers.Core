namespace Ateliers
{
    /// <summary> 
    /// アプリケーション起動タイプ 
    /// </summary>
    /// <remarks>
    /// <para> 概要: マルチプラットフォームアプリケーションがどのOSで動いてるかを示す環境変数として扱う。 </para>
    /// <para> これにより、プラットフォーム固有で処理するべき内容を分岐させる。 </para>
    /// </remarks>
    public enum AppRunType
    {
        /// <summary> 未設定 </summary>
        Non,
        /// <summary> アンドロイドアプリケーション </summary>
        Android,
        /// <summary> iOSアプリケーション </summary>
        iOS,
        /// <summary> UWPアプリケーション </summary>
        UWP,
        /// <summary> WPFアプリケーション </summary>
        WPF,
        /// <summary> サーバーサービス </summary>
        ServerService,
    }
}
