using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    public enum EValue : int
    {
        /// <summary>
        /// Tốt khi chưa qua sông
        /// </summary>
        Pawn = 30,

        /// <summary>
        /// Tốt sang sông
        /// </summary>
        PawnCrossRiver = 40,

        /// <summary>
        /// Tốt đầu
        /// </summary>
        HeadPawn = 45,

        /// <summary>
        /// Tốt sang sông ở vị trí biên
        /// </summary>
        PawnBoundaryCrossRiver = 35,

        /// <summary>
        /// Hai tốt cặp kè nhau khi sang sông
        /// </summary>
        DoublePawnCrossRiver = 60,
        /// <summary>
        /// Sĩ
        /// </summary>
        Guard = 120,

        /// <summary>
        /// Tượng
        /// </summary>
        Elephant = 125,

        /// <summary>
        /// Mã
        /// </summary>
        Horse = 270,
        
        /// <summary>
        /// Pháo
        /// </summary>
        Artillery = 285,

        /// <summary>
        /// Pháo đầu
        /// </summary>
        ArtilleryFirst = 300,

        /// <summary>
        /// Xe
        /// </summary>
        Rook = 600,

        /// <summary>
        /// Tướng
        /// </summary>
        King = 6000,

    }
}
