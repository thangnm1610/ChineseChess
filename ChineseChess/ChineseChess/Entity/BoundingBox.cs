using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChineseChess
{
    /// <summary>
    /// Đường bao quanh ô khi chọn
    /// </summary>
    public class BoundingBox : EntityChess
    {
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _texture2d = content.Load<Texture2D>("boundingbox");
        }

        public Rectangle GetBounding
        {
            get
            {
                return new Rectangle(_point.X, _point.Y, 42, 42);
            }
        }

        public BoundingBox()
        {
            _point = new Point(-1, -1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2d, Position, Color.White);
        }
    }
}
