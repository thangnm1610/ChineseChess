using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Quân Sĩ
    /// </summary>
    public class Guard : ChessPiece
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(1, 2) : new Point(1, 0);
            base.Draw(spriteBatch, p);
        }

        public Guard(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.Guard;
            _value = (int)EValue.Guard;
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();

            //nước lên bên phải
            if (_point == new Point(3, 0) || _point == new Point(3, 7) || _point.X == 4)
            {
                if (board.CheckPoint(_point.X + 1, _point.Y + 1) * _id <= 0) list.Add(new Point(_point.X + 1, _point.Y + 1));
            }

            //nước lên bên trái
            if (_point == new Point(5, 0) || _point.X == 4 || _point == new Point(5, 7))
            {
                if (board.CheckPoint(_point.X - 1, _point.Y + 1) * _id <= 0) list.Add(new Point(_point.X - 1, _point.Y + 1));
            }

            //nước xuống bên phải
            if (_point.X == 4 || _point == new Point(3, 2) || _point == new Point(3, 9))
            {
                if (board.CheckPoint(_point.X + 1, _point.Y - 1) * _id <= 0) list.Add(new Point(_point.X + 1, _point.Y - 1));
            }

            //nước xuống bên trái
            if (_point.X == 4 || _point == new Point(5, 2) || _point == new Point(5, 9))
            {
                if (board.CheckPoint(_point.X - 1, _point.Y - 1) * _id <= 0) list.Add(new Point(_point.X - 1, _point.Y - 1));
            }

            return list;
        }
    }
}
