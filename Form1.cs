using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCountsYearMonthDay
{
    public partial class Form1 : Form
    {
        CountTime countTime; // класс расчета времени 
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        // чек боксы текущей даты
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBoxDATE1.Text = DateTime.Now.ToShortDateString();
            }
            else maskedTextBoxDATE1.Clear();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                maskedTextBoxDATE2.Text = DateTime.Now.ToShortDateString();
            }
            else maskedTextBoxDATE2.Clear();
        }
        // вход в зону кнопки
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkKhaki;
        }
        // выход из зоны кнопки
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Silver;
        }

        /// кнопка рассчета
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                DateTime dt1 = Convert.ToDateTime(maskedTextBoxDATE1.Text);
                DateTime dt2 = Convert.ToDateTime(maskedTextBoxDATE2.Text);
                countTime = new CountTime(dt1, dt2);
                message += "Время между датами[";
                if (radioButton1.Checked)
                {

                    int dorb = countTime.Days % 365;
                    message += $"{Math.Abs(countTime.Years)}";
                    if (dorb >= 180) message += ".5";
                    message += $" год/лет]";
                }
                if (radioButton2.Checked)
                {
                    int dorb2 = countTime.Days % 30;
                    message += $"{Math.Abs(countTime.Months)}";
                    if (dorb2 >= 15) message += ".5";
                    message += $" месяцев]";
                }
                if (radioButton3.Checked) message += $"{Math.Abs(countTime.Days)} дней]";
                if (radioButton4.Checked) message += $"{Math.Abs(countTime.Hours)} часов]";
                if (radioButton5.Checked) message += $"{Math.Abs(countTime.Minutes)} минут]";
                if (radioButton6.Checked) message += $"{Math.Abs(countTime.Seconds)} секунд]";
                MessageBox.Show(message, "Результат");
            }
            catch
            {
                MessageBox.Show("Заполните все поля дат!", "Внимание!", MessageBoxButtons.OK);
            }
        }

    }

    class CountTime
    {
        public DateTime datatime1 { get; set; }
        public DateTime datatime2 { get; set; }
        public int Years    { get   { return Days / 365; } }
        public int Months   { get   { return this.Days / 30;  } }
        public int Days     { get   { return getDays(); } }
        public int Hours    { get   { return this.Days * 24;} }
        public int Minutes  { get   { return this.Hours * 60; } }
        public int Seconds  { get   { return this.Minutes * 60; } }
        public CountTime(DateTime d1, DateTime d2)
        {
            if(d1 > d2)
            {
                this.datatime1 = d2;
                this.datatime2 = d1;
            }
            else 
            {
                this.datatime1 = d1;
                this.datatime2 = d2;
            }
        }
        public int getDaysFromMonth(DateTime d)
        {
            int quantity1 = 0;
            int m1 = 31, m2 = 28, m3 = 31, m4 = 30,
                m5 = 31, m6 = 30, m7 = 31, m8 = 31,
                m9 = 30, m10 = 31, m11 = 30, m12 = 31;
            switch (d.Month)
            {
                case 1: quantity1 = d.Day; break;
                case 2: quantity1 = m1 + d.Day; break;
                case 3: quantity1 = m1 + m2 + d.Day; break;
                case 4: quantity1 = m1 + m2 + m3 + d.Day; break;
                case 5: quantity1 = m1 + m2 + m3 + m4 + d.Day; break;
                case 6: quantity1 = m1 + m2 + m3 + m4 + m5 + d.Day; break;
                case 7: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + d.Day; break;
                case 8: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + m7 + d.Day; break;
                case 9: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8 + d.Day; break;
                case 10: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8 + m9 + d.Day; break;
                case 11: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8 + m9 + m10 + d.Day; break;
                case 12: quantity1 = m1 + m2 + m3 + m4 + m5 + m6 + m7 + m8 + m9 + m10 + m11 + d.Day; break;
            }
            return quantity1;
        }
        public int getDays()
        {
            int leapY, QleapY = 0;
            leapY = datatime1.Year;
            while (leapY <= datatime2.Year)
            {
                if (leapY % 4 == 0 && leapY % 100 != 0 || leapY % 400 == 0)
                {
                    QleapY++;
                }
                leapY++;
            }
            int daysFromMouth1 = getDaysFromMonth(this.datatime1);
            int daysFromMouth2 = getDaysFromMonth(this.datatime2);
            if ((datatime1.Year % 4 == 0 && leapY % 100 != 0 || leapY % 400 == 0) && datatime1.Month > 2)
            { QleapY--; }
            if ((datatime2.Year % 4 == 0 && leapY % 100 != 0 || leapY % 400 == 0) && datatime2.Day <= 29 && datatime2.Month < 3)
            { QleapY--; }
            if (datatime1.Year == datatime2.Year)
                    return (daysFromMouth2 - daysFromMouth1) + QleapY;
                else
                    return (datatime2.Year - datatime1.Year) * 365 + ((daysFromMouth2 - daysFromMouth1) + QleapY);
        }
    }


}
