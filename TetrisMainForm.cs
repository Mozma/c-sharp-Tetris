using System;
using System.Drawing;
using System.Windows.Forms;

namespace c_sharp_Tetris
{
    public partial class TetrisMainForm : Form
    {
        public const int width = 14, height = 28, k = 20;

        Board board;
        Block block;
        public TetrisMainForm()
        {
            InitializeComponent();
            
            board = new Board(width, height, k);
            board.fillField(pictureBox1);


            block = new Block();
            if (board.checkSpawnArea())
                block.spawn(board);
       
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    block.MoveLeft(board);
                    break;
                case Keys.D:
                    block.MoveRight(board);
                    break;
                case Keys.S:
                    block.MoveDown(board);
                    break;
                case Keys.W:
                    block.rotateAntiClockwise(board);
              
                    break;
            }
            updateField();
        }

        private void TickTack_Tick(object sender, EventArgs e)
        {
            try
            {
                block.Move(board);
                updateField();
            }
            catch(Exception)
            {
                this.Close();
            }
        }

        public void updateField()
        {
            board.fillField(pictureBox1);
        }

    }
}
