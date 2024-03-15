using Ateliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface IEntity<T>
        where T : class
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        IRepository<T> Repository { get; }

        /// <summary>
        /// このエンティティのキーオブジェクトを取得します。
        /// </summary>
        IEnumerable<object> KeyObjects { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

        void SetRepository(IRepository<T> repository);
    }

    public interface IEntity
    {
        IRepository Repository { get; }

        /// <summary>
        /// このエンティティのキーオブジェクトを取得します。
        /// </summary>
        IEnumerable<object> KeyObjects { get; }

        void SetRepository(IRepository repository);
    }
}
