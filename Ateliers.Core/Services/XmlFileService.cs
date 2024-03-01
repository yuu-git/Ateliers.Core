using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ateliers
{
    /// <summary> 
    /// STA - XMLファイルサービス
    /// </summary>
    /// <remarks>
    /// <para> 概要: XMLファイルの読込と保存を実行する。 </para>
    /// <para> 単体機能として、指定ディレクトリに存在する .xml 拡張子のファイル一覧を返す。 </para>
    /// </remarks>
    public static class XmlFileService
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        #region --- エラーメッセージ ---

        //Todo: EncryptService: エラーメッセージの多言語対応
        /// <summary> 異常終了メッセージ: ディレクトリパスは必須です。 </summary>
        public static string MSG_ERR_010_0010 => "ディレクトリパスは必須です。";
        /// <summary> 異常終了メッセージ: 引数ディクショナリがnullです。 </summary>
        public static string MSG_ERR_020_0010 => "保存対象を指定するディクショナリがnullです。";
        /// <summary> 異常終了メッセージ: 保存するファイルパスをnullまたは空白にする事はできません。 </summary>
        public static string MSG_ERR_020_0020 => "保存するファイルパスをnullまたは空白にする事はできません。";
        /// <summary> 異常終了メッセージ: 保存するオブジェクトをnullにする事はできません。 </summary>
        public static string MSG_ERR_020_0030 => "保存するオブジェクトをnullにする事はできません";
        /// <summary> 異常終了メッセージ: 読込対象を指定するコレクションは必須です。 </summary>
        public static string MSG_ERR_030_0010 => "読込対象を指定するコレクションは必須です。";
        /// <summary> 異常終了メッセージ: 読込するファイルパスをnullまたは空白にする事はできません。 </summary>
        public static string MSG_ERR_030_0020 => "読込するファイルパスをnullまたは空白にする事はできません。";
        /// <summary> 異常終了メッセージ: XMLファイル保存中にエラーが発生しました。 / [0]。 </summary>
        public static string MSG_ERR_040_0010 => "XMLファイル保存中にエラーが発生しました。 / [0]。";
        /// <summary> 異常終了メッセージ: XMLファイル復元中にエラーが発生しました。 </summary>
        public static string MSG_ERR_050_0010 => "XMLファイル復元中にエラーが発生しました。";
        /// <summary> 異常終了メッセージ: 読込ファイルが見つかりませんでした。 </summary>
        public static string MSG_ERR_050_0020 => "読込ファイルが見つかりませんでした。";
        #endregion

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// 非同期処理で指定パス内に存在する .xml ファイルの一覧を取得します。
        /// </summary>
        /// <param name="directoryPath"> 検索するディレクトリパスを指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> XMLのファイル名称とファイルパスを返します。 指定パス内に .xml が存在しなかった場合は 0件コレクション を返します。 </returns>
        /// <exception cref="ArgumentNullException"> ディレクトリパスは必須です。 </exception>
        public static async Task<IEnumerable<FileInfo>> GetXmlListAsync(string directoryPath, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(directoryPath)) 
                throw new ArgumentNullException(nameof(directoryPath), MSG_ERR_010_0010);

            return await Task.Run(() => ExecuteGetXmlList(directoryPath), token);
        }

        /// <summary> 
        /// 指定パス内に存在する .xml ファイルの一覧を取得します。
        /// </summary>
        /// <param name="directoryPath"> 検索するディレクトリパスを指定します。 </param>
        /// <returns> XMLのファイル名称とファイルパスを返します。 指定パス内に .xml が存在しなかった場合は 0件コレクション を返します。 </returns>
        /// <exception cref="ArgumentNullException"> ディレクトリパスは必須です。 </exception>
        public static IEnumerable<FileInfo> GetXmlList(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath), MSG_ERR_010_0010);

            return ExecuteGetXmlList(directoryPath);
        }

        /// <summary> 
        /// 並列非同期処理で複数のオブジェクトをXMLにシリアライズします。 
        /// </summary>
        /// <param name="dictionary"> 書込するXMLのファイルパスとオブジェクトのコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報コレクションを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、コレクション内のモデルを null で返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 保存対象を指定するディクショナリは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するファイルパスをnullまたは空白にする事はできません。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するオブジェクトをnullにする事はできません。 </exception>
        public static async Task<IEnumerable<FileInfo>> XmlSaveParallelAsync(IDictionary<string, object> dictionary, bool nonException = false, CancellationToken token = default)
        {
            if (dictionary is null)
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0010);

            if (dictionary.Select(x => x.Key).Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0020);

            if (dictionary.Select(x => x.Value).Any(x => x is null))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0030);

            var tasks = dictionary.Select(x => ExecuteSaveFileAsync(x.Key, x.Value, nonException, token));

            return await Task.WhenAll(tasks);
        }

        /// <summary> 
        /// 非同期処理で複数のオブジェクトをXMLにシリアライズします。 
        /// </summary>
        /// <param name="dictionary"> 書込するXMLのファイルパスとオブジェクトのコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報コレクションを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、コレクション内のモデルを null で返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 保存対象を指定するディクショナリは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するファイルパスをnullまたは空白にする事はできません。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するオブジェクトをnullにする事はできません。 </exception>
        public static async Task<IEnumerable<FileInfo>> SaveFilesAsync(IDictionary<string, object> dictionary, bool nonException = false, CancellationToken token = default)
        {
            if (dictionary is null)
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0010);

            if (dictionary.Select(x => x.Key).Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0020);

            if (dictionary.Select(x => x.Value).Any(x => x is null))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0030);

            return await Task.Run(() => ExecuteSaveFiles(dictionary, nonException), token);
        }

        /// <summary> 
        /// 非同期処理でオブジェクトをXMLにシリアライズします。 
        /// </summary>
        /// <param name="filePath"> 書込するXMLのファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報を返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は null を返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 保存するファイルパスをnullまたは空白にする事はできません。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するオブジェクトをnullにする事はできません。 </exception>
        public static async Task<FileInfo> SaveFileAsync(string filePath, object obj, bool nonException = false, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0020);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_020_0030);

            return await ExecuteSaveFileAsync(filePath, obj, nonException, token);
        }

        /// <summary> 
        /// 複数のオブジェクトをXMLにシリアライズします。 
        /// </summary>
        /// <param name="dictionary"> 書込するXMLのファイルパスとオブジェクトのコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報コレクションを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、コレクション内のモデルを null で返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 保存対象を指定するディクショナリは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するファイルパスをnullまたは空白にする事はできません。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するオブジェクトをnullにする事はできません。 </exception>
        public static IEnumerable<FileInfo> SaveFiles(IDictionary<string, object> dictionary, bool nonException = false)
        {
            if (dictionary is null)
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0010);

            if (dictionary.Select(x => x.Key).Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0020);

            if (dictionary.Select(x => x.Value).Any(x => x is null))
                throw new ArgumentNullException(nameof(dictionary), MSG_ERR_020_0030);

            return ExecuteSaveFiles(dictionary, nonException);
        }

        /// <summary> 
        /// オブジェクトをXMLにシリアライズします。 
        /// </summary>
        /// <param name="filePath"> 書込するXMLのファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報を返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は default を返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 保存するファイルパスをnullまたは空白にする事はできません。 </exception>
        /// <exception cref="ArgumentNullException"> 保存するオブジェクトをnullにする事はできません。 </exception>
        public static FileInfo SaveFile(string filePath, object obj, bool nonException = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_020_0020);

            if (obj is null)
                throw new ArgumentNullException(nameof(obj), MSG_ERR_020_0030);

            return ExecuteSaveFile(filePath, obj, nonException);
        }

        /// <summary>
        /// 並列非同期処理で複数のXMLファイルの逆シリアライズします。
        /// </summary>
        /// <param name="filePaths"> 読込するXMLのファイルパスコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトのコレクションを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、復元オブジェクトのdefaultを返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 読込対象を指定するコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 読込するファイルパスをnullまたは空白にする事はできません。 </exception>
        public static async Task<IEnumerable<T>> LoadFilesParallelAsync<T>(IEnumerable<string> filePaths, bool nonException = false, CancellationToken token = default)
        {
            if (filePaths is null)
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0010);

            if (!filePaths.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0020);

            var tasks = filePaths.Select(x => ExecuteLoadFileAsync<T>(x, nonException, token));

            return await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 非同期処理で複数のXMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePaths"> 読込するXMLのファイルパスコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトのコレクションを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、復元オブジェクトのdefaultを返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 読込対象を指定するコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 読込するファイルパスをnullまたは空白にする事はできません。 </exception>
        public static async Task<IEnumerable<T>> LoadFilesAsync<T>(IEnumerable<string> filePaths, bool nonException = false, CancellationToken token = default)
        {
            if (filePaths is null)
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0010);

            if (!filePaths.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0020);

            return await Task.Run(() => ExecuteLoadFiles<T>(filePaths, nonException), token);
        }

        /// <summary>
        /// 複数のXMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePaths"> 読込するXMLのファイルパスコレクションを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、復元オブジェクトのdefaultを返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 読込対象を指定するコレクションは必須です。 </exception>
        /// <exception cref="ArgumentNullException"> 読込するファイルパスをnullまたは空白にする事はできません。 </exception>
        public static IEnumerable<T> LoadFiles<T>(IEnumerable<string> filePaths, bool nonException = false)
        {
            if (filePaths is null)
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0010);

            if (!filePaths.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException(nameof(filePaths), MSG_ERR_030_0020);

            return ExecuteLoadFiles<T>(filePaths, nonException);
        }

        /// <summary>
        /// 非同期処理でXMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePath"> 読込するXMLのファイルパスを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、復元オブジェクトのdefaultを返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 読込するファイルパスをnullまたは空白にする事はできません。 </exception>
        public static async Task<T> LoadFileAsync<T>(string filePath, bool nonException = false, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0020);

            return await ExecuteLoadFileAsync<T>(filePath, nonException, token);
        }

        /// <summary> 
        /// XMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePath"> 読込するXMLのファイルパスを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトを返します。
        /// シリアライズエラーを無視する設定で例外が発生した場合は、復元オブジェクトの default を返します。 
        /// </returns>
        /// <exception cref="ArgumentNullException"> 読込するファイルパスをnullまたは空白にする事はできません。 </exception>
        public static T LoadFile<T>(string filePath, bool nonException = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), MSG_ERR_030_0020);

            return ExecuteLoadFile<T>(filePath, nonException);
        }

        /*--- Method: internal --------------------------------------------------------------------------------------------------------------------*/
        /*--- Method: private ---------------------------------------------------------------------------------------------------------------------*/

        /// <summary> 
        /// 指定パス内に存在する .xml ファイルの一覧を取得します。
        /// </summary>
        /// <param name="directoryPath"> 検索するディレクトリパスを指定します。 </param>
        /// <returns> XMLのファイル名称とファイルパスを返します。 指定パス内に .xml が存在しなかった場合は 0件コレクション を返します。 </returns>
        private static IEnumerable<FileInfo> ExecuteGetXmlList(string directoryPath)
        {
            var xmlFiles = Directory.GetFiles(directoryPath).Where(x => Path.GetExtension(x).ToLower() == "xml").ToList();

            if (!xmlFiles.Any())
                return Enumerable.Empty<FileInfo>();

            return xmlFiles.Select(x => new FileInfo(x)).ToList();
        }

        /// <summary> 
        /// 非同期処理でオブジェクトをXMLにシリアライズを実行します。  
        /// </summary>
        /// <param name="path"> 書込するXMLのファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報を返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は null を返します。 
        /// </returns>
        private static async Task<FileInfo> ExecuteSaveFileAsync(string path, object obj, bool nonException = false, CancellationToken token = default)
        {
            return await Task.Run(() => ExecuteSaveFile(path, obj, nonException), token);
        }

        /// <summary> 
        /// 複数のオブジェクトのXMLシリアライズを実行します。 
        /// </summary>
        /// <param name="fileInfos"> 書込するXMLのファイルパスとファイルを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報を返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は null を返します。 
        /// </returns>
        private static IEnumerable<FileInfo> ExecuteSaveFiles(IDictionary<string, object> fileInfos, bool nonException = false)
        {
            return fileInfos.Select(x => ExecuteSaveFile(x.Key, x.Value, nonException)).ToList();
        }

        /// <summary> 
        /// オブジェクトをXMLにシリアライズを実行します。  
        /// </summary>
        /// <param name="filePath"> 書込するXMLのファイルパスを指定します。 </param>
        /// <param name="obj"> 保存するオブジェクトを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 保存したオブジェクトのファイル情報を返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は default を返します。 
        /// </returns>
        /// <exception cref="XmlSaveException"> XMLファイル保存中の例外を返します。(詳細は <see cref="Exception.InnerException"/> を確認して下さい。) </exception>
        private static FileInfo ExecuteSaveFile(string filePath, object obj, bool nonException = false)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    new XmlSerializer(obj.GetType()).Serialize(fs, obj);

                    return new FileInfo(filePath);
                }
            }
            catch (Exception e)
            {
                if (nonException)
                    return default;

                throw new XmlSaveException(string.Format(MSG_ERR_040_0010, filePath), e);
            }
        }

        /// <summary> 
        /// 複数のXMLファイルの逆シリアライズを実行します。 
        /// </summary>
        /// <typeparam name="T"> 復元するオブジェクトの型を指定します。 </typeparam>
        /// <param name="filePaths"> 復元する .xml のパスを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 復元オブジェクトを返します。 </returns>
        private static IEnumerable<T> ExecuteLoadFiles<T>(IEnumerable<string> filePaths, bool nonException = false)
        {
            return filePaths.Select(path => ExecuteLoadFile<T>(path, nonException)).ToList();
        }

        /// <summary>
        /// 非同期処理でXMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePath"> 読込するXMLのファイルパスを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <param name="token"> (任意) 非同期処理のキャンセルトークンを指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトを返します。
        /// シリアライズエラーを無視する設定でエラーが発生した場合は、復元オブジェクトのdefaultを返します。 
        /// </returns>
        /// <exception cref="XmlLoadException"> XMLファイル復元中の例外を返します。(詳細は <see cref="Exception.InnerException"/> を確認して下さい。) </exception>
        private static async Task<T> ExecuteLoadFileAsync<T>(string filePath, bool nonException = false, CancellationToken token = default)
        {
            return await Task.Run(() => ExecuteLoadFile<T>(filePath, nonException), token);
        }

        /// <summary> 
        /// XMLファイルの逆シリアライズを実行します。
        /// </summary>
        /// <param name="filePath"> 読込するXMLのファイルパスを指定します。 </param>
        /// <param name="nonException"> (任意) シリアライズエラーを無視する場合は true を指定します。 </param>
        /// <returns> 
        /// 復元オブジェクトを返します。
        /// シリアライズエラーを無視する設定で例外が発生した場合は、復元オブジェクトの default を返します。 
        /// </returns>
        /// <exception cref="XmlLoadException"> XMLファイル復元中の例外を返します。(詳細は <see cref="Exception.InnerException"/> を確認して下さい。) </exception>
        private static T ExecuteLoadFile<T>(string filePath, bool nonException = false)
        {
            if (!File.Exists(filePath))
            {
                if (nonException)
                    return default;

                throw new XmlLoadException(MSG_ERR_050_0010, new FileNotFoundException(MSG_ERR_050_0020, filePath));
            }

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                if (nonException)
                    return default;

                throw new XmlLoadException(MSG_ERR_050_0010, e);
            }
        }
    }
}
