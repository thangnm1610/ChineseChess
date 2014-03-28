using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChineseChess
{
    /// <summary>
    /// Quân tốt
    /// </summary>
    public class Pawn : ChessPiece
    {
        /// <summary>
        /// Vẽ chính nó lên màn hình
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Point p;
            p = _color == EColor.Black ? new Point(3, 2) : new Point(3, 0);
            base.Draw(spriteBatch, p);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="id">Mã quân cờ</param>
        /// <param name="color">màu cờ</param>
        /// <param name="position">vị trí(điểm)</param>
        public Pawn(int id, EColor color, Point position)
            : base(id, color, position)
        {
            _type = EType.Pawn;
            _value = (int)EValue.Pawn;
        }

        /// <summary>
        /// Lấy nước đi kế tiếp
        /// </summary>
        /// <param name="board">bàn cờ</param>
        /// <returns>danh sách vị trí</returns>
        public override List<Point> GetNextMoves(ChessBoard board)
        {
            List<Point> list = new List<Point>();
            
            //id dương âm xác định hướng tấn công, khởi tạo những quân id > 0 ở trên, < 0 ở dưới
            //xác định tốt không được đi lùi
            if (_id > 0)
            {
                //con tốt nào cũng có nước đi tiến 1 nước trừ _point.Y = 9
                if (_point.Y < 9)
                {
                    if (board.CheckPoint(_point.X, _point.Y + 1) <= 0)
                    {
                        list.Add(new Point(_point.X, _point.Y + 1));
                    }
                }

                //xác định nước đi ngang khi qua sông
                if (_point.Y >= 5)
                {
                    //vị trí trừ x = 8(sát mép phải) thì có thể đi ngang sang phải 
                    if (_point.X < 8)
                    {
                        if (board.MatrixPosition[_point.X + 1, _point.Y] <= 0)
                        {
                            list.Add(new Point(_point.X + 1, _point.Y));
                        }
                    }

                    //các vị trí trừ sát mép trái đều có thể đi ngang sang trái
                    if (_point.X > 0)
                    {
                        if (board.MatrixPosition[_point.X - 1, _point.Y] <= 0)
                        {
                            list.Add(new Point(_point.X - 1, _point.Y));
                        }
                    }
                }
            }

            //tương tự với id < 0
            else
            {
                //nước đi tiến với quân có id < 0 là 1 nước giảm tọa độ Y đi 1
                if (_point.Y > 0)
                {
                    
                    if (board.CheckPoint(_point.X, _point.Y - 1) >= 0)
                    {
                        list.Add(new Point(_point.X, _point.Y - 1));
                    }
                    
                }

                //sang sông
                if (_point.Y < 5)
                {
                    //nước đi ngang bên phải
                    if (_point.X < 8)
                    {
                        if (board.MatrixPosition[_point.X + 1, _point.Y] >= 0)
                        {
                            list.Add(new Point(_point.X + 1, _point.Y));
                        }
                       
                    }
                    //nước đi ngang bên trái
                    if (_point.X > 0)
                    {
                        if (board.MatrixPosition[_point.X - 1, _point.Y] >= 0)
                        {
                            list.Add(new Point(_point.X - 1, _point.Y));
                        }
                        
                    }
                }
            }

            return list;
        }


    }
}
