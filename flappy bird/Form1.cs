using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;    

namespace flappy_bird
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 7;
        int gravity = 15;
        int score = 0;

        bool gameOver = false;
        
        public Form1()
        {
            InitializeComponent();  
        }

        protected override void OnFormClosing(FormClosingEventArgs eventArgs)
        {
            var result = MessageBox.Show("Выйти из игры  ?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                eventArgs.Cancel = true;
        }

        Random r = new Random();
        private void gameTimerEvent(object sender, EventArgs e)
        {
            bird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;


            if(pipeTop.Left < -100)
            {
                pipeTop.Height = r.Next(100, 300);
                pipeTop.Left = 800;
                score++;
            }
            
            if(pipeBottom.Left < -100)
            {
                pipeBottom.Top = pipeTop.Height+250;
                pipeBottom.Left = 800;
                
            }

            if (bird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                (bird.Bounds.IntersectsWith(pipeTop.Bounds)) ||
                (bird.Bounds.IntersectsWith(ground.Bounds)) ||
                (bird.Top < -25))
            {
                endGame();
            }
             

            if(score >= 5)
            {
                pipeSpeed = 15;   
            }
        }

        private void gameKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
        }

        private void gameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 15;
            }

            if(e.KeyCode == Keys.R && gameOver)
            {
                restartGame();
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game over(( Press R to retry";
            gameOver = true;

        }
        private void restartGame()
        {
            gameOver = false;
            bird.Location = new Point(207, 315);
            pipeTop.Left = 800;
            pipeBottom.Left = 800;

            score = 0;
            pipeSpeed = 7;
            scoreText.Text = "Score: 0";
            gameTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pipeBottom_Click(object sender, EventArgs e)
        {

        }
    }
}