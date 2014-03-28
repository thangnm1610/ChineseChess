using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ChineseChess
{
    /// <summary>
    /// nước đi
    /// </summary>
    public class Move
    {
        /// <summary>
        /// mã quân cờ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// vị trí cũ
        /// </summary>
        public Point OldPoint { get; set; }

        /// <summary>
        /// Giá trị của điểm đích trong ma trận tọa độ
        /// </summary>
        /// <remarks>là Id của quân cờ nếu tồn tại, hoặc 0 nếu ko có quân nào</remarks>
        public int ValueOfDestinationPoint { get; set; }

        public Move(int id, Point old, int value)
        {
            Id = id;
            OldPoint = old;
            ValueOfDestinationPoint = value;
        }   
    }
}
