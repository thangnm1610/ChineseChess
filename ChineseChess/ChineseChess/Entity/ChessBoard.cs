using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections;

namespace ChineseChess
{
    /// <summary>
    /// Bàn cờ
    /// </summary>
    public class ChessBoard : Entity
    {
        #region Constructor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ChessBoard()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 10; j++) MatrixPosition[i, j] = 0;

            InitializeChessBoard();
        }

        /// <summary>
        /// Khởi tạo bàn cờ
        /// </summary>
        public void InitializeChessBoard()
        {
            _chessPieces.Clear();
            //Khởi tạo quân đỏ

            //pháo, mã, xe
            AddChess(new Horse(13, EColor.Red, new Point(1, 0)));
            AddChess(new Horse(14, EColor.Red, new Point(7, 0)));
            AddChess(new Artillery(6, EColor.Red, new Point(1, 2)));
            AddChess(new Artillery(7, EColor.Red, new Point(7, 2)));
            AddChess(new Rook(15, EColor.Red, new Point(0, 0)));
            AddChess(new Rook(16, EColor.Red, new Point(8, 0)));

            //tướng, sĩ, tượng
            AddChess(new Guard(9, EColor.Red, new Point(3, 0)));
            AddChess(new Guard(10, EColor.Red, new Point(5, 0)));
            AddChess(new Elephant(11, EColor.Red, new Point(2, 0)));
            AddChess(new Elephant(12, EColor.Red, new Point(6, 0)));
            AddChess(new King(8, EColor.Red, new Point(4, 0)));

            //5 quân tốt
            AddChess(new Pawn(1, EColor.Red, new Point(0, 3)));
            AddChess(new Pawn(2, EColor.Red, new Point(2, 3)));
            AddChess(new Pawn(3, EColor.Red, new Point(4, 3)) { Value = (int)EValue.HeadPawn });
            AddChess(new Pawn(4, EColor.Red, new Point(6, 3)));
            AddChess(new Pawn(5, EColor.Red, new Point(8, 3)));

            //Khởi tạo quân đen

            //pháo mã xe
            AddChess(new Horse(-13, EColor.Black, new Point(1, 9)));
            AddChess(new Horse(-14, EColor.Black, new Point(7, 9)));
            AddChess(new Artillery(-6, EColor.Black, new Point(1, 7)));
            AddChess(new Artillery(-7, EColor.Black, new Point(7, 7)));
            AddChess(new Rook(-15, EColor.Black, new Point(0, 9)));
            AddChess(new Rook(-16, EColor.Black, new Point(8, 9)));

            //tướng, sĩ, tượng
            AddChess(new Guard(-9, EColor.Black, new Point(3, 9)));
            AddChess(new Guard(-10, EColor.Black, new Point(5, 9)));
            AddChess(new Elephant(-11, EColor.Black, new Point(2, 9)));
            AddChess(new Elephant(-12, EColor.Black, new Point(6, 9)));
            AddChess(new King(-8, EColor.Black, new Point(4, 9)));

            //5 quân tốt
            AddChess(new Pawn(-1, EColor.Black, new Point(0, 6)));
            AddChess(new Pawn(-2, EColor.Black, new Point(2, 6)));
            AddChess(new Pawn(-3, EColor.Black, new Point(4, 6)) { Value = (int)EValue.HeadPawn });
            AddChess(new Pawn(-4, EColor.Black, new Point(6, 6)));
            AddChess(new Pawn(-5, EColor.Black, new Point(8, 6)));
        }

        #endregion

        #region Property

        /// <summary>
        /// Ma trận tọa độ của quân cờ
        /// </summary>
        public int[,] MatrixPosition = new int[9, 10];

        /// <summary>
        /// Ngăn xếp các nước đi Undo
        /// </summary>
        public Stack<Move> StackUndo = new Stack<Move>();

        /// <summary>
        /// Ngăn xếp các nước đi Redo
        /// </summary>
        public Stack<Move> StackRedo = new Stack<Move>();

        private List<ChessPiece> _chessPieces = new List<ChessPiece>();

        /// <summary>
        /// Danh sách quân cờ trên bàn cờ
        /// </summary>
        public List<ChessPiece> ChessPieces
        {
            get { return _chessPieces; }
            set { _chessPieces = value; }
        }

        public BoundingBox SelectedBox1 = new BoundingBox();

        public BoundingBox SelectedBox2 = new BoundingBox();

