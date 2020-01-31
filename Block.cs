﻿using System;

namespace c_sharp_Tetris
{
    class Block
    {
        public int[,] blockShape { get; set; }

        public int startX { get; set; }
        public int startY { get; set; }


        Shape shape = new Shape();
        public Block()
        {
            blockShape = shape.BlockShape;
        }



        public void spawn(Board board)
        {
            blockShape = shape.newShape();

            startX = (board.Width / 2) - 2;
            startY = 0;
            for (var row = startY; row < startY + 4; row++)
            {
                for (var col = startX; col < startX + 4; col++)
                {
                    if (blockShape[row - startY,col - startX] == 1)
                    {
                        board.BoardStatus[row + 1, col] = 2;
                    }
                }
            }
        }

        #region movement
        internal void Move(Board board)
        {
            if (checkMove(board, "down"))
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
                fixate(board);
                spawn(board);
            }
        }

        internal void MoveLeft(Board board)
        {
            if (checkMove(board, "left"))
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

        internal void MoveRight(Board board)
        {
            if (checkMove(board, "right"))
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
        
        internal void MoveDown(Board board)
        {
            if (checkMove(board, "down"))
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

        public void rotateAntiClockwise(Board board)
        {
            if (checkRotation(board))
            {
                int[,] temp = new int[blockShape.GetLength(0), blockShape.GetLength(1)];

                for (int col = 0; col < blockShape.GetLength(0); col++)
                    for (int row = 0; row < blockShape.GetLength(1); row++)
                        temp[blockShape.GetLength(1) - 1 - row, col] = blockShape[col, row];

                blockShape = temp;

                updateBlock(board);
            }
        }
        #endregion

        /// <summary>
        /// Обновляет состояние блока размером 4х4.
        /// </summary>
        /// <param name="board"></param>
        internal void updateBlock(Board board)
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

        /// <summary>
        /// Проверяет выбранное направление на возможность хода.
        /// </summary>

        public bool checkMove(Board board, string direction)
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

        public bool checkRotation(Board board)
        {
            for (int col = startX; col < startX + 4; col++)
                for (int row = startY; row < startY + 4; row++)
                    //if (board.BoardStatus[startY + 4 - 1 - row, col] == 3 || board.BoardStatus[startY + 4 - 1 - row, col] == 1)
                    if(board.BoardStatus[startY,startX] == 3 || board.BoardStatus[startY, startX] == 1)
                        return false;

            return true;
        }

        public void fixate(Board board)
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
        }

    }
}
