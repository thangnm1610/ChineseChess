using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Quân Mã
    /// </summary>
    public class Horse : ChessPiece
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(1, 3) : new Point(1, 1);
            base.Draw(spriteBatch, p);
        }

        public Horse(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.Horse;
            _value = (int)EValue.Horse;
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();

            //quân có id dương
            if (_id > 0)
            {
                //nước tiến 2
                if (_point.Y <= 7)
                {
                    if (_point.X > 0)
                    {
                        if (board.MatrixPosition[_point.X - 1, _point.Y + 2] <= 0 && board.MatrixPosition[_point.X, _point.Y + 1] == 0)
                            list.Add(new Point(_point.X - 1, _point.Y + 2));
                    }

                    if (_point.X < 8)
                    {
                        if (board.MatrixPosition[_point.X + 1, _point.Y + 2] <= 0 && board.MatrixPosition[_point.X, _point.Y + 1] == 0)
                            list.Add(new Point(_point.X + 1, _point.Y + 2));
                    }
                }

                //nước tiến 1
                if (_point.Y <= 8)
                {
                    if (_point.X > 1)
                    {
                        if (board.MatrixPosition[_point.X - 2, _point.Y + 1] <= 0 && board.MatrixPosition[_point.X - 1, _point.Y] == 0)
                            list.Add(new Point(_point.X - 2, _point.Y + 1));
                    }

                    if (_point.X < 7)
                    {
                        if (board.MatrixPosition[_point.X + 2, _point.Y + 1] <= 0 && board.MatrixPosition[_point.X + 1, _point.Y] == 0)
                            list.Add(new Point(_point.X + 2, _point.Y + 1));
                    }
                }

                //nước lùi 1
                if (_point.Y >= 1)
                {
                    if (_point.X > 1)
                    {
                        if (board.MatrixPosition[_point.X - 2, _point.Y - 1] <= 0 && board.MatrixPosition[_point.X - 1, _point.Y] == 0)
                            list.Add(new Point(_point.X - 2, _point.Y - 1));
                    }

                    if (_point.X < 7)
                    {
                        if (board.MatrixPosition[_point.X + 2, _point.Y - 1] <= 0 && board.MatrixPosition[_point.X + 1, _point.Y] == 0)
                            list.Add(new Point(_point.X + 2, _point.Y - 1));
                    }
                }

                //nước lùi 2
                if (_point.Y >= 2)
                {
                    if (_point.X > 0)
                    {
                        if (board.MatrixPosition[_point.X - 1, _point.Y - 2] <= 0 && board.MatrixPosition[_point.X, _point.Y - 1] == 0)
                            list.Add(new Point(_point.X - 1, _point.Y - 2));
                    }

                    if (_point.X < 8)
                    {
                        if (board.MatrixPosition[_point.X + 1, _point.Y - 2] <= 0 && board.MatrixPosition[_point.X, _point.Y - 1] == 0)
                            list.Add(new Point(_point.X + 1, _point.Y - 2));
                    }
                }
                
            }
            //tương tự
            else
            {
                if (_point.Y <= 7)
                {
                    if (_point.X > 0)
                    {
                        if (board.CheckPoint(_point.X - 1, _point.Y + 2) >= 0 && board.CheckPoint(_point.X, _point.Y + 1) == 0)
                            list.Add(new Point(_point.X - 1, _point.Y + 2));
                    }

                    if (_point.X < 8)
                    {
                        if (board.CheckPoint(_point.X + 1, _point.Y + 2) >= 0 && board.CheckPoint(_point.X, _point.Y + 1) == 0)
                            list.Add(new Point(_point.X + 1, _point.Y + 2));
                    }
                }

                if (_point.Y <= 8)
                {
                    if (_point.X > 1)
                    {
                        if (board.CheckPoint(_point.X - 2, _point.Y + 1) >= 0 && board.CheckPoint(_point.X - 1, _point.Y) == 0)
                            list.Add(new Point(_point.X - 2, _point.Y + 1));
                    }

                    if (_point.X < 7)
                    {
                        if (board.CheckPoint(_point.X + 2, _point.Y + 1) >= 0 && board.CheckPoint(_point.X + 1, _point.Y) == 0)
                            list.Add(new Point(_point.X + 2, _point.Y + 1));
                    }
                }

                if (_point.Y >= 1)
                {
                    if (_point.X > 1)
                    {
                        if (board.CheckPoint(_point.X - 2, _point.Y - 1) >= 0 && board.CheckPoint(_point.X - 1, _point.Y) == 0)
                            list.Add(new Point(_point.X - 2, _point.Y - 1));
                    }

                    if (_point.X <= 6)
                    {
                        if (board.CheckPoint(_point.X + 2, _point.Y - 1) >= 0 && board.CheckPoint(_point.X + 1, _point.Y) == 0)
                            list.Add(new Point(_point.X + 2, _point.Y - 1));
                    }
                }

                if (_point.Y >= 2)
                {
                    if (_point.X > 0)
                    {
                        if (board.CheckPoint(_point.X - 1, _point.Y - 2) >= 0 && board.CheckPoint(_point.X, _point.Y - 1) == 0)
                            list.Add(new Point(_point.X - 1, _point.Y - 2));
                    }

                    if (_point.X < 8)
                    {
                        if (board.CheckPoint(_point.X + 1, _point.Y - 2) >= 0 && board.CheckPoint(_point.X, _point.Y - 1) == 0)
                            list.Add(new Point(_point.X + 1, _point.Y - 2));
                    }
                }
            }
            return list;
        }
    }
}
