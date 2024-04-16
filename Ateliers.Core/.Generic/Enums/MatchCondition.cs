using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 一致条件
    /// </summary>
    /// <remarks>
    /// <para> 概要: 主に文字列検査時に使用する。 </para>
    /// <para> 『部分一致または後方一致』のような複数条件の場合、フラグとして『2 + 8 = 10』として扱う。 </para>
    /// </remarks>
    [Flags]
    public enum MatchCondition : int
    {
        /// <summary> 完全一致を条件とします。 </summary>
        PerfectMatch = 1,
        /// <summary> 部分一致を条件とします。 </summary>
        PartialMatch = 2,
        /// <summary> 前方一致を条件とします。 </summary>
        ForwardMatch = 4,
        /// <summary> 後方一致を条件とします。 </summary>
        BackwardMatch = 8,
    }
}
