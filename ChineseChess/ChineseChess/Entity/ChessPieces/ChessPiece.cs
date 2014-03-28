using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ChineseChess
{
    /// <summary>
    /// Quân cờ
    /// </summary>
    public class ChessPiece : EntityChess
    {
        #region Properties

        const byte ImageWidth = 109;
        const byte ImageHeight = 108;
        const byte WidthDisplay = 42;
        
        /// <summary>
        /// mã quân cờ, quân dương năm phía trên, quân âm nằm phía dưới 
        /// </summary>
        /// <remarks></remarks>
        protected int _id;

        /// <summary>
        /// mã quân cờ
        /// </summary>
        /// <remarks>Mặc định id dương là phía trên bàn cờ, id âm phía dưới bàn cờ</remarks>
        public int Id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Trạng thái còn sống hay đã chết
        /// </summary>
        protected bool _isDied = false;

        /// <summary>
        /// Đã chết hay chưa
        /// </summary>
        public bool IsDied
        {
            get { return _isDied; }
            set
            {
                _isDied = value;
            }
        }

        /// <summary>
        /// Màu sắc của quân cờ
        /// </summary>
        protected EColor _color;

        public EColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        protected EType _type;

        /// <summary>
        /// Loại quân
        /// </summary>
        public EType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        protected int _value;

        /// <summary>
        /// Giá trị của quân cờ
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        #endregion

        #region Game Method

        /// <summary>
        /// Load tài nguyên của quân cờ
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            _texture2d = content.Load<Texture2D>("bocotuong");
        }

        /// <summary>
        /// Update vị trí quân cờ
        /// </summary>
        /// <param name="gameTime"></param>
        public new virtual void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Vẽ quân cờ lên màn hình
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="p">Tọa độ của quân cờ trong bộ cờ (tài nguyên)</param>
        protected void Draw(SpriteBatch spriteBatch, Point p)
        {
            float scale = (float)WidthDisplay / (float)ImageWidth;
            Rectangle rect = new Rectangle(p.X * ImageWidth, p.Y * ImageHeight, ImageWidth, ImageHeight);
            
            spriteBatch.Draw(_texture2d, Position, rect, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        
        /// <summary>
        /// Vẽ lên màn hình
        /// </summary>
        /// <param name="spriteBatch"></param>
        public new virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        #endregion

        /// <summary>
        /// Di chuyển quân cờ
        /// </summary>
        /// <param name="destination">vị trí đích</param>
        /// <param name="board">bàn cờ</param>
        /// <returns>True nếu di chuyển thành công, false ngược lại</returns>
        public virtual bool Move(Point destination, ChessBoard board)
        {
            if (destination == new Point(-1, -1)) return false;

            var list = GetNextMoves(board);
            foreach (var pos in list)
            {
                if (destination == pos)
                {
                    board.MatrixPosition[_point.X, _point.Y] = 0;

                    var chess = board[destination];
                    if (chess != null)
                    {
                        chess._isDied = true;
                        chess.Point = new Microsoft.Xna.Framework.Point(-1, -1);
                    }
                    _point = destination;
                    board.MatrixPosition[destination.X, destination.Y] = _id;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Đổi màu quân cờ
        /// </summary>
        public void ChangeColor()
        {
            if (_color == EColor.Black) _color = EColor.Red;
            else _color = EColor.Black;
        }

        /// <summary>
        /// Lấy danh sách các nước đi kế tiếp
        /// </summary>
        /// <remarks>Dựa vào vị trí của nó trên bàn cờ để có được danh sách nước đi hợp lệ</remarks>
        /// <param name="board">bàn cờ</param>
        /// <returns>Danh sách các điểm có thể đi</returns>
        public virtual List<Point> GetNextMoves(ChessBoard board)
        {
            return null;
        }

        /// <summary>
        /// Sao chép 1 quân cờ
        /// </summary>
        /// <param name="piece"></param>
        public void Clone(ChessPiece piece)
        {
            _isDied = piece._isDied;
            _color = piece._color;
            _value = piece._value;
            _point = piece._point;
            _position = piece._position;
        }

        #region Constructor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ChessPiece(EColor color)
        {
            _color = color;
        }

        public ChessPiece(int id, EColor color, Point point)
        {
            _id = id;
            _color = color;
            _point = point;
        }

        public ChessPiece(ChessPiece piece)
        {
            _id = piece.Id;
            _isDied = piece._isDied;
            _color = piece._color;
            _type = piece._type;
            _value = piece._value;
            _point = piece._point;
            _texture2d = piece._texture2d;
            _position = piece._position;
        }

        #endregion
    }
}
