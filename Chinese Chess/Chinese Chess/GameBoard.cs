using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    class GameBoard
    {
        public SquareNode[,] mapBoard = new SquareNode[7, 9]; // <--- Data yang disimpan ke dalam Map

        public GameBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //Menyiapkan variabel mapBoard
                    mapBoard[j, i] = new SquareNode(false);

                    //Membuat tampilan Peta
                    mapBoard[j, i].TheButton = new Button();
                    mapBoard[j, i].TheButton.Size = new Size(75, 75);
                    mapBoard[j, i].TheButton.Location = new Point(25 + j * 75, 25 + i * 75);

                }
            }

            //Menyiapkan variabel mapBoard air
            mapBoard[1, 3].IsWater = true;
            mapBoard[2, 3].IsWater = true;
            mapBoard[1, 4].IsWater = true;
            mapBoard[2, 4].IsWater = true;
            mapBoard[1, 5].IsWater = true;
            mapBoard[2, 5].IsWater = true;

            mapBoard[4, 3].IsWater = true;
            mapBoard[5, 3].IsWater = true;
            mapBoard[4, 4].IsWater = true;
            mapBoard[5, 4].IsWater = true;
            mapBoard[4, 5].IsWater = true;
            mapBoard[5, 5].IsWater = true;

            //PLAYER 1 (atas)
            mapBoard[6, 2].CurrentPiece = new AnimalPiece(8, 1); //elephant
            mapBoard[4, 2].CurrentPiece = new AnimalPiece(3, 1); //wolf
            mapBoard[2, 2].CurrentPiece = new AnimalPiece(5, 1); //leopard 
            mapBoard[0, 2].CurrentPiece = new AnimalPiece(1, 1); //rat

            mapBoard[5, 1].CurrentPiece = new AnimalPiece(2, 1); //cat
            mapBoard[1, 1].CurrentPiece = new AnimalPiece(4, 1); //dog

            mapBoard[6, 0].CurrentPiece = new AnimalPiece(6, 1); //tiger
            mapBoard[0, 0].CurrentPiece = new AnimalPiece(7, 1); //lion

            //PLAYER 2 (bawah)
            mapBoard[0, 6].CurrentPiece = new AnimalPiece(8, 2); //elephant
            mapBoard[2, 6].CurrentPiece = new AnimalPiece(3, 2); //wolf
            mapBoard[4, 6].CurrentPiece = new AnimalPiece(5, 2); //leopard 
            mapBoard[6, 6].CurrentPiece = new AnimalPiece(1, 2); //rat

            mapBoard[1, 7].CurrentPiece = new AnimalPiece(2, 2); //cat
            mapBoard[5, 7].CurrentPiece = new AnimalPiece(4, 2); //dog

            mapBoard[0, 8].CurrentPiece = new AnimalPiece(6, 2); //tiger
            mapBoard[6, 8].CurrentPiece = new AnimalPiece(7, 2); //lion

            //Ganti warna Button menjadi Biru
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if(mapBoard[j, i].IsWater)
                    {
                        mapBoard[j, i].TheButton.BackColor = Color.Blue;
                    }
                }

            }
        }
    }
}
