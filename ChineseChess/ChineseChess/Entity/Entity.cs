using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChineseChess
{
    /// <summary>
    /// Thực thể Game
    /// </summary>
    public class Entity
    {
        protected Texture2D _texture2d;

        protected Vector2 _position = Vector2.Zero;

        /// <summary>
        /// Load tài nguyên
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
        }

        /// <summary>
        /// Vẽ lên màn hình
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2d, _position, Color.White);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture2d.Width, _texture2d.Height);
        }

        public Entity(Texture2D texture, Vector2 position)
        {
            _texture2d = texture;
            _position = position;
        }

        public Entity() { }
    }
}
