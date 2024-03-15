using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// xx月フラグ
    /// </summary>
    /// <remarks>
    /// <para> 概要: 1月~12月をフラグとして扱う。<see cref="Enum.HasFlag"/>と合わせて使用することで日付判定を行う。 </para>
    /// <para> メリットとしては『1月と6月に特定処理をする』の場合、データとして『1 + 32 =33』のint値で簡単に保持することができる。 </para>
    /// <para> ※ .NET Core 2.1 より以前は <see cref="Enum.HasFlag"/> の処理が重いため注意 </para>
    /// </remarks>
    [Flags]
    public enum MonthFlags
    {
        /// <summary> 1月(January) </summary>
        Jan = 1,
        /// <summary> 2月(February) </summary>
        Feb = 2,
        /// <summary> 3月(March) </summary>
        Mar = 4,
        /// <summary> 4月(April) </summary>
        Apr = 8,
        /// <summary> 5月(May) </summary>
        May = 16,
        /// <summary> 6月(June) </summary>
        Jun = 32,
        /// <summary> 7月(July) </summary>
        Jul = 64,
        /// <summary> 8月(August) </summary>
        Aug = 128,
        /// <summary> 9月(September) </summary>
        Sep = 256,
        /// <summary> 10月(October) </summary>
        Oct = 512,
        /// <summary> 11月(November) </summary>
        Nov = 1024,
        /// <summary> 12月(December) </summary>
        Dec = 2048,
    }
}
