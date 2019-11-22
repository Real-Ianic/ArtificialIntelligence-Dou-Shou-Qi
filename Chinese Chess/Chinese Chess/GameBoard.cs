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
            Image bg = Image.FromFile("resource/wood.jpg");

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //Menyiapkan variabel mapBoard
                    mapBoard[j, i] = new SquareNode(false,false,false,false);

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

            //trap player 1
            mapBoard[2, 0].IsTrapP1 = true;
            mapBoard[4, 0].IsTrapP1 = true;
            mapBoard[3, 1].IsTrapP1 = true;
            mapBoard[3, 0].IsDen1 = true; //home player 1

            //trap player 2
            mapBoard[2, 8].IsTrapP2 = true;
            mapBoard[4, 8].IsTrapP2 = true;
            mapBoard[3, 7].IsTrapP2 = true;
            mapBoard[3, 8].IsDen2 = true; //trapl player 2

            //PLAYER 1 animals (atas)
            mapBoard[6, 2].CurrentPiece = new AnimalPiece(8, 1); //elephant
            mapBoard[4, 2].CurrentPiece = new AnimalPiece(3, 1); //wolf
            mapBoard[2, 2].CurrentPiece = new AnimalPiece(5, 1); //leopard 
            mapBoard[0, 2].CurrentPiece = new AnimalPiece(1, 1); //rat

            mapBoard[5, 1].CurrentPiece = new AnimalPiece(2, 1); //cat
            mapBoard[1, 1].CurrentPiece = new AnimalPiece(4, 1); //dog

            mapBoard[6, 0].CurrentPiece = new AnimalPiece(6, 1); //tiger
            mapBoard[0, 0].CurrentPiece = new AnimalPiece(7, 1); //lion

            //PLAYER 2 animals (bawah)
            mapBoard[0, 6].CurrentPiece = new AnimalPiece(8, 2); //elephant
            mapBoard[2, 6].CurrentPiece = new AnimalPiece(3, 2); //wolf
            mapBoard[4, 6].CurrentPiece = new AnimalPiece(5, 2); //leopard 
            mapBoard[6, 6].CurrentPiece = new AnimalPiece(1, 2); //rat

            mapBoard[1, 7].CurrentPiece = new AnimalPiece(2, 2); //cat
            mapBoard[5, 7].CurrentPiece = new AnimalPiece(4, 2); //dog

            mapBoard[0, 8].CurrentPiece = new AnimalPiece(6, 2); //tiger
            mapBoard[6, 8].CurrentPiece = new AnimalPiece(7, 2); //lion

            //Ganti warna Button
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (mapBoard[j, i].IsWater) //air
                    {
                        mapBoard[j, i].TheButton.BackColor = Color.Blue;
                    }
                    else if (mapBoard[j, i].IsTrapP1 || mapBoard[j, i].IsTrapP2) //trap
                    {
                        mapBoard[j, i].TheButton.BackColor = Color.IndianRed;
                        mapBoard[j, i].TheButton.Text = "TRAP";
                        mapBoard[j, i].TheButton.ForeColor = Color.Black;
                        mapBoard[j, i].TheButton.TextAlign = ContentAlignment.BottomCenter;
                    }
                    else if (mapBoard[j, i].IsDen1 || mapBoard[j, i].IsDen2) //home
                    {
                        mapBoard[j, i].TheButton.BackColor = Color.DarkRed;
                        mapBoard[j, i].TheButton.ForeColor = Color.Black;
                        if (mapBoard[j, i].IsDen1) mapBoard[j, i].TheButton.Text = "P1 DEN";
                        else if (mapBoard[j, i].IsDen2) mapBoard[j, i].TheButton.Text = "P2 DEN";
                        mapBoard[j, i].TheButton.TextAlign = ContentAlignment.BottomCenter;
                    }
                    else //board
                    {
                        mapBoard[j, i].TheButton.BackColor = Color.BurlyWood;
                    }
                    
                }

            }
        }
    }
}
