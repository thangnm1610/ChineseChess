using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Quân Xe
    /// </summary>
    public class Rook : ChessPiece 
    {
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(0, 3) : new Point(0, 1);
            base.Draw(spriteBatch, p);
        }

        public Rook(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.Rook;
            _value = (int)EValue.Rook;
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();

            int x = _point.X;
            int y = _point.Y;


            int i = x - 1;
            int j = y - 1;

            //tim ve phia duoi
            while (true)
            {
                if (j < 0 || j > 9) break;

                if (board.MatrixPosition[x, j] == 0)
                {
                    list.Add(new Point(x, j));
                }
                else if (board.GetPoint(x, j) * _id > 0) break;
                else
                {
                    list.Add(new Point(x, j));
                    break;
                }

                j--;
            }

            //tim phia tren
            j = y + 1;
            while (true)
            {
                if (j < 0 || j > 9) break;

                if (board.MatrixPosition[x, j] == 0)
                {
                    list.Add(new Point(x, j));
                }
                else if (board.GetPoint(x, j) * _id > 0) break;
                else
                {
                    list.Add(new Point(x, j));
                    break;
                }
                j++;
            }

            //tim cho trong ben trai quan xe
            while (true)
            {
                if (i < 0 || i > 8) break;

                if (board.MatrixPosition[i, y] == 0)
                {
                    list.Add(new Point(i, y));
                }
                else if (board.GetPoint(i, y) * _id > 0) break;
                else
                {
                    list.Add(new Point(i, y));
                    break;
                }

                i--;
            }

            //tim ve ben phai quan xe
            i = x + 1;
            while (true)
            {
                if (i < 0 || i > 8) break;

                if (board.MatrixPosition[i, y] == 0)
                {
                    list.Add(new Point(i, y));
                }
                else if (board.GetPoint(i, y) * _id > 0) break;
                else
                {
                    list.Add(new Point(i, y));
                    break;
                }

                i++;
            }

            return list;
        }
    }
}
