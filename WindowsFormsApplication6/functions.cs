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
        Random fixrand = new Random();
        int randomnum;
        public void form1()
        {
            InitializeComponent();
            panel1.Location = new Point(0, 0);           
            plane1 = new Bitmap(Properties.Resources.plane_noback);
            plane2 = new Bitmap(Properties.Resources.戰機02去背);
            plane3 = new Bitmap(Properties.Resources.戰機03去背);
            plane4 = new Bitmap(Properties.Resources.戰機04去背);
            planeai = new Bitmap(Properties.Resources.fight01);
            black = new Bitmap(Properties.Resources.黑洞);
        }
        public void button1click()
        {
            panel1.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(0, 0);
            button1.Enabled = false;
        }
        public void button2click()
        {           
        }
        public void button3click()
        {           
        }
        public void button4click()
        {            
        }
        public void button5click()
        {           
        }
        public void button6click()
        {
            ai = true;
            panel3.Visible = false;            
            button6.Enabled = false;
            button7.Enabled = false;
            panel4.Visible = true;
            panel4.Location = new Point(0, 0);
            is_AI_change_hard_mode = 1;

        }
        public void button7click()
        {
            ai = false;
            panel3.Visible = false;
            button6.Enabled = false;
            button7.Enabled = false;
            clocklabel.Visible = true;
            label5.Visible = true;
        }
        public void startpage()
        {
            plane1x = x - 300;
            clocklabel.Text = "按 Enter 開始  按 P 回上夜";
            clocklabel.Font = new Font(FontFamily.GenericSansSerif, 76, FontStyle.Bold);
            clocklabel.Location = new Point(x / 2 - clocklabel.Width / 2, 0);
        }
        public void plane1_hit_wall()
        {
            if (plane1x > x || plane1x < 0 || plane1y > y || plane1y < 0)           //飛機 1 碰壁判斷
            {
                if (plane1x > x - 3)
                {
                    plane1x = x - 3;
                }
                if (plane1x < 3)
                {
                    plane1x = 3;
                }
                if (plane1y > y - 3)
                {
                    plane1y = y - 3;
                }
                if (plane1y < 3)
                {
                    plane1y = 3;
                }
             //   R_text_label.Text = "撞牆囉";
            }
            else if (plane1_over_speed != 0)
            {
              //  R_text_label.Text = " 超速! 超速! 扣分數!";
            }
            else
            {
             //   R_text_label.Text = "加油好嗎";
            }
        }
        public void plane2_hit_wall()
        {
            if (plane2x > x || plane2x < 0 || plane2y > y || plane2y < 0)
            {
                if (plane2x > x - 3)
                {
                    plane2x = x - 3;
                }
                if (plane2x < 3)
                {
                    plane2x = 3;
                }
                if (plane2y > y - 3)
                {
                    plane2y = y - 3;
                }
                if (plane2y < 3)
                {
                    plane2y = 3;
                }
              //  L_text_label.Text = "撞牆囉";
            }
            else if (plane2_over_speed != 0)
            {
              //  L_text_label.Text = " 超速! 超速! 扣分數!";
            }
            else
            {
              //  L_text_label.Text = "加油好嗎";
            }
        }
        public void plane_AI_hit_wall()
        {
            if (planeaix > x || planeaix < 0 || planeaiy > y || planeaiy < 0)  //飛機 AI 碰壁判斷
            {
                if (planeaix > x - 3)
                {
                    planeaix = x - 3;
                }
                if (planeaix < 3)
                {
                    planeaix = 3;
                }
                if (planeaiy > y - 3)
                {
                    planeaiy = y - 3;
                }
                if (planeaiy < 3)
                {
                    planeaiy = 3;
                }
            }
        }
        public void shoot_case1()
        {
            if (hit1[0] == 1 && hit1[1] >= 0)                //  飛機 1 是否被射中
            {
                // 分數label變大  時間設為200  可以倒數
                hit1[1]--;                                    // 300 倒數
                if (hit1[3] > 0)                              // 顯示  或是  隱藏   的時間間格倒數
                {
                    hit1[3]--;
                }
                else
                {
                    hit1[2] = (hit1[2] + 1) % 2;
                    hit1[3] = 10;
                }
            }
            else
            {
                hit1[0] = 0;
                hit1[2] = 0;
                point2size_count = 410;                         //   飛機 1 被射中  2 號得分  字體放大
            }
            if (hit1[0] == 1 && point2size_count > 10)                              //    飛機 1 被射中  2 號得分  字體變小 動畫
            {
                point2size_count -= 20;
                label6.Font = new Font(FontFamily.GenericSansSerif, 24 * point2size_count / 10, FontStyle.Bold);
            }
        }
        public void shoot_case2()
        {
            if (ai == false)
            {
                if (hit2[0] == 1 && hit2[1] >= 0)        //  飛機 2 是否被射中   
                {
                    hit2[1]--;                   // 5 秒鐘倒數
                    if (hit2[3] > 0)                // 顯示  或是  隱藏   的時間間格倒數
                    {
                        hit2[3]--;
                    }
                    else
                    {
                        hit2[2] = (hit2[2] + 1) % 2;
                        hit2[3] = 10;
                    }
                }
                else
                {
                    hit2[0] = 0;
                    hit2[2] = 0;
                    point1size_count = 410;                         //   飛機 2 被射中  1 號得分  字體放大
                }
                if (hit2[0] == 1 && point1size_count > 10)                              //    飛機 2 被射中  1 號得分  字體變小 動畫
                {
                    point1size_count -= 20;
                    label5.Font = new Font(FontFamily.GenericSansSerif, 24 * point1size_count / 10, FontStyle.Bold);
                }
            }
            else
            {
                if (hitai[0] == 1 && hitai[1] >= 0)        //  飛機 ai 是否被射中   
                {
                    hitai[1]--;                   // 5 秒鐘倒數
                    if (hitai[3] > 0)                // 顯示  或是  隱藏   的時間間格倒數
                    {
                        hitai[3]--;
                    }
                    else
                    {
                        hitai[2] = (hitai[2] + 1) % 2;
                        hitai[3] = 10;
                    }
                }
                else
                {
                    hitai[0] = 0;
                    hitai[2] = 0;
                    point1size_count = 410;                         //   飛機 ai 被射中  1 號得分  字體放大
                }
                if (hitai[0] == 1 && point1size_count > 10)                              //    飛機 ai 被射中  1 號得分  字體變小 動畫
                {
                    point1size_count -= 20;
                    label5.Font = new Font(FontFamily.GenericSansSerif, 24 * point1size_count / 10, FontStyle.Bold);
                }
            }
        }
        public void show_point()
        {
     //       R_text_label.Location = new Point(x - label5.Width- R_text_label.Width, 0);
     //       L_text_label.Location = new Point(label6.Width + L_text_label.Width, 0);
            label5.Location = new Point(x - label5.Width, 0);
            label6.Location = new Point(0, 0);
            label5.Text = point1.ToString();
            label6.Text = point2.ToString();
        }
        public void everything_move()
        {
            if (u == 1)
            {
                if (plane1_over_speed == 0)          //  沒超速  speedx speedy  +    (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.05)
                {
                    speed1x += (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.05);
                    speed1y += -(float)(Math.Cos((angle1 - 90) * w_to_pi) * 0.05);
                }
                else                                //  超速    speedx speedy  -    (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.05)
                {
                    speed1x /= (float)1.009;
                    speed1y /= (float)1.009;                  
                   // point1--;
                }

                if (speed1x * speed1x + speed1y * speed1y > 49)          //   判斷是否超速
                {
                    plane1_over_speed = 1;
                }
                if (speed1x * speed1x + speed1y * speed1y < 2)             //   判斷是否過慢
                {
                    plane1_over_speed = 0;
                }               

            }
            if (l == 1)
            {
                angle1 += w1;
            }
            if (r == 1)
            {
                angle1 -= w1;
            }
           
            if (ai == false)
            {
                for (int i = 0; i < bullmax; i++)                       //    子彈 1 for 迴圈
                {
                    if (bull1[i, 0] < x && bull1[i, 0] > 0 && bull1[i, 1] > 0 && bull1[i, 1] < y)         // 子彈 1 飛出界外
                    {
                        bull1[i, 0] += bull1[i, 2];
                        bull1[i, 1] += bull1[i, 3];
                        bull1[i, 4] += 75;
                    }
                    else
                    {
                        bull1[i, 0] = -100;
                        bull1[i, 1] = -100;
                    }
                    if (((bull1[i, 0] - plane2x) * (bull1[i, 0] - plane2x)) + ((bull1[i, 1] - plane2y) * (bull1[i, 1] - plane2y)) < 1000 && hit2[0] == 0)   //子彈 1 命中 飛機 2
                    {
                        hit2[0] = 1;
                        hit2[1] = 300;
                        point1+=200;
                        //  Thread.Sleep(1000);
                    }
                }
                if (ww == 1)
                {
                    if (plane2_over_speed == 0)          //  沒超速  speedx speedy  +    (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.05)
                    {
                        speed2x += (float)(Math.Sin((angle2 - 90) * w_to_pi) * 0.05);
                        speed2y += -(float)(Math.Cos((angle2 - 90) * w_to_pi) * 0.05);
                    }
                    else                                //  超速    speedx speedy  -    (float)(Math.Sin((angle1 - 90) * w_to_pi) * 0.05)
                    {
                        speed2x /= (float)1.009;
                        speed2y /= (float)1.009;
                   //     L_text_label.Text += " 超速! 超速! 扣分數!";
                        //point2--;
                    }

                    if (speed2x * speed2x + speed2y * speed2y > 49)          //   判斷是否超速
                    {
                        plane2_over_speed = 1;
                    }
                    if (speed2x * speed2x + speed2y * speed2y < 2)             //   判斷是否過慢
                    {
                        plane2_over_speed = 0;
                    }
                }
                if (a == 1)
                {
                    angle2 += w2;
                }
                if (d == 1)
                {
                    angle2 -= w2;
                }
                for (int i = 0; i < bullmax; i++)                       //    子彈 2 for 迴圈
                {
                    if (bull2[i, 0] < x && bull2[i, 0] > 0 && bull2[i, 1] > 0 && bull2[i, 1] < y)         // 子彈 2 飛出界外
                    {
                        bull2[i, 0] += bull2[i, 2];
                        bull2[i, 1] += bull2[i, 3];
                        bull2[i, 4] += 75;
                    }
                    else
                    {
                        bull2[i, 0] = -100;
                        bull2[i, 1] = -100;
                    }
                    if (((bull2[i, 0] - plane1x) * (bull2[i, 0] - plane1x)) + ((bull2[i, 1] - plane1y) * (bull2[i, 1] - plane1y)) < 1000 && hit1[0] == 0)   //子彈 2 命中  飛機 1
                    {
                        hit1[0] = 1;
                        hit1[1] = 300;
                        point2+=200;
                        // Thread.Sleep(1000);
                    }
                }
                //////////////////////////////  1 VS 1  UFO  亂移動   //////////////////////////////////
                angleai = fixrand.Next(0, 75);
                if (random_follow_who%2 == 1)
                {
                    wtf1 = (plane1x - planeaix) * (plane1x - planeaix) + (plane1y - planeaiy) * (plane1y - planeaiy);
                    wtf = Math.Sqrt(wtf1);                   
                }
                else
                {
                    wtf1 = (plane1x - planeaix) * (plane1x - planeaix) + (plane1y - planeaiy) * (plane1y - planeaiy);
                    wtf = Math.Sqrt(wtf1);
                }               
                if (speedaix * speedaix + speedaiy * speedaiy < 25)
                {
                    if (random_follow_who%2 == 1)
                    {
                        speedaix += (float)(((plane1x - planeaix) / wtf) * 0.04);
                        speedaiy += (float)(((plane1y - planeaiy) / wtf) * 0.04);
                    }
                    else
                    {
                        speedaix += (float)(((plane2x - planeaix) / wtf) * 0.04);
                        speedaiy += (float)(((plane2y - planeaiy) / wtf) * 0.04);
                    }
                }
                else
                {
                    speedaix--;
                    speedaiy--;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                for (int i = 0; i < bullmax; i++)                       //    子彈 1 for 迴圈
                {
                    if (bull1[i, 0] < x && bull1[i, 0] > 0 && bull1[i, 1] > 0 && bull1[i, 1] < y)         // 子彈 1 飛出界外
                    {
                        bull1[i, 0] += bull1[i, 2];
                        bull1[i, 1] += bull1[i, 3];
                        bull1[i, 4] += 75;
                    }
                    else
                    {
                        bull1[i, 0] = -100;
                        bull1[i, 1] = -100;
                    }
                    if (((bull1[i, 0] - planeaix) * (bull1[i, 0] - planeaix)) + ((bull1[i, 1] - planeaiy) * (bull1[i, 1] - planeaiy)) < 1000 && hitai[0] == 0)   //子彈 1 命中 飛機 ai
                    {
                        hitai[0] = 1;
                        hitai[1] = 300;
                        point1+=200;
                        //  Thread.Sleep(1000);
                    }
                }
                randomnum = fixrand.Next(0,75);
                angleai = randomnum;               
                
                wtf1 = (plane1x - planeaix) * (plane1x - planeaix) + (plane1y - planeaiy) * (plane1y - planeaiy);
                wtf = Math.Sqrt(wtf1);
                if (ai == true)
                {
                    if (wtf < 90)
                    {
                        point1--;
                    }
                    if (wtf < 150)
                    {
                        plane1 = new Bitmap(Properties.Resources.blood);
                    }
                    else
                    {
                        plane1 = new Bitmap(Properties.Resources.plane_noback);
                    }
                }

                    if (speedaix * speedaix + speedaiy * speedaiy < 25)
                    {

                        speedaix += (float)(((plane1x - planeaix) / wtf) * 0.04);
                        speedaiy += (float)(((plane1y - planeaiy) / wtf) * 0.04);
                    }
                    else
                    {
                        if (a == 1 || d == 1)
                        {
                            speedaix += (float)(Math.Sin(angleai - angle2 - 90) * 0.04);
                            speedaiy += (float)(Math.Cos(angleai - angle2 - 90) * 0.04);
                        }
                        else
                        {
                            speedaix = (float)(((plane1x - planeaix) / wtf) * 4.5);
                            speedaiy = (float)((((plane1y - planeaiy) / wtf)) * 4.5);
                        }
                    }
                
                

                for (int i = 0; i < bullmax; i++)                       //    子彈 ai for 迴圈
                {
                    if (bullai[i, 0] < x && bullai[i, 0] > 0 && bullai[i, 1] > 0 && bullai[i, 1] < y)         // 子彈 ai 飛出界外
                    {
                        bullai[i, 0] += bullai[i, 2];
                        bullai[i, 1] += bullai[i, 3];
                        bullai[i, 4] += 75;
                    }
                    else
                    {
                        bullai[i, 0] = -100;
                        bullai[i, 1] = -100;
                    }
                    if (((bullai[i, 0] - plane1x) * (bullai[i, 0] - plane1x)) + ((bullai[i, 1] - plane1y) * (bullai[i, 1] - plane1y)) < 500 && hit1[0] == 0)   //子彈 ai 命中  飛機 1
                    {
                        hit1[0] = 1;
                        hit1[1] = 300;
                        point2+=200;
                        // Thread.Sleep(1000);
                    }
                }
            }
        }
        public void blackholefunction()
        {
            if (blackhole==0)
            {
                //  random
                randomnum = fixrand.Next(50, 2000);
                blackhole_x = randomnum;
                randomnum = fixrand.Next(50, 800);              
                blackhole_y = randomnum;
                blackhole_time = 15;
                randomnum = fixrand.Next(1, 10);
                if(randomnum==1)
                    blackhole = 1;
            }
            else
            {
                blackhole_time--;               
            }
            if (blackhole_time == 0)
                blackhole = 0;
        }
        public void blackhole_move_plane()
        {
            //label7.Text = blackhole_x.ToString() + blackhole_y.ToString();

            if (plane1x < x && plane1x > 0 && plane1y < y && plane1y > 0)
            {
                if ((blackhole_x - plane1x) * (blackhole_x - plane1x) + (blackhole_y - plane1y) * (blackhole_y - plane1y) < 1000)
                {
                    plane1x = blackhole_x;
                    plane1y = blackhole_y;
                }
                else if ((blackhole_x - plane1x) * (blackhole_x - plane1x) + (blackhole_y - plane1y) * (blackhole_y - plane1y) < 100000)
                {
                    plane1x += (float)2.0 * (blackhole_x - plane1x) / (plane1x - blackhole_x) + (float)0.02 * (blackhole_x - plane1x);
                    plane1y += (float)2.0 * (blackhole_y - plane1y) / (plane1y - blackhole_y) + (float)0.02 * (blackhole_y - plane1y);
                }
            }
            if (ai == false)
            {
                if (plane2x < x && plane2x > 0 && plane2y < y && plane2y > 0)
                {
                    if ((blackhole_x - plane2x) * (blackhole_x - plane2x) + (blackhole_y - plane2y) * (blackhole_y - plane2y) < 1000)
                    {
                        plane2x = blackhole_x;
                        plane2y = blackhole_y;
                    }
                    else if ((blackhole_x - plane2x) * (blackhole_x - plane2x) + (blackhole_y - plane2y) * (blackhole_y - plane2y) < 100000)
                    {
                        plane2x += (float)2.0 * (blackhole_x - plane2x) / (plane2x - blackhole_x) + (float)0.02 * (blackhole_x - plane2x);
                        plane2y += (float)2.0 * (blackhole_y - plane2y) / (plane2y - blackhole_y) + (float)0.02 * (blackhole_y - plane2y);
                    }
                }
            }
            else
            {
                if (planeaix < x && planeaix > 0 && planeaiy < y && planeaiy > 0)
                {
                    if ((blackhole_x - planeaix) * (blackhole_x - planeaix) + (blackhole_y - planeaiy) * (blackhole_y - planeaiy) < 1000)
                    {
                        planeaix = blackhole_x;
                        planeaiy = blackhole_y;
                    }
                    else if ((blackhole_x - planeaix) * (blackhole_x - planeaix) + (blackhole_y - planeaiy) * (blackhole_y - planeaiy) < 100000)
                    {
                        planeaix += (float)2.0 * (blackhole_x - planeaix) / (planeaix - blackhole_x) + (float)0.02 * (blackhole_x - planeaix);
                        planeaiy += (float)2.0 * (blackhole_y - planeaiy) / (planeaiy - blackhole_y) + (float)0.02 * (blackhole_y - planeaiy);
                    }
                }
            }

        }      //    blackhole_move_plane()

    }
}
