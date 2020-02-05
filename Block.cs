using System;
using System.Drawing;
using System.Windows.Forms;

namespace c_sharp_Tetris
{
    class Block
    {
        public int[,] blockShape { get; set; }

        public int startX { get; set; }
        public int startY { get; set; }
        public Board board { get; set; } 

     
        Shape shape = new Shape();
        public Block(Board board)
        {
            blockShape = shape.BlockShape;
            this.board = board;
        }

        public void spawn()
        {
            if (board.checkSpawnArea())
            {
                blockShape = shape.newShape();

                startX = (board.Width / 2) - 2;
                startY = 0;
                for (var row = startY; row < startY + 4; row++)
                {
                    for (var col = startX; col < startX + 4; col++)
                    {
                        if (blockShape[row - startY, col - startX] == 1)
                        {
                            board.BoardStatus[row + 1, col] = 2;
                        }
                    }
                }
            }
            else
            {
               // End game block. 
                if (MessageBox.Show("Start a New Game?", "Game Over!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    startNewGame();
                else
                    endGame();
            }

        }
        /// <summary>
        /// Throw exception upper.
        /// </summary>

        private void endGame()
        {
            throw new Exception();
        }

        private void startNewGame()
        {
            board.clear();
            spawn();
        }

        #region movement
        internal void Move()
        {
            if (checkMove("down"))
            {
                
                for (var row = board.Height - 1; row >= 0; row--)
                {
                    for (var col = board.Width - 1; col >= 0; col--)
                    {
                        if (board.BoardStatus[row, col] == 2)
                        {
                            board.BoardStatus[row, col] = 0;
                            board.BoardStatus[row + 1, col] = 2;
                        }
                    }
                }
                startY++;
            }
            else
            {
                fixate();
                spawn();
            }
        }

        internal void MoveLeft()
        {
            if (checkMove("left"))
            {
                for (var col = 1; col < board.Width; col++)
                {
                    for (var row = board.Height - 1; row >= 0; row--)
                    {
                        if ((board.BoardStatus[row, col] == 2))
                        {
                            
                            board.BoardStatus[row, col] = 0;
                            board.BoardStatus[row, col - 1] = 2;
                        }
                    }
                }
                startX--;
            }
        }

        internal void MoveRight()
        {
            if (checkMove("right"))
            { 
                for (var col = board.Width - 1; col >= 0; col--)
                {
                    for (var row = board.Height - 1; row >= 0; row--)
                    {
                        if ((board.BoardStatus[row, col] == 2))
                        {   
                            board.BoardStatus[row, col] = 0;
                            board.BoardStatus[row, col + 1] = 2;
                        }
                    }
                }
                startX++;
            }
        }
        
        internal void MoveDown()
        {
            if (checkMove("down"))
            {
                for (var row = board.Height - 1; row >= 0; row--)
                {
                    for (var col = board.Width - 1; col >= 0; col--)
                    {
                        if (board.BoardStatus[row, col] == 2)
                        {
                           
                            board.BoardStatus[row, col] = 0;
                            board.BoardStatus[row + 1, col] = 2;
                        }
                    }
                }
                startY++;
            }
        }

        //public void rotateClockwise()
        //{
        //    rotateAntiClockwise();
        //    rotateAntiClockwise();
        //    rotateAntiClockwise();
        //}

        public void rotateAntiClockwise()
        {
            if (checkRotation())
            {
                int[,] temp = new int[blockShape.GetLength(0), blockShape.GetLength(1)];

                for (int col = 0; col < blockShape.GetLength(0); col++)
                    for (int row = 0; row < blockShape.GetLength(1); row++)
                        temp[blockShape.GetLength(1) - 1 - row, col] = blockShape[col, row];

                blockShape = temp;

                updateBlock();
            }
        }
        #endregion

        /// <summary>
        /// Обновляет состояние блока размером 4х4.
        /// </summary>
        /// <param name="board"></param>
        internal void updateBlock()
        {
            try
            {
                //delete all red blocks
                for (var col = 1; col < board.Width; col++)
                {
                    for (var row = 1; row < board.Height; row++)
                    {
                        if ((board.BoardStatus[row, col] == 2))
                        {
                            board.BoardStatus[row, col] = 0;
                        }
                    }
                }

                //set new red blocks
                for (var col = startX; col < startX + 4; col++)
                {
                    for (var row = startY; row < startY + 4; row++)
                    {

                        if (blockShape[row - startY, col - startX] == 1)
                        {
                            board.BoardStatus[row + 1, col] = 2;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Проверяет выбранное направление на возможность хода.
        /// </summary>

        public bool checkMove(string direction)
        {            
            for (var row = 1; row < board.Height-1; row++)
            {                
                for (var col = 1; col < board.Width-1; col++)
                {
                    if (board.BoardStatus[row, col] == 2)
                    {
                        switch (direction.ToLower())
                        {
                            case "down":
                                if (board.BoardStatus[row + 1, col] == 1 || board.BoardStatus[row + 1, col] == 3)
                                    return false;
                                break;

                            case "left":
                                if (board.BoardStatus[row, col - 1] == 1 || board.BoardStatus[row, col - 1] == 3)
                                    return false;
                                break;

                            case "right":
                                if (board.BoardStatus[row, col + 1 ] == 1 || board.BoardStatus[row, col + 1] == 3)
                                    return false;
                                break;
                        }    
                        
                    }
                }
            }
            return true;
        }
        public bool checkRotation()
        {
            for (int col = startX; col < startX + 4; col++)
                for (int row = startY; row < startY + 4; row++)
                {
                    try
                    {
                        if (board.BoardStatus[row, col] == 1 || board.BoardStatus[row, col] == 3)
                            return false;                                                
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return false;
                    }
                }
            return true;
        }
        public void fixate()
        {
            for (var col = 1; col < board.Width; col++)
            {
                for (var row = 1; row < board.Height; row++)
                {
                    if ((board.BoardStatus[row, col] == 2))
                    {
                        board.BoardStatus[row, col] = 3;
                    }
                }
            }
           
            board.checkRows();
        }

    }
}
