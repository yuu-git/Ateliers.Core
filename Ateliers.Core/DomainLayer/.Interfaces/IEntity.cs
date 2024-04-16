using Ateliers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    public interface IEntity
    {
        /// <summary>
        /// このエンティティのキーオブジェクトを取得します。
        /// </summary>
        IEnumerable<object> KeyObjects { get; }
    }
}
