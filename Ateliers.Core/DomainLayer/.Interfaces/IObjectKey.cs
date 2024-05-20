using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// [IF] オブジェクトキー
    /// </summary>
    /// <remarks>
    /// オブジェクトの一意性を示すキーを表します。
    /// </remarks>
    public interface IObjectKey
    {
        /*--- Property/Field Definitions ----------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// オブジェクトの一意性を示すキーを取得します。
        /// </summary>
        string ObjectKey { get; }

        /*--- Method: public ----------------------------------------------------------------------------------------------------------------------*/

    }
}