        /// <summary>
        /// đội quân có mã dương (thường phía trên bàn cờ)
        /// </summary>
        public List<ChessPiece> PositiveTeam
        {
            get
            {
                List<ChessPiece> list = new List<ChessPiece>();
                foreach (var chess in _chessPieces)
                {
                    if (chess.Id > 0 && chess.IsDied == false) list.Add(chess);
                }

                return list;
            }
        }

        /// <summary>
        /// đội quân có mã âm
        /// </summary>
        /// <remarks>thường ở nửa dưới bàn cờ</remarks>
        public List<ChessPiece> NegativeTeam
        {
            get
            {
                List<ChessPiece> list = new List<ChessPiece>();
                foreach (var chess in _chessPieces)
                {
                    if (chess.Id < 0 && chess.IsDied == false) list.Add(chess);
                }

                return list;
            }
        }

        #endregion

        #region Override Load and Draw Method

        /// <summary>
        /// Load tài nguyên
        /// </summary>
        /// <param name="content">ContentManger</param>
        public override void LoadContent(ContentManager content)
        {
            _texture2d = content.Load<Texture2D>("chineseboard");

            foreach(var chess in _chessPieces)
            {
                chess.LoadContent(content);
            }

            SelectedBox1.LoadContent(content);
            SelectedBox2.LoadContent(content);
        }

        /// <summary>
        /// Vẽ bàn cờ
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            float scale = (float)480 / (float)571;
            spriteBatch.Draw(_texture2d, _position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);

            foreach(var chess in _chessPieces)
            {
                if(chess.IsDied == false) chess.Draw(spriteBatch);
            }

