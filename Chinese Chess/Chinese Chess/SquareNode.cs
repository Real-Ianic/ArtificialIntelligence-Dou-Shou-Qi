using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    class SquareNode
    {
        private AnimalPiece currentPiece;
        private bool isWater;

        public SquareNode(bool isWater)
        {
            isWater = isWater;
        }

        public bool IsWater { get => isWater; set => isWater = value; }
        internal AnimalPiece CurrentPiece { get => currentPiece; set => currentPiece = value; }
    }
}
