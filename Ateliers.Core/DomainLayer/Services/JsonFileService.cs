using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;


namespace Ateliers
{
    /// <summary>
    /// STA - JSONファイルサービス
    /// </summary>
    /// <remarks>
    /// <para> 概要： JSONファイルの保存と読込を実行する。 <see cref="EncryptService"/> との連携により、JSONファイルの暗号保存と復号読込もサポートする。 </para>
    /// <para> また、単体機能として文字列をJSON形式シリアライズとデシリアライズを提供する。 </para>
    /// <para> 他サービスと併用することで、JSON形式でのDB保存およびDB読込などの機能を実現できる。 </para>
    /// <para> 非同期処理には対応していないため、ファイルサイズが大きい場合などは、利用先で非同期処理の実装を検討して下さい。 </para>
    /// </remarks>
    public static class JsonFileService
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- エラーメッセージ ---

        /// <summary> 異常終了メッセージ: JSONに変換するオブジェクトコレクションは必須です。 </summary>
        public static string MSG_ERR_010_0010 => "JSONに変換するオブジェクトコレクションは必須です。";
        /// <summary> 異常終了メッセージ: JSONに変換するオブジェクトをnullにする事はできません。 </summary>
        public static string MSG_ERR_010_0020 => "JSONに変換するオブジェクトをnullにする事はできません。";
        /// <summary> 異常終了メッセージ: オブジェクトに変換する為のJSON文字列コレクションは必須です。 </summary>
        public static string MSG_ERR_010_0030 => "オブジェクトに変換する為のJSON文字列コレクションは必須です。";
        /// <summary> 異常終了メッセージ: オブジェクトに変換する為のJSON文字列は必須です。 </summary>
        public static string MSG_ERR_010_0040 => "オブジェクトに変換する為のJSON文字列は必須です。";
        /// <summary> 異常終了メッセージ: 読込するJSONファイルのパスは必須です。 </summary>
        public static string MSG_ERR_020_0010 => "読込するJSONファイルのパスは必須です。";
        /// <summary> 異常終了メッセージ: 読込するファイルが見つかりませんでした。 </summary>
        public static string MSG_ERR_020_0020 => "読込するファイルが見つかりませんでした。";
        /// <summary> 異常終了メッセージ: 復号に使用するパスワードは必須です。 </summary>
        public static string MSG_ERR_020_0030 => "復号に使用するパスワードは必須です。";
        /// <summary> 異常終了メッセージ: JSONとして保存するファイルパスをnullまたは空白にする事はできません。 </summary>
        public static string MSG_ERR_030_0010 => "JSONとして保存するファイルパスをnullまたは空白にする事はできません。";
        /// <summary> 異常終了メッセージ: JSONとして保存するオブジェクトをnullにする事はできません。 </summary>
        public static string MSG_ERR_030_0020 => "JSONとして保存するオブジェクトをnullにする事はできません。";
        /// <summary> 異常終了メッセージ: 暗号のパスワードをnullまたは空白にする事はできません。 </summary>
        public static string MSG_ERR_030_0030 => "暗号のパスワードをnullまたは空白にする事はできません。";
        /// <summary> 異常終了メッセージ: 既にJSONファイルが存在しており、上書きは許可されません。 / [0] </summary>
        public static string MSG_ERR_030_0040 => "既にJSONファイルが存在しており、上書きは許可されません。 / [0]";
        /// <summary> 異常終了メッセージ: 保存するJSONファイル情報コレクションは必須です。 </summary>
        public static string MSG_ERR_030_0050 => "保存するJSONファイル情報コレクションは必須です。";
        /// <summary> 異常終了メッセージ: エンコード形式の指定は必須です。 </summary>
        public static string MSG_ERR_030_0060 => "エンコード形式の指定は必須です。";
        /// <summary> 異常終了メッセージ: ファイル操作中にエラーが発生しました。 </summary>
        public static string MSG_ERR_030_0070 => "ファイル操作中にエラーが発生しました。 / [0]";
        #endregion

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 複数のオブジェクトをJSON形式の文字列に変換します。
        /// </summary>
        /// <param name="objs"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列のコレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static IEnumerable<string> Serializes(IEnumerable<object> objs)
        {
            if (objs is null)
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0010);

