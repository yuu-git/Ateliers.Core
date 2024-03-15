using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ateliers
{
    /// <summary> 
    /// ソート方向 
    /// </summary>
    /// <remarks>
    /// <para> 概要: ソート処理を実行する際に、昇順降順を指定する。 </para>
    /// </remarks>
    public enum SortDirection
    {
        /// <summary> 昇順 </summary>
        Ascending,
        /// <summary> 降順 </summary>
        Descending,
    }
}
