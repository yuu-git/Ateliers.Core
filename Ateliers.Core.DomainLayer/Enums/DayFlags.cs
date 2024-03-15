using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliers
{
    /// <summary>
    /// 日付フラグ
    /// </summary>
    /// <remarks>
    /// <para> 概要: 1日~31日をフラグとして扱う。<see cref="Enum.HasFlag"/>と合わせて使用することで日付判定を行う。 </para>
    /// <para> メリットとしては『1日と7日に特定処理をする』の場合、データとして『1 + 64 = 65』のint値で簡単に保持することができる。 </para>
    /// <para> ※ .NET Core 2.1 より以前は <see cref="Enum.HasFlag"/> の処理が重いため注意 </para>
    /// </remarks>
    [Flags]
    public enum DayFlags : int
    {
        /// <summary>  1日 </summary>
        First = 1,
        /// <summary>  2日 </summary>
        Second = 2,
        /// <summary>  3日 </summary>
        Third = 4,
        /// <summary>  4日 </summary>
        Fourth = 8,
        /// <summary>  5日 </summary>
        Fifth = 16,
        /// <summary>  6日 </summary>
        Sixth = 32,
        /// <summary>  7日 </summary>
        Seventh = 64,
        /// <summary>  8日 </summary>
        Eighth = 128,
        /// <summary>  9日 </summary>
        Ninth = 256,
        /// <summary> 10日 </summary>
        Tenth = 512,
        /// <summary> 11日 </summary>
        Eleventh = 1024,
        /// <summary> 12日 </summary>
        Twelfth = 2048,
        /// <summary> 13日 </summary>
        Thirteenth = 4096,
        /// <summary> 14日 </summary>
        Fourteenth = 8192,
        /// <summary> 15日 </summary>
        Fifteenth = 16384,
        /// <summary> 16日 </summary>
        Sixteenth = 32768,
        /// <summary> 17日 </summary>
        Seventeenth = 65536,
        /// <summary> 18日 </summary>
        Eighteenth = 131072,
        /// <summary> 19日 </summary>
        Nineteenth = 262144,
        /// <summary> 20日 </summary>
        Twentieth = 524288,
        /// <summary> 21日 </summary>
        TwentyFirst = 1048576,
        /// <summary> 22日 </summary>
        TwentySecond = 2097152,
        /// <summary> 23日 </summary>
        TwentyThird = 4194304,
        /// <summary> 24日 </summary>
        TwentyFourth = 8388608,
        /// <summary> 25日 </summary>
        TwentyFifth = 16777216,
        /// <summary> 26日 </summary>
        TwentySixth = 33554432,
        /// <summary> 27日 </summary>
        TwentySeventh = 67108864,
        /// <summary> 28日 </summary>
        TwentyEighth = 134217728,
        /// <summary> 29日 </summary>
        TwentyNinth = 268435456,
        /// <summary> 30日 </summary>
        Thirtieth = 536870912,
        /// <summary> 31日 </summary>
        ThirtyFirst = 1073741824,
    }
}
