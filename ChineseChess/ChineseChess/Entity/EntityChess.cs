using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChess
{
    /// <summary>
    /// Thực thể nằm trên bàn cờ
    /// </summary>
    public class EntityChess : Entity
    {
        Vector2 goc = new Vector2(5, 3);

        protected Point _point;

        public Point Point
        {
            get { return _point; }
            set { _point = value; }
        }

        /// <summary>
        /// Tọa độ quân cờ so với màn hình
        /// </summary>
        public Vector2 Position
        {
            get
            {
                Vector2 temp = new Vector2();

                if (_point.Y > 4)
                {
                    temp.X = goc.X + 53 * _point.X;
                    temp.Y = goc.Y + 55 * _point.Y + 1;
                }
                else
                {
                    temp.X = goc.X + 53 * _point.X;
                    temp.Y = goc.Y + 55 * _point.Y;
                }
                _position = temp;

                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public EntityChess()
        {
        }
    }
}
