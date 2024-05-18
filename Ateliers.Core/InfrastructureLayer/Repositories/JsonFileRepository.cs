using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliers.Core.InfrastructureLayer
{
    /// <summary>
    /// JSONファイルリポジトリ
    /// </summary>
    public class JsonFileRepository : IFileRepository, IRepository
    {
        /*--- * structers -------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataDictionaryPath"> JSONデータディレクトリパスを指定します。 </param>
        /// <param name="createIfNotExists"> (任意) データディレクトリが存在しない場合に作成するかどうかを指定します。 </param>
        /// <exception cref="ArgumentException"> データディレクトリパスが指定されていない場合にスローされます。 </exception>
        /// <exception cref="DirectoryNotFoundException"> 指定されたデータディレクトリが存在しない場合にスローされます。 </exception>
        public JsonFileRepository(string dataDictionaryPath, bool createIfNotExists = false)
        {
            if (string.IsNullOrWhiteSpace(dataDictionaryPath))
            {
                throw new ArgumentException("データディレクトリパスが指定されていません。", nameof(dataDictionaryPath));
            }

            if (!Directory.Exists(dataDictionaryPath))
            {
                if (createIfNotExists)
                {
                    Directory.CreateDirectory(dataDictionaryPath);
                }
                else
                {
                    throw new DirectoryNotFoundException("指定されたデータディレクトリが存在しません。");
                }
            }

            DataDictionary = new DirectoryInfo(dataDictionaryPath);
        }

        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <inheritdoc/>
        public DirectoryInfo DataDictionary { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティをファイル名検索し、条件に一致する1件のエンティティを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="fileName"> 取得するエンティティのファイル名を指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 検索結果となるエンティティを返します。 </returns>
        /// <exception cref="ArgumentException"> ファイル名が指定されていない場合にスローされます。 </exception>
        /// <exception cref="FileNotFoundException"> 指定されたファイルが存在しない場合にスローされます。 </exception>
        /// <remarks>
        /// Get と Find の違いについて: <br/>
        /// この <see cref="GetByFileNameAsync{TEntity}(string, CancellationToken)"/> メソッドは、ファイルが存在しない場合は例外エラーとなります。<br/>
        /// 例外エラーとしたくない場合は <see cref="FindByFileNameAsync{TEntity}(string, CancellationToken)"/> メソッドを使用してください。
        /// </remarks>
        public async Task<TEntity> GetByFileNameAsync<TEntity>(string fileName, CancellationToken token = default) where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("ファイル名が指定されていません。", nameof(fileName));
            }

            if (!File.Exists(Path.Combine(DataDictionary.FullName, fileName)))
            {
                throw new FileNotFoundException("指定されたファイルが存在しません。", fileName);
            }

            using (var stream = new FileStream(Path.Combine(DataDictionary.FullName, fileName), FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();
                return JsonSerializer.Deserialize<TEntity>(json);
            }
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティをファイル名検索し、条件に一致する1件のエンティティを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="fileName"> 取得するエンティティのファイル名を指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 
        /// 検索結果となるエンティティを返します。 <br/>
        /// 対象のファイルが存在しない場合は、<see langword="default"/> を返します。
        /// </returns>
        /// <exception cref="ArgumentException"> ファイル名が指定されていない場合にスローされます。 </exception>
        /// <remarks>
        /// Find と Get の違いについて: <br/>
        /// この <see cref="FindByFileNameAsync{TEntity}(string, CancellationToken)"/> メソッドは、ファイルが存在しない場合は <see langword="default"/> を返します。<br/>
        /// 例外エラーとしたい場合は <see cref="GetByFileNameAsync{TEntity}(string, CancellationToken)"/> メソッドを使用してください。
        /// </remarks>
        public async Task<TEntity> FindByFileNameAsync<TEntity>(string fileName, CancellationToken token = default) where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("ファイル名が指定されていません。", nameof(fileName));
            }

            var file = DataDictionary.GetFiles(fileName).FirstOrDefault();
            if (file is null)
            {
                return default;
            }

            using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();
                return JsonSerializer.Deserialize<TEntity>(json);
            }
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下の全てのエンティティを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 取得したエンティティコレクションを返します。 </returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken token = default) where TEntity : class
        {
            var files = DataDictionary.GetFiles("*.json");
            var allEntities = new List<TEntity>();

            foreach (var file in files)
            {
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var entities = JsonSerializer.Deserialize<List<TEntity>>(json);
                    allEntities.AddRange(entities);
                }
            }

            return allEntities;
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを条件検索し、条件に一致する1件のエンティティを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="findExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns>
        /// 検索結果となるエンティティコレクションを返します。 <br/>
        /// 一致するエンティティが存在しない場合は <see langword="default"/> を返します。
        /// </returns>
        /// <remarks>
        /// 【注意！】<br/>
        /// このメソッドは、<see cref="DataDictionary"/> 配下にある全てのJSONファイルを読み込んで検索を行います。<br/>
        /// パフォーマンスに重大な影響が出る可能性があるため、積極的な使用は非推奨です。<br/>
        /// </remarks>
        public async Task<TEntity> FindByExpressionAsync<TEntity>(Expression<Func<TEntity, bool>> findExpression, CancellationToken token = default) where TEntity : class
        {
            var files = DataDictionary.GetFiles("*.json");
            var allEntities = new List<TEntity>();

            foreach (var file in files)
            {
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var entities = JsonSerializer.Deserialize<List<TEntity>>(json);
                    allEntities.AddRange(entities);
                }
            }

            return allEntities.AsQueryable().FirstOrDefault(findExpression.Compile());
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを条件検索し、条件に一致するエンティティコレクションを取得します。
        /// </summary>
        /// <typeparam name="TEntity"> 取得するエンティティの型を指定します。 </typeparam>
        /// <param name="searchExpression"> エンティティの検索条件を指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns>
        /// 検索結果となるエンティティコレクションを返します。 <br/>
        /// 一致するエンティティが存在しない場合は、空のコレクションを返します。
        /// </returns>
        /// <remarks>
        /// 【注意！】<br/>
        /// このメソッドは、<see cref="DataDictionary"/> 配下にある全てのJSONファイルを読み込んで検索を行います。<br/>
        /// パフォーマンスに重大な影響が出る可能性があるため、積極的な使用は非推奨です。<br/>
        /// </remarks>
        public async Task<IEnumerable<TEntity>> SearchByExpression<TEntity>(Expression<Func<TEntity, bool>> searchExpression, CancellationToken token = default) where TEntity : class
        {
            var files = DataDictionary.GetFiles("*.json");
            var allEntities = new List<TEntity>();

            foreach (var file in files)
            {
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var entities = JsonSerializer.Deserialize<List<TEntity>>(json);
                    allEntities.AddRange(entities);
                }
            }

            return allEntities.AsQueryable().Where(searchExpression.Compile()).ToList();
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下に複数のエンティティを保存します。
        /// </summary>
        /// <typeparam name="TEntity"> 保存するエンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 保存するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 保存したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティコレクションが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていないものが含まれている場合にスローされます。 </exception>
        /// <remarks> 保存対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> InsertAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entitys is null)
            {
                throw new ArgumentNullException(nameof(entitys));
            }

            if (!entitys.Any())
            {
                return Task.FromResult(0);
            }

            if (entitys.Any(x => string.IsNullOrWhiteSpace(x.ObjectKey)))
            {
                throw new ArgumentException("エンティティのキーが指定されていないものが含まれています。", nameof(entitys));
            }

            var count = 0;
            foreach (var entity in entitys)
            {
                var json = JsonSerializer.Serialize(entity);
                var fileName = entity.ObjectKey;
                if (!fileName.EndsWith(".json"))
                {
                    fileName += ".json";
                }

                JsonFileService.SaveFile(Path.Combine(DataDictionary.FullName, fileName), json);
                count++;
            }

            return Task.FromResult(count);
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下にエンティティを保存します。
        /// </summary>
        /// <typeparam name="TEntity"> 保存するエンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 保存するエンティティを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 保存したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていない場合にスローされます。 </exception>
        /// <remarks> 保存対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> InsertAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(entity.ObjectKey))
            {
                throw new ArgumentException("エンティティのキーが指定されていません。", nameof(entity));
            }

            var json = JsonSerializer.Serialize(entity);
            var fileName = entity.ObjectKey;
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }

            JsonFileService.SaveFile(Path.Combine(DataDictionary.FullName, fileName), json);

            return Task.FromResult(1);
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを更新します。
        /// </summary>
        /// <typeparam name="TEntity"> 更新するエンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 更新するエンティティを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 更新したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていない場合にスローされます。 </exception>
        /// <remarks> 更新対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> UpdateAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(entity.ObjectKey))
            {
                throw new ArgumentException("エンティティのキーが指定されていません。", nameof(entity));
            }

            var json = JsonSerializer.Serialize(entity);
            var fileName = entity.ObjectKey;
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }

            JsonFileService.SaveFile(Path.Combine(DataDictionary.FullName, fileName), json);

            return Task.FromResult(1);
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを更新します。
        /// </summary>
        /// <typeparam name="TEntity"> 更新するエンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 更新するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 更新したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティコレクションが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていないものが含まれている場合にスローされます。 </exception>
        /// <remarks> 更新対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entitys is null)
            {
                throw new ArgumentNullException(nameof(entitys));
            }

            if (!entitys.Any())
            {
                return Task.FromResult(0);
            }

            if (entitys.Any(x => string.IsNullOrWhiteSpace(x.ObjectKey)))
            {
                throw new ArgumentException("エンティティのキーが指定されていないものが含まれています。", nameof(entitys));
            }

            var count = 0;
            foreach (var entity in entitys)
            {
                var json = JsonSerializer.Serialize(entity);
                var fileName = entity.ObjectKey;
                if (!fileName.EndsWith(".json"))
                {
                    fileName += ".json";
                }

                JsonFileService.SaveFile(Path.Combine(DataDictionary.FullName, fileName), json);
                count++;
            }

            return Task.FromResult(count);
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを削除します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entity"> 削除するエンティティを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 削除したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていない場合にスローされます。 </exception>
        /// <remarks> 削除対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> DeleteAsync<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(entity.ObjectKey))
            {
                throw new ArgumentException("エンティティのキーが指定されていません。", nameof(entity));
            }

            var fileName = entity.ObjectKey;
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }

            var file = new FileInfo(Path.Combine(DataDictionary.FullName, fileName));
            if (file.Exists)
            {
                file.Delete();
                return Task.FromResult(1);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// <see cref="DataDictionary"/> 配下のエンティティを削除します。
        /// </summary>
        /// <typeparam name="TEntity"> 削除するエンティティの型を指定します。 </typeparam>
        /// <param name="entitys"> 削除するエンティティコレクションを指定します。 </param>
        /// <param name="token"> 未使用、指定不要 </param>
        /// <returns> 削除したエンティティの数を返します。 </returns>
        /// <exception cref="ArgumentNullException"> エンティティコレクションが指定されていない場合にスローされます。 </exception>
        /// <exception cref="ArgumentException"> エンティティのキーが指定されていないものが含まれている場合にスローされます。 </exception>
        /// <remarks> 削除対象のファイル名は <see cref="IObjectKey.ObjectKey"/> の名前になります。 </remarks>
        public Task<int> DeleteAsync<TEntity>(IEnumerable<TEntity> entitys, CancellationToken token = default)
            where TEntity : class, IObjectKey
        {
            if (entitys is null)
            {
                throw new ArgumentNullException(nameof(entitys));
            }

            if (!entitys.Any())
            {
                return Task.FromResult(0);
            }

            if (entitys.Any(x => string.IsNullOrWhiteSpace(x.ObjectKey)))
            {
                throw new ArgumentException("エンティティのキーが指定されていないものが含まれています。", nameof(entitys));
            }

            var count = 0;
            foreach (var entity in entitys)
            {
                var fileName = entity.ObjectKey;
                if (!fileName.EndsWith(".json"))
                {
                    fileName += ".json";
                }

                var file = new FileInfo(Path.Combine(DataDictionary.FullName, fileName));
                if (file.Exists)
                {
                    file.Delete();
                    count++;
                }
            }

            return Task.FromResult(count);
        }
    }
}
