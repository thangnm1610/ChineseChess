using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Quân Pháo
    /// </summary>
    public class Artillery : ChessPiece
    {
        public Artillery(int id, EColor eColor, Point vector2)
            : base(id, eColor, vector2)
        {
            _type = EType.Artillery;
            _value = (int)EValue.Artillery;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(2, 3) : new Point(2, 1);
            base.Draw(spriteBatch, p);
        }

        public override List<Point> GetNextMoves(ChessBoard board)
        {
            var list = new List<Point>();
            int i, j;
            int x = _point.X;
            int y = _point.Y;

            if (_id > 0)
            {
                i = x - 1;
                //tìm về phía trái quân cờ
                while (true)
                {
                    if (i < 0 || i > 8) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[i, y] == 0)
                    {
                        list.Add(new Point(i, y));
                    }
                    //nếu có quân cờ, tìm tiếp về bên trái xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i - 1;
                        while (true)
                        {
                            if (j < 0) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[j, y] > 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[j, y] < 0)
                            {
                                list.Add(new Point(j, y));
                                break;
                            }
                            j--;
                        }
                        break;
                    }
                    i--;
                }

                i = x + 1;
                //tìm về phía phải quân cờ
                while (true)
                {
                    if (i < 0 || i > 8) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[i, y] == 0)
                    {
                        list.Add(new Point(i, y));
                    }
                    //nếu có quân cờ, tìm tiếp về bên phải xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i + 1;
                        while (true)
                        {
                            if (j < 0 || j > 8) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[j, y] > 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[j, y] < 0)
                            {
                                list.Add(new Point(j, y));
                                break;
                            }
                            j++;
                        }
                        break;
                    }
                    i++;
                }

                i = y - 1;
                //tìm về phía trên quân cờ
                while (true)
                {
                    if (i < 0 || i > 9) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[x, i] == 0)
                    {
                        list.Add(new Point(x, i));
                    }
                    //nếu có quân cờ, tìm tiếp về bên trên xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i - 1;
                        while (true)
                        {
                            if (j < 0) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[x, j] > 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[x, j] < 0)
                            {
                                list.Add(new Point(x, j));
                                break;
                            }
                            j--;
                        }
                        break;
                    }
                    i--;
                }

                i = y + 1;
                //tìm về phía dưới quân cờ
                while (true)
                {
                    if (i < 0 || i > 9) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[x, i] == 0)
                    {
                        list.Add(new Point(x, i));
                    }
                    //nếu có quân cờ, tìm tiếp về bên dưới xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i + 1;
                        while (true)
                        {
                            if (j < 0 || j > 9) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[x, j] > 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[x, j] < 0)
                            {
                                list.Add(new Point(x, j));
                                break;
                            }
                            j++;
                        }
                        break;
                    }
                    i++;
                }
            }
            //tương tự với quân âm
            else
            {
                i = x - 1;
                //tìm về phía trái quân cờ
                while (true)
                {
                    if (i < 0 || i > 8) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[i, y] == 0)
                    {
                        list.Add(new Point(i, y));
                    }
                    //nếu có quân cờ, tìm tiếp về bên trái xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i - 1;
                        while (true)
                        {
                            if (j < 0) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[j, y] < 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[j, y] > 0)
                            {
                                list.Add(new Point(j, y));
                                break;
                            }
                            j--;
                        }
                        
                        //không có quân cờ nào thoát vòng lặp
                        break;
                    }
                    i--;
                }

                i = x + 1;
                //tìm về phía phải quân cờ
                while (true)
                {
                    if (i < 0 || i > 8) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[i, y] == 0)
                    {
                        list.Add(new Point(i, y));
                    }
                    //nếu có quân cờ, tìm tiếp về bên phải xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i + 1;
                        while (true)
                        {
                            if (j < 0 || j > 8) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[j, y] < 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[j, y] > 0)
                            {
                                list.Add(new Point(j, y));
                                break;
                            }
                            j++;
                        }
                        break;
                    }
                    i++;
                }

                i = y - 1;
                //tìm về phía trên quân cờ
                while (true)
                {
                    if (i < 0 || i > 9) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[x, i] == 0)
                    {
                        list.Add(new Point(x, i));
                    }
                    //nếu có quân cờ, tìm tiếp về bên trên xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i - 1;
                        while (true)
                        {
                            if (j < 0) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[x, j] < 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[x, j] > 0)
                            {
                                list.Add(new Point(x, j));
                                break;
                            }
                            j--;
                        }
                        break;
                    }
                    i--;
                }

                i = y + 1;
                //tìm về phía dưới quân cờ
                while (true)
                {
                    if (i < 0 || i > 9) break;

                    //nếu vị trí này trên bàn cờ trống, thêm nước đi cho quân cờ
                    if (board.MatrixPosition[x, i] == 0)
                    {
                        list.Add(new Point(x, i));
                    }
                    //nếu có quân cờ, tìm tiếp về bên dưới xem có quân địch không, có thì thêm nước đi
                    else
                    {
                        j = i + 1;
                        while (true)
                        {
                            if (j < 0 || j > 9) break;
                            //gặp quân mình thì thoát vòng lặp
                            if (board.MatrixPosition[x, j] < 0) break;
                            //gặp quân địch thêm nước đi rồi thoát
                            else if (board.MatrixPosition[x, j] > 0)
                            {
                                list.Add(new Point(x, j));
                                break;
                            }
                            j++;
                        }
                        break;
                    }
                    i++;
                }
            }
            return list;
        }
    }

}
