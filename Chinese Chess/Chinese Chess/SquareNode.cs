using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chinese_Chess
{
    class SquareNode
    {
        private AnimalPiece currentPiece;
        private bool isWater, isDen1, isDen2, isTrapP1, isTrapP2;
        private Button theButton;

        public SquareNode(bool isWater, bool isDen, bool isTrapP1, bool isTrapP2)
        {
            TheButton = new Button();
        }

        public bool IsWater { get => isWater; set => isWater = value; }
        public bool IsDen1 { get => isDen1; set => isDen1 = value; }
        public bool IsDen2 { get => isDen2; set => isDen2 = value; }
        public bool IsTrapP1 { get => isTrapP1; set => isTrapP1 = value; }
        public bool IsTrapP2 { get => isTrapP2; set => isTrapP2 = value; }
        public Button TheButton { get => theButton; set => theButton = value; }
        internal AnimalPiece CurrentPiece { get => currentPiece; set => currentPiece = value; }

        public void setbg(Button n, Image x)
        {
            n.BackgroundImage = x;
            n.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
