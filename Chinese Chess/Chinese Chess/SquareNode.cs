using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    class SquareNode
    {
        private AnimalPiece currentPiece;
        private bool isWater, isDen, isTrap;
        private Button theButton;

        public SquareNode(bool isWater, bool isDen, bool isTrap)
        {
            theButton = new Button();
        }

        public bool IsWater { get => isWater; set => isWater = value; }
        public bool IsDen { get => isDen; set => isDen = value; }
        public bool IsTrap { get => isTrap; set => isTrap = value; }
        internal AnimalPiece CurrentPiece { get => currentPiece; set => currentPiece = value; }
        public Button TheButton { get => theButton; set => theButton = value; }
    }
}
