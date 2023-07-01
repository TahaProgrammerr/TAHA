using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheFighterJet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        //Programmed by taha in the first third month of learning C# 
        Timer GameTimer = new Timer();
        Timer Speed = new Timer();
        Random R = new Random();
        DialogResult d;
        PictureBox alien;
        PictureBox alien2;
        PictureBox bullet;
        Label Rocket;
        int count = 0;
        int score = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            //---------main-design------------//
            Airplane.BackColor = Color.DodgerBlue;
            Airplane.Location = new Point(93, 189);
            this.BackColor = Color.DodgerBlue;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            Life.BackColor = Color.DodgerBlue;
            //------End-main-design----------//
            //------MessageBox----------//
   
            //------End-MessageBox--------//
            const int b = 3;
            for (int i = 1; i <= b; i++)
            {
                this.Controls.Add(bullet);
                bullet = new PictureBox();
                bullet.Image = Image.FromFile(Application.StartupPath + "\\bullet2.jpg");
                bullet.Width = 30; bullet.Height = 30;
                bullet.SizeMode = PictureBoxSizeMode.Zoom;
                int x = this.Width - bullet.Width;
                int y = R.Next(bullet.Height + this.ClientSize.Height);
                bullet.Location = new Point(x, y);
                bullet.Name = "bullet";
            }
            const int L = 1;
            for (int i = 1; i <= L; i++)
            {
                alien = new PictureBox();
                this.Controls.Add(alien);
                int x = this.Width - alien.Width;
                int y = R.Next(alien.Height + this.ClientSize.Height);
                alien.Location = new Point(x, y);
                alien.Image = Image.FromFile(Application.StartupPath + "\\alien.gif");
                alien.SizeMode = PictureBoxSizeMode.Zoom;
                alien.Width = 40; alien.Height = 40;
                alien.Name = "alien";
            }
            for (int i = 1; i <= L; i++)
            {
                alien2 = new PictureBox();
                this.Controls.Add(alien2);
                int x = this.Width - alien2.Width;
                int y = R.Next(alien2.Height + this.ClientSize.Height);
                alien2.Location = new Point(x, y);
                alien2.Image = Image.FromFile(Application.StartupPath + "\\alien2.gif");
                alien2.SizeMode = PictureBoxSizeMode.Zoom;
                alien2.Width = 40; alien2.Height = 40;
                alien2.Name = "alien2";
            }
            //-----------------------------
            d = MessageBox.Show("Destroy 20 alien to win!", "Start the game?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (d == DialogResult.OK)
            {
                GameTimer.Start();
                GameTimer.Interval = 70;
                Speed.Start();
                Speed.Interval = 1500;//1.5 Sec;
            }
            //----------------------------
            GameTimer.Tick += GameTimer_Tick;
            this.KeyDown += Form1_KeyDown;
            Speed.Tick += Speed_Tick;
        }
        private void Speed_Tick(object sender, EventArgs e)
        {
            GameTimer.Interval -= 2;
            if (GameTimer.Interval < 20)
            {
                GameTimer.Interval = 20;
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is Label && item.Name == "Rocket")
                {
                    item.Left += 20;

                    if (item.Bounds.IntersectsWith(hurdle1.Bounds) || item.Bounds.IntersectsWith(hurdle2.Bounds))
                    {
                        item.Hide();
                        item.Left = Airplane.Left + Airplane.Width;
                        item.Top = Airplane.Top + Airplane.Height / 2; item.Top -= 7;
                    }
                    else if (item.Bounds.IntersectsWith(alien.Bounds))
                    {
                        score++;
                        label1.Text = score.ToString();
                        alien.Left = this.ClientSize.Width;
                        int x = this.Width - alien.Width;
                        int y = R.Next(0, this.Height - alien.Height);
                        alien.Location = new Point(x, y);
                        item.Hide();
                        item.Left = Airplane.Left + Airplane.Width;
                        item.Top = Airplane.Top + Airplane.Height / 2; item.Top -= 7;
                    }
                    else if (item.Bounds.IntersectsWith(alien2.Bounds))
                    {
                        score++;
                        label1.Text = score.ToString();
                        alien2.Left = this.ClientSize.Width;
                        int x = this.Width - alien2.Width;
                        int y = R.Next(0, this.Height - alien2.Height);
                        alien2.Location = new Point(x, y);
                        item.Hide();
                        item.Left = Airplane.Left + Airplane.Width;
                        item.Top = Airplane.Top + Airplane.Height / 2; item.Top -= 7;
                    }
                    if (label1.Text == "" + 20)
                    {
                        GameTimer.Stop();
                        Speed.Stop();
                        DialogResult D = MessageBox.Show("You Win,do you wanna start again?", "Winner", MessageBoxButtons.YesNo
                            , MessageBoxIcon.Asterisk);
                        if (D == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        else
                            this.Close();
                    }

                }
                if (item is PictureBox && item.Name.StartsWith("h"))
                {
                    item.Left -= 10;
                    if (item.Left < 0)
                    {
                        item.Left = this.ClientSize.Width;
                    }
                    else if (item.Bounds.IntersectsWith(Airplane.Bounds))
                    {
                        GameTimer.Stop();
                        DialogResult d = MessageBox.Show("You lose,wanna try again?", "Lose",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (d == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        else
                            this.Close();
                    }
                }
                if (item is PictureBox && item.Name == "alien")
                {
                    item.Left -= 5;
                    if (item.Bounds.IntersectsWith(hurdle1.Bounds) || item.Bounds.IntersectsWith(hurdle2.Bounds))
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(Airplane.Bounds))
                    {
                        GameTimer.Stop();
                        DialogResult d = MessageBox.Show("You lose,wanna try again?", "Lose",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (d == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        else
                            this.Close();
                    }
                }
                else if (item is PictureBox && item.Name == "alien2")
                {
                    item.Left -= 5;
                    if (item.Bounds.IntersectsWith(hurdle1.Bounds) || item.Bounds.IntersectsWith(hurdle2.Bounds))
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(alien.Bounds))
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(Airplane.Bounds))
                    {
                        GameTimer.Stop();
                        DialogResult d = MessageBox.Show("You lose,wanna try again?", "Lose",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (d == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        else
                            this.Close();
                    }
                }
                else if (item is PictureBox && item.Name == "bullet")
                {
                    item.Left -= 15;
                    if (item.Left < 0)
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(hurdle1.Bounds) || item.Bounds.IntersectsWith(hurdle2.Bounds))
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(Airplane.Bounds) && item.Left <= Airplane.Left || item.Top == Airplane.Left
                         && item.Top == Airplane.Bottom)
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                        count++;
                        Life.Text = count.ToString();
                        if (Life.Text == "" + 6)
                        {
                            DialogResult d = MessageBox.Show("You lose,wanna try again?", "Lose",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (d == DialogResult.Yes)
                            {
                                Application.Restart();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                    }
                    else if (item.Bounds.IntersectsWith(alien.Bounds) || item.Bounds.IntersectsWith(alien2.Bounds))
                    {
                        item.Left = this.ClientSize.Width;
                        int x = this.Width - item.Width;
                        int y = R.Next(0, this.Height - item.Height);
                        item.Location = new Point(x, y);
                        //-------------------------------
                    }
                    else if (item.Bounds.IntersectsWith(alien.Bounds))
                    {
                        alien.Left = this.ClientSize.Width;
                        int x = this.Width - alien.Width;
                        int y = R.Next(0, this.Height - alien.Height);
                        alien.Location = new Point(x, y);
                    }
                    else if (item.Bounds.IntersectsWith(alien2.Bounds))
                    {
                        alien2.Left = this.ClientSize.Width;
                        int x = this.Width - alien2.Width;
                        int y = R.Next(0, this.Height - alien2.Height);
                        alien2.Location = new Point(x, y);
                    }
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                GameTimer.Stop();
                DialogResult d = MessageBox.Show("Close the game?", "Exit", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                    GameTimer.Start();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (Airplane.Top > 0)
                {
                    Airplane.Top -= 5;

                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (Airplane.Top + Airplane.Height < this.ClientSize.Height)
                {
                    Airplane.Top += 5;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                Rocket = new Label();
                Rocket.Name = "Rocket";
                Rocket.Width = 10; Rocket.Height = 5;
                Rocket.BackColor = Color.DarkOrange;
                this.Controls.Add(Rocket);
                Rocket.Left = Airplane.Left + Airplane.Width;
                Rocket.Top = Airplane.Top + Airplane.Height / 2; Rocket.Top -= 7;

            }

        }
    }
}
