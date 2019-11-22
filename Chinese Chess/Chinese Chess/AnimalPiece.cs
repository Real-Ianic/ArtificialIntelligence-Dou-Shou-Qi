using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Chinese_Chess
{
    class AnimalPiece
    {
        private string name;
        private int strength;
        private int maxstrength;
        private bool canJump;
        private bool canSwim;
        private int player;
        private Image icon;
        

        

        public AnimalPiece(int strength,int player)
        {
            this.Strength = strength;
            this.player = player;
            this.maxstrength = strength;
            if(strength == 1)
            {
                //Its a fookin mouse
                Name = "Mouse" + "\n\n\n\n(player " + player  + ")";
                icon = Image.FromFile("animalicon/mouse.png");
                CanJump = false;
                CanSwim = true;
            }
            else if(strength == 2)
            {
                //Its a neko (cat)
                Name = "Cat" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/cat.png");

                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 3)
            {
                //Its a wolf
                Name = "Wolf" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/wolf.png");
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 4)
            {
                //Its a dog, bark bark
                Name = "Dog" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/dog.png");
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 5)
            {
                //Leopard / panther, idk i say leopard
                Name = "Leopard" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/leopard.png");
                CanJump = false;
                CanSwim = false;
            }
            else if(strength == 6)
            {
                //Tiger
                Name = "Tiger" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/tiger.png");
                CanJump = true;
                CanSwim = false;
            }
            else if(strength == 7)
            {
                //Lion
                Name = "Lion" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/lion.png");
                CanJump = true;
                CanSwim = false;
            }
            else if(strength == 8)
            {
                //Its hadoop
                Name = "Elephant" + "\n\n\n\n(player " + player + ")";
                icon = Image.FromFile("animalicon/elephant.png");
                CanJump = false;
                CanSwim = false;
            }
            else
            {
                Console.WriteLine("NANI KORE");
                Name = "ERROR404 - ANIMAL NOT FOUND";
            }
        }

        //Returns The Highest 
        public bool checkEat(AnimalPiece tobeEaten)
        {
            if (this.strength >= tobeEaten.strength) return true;
            else return false;
        }

        public string Name { get => name; set => name = value; }
        public int Strength { get => strength; set => strength = value; }
        public int maxStrength { get => maxstrength; set => maxstrength = value; }
        public bool CanJump { get => canJump; set => canJump = value; }
        public bool CanSwim { get => canSwim; set => canSwim = value; }
        public Image Icon { get => icon; set => icon = value; }
        public int Player { get => player; }
    }
}

