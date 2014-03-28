using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Windows.Threading;

namespace ChineseChess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// nhạc nền
        /// </summary>
        Song songBackground;

        /// <summary>
        /// bàn cờ
        /// </summary>
        ChessBoard board;

        /// <summary>
        /// quân cờ được chọn
        /// </summary>
        ChessPiece selectedChess;

        /// <summary>
        /// trạng thái chọn
        /// </summary>
        /// <remarks>kiểm tra xem quân cờ đã được chọn hay chưa</remarks>
        bool isSelected = false;

        /// <summary>
        /// Lượt đi của người chơi, 0 là quân đen, 1 là quân đỏ, khởi tạo đen được đi trước
        /// </summary>
        int turn = -1;

        SpriteFont font;
        string turnString;
        string boardState = "";
        Entity btnUndo;
        Computer computer;
        int i = 0;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //thông số màn hình
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;

            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            i++;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            board = new ChessBoard();
            btnUndo = new Entity(Content.Load<Texture2D>("undo"), new Vector2(0, 650));
            computer = new Computer();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            board.LoadContent(Content);
            font = Content.Load<SpriteFont>("SpriteFont1");
            songBackground = Content.Load<Song>("CoTuong");
            //MediaPlayer.Play(songBackground);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            TouchCollection touches = TouchPanel.GetState();

            if (turn == -1)
            {
                if (touches.Count > 0)
                {
                    if (touches[0].State == TouchLocationState.Pressed)
                    {
                        //lấy điểm chạm
                        Point point = board.GetPosition(touches[0]);

                        if (point != new Point(-1, -1))
                        {
                            if (isSelected == false)
                            {
                                selectedChess = board[point];

                                if (selectedChess != null)
                                {
                                    Debug.WriteLine(selectedChess.Value);
                                    Debug.WriteLine(selectedChess.GetNextMoves(board).Count);

                                    if ((selectedChess.Id * turn) > 0)
                                    {
                                        isSelected = true;
                                        board.SelectedBox1.Point = point;
                                        board.SelectedBox2.Point = new Point(-1, -1);
                                    }
                                }

                            }
                            else
                            {
                                
                                //nếu chọn tiếp phải quân mình thì đổi quân chọn
                                if ((board.CheckPoint(point) * selectedChess.Id) > 0)
                                {
                                    selectedChess = board[point];
                                    board.SelectedBox1.Point = point;
                                    
                                }
                                //nếu không phải tức là quân địch hoặc trống cho phép di chuyển
                                else
                                {
                                    if (board.MovePiece(selectedChess, point) == true)
                                    {
                                        board.SelectedBox2.Point = point;
                                        isSelected = false;
                                        selectedChess = null;
                                        //if (turn == -1)
                                        //    if (board.CheckmatePositiveTeam()) boardState = "Chiếu tướng đỏ";
                                        //    else boardState = "";
                                        //else
                                        //    if (board.CheckmateNegativeTeam()) boardState = "Chieu tuong đen";
                                        //    else boardState = "";
                                        turn = turn == -1 ? 1 : -1; //đổi lượt khi di chuyển thành công
                                        Debug.WriteLine(board.Value);
                                    }
                                }
                            }
                        }

                        if (btnUndo.GetRectangle().Contains((int)touches[0].Position.X, (int)touches[0].Position.Y))
                        {
                            if (board.Undo())
                                turn = turn == -1 ? 1 : -1; //đổi lượt
                        }

                    }
                }
            }
            else
            {
                
                var b = computer.GetNextBoard(board);
                board.Clone(b);
                turn = turn == -1 ? 1 : -1; //đổi lượt
                //Debug.WriteLine(board.GetValue());
            }

            if (turn == -1)
            {
                turnString = "Black's Turn";
            }
            else turnString = "Red's Turn";

            if (board.CheckWinNegativeTeam() || board.CheckWinPositiveTeam())
            {
                boardState = "Hết cờ";
            }

        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            board.Draw(spriteBatch);
            spriteBatch.DrawString(font, turnString, new Vector2(200, 600), Color.White);
            spriteBatch.DrawString(font, boardState, new Vector2(0, 620), Color.Yellow);
            btnUndo.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