            SelectedBox1.Draw(spriteBatch);
            SelectedBox2.Draw(spriteBatch);
        }

        #endregion

        #region Get Array Method

        /// <summary>
        /// Lấy quân cờ theo tọa độ
        /// </summary>
        /// <param name="x">ngang</param>
        /// <param name="y">dọc</param>
        /// <returns>quân cờ</returns>
        public ChessPiece this[int x, int y]
        {
            get
            {
                if (x == -1 || y == -1) return null;
                return _chessPieces.FirstOrDefault<ChessPiece>(chess => chess.Id == MatrixPosition[x, y]);
            }
        }

        /// <summary>
        /// Lấy quân cờ theo điểm
        /// </summary>
        /// <param name="p">điểm</param>
        /// <returns>quân cờ</returns>
        public ChessPiece this[Point p]
        {
            get
            {
                if (p == new Point(-1, -1)) return null;
                return _chessPieces.FirstOrDefault<ChessPiece>(chess => chess.Id == MatrixPosition[p.X, p.Y]);
            }
        }

        /// <summary>
        /// Lấy quân cờ theo id
        /// </summary>
        /// <param name="id">mã quân cờ</param>
        /// <returns>quân cờ</returns>
        public ChessPiece this[int id]
        {
            get
            {
                return _chessPieces.FirstOrDefault<ChessPiece>(chess => chess.Id == id);
            }
        }

        #endregion

        /// <summary>
        /// Lấy vị trí của điểm chạm
        /// </summary>
        /// <param name="touch"></param>
        /// <returns>Tọa độ điểm chạm so với bàn cờ</returns>
        public Point GetPosition(TouchLocation touch)
        {
            
            int x = -1;

            int xCoordinate = (int)touch.Position.X;
            //System.Diagnostics.Debug.WriteLine("width = " + xCoordinate);
            if (xCoordinate < 5 || xCoordinate > 472) x = -1;
            else if( xCoordinate >= 5 && xCoordinate <= 26) x = 0;
            else
            {
                int temp = (int)((xCoordinate - 26) / 55);
                //System.Diagnostics.Debug.WriteLine("temp = " + temp);
                if (xCoordinate >= (26 + temp * 55) && xCoordinate <= (26 + temp * 55 + 21)) x = temp;
                else if (xCoordinate >= (26 + (temp + 1) * 55 - 21) && xCoordinate <= (26 + (temp + 1) * 55)) x = temp + 1; 
            }
            //System.Diagnostics.Debug.WriteLine("x = " + x);

            int y = -1;

            int yCoordinate = (int)touch.Position.Y;
            //System.Diagnostics.Debug.WriteLine("height = " + yCoordinate);
            if (yCoordinate < 3 || yCoordinate > 540) y = -1;
            else if (yCoordinate >= 3 && yCoordinate <= 25) y = 0;
            else
            {
                int yTemp = (int)((yCoordinate - 25) / 55);
                //System.Diagnostics.Debug.WriteLine("temp = " + yTemp);
                if (yCoordinate >= (25 + yTemp * 55) && yCoordinate <= (25 + yTemp * 55 + 22)) y = yTemp;
                else if (yCoordinate >= (25 + (yTemp + 1) * 55 - 22) && yCoordinate <= (25 + (yTemp + 1) * 55)) y = yTemp + 1;
            }
           // System.Diagnostics.Debug.WriteLine("y = " + y);

            if (x == -1 || y == -1) return new Point(-1, -1);
            return new Point(x, y);

        }

        /// <summary>
        /// Đổi màu cờ 2 bên
        /// </summary>
        public void ChangeColor()
        {
            foreach (var chess in _chessPieces)
            {
                chess.ChangeColor();
            }
        }

        /// <summary>
        /// Thêm 1 quân cờ vào bàn cờ
        /// </summary>
        /// <param name="chess">quân cờ</param>
        public void AddChess(ChessPiece chess)
        {
            _chessPieces.Add(chess);
            MatrixPosition[chess.Point.X, chess.Point.Y] = chess.Id;
        }

        /// <summary>
        /// Thay đổi vị trí con cờ và ma trận điểm tại điểm đó
        /// </summary>
        /// <param name="chess">quân cờ</param>
        /// <param name="p">điểm đích</param>
        public void SetPoint(ChessPiece chess, Point p)
        {
            chess.Point = p;
            MatrixPosition[p.X, p.Y] = chess.Id;
        }

        /// <summary>
        /// Lấy giá trị của điểm trong mảng 2 chiều
        /// </summary>
        /// <param name="p">điểm</param>
        /// <returns>id của quân cờ, 0 nếu điểm trống</returns>
        public int GetPoint(Point p)
        {
            return MatrixPosition[p.X, p.Y];
        }

        public int GetPoint(int x, int y)
        {
            return MatrixPosition[x, y];
        }
        /// <summary>
        /// Set giá trị của điểm trong mảng 2 chiều
        /// </summary>
        /// <param name="p">điểm</param>
        /// <param name="value">Id của quân cờ hoặc 0 nếu muốn vị trí đó là trống</param>
        public void SetPoint(Point p, int value)
        {
            MatrixPosition[p.X, p.Y] = value;
        }

        /// <summary>
        /// Kiểm tra trạng thái của ô trên bàn cờ
        /// </summary>
        /// <param name="p">điểm</param>
        /// <returns>Trả về 1 nếu quân dương, 0 nếu trống, -1 nếu quân âm</returns>
        public int CheckPoint(Point p)
        {
            if (MatrixPosition[p.X, p.Y] == 0) return 0;
            else if (MatrixPosition[p.X, p.Y] > 0) return 1;
            else return -1;
        }

        /// <summary>
        /// Kiểm tra trạng thái ô trên bàn cờ
        /// </summary>
        /// <param name="x">ngang</param>
        /// <param name="y">dọc</param>
        /// <returns></returns>
        public int CheckPoint(int x, int y)
        {
            if (MatrixPosition[x, y] == 0) return 0;
            else if (MatrixPosition[x, y] > 0) return 1;
            else return -1;
        }

        /// <summary>
        /// Chiếu tướng quân dương (quân dương bị chiêu tướng)
        /// </summary>
        /// <returns>true or false</returns>
        public bool CheckmatePositiveTeam()
        {
            bool isdie = false;
            King positiveKing = (King)this[8];
            
            foreach(var piece in NegativeTeam)
            {
                foreach(var move in piece.GetNextMoves(this))
                {
                    this.MovePiece(piece, move);
                    if (positiveKing.IsDied == true) isdie = true;
                    this.Undo();
                    if (isdie) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Chiếu tướng quân âm (quân âm bị chiêu tướng)
        /// </summary>
        /// <returns>true or false</returns>
        public bool CheckmateNegativeTeam()
        {
            bool isdie = false;
            King negativeKing = (King)this[-8];

            foreach (var piece in PositiveTeam)
            {
                foreach (var move in piece.GetNextMoves(this))
                {
                    this.MovePiece(piece, move);
                    if (negativeKing.IsDied == true) isdie = true;
                    this.Undo();
                    if (isdie) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra quân dương giành chiến thắng
        /// </summary>
        /// <returns></returns>
        public bool CheckWinPositiveTeam()
        {
            bool isEnded = true;
            foreach (var piece in NegativeTeam)
            {
                foreach (var move in piece.GetNextMoves(this))
                {
                    this.MovePiece(piece, move);
                    if (!CheckmateNegativeTeam()) isEnded = false;
                    this.Undo();
                    if (!isEnded) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra quân âm dành chiến thắng
        /// </summary>
        /// <returns></returns>
        public bool CheckWinNegativeTeam()
        {
            bool isEnded = true;
            foreach (var piece in PositiveTeam)
            {
                foreach (var move in piece.GetNextMoves(this))
                {
                    this.MovePiece(piece, move);
                    if (!CheckmatePositiveTeam()) isEnded = false;
                    this.Undo();
                    if (!isEnded) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Tính giá trị bàn cờ (hàm lượng gái
        /// </summary>
        /// <returns>giá trị bàn cờ</returns>
        public int Value
        {
            get
            {
                int s = 0;

                foreach (var chess in _chessPieces)
                {
                    if (chess.IsDied == false)
                    {
                        if (chess.Id > 0) s += chess.Value;
                        else s -= chess.Value;
                    }
                }
                return s;
            }
        }

        /// <summary>
        /// Di chuyển 1 quân cờ
        /// </summary>
        /// <param name="piece">con cờ</param>
        /// <param name="destination">vị trí đích</param>
        /// <returns>true nếu di chuyển thành công, ngược lại false</returns>
        /// <remarks>Đồng thời thêm vào Stack Undo</remarks>
        public bool MovePiece(ChessPiece piece, Point destination)
        {
            Move m = new Move(piece.Id, piece.Point, this.GetPoint(destination));

            if (piece.Move(destination, this))
            {
                StackUndo.Push(m);
                StackRedo.Clear();
                return true;
            }

            return false;
        }

        #region Undo - Redo

        /// <summary>
        /// Khôi phục trạng thái bàn cờ 1 nước đi
        /// </summary>
        public bool Undo()
        {
            if (StackUndo.Count > 0)
            {
                Move move = StackUndo.Pop();
                StackRedo.Push(move);

                //quân cờ di chuyển
                ChessPiece movePiece = this[move.Id];

                int pointValue = move.ValueOfDestinationPoint;
                if (pointValue != 0)
                {
                    //khôi phục quân cờ bị ăn
                    ChessPiece p = this[pointValue]; //quân cờ bị ăn
                    SetPoint(p, movePiece.Point); //set lại vị trí quân cờ và ma trận điểm
                    p.IsDied = false;
                }
                //khôi phục vị trí đó là trống
                else MatrixPosition[movePiece.Point.X, movePiece.Point.Y] = pointValue;
                SetPoint(movePiece, move.OldPoint);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Redo 1 nước đi
        /// </summary>
        public void Redo()
        {
            if (StackRedo.Count > 0)
            {
                Move move = StackRedo.Pop();

                //Todo ....
            }
        }

        #endregion

        #region ToString()
        /// <summary>
        /// In ra ma trận điểm
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            for(int i = 0; i < 10; i++)
            {
                s += string.Format("{0,4} {1,4} {2,4} {3,4} {4,4} {5,4} {6,4} {7,4} {8,4}\n", GetPoint(0, i), GetPoint(1, i), GetPoint(2, i), GetPoint(3, i),
                        GetPoint(4, i), GetPoint(5, i), GetPoint(6, i), GetPoint(7, i), GetPoint(8, i));
            }
            return s;
        }

        #endregion

        #region Clone Method

        /// <summary>
        /// Sao chép 1 bàn cờ
        /// </summary>
        /// <returns>Trả về 1 bản copy mới</returns>
        public ChessBoard Clone()
        {
            var board = new ChessBoard();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 10; j++) board.MatrixPosition[i, j] = this.MatrixPosition[i, j];

            foreach (var chess in this._chessPieces)
            {
                foreach (var piece in board._chessPieces)
                {
                    if (piece.Id == chess.Id)
                    {
                        piece.Clone(chess);
                    }
                }
            }
            return board;
        }

        /// <summary>
        /// Sao chép bàn cờ
        /// </summary>
        /// <param name="board">bàn cờ</param>
        public void Clone(ChessBoard board)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 10; j++) MatrixPosition[i, j] = board.MatrixPosition[i, j];

            foreach (var chess in board._chessPieces)
            {
                foreach (var piece in this._chessPieces)
                {
                    if (piece.Id == chess.Id)
                    {
                        piece.Clone(chess);
                    }
                }
            }
        }

        #endregion
    }
}
