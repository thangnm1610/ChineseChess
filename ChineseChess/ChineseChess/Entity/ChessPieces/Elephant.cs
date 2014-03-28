using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    public class Elephant : ChessPiece
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(2, 2) : new Point(2, 0);
            base.Draw(spriteBatch, p);
        }

        public Elephant(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.Elephant;
            _value = (int)EValue.Elephant;
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();

            if (_point.Y == 2 || _point.Y == 7)
            {
                if (_point.X != 8)
                {
                    if (board.CheckPoint(_point.X + 1, _point.Y + 1) == 0 && (board.CheckPoint(_point.X + 2, _point.Y + 2) * _id) <= 0)
                        list.Add(new Point(_point.X + 2, _point.Y + 2));
                    if (board.MatrixPosition[_point.X + 1, _point.Y - 1] == 0 && (board.MatrixPosition[_point.X + 2, _point.Y - 2] * _id) <= 0)
                        list.Add(new Point(_point.X + 2, _point.Y - 2));
                }

                if (_point.X != 0)
                {
                    if (board.MatrixPosition[_point.X - 1, _point.Y + 1] == 0 && (board.MatrixPosition[_point.X - 2, _point.Y + 2]*_id) <= 0)
                        list.Add(new Point(_point.X - 2, _point.Y + 2));
                    if (board.MatrixPosition[_point.X - 1, _point.Y - 1] == 0 && (board.MatrixPosition[_point.X - 2, _point.Y - 2] * _id) <= 0)
                        list.Add(new Point(_point.X - 2, _point.Y - 2));
                }
            }
            else if (_point.Y == 0 || _point.Y == 5)
            {
                if (board.MatrixPosition[_point.X - 1, _point.Y + 1] == 0 && (board.MatrixPosition[_point.X - 2, _point.Y + 2] * _id) <= 0)
                    list.Add(new Point(_point.X - 2, _point.Y + 2));
                if (board.MatrixPosition[_point.X + 1, _point.Y + 1] == 0 && (board.MatrixPosition[_point.X + 2, _point.Y + 2] * _id) <= 0)
                    list.Add(new Point(_point.X + 2, _point.Y + 2));
            }
            else if (_point.Y == 4 || _point.Y == 9)
            {
                if (board.MatrixPosition[_point.X + 1, _point.Y - 1] == 0 && (board.MatrixPosition[_point.X + 2, _point.Y - 2] * _id) <= 0)
                    list.Add(new Point(_point.X + 2, _point.Y - 2));
                if (board.MatrixPosition[_point.X - 1, _point.Y - 1] == 0 && (board.MatrixPosition[_point.X - 2, _point.Y - 2] * _id) <= 0)
                    list.Add(new Point(_point.X - 2, _point.Y - 2));
            }
            return list;
        }

    }
}
