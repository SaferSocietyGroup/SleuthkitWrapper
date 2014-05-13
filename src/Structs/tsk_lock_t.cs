using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// Dummy struct as a placeholder for tsk_lock_t struct in TSK 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct tsk_lock_t
    {
        /// <summary>
        /// The placeholder1.
        /// </summary>
        internal int placeholder1;

        /// <summary>
        /// The placeholder2
        /// </summary>
        internal int placeholder2;

        /// <summary>
        /// The placeholder3
        /// </summary>
        internal int placeholder3;

        /// <summary>
        /// The placeholder4
        /// </summary>
        internal int placeholder4;

        /// <summary>
        /// The placeholder5
        /// </summary>
        internal int placeholder5;

        /// <summary>
        /// The placeholder6
        /// </summary>
        internal int placeholder6;
    }
}
