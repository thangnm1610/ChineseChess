using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Windows.Threading;

namespace ChineseChess
{
    /// <summary>
    /// Máy tính
    /// </summary>
    public class Computer
    {
        
        public ChessBoard Board { get; set; }
        public int Value { get; set; }
        
        public int i = 0;
        
        public ChessBoard GetNextBoard(ChessBoard board)
        {
            Computer com = new Computer();
            i = 0;
            com = AlphaBetaPruning(board, -10000, 10000, 5, 1);
            Debug.WriteLine("Count = " + i);
            return com.Board;
        }

        /// <summary>
        /// Sinh các thế cờ quân mã dương
        /// </summary>
        /// <param name="board">bàn cờ hiện tại</param>
        /// <returns>danh sách các thế cờ(bàn cờ)</returns>
        public List<ChessBoard> GenerateBoardPositiveTeam(ChessBoard board)
        {
            var list = new List<ChessBoard>();
            
            foreach (var chess in board.PositiveTeam)
            {
                foreach (var move in chess.GetNextMoves(board))
                {
                    board.MovePiece(chess, move); //đi thử quân cờ, quân cờ chess thuộc board nên phải di chuyển rồi mới copy
                    var boardTemp = board.Clone(); //copy bàn cờ
                    board.Undo(); //undo
                    list.Add(boardTemp); //thêm bàn cờ với nước đi kế tiếp
                }
            }

            return list;
        }

        /// <summary>
        /// sinh các thế cờ quân mã âm
        /// </summary>
        /// <param name="board">bàn cờ hiện tại</param>
        /// <returns>danh sách các thế cờ(bàn cờ)</returns>
        public List<ChessBoard> GenerateBoardNegativeTeam(ChessBoard board)
        {
            var list = new List<ChessBoard>();

            foreach (var chess in board.NegativeTeam)
            {
                foreach (var move in chess.GetNextMoves(board))
                {
                    board.MovePiece(chess, move);
                    var boardTemp = board.Clone();
                    board.Undo();
                    list.Add(boardTemp);
                }
            }

            return list;
        }

        /// <summary>
        /// Cắt tỉa Alpha - Beta
        /// </summary>
        /// <param name="board">thế cờ hiện tại</param>
        /// <param name="alpha">giá trị âm vô cùng</param>
        /// <param name="beta">giá trị dương vô cùng</param>
        /// <param name="depth">độ sâu tìm kiếm</param>
        /// <param name="player">người chơi 1 là Max, -1 là Min</param>
        /// <returns></returns>
        /// 
        public Computer AlphaBetaPruning(ChessBoard board, int alpha, int beta, int depth, int player)
        {


            Computer com = new Computer();

            if (depth == 0 || board.CheckWinNegativeTeam() || board.CheckWinPositiveTeam())
            {
                com.Value = board.Value;
                com.Board = board.Clone();
                return com;
            }

            if (player == 1)
            {
                foreach (var b in GenerateBoardPositiveTeam(board))
                {

                    int temp = AlphaBetaPruning(b, alpha, beta, depth - 1, -1).Value;
                    //Debug.WriteLine("temp1 = {0}, alpha = {1}, depth = {2}", temp, alpha, depth);
                    i++;
                    if (temp > alpha)
                    {
                        alpha = temp;
                        com.Value = alpha;
                        com.Board = b.Clone();
                    }
                    
                    if (beta <= alpha) return com;
                }

                return com;
            }
            else
            {
                foreach (var b in GenerateBoardNegativeTeam(board))
                {

                    int temp = AlphaBetaPruning(b, alpha, beta, depth - 1, 1).Value;
                    i++;
                    //Debug.WriteLine("temp2 = {0}, beta = {1}, depth = {2}", temp, beta, depth);
                    if (temp < beta)
                    {
                        beta = temp;
                        com.Board = b.Clone();
                        com.Value = beta;
                    }

                    if (beta <= alpha) return com;
                }
                
                return com;
            }

            
        }
        
    }
}
