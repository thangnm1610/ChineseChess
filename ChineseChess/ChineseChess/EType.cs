using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Kiểu quân cờ
    /// </summary>
    /// 
    public enum EType : byte
    {
        
        /// <summary>
        /// Tướng
        /// </summary>
        King,

        /// <summary>
        /// Sĩ
        /// </summary>
        Guard,

        /// <summary>
        /// Tượng
        /// </summary>
        Elephant,

        /// <summary>
        /// Xe
        /// </summary>
        Rook,

        /// <summary>
        /// Pháo
        /// </summary>
        Artillery,

        /// <summary>
        /// Mã
        /// </summary>
        Horse,

        /// <summary>
        /// Tốt
        /// </summary>
        Pawn
    }
}
