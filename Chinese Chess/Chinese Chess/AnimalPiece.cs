using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinese_Chess
{
    class AnimalPiece
    {
        private string name;
        private int strength;
        private bool canJump;
        private bool canSwim;
        private int player;

        public AnimalPiece(int strength,int player)
        {
            this.Strength = strength;
            this.player = player;
            if(strength == 1)
            {
                //Its a fookin mouse
                Name = "Rat";
                CanJump = false;
                CanSwim = true;
            }
            else if(strength == 2)
            {
                //Its a neko (cat)
                Name = "Cat";
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 3)
            {
                //Its a wolf
                Name = "Wolf";
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 4)
            {
                //Its a dog, bark bark
                Name = "Dog";
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 5)
            {
                //Leopard / panther, idk i say leopard
                Name = "Leopard";
                CanJump = true;
                CanSwim = false;
            }
            else if(strength == 6)
            {
                //Tiger
                Name = "Tiger";
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 7)
            {
                //Lion
                Name = "Lion";
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 8)
            {
                //Its hadoop
                Name = "Elephant";
                CanJump = false;
                CanSwim = false;
            }
            else
            {
                Console.WriteLine("NANI KORE");
                Name = "ERROR404 - ANIMAL NOT FOUND";
            }
        }

        public string Name { get => name; set => name = value; }
        public int Strength { get => strength; set => strength = value; }
        public bool CanJump { get => canJump; set => canJump = value; }
        public bool CanSwim { get => canSwim; set => canSwim = value; }
    }
}
