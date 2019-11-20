using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chinese_Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.MdiParent = this;
            menu.Show();
        }

        public void NewGame(String gameType)
        {
            Game newGame = new Game(gameType);
            newGame.MdiParent = this;
            newGame.Show();
        }
    }
}
