using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Ateliers
{
    /// <summary>
    /// STA - JSONファイルサービス
    /// </summary>
    /// <remarks>
    /// <para> 概要： JSONファイルの保存と読込を実行する。 <see cref="EncryptService"/> との連携により、JSONファイルの暗号保存と復号読込もサポートする。 </para>
    /// <para> また、単体機能として文字列をJSON形式シリアライズとデシリアライズを提供する。 </para>
    /// <para> 他サービスと併用することで、JSON形式でのDB保存およびDB読込などの機能を実現できる。 </para>
    /// </remarks>
    public static class JsonFileService
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- エラーメッセージ ---

        //Todo: EncryptService: エラーメッセージの多言語対応
        /// <summary> 異常終了メッセージ: JSONに変換するオブジェクトコレクションは必須です。 </summary>
        public static string MSG_ERR_010_0010 => "JSONに変換するオブジェクトコレクションは必須です。";
        /// <summary> 異常終了メッセージ: JSONに変換するオブジェクトをnullにする事はできません。 </summary>
        public static string MSG_ERR_010_0020 => "JSONに変換するオブジェクトをnullにする事はできません。";
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
        #endregion

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 非同期処理で複数のオブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="objs"> 変換するオブジェクトコレクションを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> JSON化した文字列のコレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static async Task<IEnumerable<string>> ConvertsAsync(IEnumerable<object> objs, CancellationToken token = default)
        {
            if (objs is null)
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0010);

            if (objs.Any(x => x is null))
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0020);

            return await Task.Run(() => ExecuteConverts(objs), token);
        }

        /// <summary>
        /// 複数のオブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="objs"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列のコレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static IEnumerable<string> Converts(IEnumerable<object> objs)
        {
            if (objs is null)
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0010);

            if (objs.Any(x => x is null))
                throw new ArgumentNullException(nameof(objs), MSG_ERR_010_0020);

            return ExecuteConverts(objs);
        }

        /// <summary>
        /// 非同期処理でオブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="obj"> 変換するオブジェクトを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> JSON化した文字列を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static async Task<string> ConvertAsync(object obj, CancellationToken token = default)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_010_0020);

            return await Task.Run(() => ExecuteConvert(obj), token);
        }

        /// <summary>
        /// オブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="obj"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONに変換するオブジェクトをnullにする事はできません。 </exception>
        public static string Convert(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_010_0020);

            return ExecuteConvert(obj);
        }

        /// <summary>
        /// 非同期処理で暗号化されたJSON形式ファイルを復号化して読み込みます。
        /// </summary>
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <param name="password"> 暗号状態を復号するパスワードを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 読込するJSONファイルのパスは必須です。 </exception>
        /// <exception cref="FileNotFoundException"> 読込ファイルは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 復号に使用するパスワードは必須です。 </exception>
        public static async Task<T> LoadFileToDecryptAsync<T>(string filePath, string password, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0010);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(MSG_ERR_020_0020, filePath);

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), MSG_ERR_020_0030);

            return await Task.Run(() => ExecuteLoadFileToDecrypt<T>(filePath, password));
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

            return ExecuteLoadFileToDecrypt<T>(filePath, password);
        }

        /// <summary>
        /// 非同期処理でJSON形式ファイルを読み込みます。
        /// </summary>
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        /// <exception cref="ArgumentNullException"> 読込するJSONファイルのパスは必須です。 </exception>
        /// <exception cref="FileNotFoundException"> 読込ファイルは必須です。 </exception>
        public static async Task<T> LoadFileAsync<T>(string filePath, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0010);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(MSG_ERR_020_0020, filePath);

            return await Task.Run(() => ExecuteLoadFile<T>(filePath), token);
        }

        /// <summary>
        /// JSON形式ファイルを読み込み、オブジェクトとして返却します。
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

            return ExecuteLoadFile<T>(filePath);
        }

        /// <summary>
        /// 非同期処理で暗号化したJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="password"> (任意) 暗号化に使用するパスワードを指定します。nullや空白は許可されません。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 暗号化のパスワードは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static async Task<FileInfo> SaveFileToEncryptAsync(string filePath, object obj, bool isOverride = false, string password = "ateliers.dev", CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), MSG_ERR_030_0030);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            return await Task.Run(() => ExecuteSaveFileToEncrypt(filePath, obj, password), token);
        }

        /// <summary>
        /// 暗号化したJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="password"> (任意) 暗号化に使用するパスワードを指定します。nullや空白は許可されません。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 暗号化のパスワードは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static FileInfo SaveFileToEncrypt(string filePath, object obj, bool isOverride = false, string password = "ateliers.dev")
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), MSG_ERR_030_0030);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            return ExecuteSaveFileToEncrypt(filePath, obj, password);
        }

        /// <summary>
        /// 非同期処理で複数のJSONファイルを保存します。
        /// </summary>
        /// <param name="files"> 保存情報コレクションを指定します。 (filePath = 保存するファイルパス, obj = 保存するオブジェクト)  </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 保存したファイル情報コレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSON保存情報コレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static async Task<IEnumerable<FileInfo>> SaveFilesAsync(IEnumerable<(string filePath, object obj)> files, bool isOverride = false, CancellationToken token = default)
        {
            if (files is null)
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0050);

            if (files.Any(x => string.IsNullOrEmpty(x.filePath)))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0010);

            if (files.Any(x => x.obj is null))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0020);

            if (!isOverride)
            {
                var ngFiles = files.Where(x => File.Exists(x.filePath));
                if (ngFiles.Any())
                    throw new IOException(string.Format(MSG_ERR_030_0040, string.Join(", ", ngFiles.Select(x => x.filePath))));
            }

            return await Task.Run(() => ExecuteSaveFiles(files), token);
        }

        /// <summary>
        /// 複数のJSONファイルを保存します。
        /// </summary>
        /// <param name="files"> 保存情報コレクションを指定します。 (filePath = 保存するファイルパス, obj = 保存するオブジェクト)  </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <returns> 保存したファイル情報コレクションを返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSON保存情報コレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static IEnumerable<FileInfo> SaveFiles(IEnumerable<(string filePath, object obj)> files, bool isOverride = true)
        {
            if (files is null)
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0050);

            if (files.Any(x => string.IsNullOrEmpty(x.filePath)))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0010);

            if (files.Any(x => x.obj is null))
                throw new ArgumentNullException(nameof(files), MSG_ERR_030_0020);

            if (!isOverride)
            {
                var ngFiles = files.Where(x => File.Exists(x.filePath));
                if (ngFiles.Any())
                    throw new IOException(string.Format(MSG_ERR_030_0040, string.Join(", ", ngFiles.Select(x => x.filePath))));
            }

            return ExecuteSaveFiles(files);
        }

        /// <summary>
        /// 非同期処理でJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static async Task<FileInfo> SaveFileAsync(string filePath, object obj, bool isOverride = false, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            return await Task.Run(() => ExecuteSaveFile(filePath, obj), token);
        }

        /// <summary>
        /// JSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="isOverride"> (任意) 上書き保存を許可する場合は ture, しない場合は false を指定します。　*デフォルト: false </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        /// <exception cref="ArgumentNullException"> JSONとして保存するファイルパスは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> JSONとして保存するオブジェクトは必須です。 </exception>
        /// <exception cref="IOException"> ファイルが存在していて、上書きが許可されない場合に発生します。 </exception>
        public static FileInfo SaveFile(string filePath, object obj, bool isOverride = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0010);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_030_0020);

            if (!isOverride && File.Exists(filePath))
                throw new IOException(string.Format(MSG_ERR_030_0040, filePath));

            return ExecuteSaveFile(filePath, obj);
        }

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 複数のオブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="objs"> 変換するオブジェクトコレクションを指定します。 </param>
        /// <returns> JSON化した文字列のコレクションを返します。 </returns>
        private static IEnumerable<string> ExecuteConverts(IEnumerable<object> objs)
        {
            return objs.Select(x => ExecuteConvert(x)).ToList();
        }

        /// <summary>
        /// オブジェクトをJSON形式にシリアライズします。
        /// </summary>
        /// <param name="obj"> 変換するオブジェクトを指定します。 </param>
        /// <returns> JSON化した文字列を返します。 </returns>
        private static string ExecuteConvert(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 暗号化されたJSON形式ファイルを復号化して読み込みます。
        /// </summary
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <param name="password"> 暗号状態を復号するパスワードを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        private static T ExecuteLoadFileToDecrypt<T>(string filePath, string password)
        {
            var baseJson = File.ReadAllText(filePath);
            var json = EncryptService.RFC2898.DecryptString(baseJson, password);

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// JSON形式ファイルを読み込みます。
        /// </summary
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePath"> 読み込みするJSONファイルのパスを指定します。 </param>
        /// <returns> 復元したオブジェクトを返します。 </returns>
        private static T ExecuteLoadFile<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 暗号化したJSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="password"> (任意) 暗号化に使用するパスワードを指定します。nullや空白は許可されません。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        private static FileInfo ExecuteSaveFileToEncrypt(string filePath, object obj, string password)
        {
            var baseJson = JsonConvert.SerializeObject(obj);
            var json = EncryptService.RFC2898.EncryptString(baseJson, password);

            File.WriteAllText(filePath, json);

            return new FileInfo(filePath);
        }

        /// <summary>
        /// 複数のJSONファイルを保存します。
        /// </summary>
        /// <param name="files"> 保存情報コレクションを指定します。 (filePath = 保存するファイルパス, obj = 保存するオブジェクト)  </param>
        /// <returns> 保存したファイル情報コレクションを返します。 </returns>
        private static IEnumerable<FileInfo> ExecuteSaveFiles(IEnumerable<(string filePath, object obj)> files)
        {
            return files.Select(x => ExecuteSaveFile(x.filePath, x.obj)).ToList();
        }

        /// <summary>
        /// JSONファイルを保存します。
        /// </summary>
        /// <param name="filePath"> 保存するファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <returns> 保存したファイル情報を返します。 </returns>
        private static FileInfo ExecuteSaveFile(string filePath, object obj)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(obj));

            return new FileInfo(filePath);
        }
    }
}
