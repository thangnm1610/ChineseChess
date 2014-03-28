using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Quân Tướng
    /// </summary>
    public class King : ChessPiece
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(0, 2) : new Point(0, 0);
            base.Draw(spriteBatch, p);
        }

        public King(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.King;
            _value = (int)EValue.King;
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();

            if (_id > 0)
            {
                //có thêm 1 nước đi bên phải nếu x < 5
                if (_point.X < 5)
                {
                    if (board.CheckPoint(_point.X + 1, _point.Y) <= 0) list.Add(new Point(_point.X + 1, _point.Y));
                }

                //có thêm 1 nước đi bên trái nếu x > 3
                if (_point.X > 3)
                {
                    if (board.CheckPoint(_point.X - 1, _point.Y) <= 0)
                        list.Add(new Point(_point.X - 1, _point.Y));
                }

                if (_point.Y > 0)
                {
                    if (board.CheckPoint(_point.X, _point.Y - 1) <= 0)
                        list.Add(new Point(_point.X, _point.Y - 1));
                }

                if (_point.Y < 2)
                {
                    if (board.CheckPoint(_point.X, _point.Y + 1) <= 0)
                        list.Add(new Point(_point.X, _point.Y + 1));
                }
            }
            else
            {
                //có thêm 1 nước đi bên phải nếu x < 5
                if (_point.X < 5)
                {
                    if (board.CheckPoint(_point.X + 1, _point.Y) >= 0) list.Add(new Point(_point.X + 1, _point.Y));
                }

                //có thêm 1 nước đi bên trái nếu x > 3
                if (_point.X > 3)
                {
                    if (board.CheckPoint(_point.X - 1, _point.Y) >= 0)
                        list.Add(new Point(_point.X - 1, _point.Y));
                }

                //nước lùi
                if (_point.Y > 0 || _point.Y > 7 )
                {
                    if (board.CheckPoint(_point.X, _point.Y - 1) >= 0)
                        list.Add(new Point(_point.X, _point.Y - 1));
                }

                //nước tiến
                if (_point.Y < 2 || _point.Y < 9)
                {
                    if (board.CheckPoint(_point.X, _point.Y + 1) >= 0)
                        list.Add(new Point(_point.X, _point.Y + 1));
                }
            }
            return list;
        }
    }
}
