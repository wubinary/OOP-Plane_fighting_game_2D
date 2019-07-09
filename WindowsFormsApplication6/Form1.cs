using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
// 使用Visual Basic
// Project上按右鍵->Add Reference->.Net->Microsoft.VisualBasic
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
// 使用 SystemSound 和 SoundPlayer
using System.Media;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        int u, l, r, ww, a, d,bull1count=0,bull2count=0 ,bullaicount=0, Time=0,min,sec;
        int[] hit1 = new int[5], hit2 = new int[5], hitai = new int[5];      //  [0]: 0 正常情況 1 被射中   [1]:被射中防護罩 時間倒數 500    [2] 0 顯示 1 隱藏    [3] 顯示隱藏時間倒數 10
        int point1 = 0, point2 = 0;
        int blackhole = 0;
        int plane1_over_speed = 0, plane2_over_speed = 0;
        int x, y,b=0 ,q1=0 , q2=0;
        int is_AI_change_hard_mode = 0;
        int backpage=0;
        int random_follow_who = 0;
        float w1 = 3, w2 = 3,wai=3, angle1 = 180, angle2=0 ,angleai=0;     //  旋轉
        float blackhole_x, blackhole_y,blackhole_time;
        float plane1x = 800, plane1y = 300, plane2x = 200, plane2y = 300, speed1x = 0, speed1y = 0, speed2x=0, speed2y=0; //人為操控部分之位置
        float planeaix = 200, planeaiy = 300 , speedaix=0 , speedaiy=0 ; //Ai位置
        float AI_hard=10;
        float point1size_count, point2size_count;        //  得分的時候  分數label變大  倒數時間
        float[,] bull1 = new float[bullmax, 5], bull2 = new float[bullmax, 5];   // bull  [4]   x y  dx dy   angle

        private void clocklabel_Click(object sender, EventArgs e)
        {

        }

       

        float[,] bullai = new float[bullmax, 5]; // AI子彈陣列
        double wtf, wtf1;
        double w_to_pi = 3.14159 / 180;
        bool ai, aibull = false;
        Pen mypen1 = new Pen(Color.Red, 3);
        Pen mypen2 = new Pen(Color.Yellow, 3);
        Image plane1, plane2, planeai, plane3, plane4, black;
        [DllImport("msvcrt")]
        static extern char _getch();
        //static extern char _getch();        
        const int bullmax = 105;
        Image gundam01;
        Image gundam02;


        private void AI_hard_scroll(object sender, ScrollEventArgs e)
        {
           
            AI_hard=e.NewValue;
            label9.Text ="調整遊戲難度  按 ENTER 開始";
            timer5.Interval = (int)( -30*(AI_hard)+3000);
        }

        private void AI_hard_change(object sender, EventArgs e)
        {

        }
        public Form1()
        {
            form1();       
        }  
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(750, 500);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

           ////////////////////////////////////  視窗放大  ////////////////////////////////////////////
            this.WindowState = FormWindowState.Maximized;
            x = this.ClientSize.Width;
            y = this.ClientSize.Height;
            //////////////////////////////////////////////////////////////////////////////////////////
     

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~有黑洞 ???  畫黑洞  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  
            if(blackhole==1)
            {
                e.Graphics.DrawImage(black,blackhole_x-125,blackhole_y-125,250,250);
            }
            ///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            ///////////////////////     時間判斷   graph 畫面   /////////////////////////////////////////////
            if (Time == 0)
            {
                point1 = 0;
                point2 = 0;
                startpage();                                           //   遊戲尚未開始時的畫面 
            }
            else
            {
                if (ai == false)
                {
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~   雙人模式  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~   102 ~ 164
                    if (hit1[2] == 0)
                    {                                                                                      //畫1飛機
                        e.Graphics.TranslateTransform(plane1x, plane1y);
                        e.Graphics.RotateTransform(angle1);
                        if(true)
                            e.Graphics.DrawImage(plane1, -35,-35, 70, 70);
                        /*else if(q1==2)
                            e.Graphics.DrawImage(plane2, -32, -33, 65, 65);
                        else if(q1==3)
                            e.Graphics.DrawImage(plane3, -32, -33, 65, 65);
                        else if(q1==4)
                            e.Graphics.DrawImage(plane4, -32, -33, 65, 65);*/   
                        e.Graphics.RotateTransform(-angle1);
                        e.Graphics.TranslateTransform(-plane1x, -plane1y);
                    }                 
                    if (hit2[2] == 0)
                    {                                                                                      //  畫2飛機
                        e.Graphics.TranslateTransform(plane2x, plane2y);
                        e.Graphics.RotateTransform(angle2);
                        e.Graphics.DrawImage(plane2, -35, -35, 70, 70);                      
                        e.Graphics.RotateTransform(-angle2);
                        e.Graphics.TranslateTransform(-plane2x, -plane2y);
                    }


                    plane1_hit_wall();                                                                  //飛機 1 碰壁判斷
                    plane2_hit_wall();                                                                  //飛機 2 碰壁判斷
                   

                    for (int i = 0; i < bullmax; i++)
                    {
                                                                                                         //   畫1號的子彈
                        e.Graphics.TranslateTransform(bull1[i, 0], bull1[i, 1]);
                        e.Graphics.RotateTransform(bull1[i, 4]);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 50, -bull1[i, 3] * 50, bull1[i, 2] * 50, bull1[i, 3] * 50);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30 + 20, -bull1[i, 3] * 30, bull1[i, 2] * 30 + 20, bull1[i, 3] * 30);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30 - 20, -bull1[i, 3] * 30, bull1[i, 2] * 30 - 20, bull1[i, 3] * 30);
                        e.Graphics.RotateTransform(-bull1[i, 4]);
                        e.Graphics.TranslateTransform(-bull1[i, 0], -bull1[i, 1]);
                        bull1[i, 0] += bull1[i, 2] * 50;
                        bull1[i, 1] += bull1[i, 3] * 50;

                                                                                                          //   畫2號的子彈
                        e.Graphics.TranslateTransform(bull2[i, 0], bull2[i, 1]);
                        e.Graphics.RotateTransform(bull2[i, 4]);
                        e.Graphics.DrawLine(mypen2, -bull2[i, 2] * 50, -bull2[i, 3] * 50, bull2[i, 2] * 50, bull2[i, 3] * 50);
                        e.Graphics.DrawLine(mypen2, -bull2[i, 2] * 30 + 20, -bull2[i, 3] * 30, bull2[i, 2] * 30 + 20, bull2[i, 3] * 30);
                        e.Graphics.DrawLine(mypen2, -bull2[i, 2] * 30 - 20, -bull2[i, 3] * 30, bull2[i, 2] * 30 - 20, bull2[i, 3] * 30);
                        e.Graphics.RotateTransform(-bull2[i, 4]);
                        e.Graphics.TranslateTransform(-bull2[i, 0], -bull2[i, 1]);
                        bull2[i, 0] += bull2[i, 2] * 50;
                        bull2[i, 1] += bull2[i, 3] * 50;

                    }
                    e.Graphics.TranslateTransform(planeaix, planeaiy);                      //畫AI 恐怖 UFO飛機
                    e.Graphics.RotateTransform(angleai);
                    e.Graphics.DrawImage(planeai, -50, -50, 100, 100);
                    e.Graphics.RotateTransform(-angleai);
                    e.Graphics.TranslateTransform(-planeaix, -planeaiy);
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                }  // AI true
                else            
                {
                    /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ AI 模式 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  169 ~ 225
                    if (hit1[2] == 0)
                    {                                                                            //畫1飛機
                        e.Graphics.TranslateTransform(plane1x, plane1y);
                        e.Graphics.RotateTransform(angle1);
                        e.Graphics.DrawImage(plane1, -35, -35, 70,70);                        
                        e.Graphics.RotateTransform(-angle1);
                        e.Graphics.TranslateTransform(-plane1x, -plane1y);                     
                    } 
                                                                                                                   
                        e.Graphics.TranslateTransform(planeaix, planeaiy);                      //畫AI飛機
                        e.Graphics.RotateTransform(angleai);
                        e.Graphics.DrawImage(planeai, -40, -40, 80, 80);
                        e.Graphics.RotateTransform(-angleai);
                        e.Graphics.TranslateTransform(-planeaix, -planeaiy);

                    plane1_hit_wall();                                                      //飛機 1 碰壁判斷
                    plane_AI_hit_wall();                                                    //飛機 AI 碰壁判斷


                    for (int i = 0; i < bullmax; i++)
                    {
                        //   畫1號的子彈
                        e.Graphics.TranslateTransform(bull1[i, 0], bull1[i, 1]);
                        e.Graphics.RotateTransform(bull1[i, 4]);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 50, -bull1[i, 3] * 50, bull1[i, 2] * 50, bull1[i, 3] * 50);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30 + 20, -bull1[i, 3] * 30, bull1[i, 2] * 30 + 20, bull1[i, 3] * 30);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30 - 20, -bull1[i, 3] * 30, bull1[i, 2] * 30 - 20, bull1[i, 3] * 30);
                        e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30, -bull1[i, 3] * 30 + 20, bull1[i, 2] * 30 , bull1[i, 3] * 30+ 20);
                        //e.Graphics.DrawLine(mypen1, -bull1[i, 2] * 30, -bull1[i, 3] * 30 - 20, bull1[i, 2] * 30 , bull1[i, 3] * 30- 20);
                        e.Graphics.RotateTransform(-bull1[i, 4]);
                        e.Graphics.TranslateTransform(-bull1[i, 0], -bull1[i, 1]);
                        bull1[i, 0] += bull1[i, 2] * 50;
                        bull1[i, 1] += bull1[i, 3] * 50;

                        //   畫AI的子彈
                        e.Graphics.TranslateTransform(bullai[i, 0], bullai[i, 1]);
                        e.Graphics.RotateTransform(bullai[i, 4]);
                        e.Graphics.DrawLine(mypen2, -bullai[i, 2] * 50, -bullai[i, 3] * 50, bullai[i, 2] * 50, bullai[i, 3] * 50);
                        e.Graphics.DrawLine(mypen2, -bullai[i, 2] * 30 + 20, -bullai[i, 3] * 30, bullai[i, 2] * 30 + 20, bullai[i, 3] * 30);
                        e.Graphics.DrawLine(mypen2, -bullai[i, 2] * 30 - 20, -bullai[i, 3] * 30, bullai[i, 2] * 30 - 20, bullai[i, 3] * 30);
                        e.Graphics.RotateTransform(-bullai[i, 4]);
                        e.Graphics.TranslateTransform(-bullai[i, 0], -bullai[i, 1]);
                        bullai[i, 0] += bullai[i, 2] * 50;
                        bullai[i, 1] += bullai[i, 3] * 50;

                    }     // for  0 >> bullmax
                    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                }    // ai false

            }  // time != 0

            ////////////////////////////////////////////    時間判斷   graph 畫面   /////////////////////////////////////////////

        }  // Form 1 Paint
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            panel1.Width = this.Width;
            panel1.Height = this.Height;
            button1.Location = new Point(675, 650);
            label1.Location = new Point(550, 200);
            label2.Location = new Point(650, 250);
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            panel3.Location = new Point(0, 0);
            panel3.Width = this.Width;
            panel3.Height = this.Height;
            label7.Location = new Point(this.Width/2-210,100);
            button6.Location = new Point(200, 300);
            button7.Location = new Point(900, 300);
            label5.Visible = false;           
        }
        private void change_hard_paint(object sender, PaintEventArgs e)
        { 
            this.WindowState = FormWindowState.Maximized;
            panel4.Width = this.Width;
            panel4.Height = this.Height;
            label9.Location = new Point(740,250);
            label10.Location = new Point(625, 300);
            hScrollBar1.Location = new Point(625, 320);           
        }
        private void button8_Click(object sender, EventArgs e)
        {          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1click();           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2click();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button3click();          
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button4click();            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button5click();           
        }       
        private void button6_Click(object sender, EventArgs e)
        {
            button6click();         
        }
        private void button7_Click(object sender, EventArgs e)
        {
            button7click();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {            
                if (e.KeyCode == Keys.Up)
                {
                    u = 1;
                }
                if (e.KeyCode == Keys.Left)
                {
                    l = 1;
                }
                if (e.KeyCode == Keys.Right)
                {
                    r = 1;
                }

            if (ai == false)
            {
                if (e.KeyCode == Keys.W)
                {
                    ww = 1;
                }
                if (e.KeyCode == Keys.A)
                {
                    a = 1;
                }
                if (e.KeyCode == Keys.D)
                {
                    d = 1;
                }
            }
                this.Invalidate();
                 e.SuppressKeyPress = true;
        }
        private void Form1_KeyUp(object sebder, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Up)
            {
                u = 0;
            }
            if (e.KeyCode == Keys.Left)
            {
                l = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                r = 0;
            }           
            if (ai == false)
            {
                if (e.KeyCode == Keys.W)
                {
                    ww = 0;
                }
                if (e.KeyCode == Keys.A)
                {
                    a = 0;
                }
                if (e.KeyCode == Keys.D)
                {
                    d = 0;
                }              
            }         
            if (is_AI_change_hard_mode == 1 && e.KeyCode == Keys.Enter)
            {
                panel4.Visible = false;
                clocklabel.Visible = true;
                label5.Visible = true;
                is_AI_change_hard_mode = 0;
            }         
            if (e.KeyCode == Keys.Enter && Time < 1)
            {
                Time = 150;
                plane1x = x - 300;
                plane1y = 300;
                speed1x = 0;
                speed1y = 0;
                w1 = 3;
                angle1 = 180;
                if (ai == false)
                {
                    plane2x = 200;
                    plane2y = 300;
                    speed2x = 0;
                    speed2y = 0;
                    angle2 = 0;
                    w2 = 3;
                }
                else
                {
                    planeaix = 200;
                    planeaiy = 300;
                    speedaix = 0;
                    speedaiy = 0;
                    angleai = 0;
                    wai = 3;
                }
            }   //  e.KeyCode==Keys.Enter &&  Time<1
            if (e.KeyCode == Keys.P)
            {
                panel3.Visible = true;
                panel3.Location = new Point(0, 0);
                button6.Enabled = true;
                button7.Enabled = true;
                Time = 0;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            everything_move();             // 每一樣東西的 x y 加上  dx dy   
            if (blackhole == 1)
            {            
                    blackhole_move_plane();                        //     黑洞造成的移動             
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            // 此 timer 是用來判斷是否被射中 並閃爍
            //  hit1[5]   >>>>>>   [0]: 0 正常情況 1 被射中   [1]:被射中防護罩 時間倒數 500    [2] 0 顯示 1 隱藏    [3] 顯示隱藏時間倒數 10
            shoot_case1();
            shoot_case2();
            show_point();                    
            this.Invalidate();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Time > 0)
            {
                clocklabel.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                Time--;
                min = Time / 60;
                sec = Time % 60;
                clocklabel.Text = "Time " + min.ToString() + " : " + sec.ToString()+"按 P 跳出";
                clocklabel.Location = new Point(x/2 - clocklabel.Width/2, 0);
            }

            blackholefunction();         //黑洞 function           
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            aibull = true;
            
            if (ai == false)
            {
                bull2[bull2count, 0] = plane2x;
                bull2[bull2count, 1] = plane2y;
                bull2[bull2count, 2] = (float)(Math.Sin((angle2 - 90) * w_to_pi) * 0.06) * (float)1.5;
                bull2[bull2count, 3] = -(float)(Math.Cos((angle2 - 90) * w_to_pi) * 0.06) * (float)1.5;
                bull2[bull2count, 4] = angle2;
                bull2count = (bull2count + 1) % bullmax;
            }
            if (true)
            {
                bull1[bull1count, 0] = plane1x;
                bull1[bull1count, 1] = plane1y;
                bull1[bull1count, 2] = (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.06) * (float)1.5;
                bull1[bull1count, 3] = -(float)(Math.Cos((angle1 - 90) * w_to_pi) * 0.06) * (float)1.5;
                bull1[bull1count, 4] = angle1;
                bull1count = (bull1count + 1) % bullmax;
            }
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (ai == true)
            {
                bullai[bullaicount, 0] = planeaix;
                bullai[bullaicount, 1] = planeaiy;
                bullai[bullaicount, 2] = (float)(((plane1x - planeaix) / wtf) * 0.06) * (float)1.5;
                bullai[bullaicount, 3] = (float)(((plane1y - planeaiy) / wtf) * 0.06) * (float)1.5;
                bullai[bullaicount, 4] = angleai;
                bullaicount = (bullaicount + 1) % bullmax;
            }
            aibull = false;
            random_follow_who = fixrand.Next(1,50);
        }
        private void xy_move_timer_Tick(object sender, EventArgs e)
        {
            plane1x += speed1x;
            plane1y += speed1y;

            if (ai == false)
            {
                plane2x += speed2x ;
                plane2y += speed2y ;
                planeaix += speedaix * 50;
                planeaiy += speedaiy * 50;
            }
            else
            {
                planeaix += speedaix * (AI_hard / 100);
                planeaiy += speedaiy * (AI_hard / 100);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void label6_Click(object sender, EventArgs e)
        {
        }
      }
}
