using System;

using System.Drawing;
using System.Windows.Forms;

namespace c_sharp_Tetris
{
    class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private int PixelSize { get; set; }
        public Color BoardColor { get; set; }
        public int[,] BoardStatus { get; set; }

        public Board(int width, int height, int pixelSize)
        {
            Width = width;
            Height = height;
            PixelSize = pixelSize;
            BoardColor = Color.Aquamarine;

            initializeIt();
            createField();
            
        }

        Bitmap bitField;
        Graphics gr;
        Pen pen;
        Brush br,br1,br2,br3;
        /// <summary>
        ///  Этот метод просто объявляет все основные данные поля
        /// </summary>
        private void initializeIt()
        {
            bitField = new Bitmap(PixelSize * Width, PixelSize * Height);
            gr = Graphics.FromImage(bitField);
            pen = new Pen(BoardColor);
            br = new SolidBrush(BoardColor);
            br1 = new SolidBrush(Color.Black);
            br2 = new SolidBrush(Color.Red);
            br3 = new SolidBrush(Color.Green);
            gr.Clear(Color.Black);
        }

        /// <summary>
        /// Создаёт массив вида [] где по краям 1, а в центре 0
        /// </summary>
        private void createField()
        {
            BoardStatus = new int[Height, Width];

            // 1 - border. 0 - field.
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    BoardStatus[row, col] = 0;

                    if ((row == Height - 1) || (row == 0))
                        BoardStatus[row, col] = 1;
                }

                BoardStatus[row, 0] = 1;
                BoardStatus[row, Width - 1] = 1;
            }
        }


        /// <summary>
        /// Рисует квадраты по шаблону
        /// </summary>
        /// <param name="board"></param>
        public void fillField(PictureBox board)
        {
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    drawStatus(bitField, BoardStatus[row, col], row, col);
                }
            }
            board.Image = bitField;

        }

        private void drawStatus(Bitmap bitField, int status, int row, int col)
        {
            // col - x. row - y. 
            // 0 - empty, 1 - border, 2 - unfixed block, 3 - fixated block;  

            switch (status)
            {
                case 0:
                    gr.FillRectangle(br1, col * PixelSize, row * PixelSize, PixelSize, PixelSize);
                    gr.DrawRectangle(pen, col * PixelSize, row * PixelSize, PixelSize, PixelSize);
                    break;

                case 1:
                    gr.FillRectangle(br, col * PixelSize, row * PixelSize, PixelSize, PixelSize);
                    break;
                case 2:
                    gr.FillRectangle(br2, col * PixelSize, row * PixelSize, PixelSize, PixelSize);
                    break;
                case 3:
                    gr.FillRectangle(br3, col * PixelSize, row * PixelSize, PixelSize, PixelSize);
                    break;
            }
           
        }
    }
}