            if (objs.Any(x => x is null))
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0020);

            return objs.Select(x => JsonSerializer.Serialize(x)).ToList();
        }

        /// <summary>
        /// オブジェクトをJSON形式の文字列に変換します。
        /// </summary>
        /// <param name="obj"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static string Serialize(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_010_0020);

            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// 複数のJSON形式の文字列を指定オブジェクトに変換します。
        /// </summary>
        /// <typeparam name="T"> 文字列を復元後の型を指定します。 </typeparam>
        /// <param name="jsons"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列のコレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 変換に使用するJSON形式の文字列コレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 変換に使用するJSON形式の文字列は必須です。 </exception>
        public static IEnumerable<T> Deserializes<T>(IEnumerable<string> jsons)
        {
            if (jsons is null)
                throw new ArgumentNullException(nameof(jsons), MSG_ERR_010_0030);

            if (jsons.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(jsons), MSG_ERR_010_0040);

            return jsons.Select(x => JsonSerializer.Deserialize<T>(x)).ToList();
        }

        /// <summary>
        /// JSON形式の文字列を指定オブジェクトに変換します。
        /// </summary>
        /// <typeparam name="T"> 文字列を復元後の型を指定します。 </typeparam>
        /// <param name="json"> JSON形式の文字列を指定します。 </param>
        /// <returns> 文字列から変換したオブジェクトを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 変換に使用するJSON形式文字列は必須です。 </exception>
        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException(nameof(json), MSG_ERR_010_0040);

            return JsonSerializer.Deserialize<T>(json);
        }

        public static class DirectoryService
        {
            public static IEnumerable<FileInfo> GetJsonFileList(string directoryPath, bool isSubDirectoryTaregt = false, IEnumerable<string> fileExtensions = null)
            {
                var ext = fileExtensions ?? new List<string> { "json" };
                var files = Directory.EnumerateFiles(directoryPath, "*.*", isSubDirectoryTaregt ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Where(s => ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()));

                return files.Select(x => new FileInfo(x)).ToList();
            }
        }

        /// <summary>
        /// 暗号化されたJSON形式ファイルを復号化して読み込みます。
        /// </summary
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <param name="password"> 暗号状態を復号するパスワードを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 読込するJSONファイルのパスは必須です。 </exception>
        /// <exception cref="FileNotFoundException"> 読込ファイルは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 復号に使用するパスワードは必須です。 </exception>
        public static T LoadFileToDecrypt<T>(string filePath, string password)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0010);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(MSG_ERR_020_0020, filePath);

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), MSG_ERR_020_0030);

            return ReadFile<T>(filePath, password);
        }

        /// <summary>
        /// JSON形式ファイルを読み込みます。
        /// </summary>
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 読込するJSONファイルのパスは必須です。 </exception>
        /// <exception cref="FileNotFoundException"> 読込ファイルは必須です。 </exception>
        public static T LoadFile<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0010);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(MSG_ERR_020_0020, filePath);

            return ReadFile<T>(filePath);
        }

        /// <summary>
        /// 暗号化したJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <param name="password"> (任意) 暗号化に使用するパスワードを指定します。nullや空白は許可されません。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <remarks>
        /// <para> ファイルのエンコードはUTF-8形式で保存します。 </para>
        /// </remarks>
        public static FileInfo SaveFileToEncrypt(string filePath, object obj, string password, bool isOverride = false, FileShare shareOptions = FileShare.None)
        {
            return SaveFileToEncrypt(filePath, obj, password, Encoding.UTF8, isOverride, shareOptions);
        }

        /// <summary>
        /// 暗号化したJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="password"> 暗号化に使用するパスワードを指定します。nullや空白は許可されません。 </param>
        /// <param name="encoding"> ファイルのエンコード形式を指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> エンコード形式の指定は必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 暗号化のパスワードは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static FileInfo SaveFileToEncrypt(string filePath, object obj, string password, Encoding encoding, bool isOverride = false, FileShare shareOptions = FileShare.None)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding), MSG_ERR_030_0060);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), MSG_ERR_030_0030);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            var writeString = EncryptService.RFC2898.EncryptString(JsonSerializer.Serialize(obj), password);

            return CreateOrUpdateFile(filePath, writeString, encoding, shareOptions);
        }

        /// <summary>
        /// 複数のJSONファイルを保存します。
        /// </summary>
        /// <param name="files"> 保存情報コレクションを指定します。 (filePath = 保存するファイルパス, obj = 保存するオブジェクト)  </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <returns> 保存したファイル情報コレクションを返します。 </returns>
        /// <remarks>
        /// <para> ファイルのエンコードはUTF-8形式で保存します。 </para>
        /// <para> 留意点: ファイルのトランザクションは考慮されません。複数ファイルの整合性が重要な場合、仕組みの実装やデータベースの使用を検討して下さい。 </para>
        /// </remarks>
        public static IEnumerable<FileInfo> SaveFiles(IEnumerable<(string filePath, object obj)> files, bool isOverride = false, FileShare shareOptions = FileShare.None)
        {
            return SaveFiles(files, Encoding.UTF8, isOverride, shareOptions);
        }

        /// <summary>
        /// 複数のJSONファイルを保存します。
        /// </summary>
        /// <param name="files"> 保存情報コレクションを指定します。 (filePath = 保存するファイルパス, obj = 保存するオブジェクト)  </param>
        /// <param name="encoding"> ファイルのエンコード形式を指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <returns> 保存したファイル情報コレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSON保存情報コレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> エンコード形式の指定は必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        /// <remarks>
        /// <para> 留意点: ファイルのトランザクションは考慮されません。複数ファイルの整合性が重要な場合、仕組みの実装やデータベースの使用を検討して下さい。 </para>
        /// <para> 挙動: 上書きを許可しない場合、1つ以上のファイルが既に存在していた場合は、全てのファイル保存を中止して異常終了します。 </para>
        /// </remarks>
        public static IEnumerable<FileInfo> SaveFiles(IEnumerable<(string filePath, object obj)> files, Encoding encoding, bool isOverride = false, FileShare shareOptions = FileShare.None)
        {
            if (files is null)
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0050);

            if (files.Any(x => string.IsNullOrEmpty(x.filePath)))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0010);

            if (files.Any(x => x.obj is null))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0020);

            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding), MSG_ERR_030_0060);

            if (!isOverride)
            {
                var existsFiles = files.Where(x => File.Exists(x.filePath));
                if (existsFiles.Any())
                    throw new IOException(string.Format(MSG_ERR_030_0040, string.Join(", ", existsFiles.Select(x => x.filePath))));
            }

            return files.Select(x => CreateOrUpdateFile(x.filePath, JsonSerializer.Serialize(x.obj), encoding, shareOptions)).ToList();
        }

        /// <summary>
        /// JSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <remarks>
        /// <para> ファイルのエンコードはUTF-8形式で保存します。 </para>
        /// </remarks>
        public static FileInfo SaveFile(string filePath, object obj, bool isOverride = false, FileShare shareOptions = FileShare.None)
        { 
            return SaveFile(filePath, obj, Encoding.UTF8, isOverride, shareOptions);
        }

        /// <summary>
        /// JSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="encoding"> ファイルのエンコード形式を指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> エンコード形式の指定は必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static FileInfo SaveFile(string filePath, object obj, Encoding encoding, bool isOverride = false, FileShare shareOptions = FileShare.None)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding), MSG_ERR_030_0060);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            return CreateOrUpdateFile(filePath, JsonSerializer.Serialize(obj), encoding, shareOptions);
        }


        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// ファイルの作成や更新を実行します。
        /// </summary>
        /// <param name="filePath"> 対象ファイルのパスを指定します。 </param>
        /// <param name="writeString"> 保存する文字列を指定します。 </param>
        /// <param name="encoding"> ファイルのエンコード形式を指定します。 </param>
        /// <param name="shareOptions"> (任意) ファイルの共有オプションを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="IOException"> 何らかのファイル操作失敗時に発生します。 </exception>
        private static FileInfo CreateOrUpdateFile(string filePath, string writeString, Encoding encoding, FileShare shareOptions)
        {
            var fileMode = File.Exists(filePath) ? FileMode.Open : FileMode.Create;

            using (var fileStream = new FileStream(filePath, fileMode, FileAccess.Write, shareOptions))
            {
                try
                {
                    var data = encoding.GetBytes(writeString);
                    fileStream.Write(data, 0, data.Length);

                    return new FileInfo(filePath);
                }
                catch (IOException ex)
                {
                    throw new IOException(string.Format(MSG_ERR_030_0070, filePath), ex);
                }
            }
        }

        /// <summary>
        /// JSON形式ファイルを読み込みます。
        /// </summary
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        private static T ReadFile<T>(string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(reader.ReadToEnd());
                }
                catch (IOException ex)
                {
                    throw new IOException(string.Format(MSG_ERR_030_0070, filePath), ex);
                }
            }
        }

        /// <summary>
        /// 暗号化されたJSON形式ファイルを読み込みます。
        /// </summary
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <param name="password"> 暗号解除のパスワードを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        private static T ReadFile<T>(string filePath, string password)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                try
                {
                    var json = EncryptService.RFC2898.DecryptString(reader.ReadToEnd(), password);
                    return JsonSerializer.Deserialize<T>(json);
                }
                catch (IOException ex)
                {
                    throw new IOException(string.Format(MSG_ERR_030_0070, filePath), ex);
                }
            }
        }
    }
}
