using System;


namespace c_sharp_Tetris
{
    class Shape
    {        
        public int[,] BlockShape { get; set; }

        static Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        
        public Shape()
        {
            newShape();   
        }

        public int[,] newShape()
        {
            switch (rnd.Next(8))
            {
                case 1:
                    BlockShape = new int[,]
                                   {{ 0, 1, 0, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 1, 0, 0}};
                    break;
                case 2:
                    BlockShape = new int[,]
                                   {{ 0, 1, 0, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 0, 0, 0}};
                    break;
                case 3:
                    BlockShape = new int[,]
                                   {{ 0, 0, 0, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 0, 0, 0}};
                    break;
                case 4:
                    BlockShape = new int[,]
                                   {{ 0, 0, 1, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 0, 0, 0}};
                    break;
                case 5:
                    BlockShape = new int[,]
                                   {{ 0, 1, 0, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 0, 1, 0},
                                    { 0, 0, 0, 0}};
                    break;
                case 6:
                    BlockShape = new int[,]
                                   {{ 0, 1, 0, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 0, 0, 0}};
                    break;
                case 7:
                    BlockShape = new int[,]
                                   {{ 0, 0, 1, 0},
                                    { 0, 0, 1, 0},
                                    { 0, 1, 1, 0},
                                    { 0, 0, 0, 0}};
                    break;
            }
            return BlockShape;
        }
    }
}
