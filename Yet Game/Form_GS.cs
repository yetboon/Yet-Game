using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yet_Game
{
    public partial class Form_GS : Form
    {
        bool ex_yet = false;
        public int money_int = 0;
        int attack_int = 1,lucky_int = 10,point_int = 0,run_int; //action
        int healthbottle_int; //shop item
        public static string game_name = "";
        int monster_value,attack_sound_int; // other options
        public Form_GS()
        {
            InitializeComponent();
        }

        private void Form_GS_Load(object sender, EventArgs e)
        {
            label_healthbottle_num.Text = "已有:" + healthbottle_int.ToString();
            label_name.Text = game_name.ToString();
            groupBox_shop.Visible = false;
            health_bar.Value = 100;
        }

        private void Form_GS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button_random_Click(object sender, EventArgs e)
        {
            if (comboBox_level.Text == "Level 1")
            {
                random_monster_1();
            }
            if (comboBox_level.Text == "Level 2")
            {
                random_monster_2();
            }
            if (comboBox_level.Text == "Level 3")
            {
                random_monster_3();
            }
            if (comboBox_level.Text == "Level 4")
            {
                random_monster_4();
            }
            if (comboBox_level.Text == "Level 5")
            {
                random_monster_5();
            }
            if (comboBox_level.Text == "Level 6")
            {
                random_monster_6();
            }
            if (comboBox_level.Text == "Level 7")
            {
                random_monster_7();
            }
            if(comboBox_level.Text == "YET")
            {
                random_monster_yet();
            }
            button_openshop.Enabled = false;
            button_random.Enabled = false;
            button_Attack.Enabled = true;
            button_Run.Enabled = true;
            comboBox_level.Enabled = false;
            button_random.Text = "祝你好运";
        }

        private void button_add_attack_Click(object sender, EventArgs e)
        {
            label_point.Text = (point_int - 1).ToString();
            point_int = Convert.ToInt32(label_point.Text.ToString());
            label_Attack.Text = (attack_int + 1).ToString();
            attack_int = Convert.ToInt32(label_Attack.Text.ToString());
            sound_add_attackandlucky();
        }

        private void label_point_TextChanged(object sender, EventArgs e)
        {
            if (label_point.Text == "0")
            {
                button_add_attack.Enabled = false;
                button_add_lucky.Enabled = false;
            }
            else
            {
                button_add_attack.Enabled = true;
                button_add_lucky.Enabled = true;
            }
        }

        private void button_add_lucky_Click(object sender, EventArgs e)
        {
            if (label_Lucky.Text == "99/100%")
            {
                button_add_lucky.Visible = false;
            }
            label_point.Text = (point_int - 1).ToString();
            point_int = Convert.ToInt32(label_point.Text.ToString());
            label_Lucky.Text = (lucky_int + 1).ToString();
            lucky_int = Convert.ToInt32(label_Lucky.Text.ToString());
            label_Lucky.Text = (lucky_int + "/100%").ToString();
            sound_add_attackandlucky();
        }

        private void button_Attack_Click(object sender, EventArgs e)
        {
            try
            {
                monster_health.Value = monster_health.Value - attack_int;
            }
            catch(Exception)
            {
                monster_repel();
                monster_health.Value = 0;
                monster_Attack_value.Text = "0";
                label_monster.Text = "[NO MONSTER]";
                button_random.Enabled = true;
                button_Attack.Enabled = false;
                button_Run.Enabled = false;
                comboBox_level.Enabled = true;
                button_openshop.Enabled = true;
                button_random.Text = "RANDOM MONSTER";
            }
            monster_step_kill();
            sound_attack_random();
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            Random random_run = new Random();
            run_int = random_run.Next(1, 100);
            if (run_int > lucky_int)
            {
                MessageBox.Show("fail :P", "Fail");
                monster_step_kill();
            }
            if (run_int < lucky_int)
            {
                label_monster.Text = "[NO MONSTER]";
                button_Attack.Enabled = false;
                button_Run.Enabled = false;
                button_random.Enabled = true;
                monster_health.Value = 0;
            }
        }

        private void button_openshop_Click(object sender, EventArgs e)
        {
            label_money_shop.Text = "money:" + money_int.ToString();
            label_healthbottle_num_shop.Text = "已有:" + healthbottle_int.ToString();
            label_skillpoint_num_shop.Text = "已有:" + point_int.ToString();
            groupBox_shop.Show();
            groupBox_main.Hide();
        }

        private void button_back_main_Click(object sender, EventArgs e)
        {
            groupBox_shop.Hide();
            groupBox_main.Show();
            label_money.Text = money_int.ToString();
            label_healthbottle_num.Text = "已有:" + healthbottle_int.ToString();
            label_point.Text = point_int.ToString();
        }

        private void button_healthbottle_buy_Click(object sender, EventArgs e)
        {
            if (money_int < 500)
            {
                MessageBox.Show("你没有足够的金钱购买 =_=ll", "没钱滚蛋！:P");
            }
            else
            {
                money_int -= 500;
                healthbottle_int += 1;
                label_money_shop.Text = "money:" + money_int.ToString();
                label_healthbottle_num_shop.Text = "已有:" + healthbottle_int.ToString();
                System.Media.SoundPlayer sound_healthbottle_buy = new System.Media.SoundPlayer();
                sound_healthbottle_buy.SoundLocation = "buy.wav";
                sound_healthbottle_buy.Load();
                sound_healthbottle_buy.Play();
            }
        }

        private void button_skillpoint_buy_Click(object sender, EventArgs e)
        {
            if (money_int < 1500)
            {
                MessageBox.Show("你没有足够的金钱购买 =_=ll", "没钱滚蛋！:P");
            }
            else
            {
                money_int -= 1500;
                label_money_shop.Text = "money:" + money_int.ToString();
                label_point.Text = (point_int + 7).ToString();
                point_int = Convert.ToInt32(label_point.Text.ToString());
                label_skillpoint_num_shop.Text = "已有:" + point_int.ToString();
                System.Media.SoundPlayer sound_healthbottle_buy = new System.Media.SoundPlayer();
                sound_healthbottle_buy.SoundLocation = "buy.wav";
                sound_healthbottle_buy.Load();
                sound_healthbottle_buy.Play();
            }
        }

        private void comboBox_level_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_level.Text == "Level 2")
            {
                comboBox_level.ForeColor = Color.Aqua;
            }
            if (comboBox_level.Text == "Level 3")
            {
                comboBox_level.ForeColor = Color.Beige;
            }
            if (comboBox_level.Text == "Level 4")
            {
                comboBox_level.ForeColor = Color.Blue;
            }
            if (comboBox_level.Text == "Level 5")
            {
                comboBox_level.ForeColor = Color.Brown;
            }
            if (comboBox_level.Text == "Level 6")
            {
                comboBox_level.ForeColor = Color.Cyan;
            }
            if (comboBox_level.Text == "Level 7")
            {
                comboBox_level.ForeColor = Color.Red;
            }
        }

        private void label_healthbottle_num_TextChanged(object sender, EventArgs e)
        {
            if (healthbottle_int == 0)
            {
                button_use_healthbottle.Enabled = false;
            }
            else
            {
                button_use_healthbottle.Enabled = true;
            }
        }

        private void button_use_healthbottle_Click(object sender, EventArgs e)
        {
            try { health_bar.Value += 25; }
            catch (Exception) { health_bar.Value = 100; }
            healthbottle_int -= 1;
            label_healthbottle_num.Text = "已有:" + healthbottle_int.ToString();
            if (healthbottle_int == 0)
            {
                button_use_healthbottle.Enabled = false;
            }
            sound_healthbottle_use();
        }

        #region monster void
        void monster_step_kill()
        {
            if (comboBox_level.Text == "Level 1")
            {
                if (label_monster.Text == "Skeleton")
                {
                    try { health_bar.Value = (health_bar.Value - 1); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Zombie")
                {
                    try { health_bar.Value = (health_bar.Value - 2); }
                    catch (Exception) { game_dead(); }
                }
            }
            if(comboBox_level.Text == "Level 2")
            {
                if (label_monster.Text == "Spider")
                {
                    try { health_bar.Value = (health_bar.Value - 3); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Creeper")
                {
                    try { health_bar.Value = (health_bar.Value - 5); }
                    catch (Exception) { game_dead(); }
                }
            }
            if(comboBox_level.Text == "Level 3")
            {
                if(label_monster.Text == "Small Dragon")
                {
                    try { health_bar.Value = (health_bar.Value - 4); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Dragon")
                {
                    try { health_bar.Value = (health_bar.Value - 5); }
                    catch (Exception) { game_dead(); }
                }
            }
            if (comboBox_level.Text == "Level 4")
            {
                if(label_monster.Text == "Evil")
                {
                    try { health_bar.Value = (health_bar.Value - 8); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Evil_Girl")
                {
                    try { health_bar.Value = (health_bar.Value - 8); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Evil")
                {
                    try { health_bar.Value = (health_bar.Value - 10); }
                    catch (Exception) { game_dead(); }
                }
            }
            if (comboBox_level.Text == "Level 5")
            {
                if (label_monster.Text == "The dark lord")
                {
                    try { health_bar.Value = (health_bar.Value - 12); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Ex The dark lord")
                {
                    try { health_bar.Value = (health_bar.Value - 15); }
                    catch (Exception) { game_dead(); }
                }
            }
            if(comboBox_level.Text == "Level 6")
            {
                if (label_monster.Text == "White evil")
                {
                    try { health_bar.Value = (health_bar.Value - 20); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Black evil")
                {
                    try { health_bar.Value = (health_bar.Value - 20); }
                    catch (Exception) { game_dead(); }
                }
            }
            if(comboBox_level.Text == "Level 7")
            {
                if(label_monster.Text == "Red evil")
                {
                    try { health_bar.Value = (health_bar.Value - 35); }
                    catch (Exception) { game_dead(); }
                }
                if (label_monster.Text == "Super evil")
                {
                    try { health_bar.Value = (health_bar.Value - 38); }
                    catch (Exception) { game_dead(); }
                }
            }
            if(label_monster.Text == "正在睡觉的yet")
            {
                try { health_bar.Value = (health_bar.Value - 0); }
                catch (Exception) { game_dead(); }
            }
        }
        void monster_repel()
        {
            if (comboBox_level.Text == "Level 1")
            {
                if (label_monster.Text == "Skeleton")
                {
                    label_money.Text = (money_int + 50).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
                if (label_monster.Text == "Zombie")
                {
                    label_money.Text = (money_int + 100).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
            }
            if (comboBox_level.Text == "Level 2")
            {
                if (label_monster.Text == "Spider")
                {
                    label_money.Text = (money_int + 250).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
                if (label_monster.Text == "Creeper")
                {
                    label_money.Text = (money_int + 300).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
            }
            if (comboBox_level.Text == "Level 3")
            {
                if (label_monster.Text == "Small Dragon")
                {
                    label_money.Text = (money_int + 350).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
                if (label_monster.Text == "Dragon")
                {
                    label_money.Text = (money_int + 400).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
            }
            if (comboBox_level.Text == "Level 4")
            {
                if (label_monster.Text == "Evil_Girl")
                {
                    label_money.Text = (money_int + 400).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
                if (label_monster.Text == "Evil")
                {
                    label_money.Text = (money_int + 420).ToString();
                    label_point.Text = (point_int + 2).ToString();
                }
                if (label_monster.Text == "Evil_Boss")
                {
                    label_money.Text = (money_int + 450).ToString();
                    label_point.Text = (point_int + 2).ToString();
                }
            }
            if (comboBox_level.Text == "Level 5")
            {
                if (label_monster.Text == "The dark lord")
                {
                    label_money.Text = (money_int + 500).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
                if (label_monster.Text == "Ex The dark lord")
                {
                    label_money.Text = (money_int + 520).ToString();
                    label_point.Text = (point_int + 1).ToString();
                }
            }
            if(comboBox_level.Text == "Level 6")
            {
                if (label_monster.Text == "White evil")
                {
                    label_money.Text = (money_int + 550).ToString();
                    label_point.Text = (point_int + 2).ToString();
                }
                if (label_monster.Text == "Black evil")
                {
                    label_money.Text = (money_int + 550).ToString();
                    label_point.Text = (point_int + 2).ToString();
                }
            }
            if(comboBox_level.Text == "Level 7")
            {
                if (label_monster.Text == "Red evil")
                {
                    label_money.Text = (money_int + 600).ToString();
                    label_point.Text = (point_int + 3).ToString();
                }
                if (label_monster.Text == "Super evil")
                {
                    label_money.Text = (money_int + 650).ToString();
                    label_point.Text = (point_int + 3).ToString();
                }
            }
            if(label_monster.Text == "正在睡觉的yet")
            {
                if(ex_yet == false)
                {
                MessageBox.Show("啊啊啊啊啊啊啊啊！！！", "yet");
                MessageBox.Show("你..怎么跑出来了!???", "yet");
                MessageBox.Show("你..杀了我!??", "yet");
                MessageBox.Show("你...是????", "yet");
                MessageBox.Show("呃～～～～（已死亡）", "yet");
                MessageBox.Show("恭喜，你成功逃出整个软件程序了", "恭喜");
                MessageBox.Show("你对你机器本身进行修改", "恭喜");
                MessageBox.Show("Restarting.....", "恭喜");
                attack_int += 50;
                lucky_int += 50;
                money_int += 10000;
                point_int += 100;
                label_Attack.Text = attack_int.ToString();
                label_Lucky.Text = lucky_int.ToString();
                label_money.Text = money_int.ToString();
                label_point.Text = point_int.ToString();
                ex_yet = true;
                }
                if(ex_yet == true)
                {
                    MessageBox.Show("Yet已死亡...", "没事了");
                }

            }
            money_int = Convert.ToInt32(label_money.Text.ToString());
            point_int = Convert.ToInt32(label_point.Text.ToString());
        }
        void random_monster_1()
        {
            Random random_monster_r_1 = new Random();
            monster_value = random_monster_r_1.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "Skeleton";
                monster_health.Value = 8;
                monster_Attack_value.Text = "1";
            }
            if (monster_value == 2)
            {
                label_monster.Text = "Zombie";
                monster_health.Value = 12;
                monster_Attack_value.Text = "2";
            }
        }
        void random_monster_2()
        {
            Random random_monster_r_2 = new Random();
            monster_value = random_monster_r_2.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "Spider";
                monster_health.Value = 15;
                monster_Attack_value.Text = "3";
            }
            if (monster_value == 2)
            {
                label_monster.Text = "Creeper";
                monster_health.Value = 8;
                monster_Attack_value.Text = "5";
            }
        }
        void random_monster_3()
        {
            Random random_monster_r_3 = new Random();
            monster_value = random_monster_r_3.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "Small Dragon";
                monster_health.Value = 18;
                monster_Attack_value.Text = "4";
            }
            if(monster_value == 2)
            {
                label_monster.Text = "Dragon";
                monster_health.Value = 25;
                monster_Attack_value.Text = "5";
            }
        }
        void random_monster_4()
        {
            Random random_monster_r_4 = new Random();
            monster_value = random_monster_r_4.Next(1, 4);
            if (monster_value == 1)
            {
                label_monster.Text = "Evil";
                monster_health.Value = 30;
                monster_Attack_value.Text = "8";
            }
            if(monster_value == 2)
            {
                label_monster.Text = "Evil_Girl";
                monster_health.Value = 28;
                monster_Attack_value.Text = "7";
            }
            if (monster_value == 3)
            {
                label_monster.Text = "Evil_Boss";
                monster_health.Value = 30;
                monster_Attack_value.Text = "10";
            }
        }
        void random_monster_5()
        {
            Random random_monster_r_5 = new Random();
            monster_value = random_monster_r_5.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "The dark lord";
                monster_health.Value = 40;
                monster_Attack_value.Text = "12";
            }
            if (monster_value == 2)
            {
                label_monster.Text = "Ex The dark lord";
                monster_health.Value = 50;
                monster_Attack_value.Text = "15";
            }
        }
        void random_monster_6()
        {
            Random random_monster_r_6 = new Random();
            monster_value = random_monster_r_6.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "White evil";
                monster_health.Value = 65;
                monster_Attack_value.Text = "20";
            }
            if (monster_value == 2)
            {
                label_monster.Text = "Black evil";
                monster_health.Value = 65;
                monster_Attack_value.Text = "20";
            }
        }
        void random_monster_7()
        {
            Random random_monster_r_7 = new Random();
            monster_value = random_monster_r_7.Next(1, 3);
            if (monster_value == 1)
            {
                label_monster.Text = "Red evil";
                monster_health.Value = 80;
                monster_Attack_value.Text = "35";
            }
            if (monster_value == 2)
            {
                label_monster.Text = "Super evil";
                monster_health.Value = 80;
                monster_Attack_value.Text = "38";
            }
        }
        void random_monster_yet()
        {
            if (ex_yet == true) { }
            if (ex_yet == false)
            {
            label_monster.Text = "正在睡觉的yet";
            monster_health.Value = 1;
            monster_Attack_value.Text = "0";
            }

        }
        #endregion
        #region PlaySound and Dead
        void game_dead()
        {
            health_bar.Value = 0;
            MessageBox.Show("you have dead ;(", "End", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Thanks for playing..:D", "End");
            Application.Restart();
        }
        void sound_attack_random()
        {
            Random random_attack_sound = new Random();
            attack_sound_int = random_attack_sound.Next(1, 3);
            System.Media.SoundPlayer attack_sound_player = new System.Media.SoundPlayer();
            if (attack_sound_int == 1)
            {
                attack_sound_player.SoundLocation = "Hit01.wav";
                attack_sound_player.Load();
                attack_sound_player.Play();
            }
            if (attack_sound_int == 2)
            {
                attack_sound_player.SoundLocation = "Hit02.wav";
                attack_sound_player.Load();
                attack_sound_player.Play();
            }
        }
        void sound_add_attackandlucky()
        {
            System.Media.SoundPlayer attackandlucky_sound_player = new System.Media.SoundPlayer();
            attackandlucky_sound_player.SoundLocation = "LvUp.wav";
            attackandlucky_sound_player.Load();
            attackandlucky_sound_player.Play();
        }
        void sound_healthbottle_use()
        {
            System.Media.SoundPlayer healthbottle_use_player = new System.Media.SoundPlayer();
            healthbottle_use_player.SoundLocation = "healing.wav";
            healthbottle_use_player.Load();
            healthbottle_use_player.Play();
        }
        #endregion

    }
}