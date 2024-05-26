using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRDB
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


//------------------------------------------- ИНИЦИАЛИЗАЦИЯ -----------------------------------------

        OleDbConnection connection = new OleDbConnection("Provider = "
        + "Microsoft.ACE.OleDB.12.0; Data Source = " + Application.StartupPath + "/db.accdb");


//---------------------------------------------- ЗАГРУЗКА -------------------------------------------

        private void Form4_Load(object sender, EventArgs e) //загрузка формы
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet1.Направленность". При необходимости она может быть перемещена или удалена.
            this.направленностьTableAdapter.Fill(this.dbDataSet1.Направленность);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.УчебныйГод". При необходимости она может быть перемещена или удалена.
            this.учебныйГодTableAdapter.Fill(this.dbDataSet.УчебныйГод);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Специализация". При необходимости она может быть перемещена или удалена.
            this.специализацияTableAdapter.Fill(this.dbDataSet.Специализация);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.ФИО". При необходимости она может быть перемещена или удалена.
            this.фИОTableAdapter.Fill(this.dbDataSet.ФИО);

            if (button1.Text == "Изменить" | button1.Text == "Скопировать")
            {
                ComboBox(comboBox1, textBox2); //фио
                ComboBox(comboBox2, textBox3); //специализация
                ComboBox(comboBox3, textBox4); //учебный год
                ComboBox(comboBox4, textBox5); //четверть
                ComboBox(comboBox5, textBox6); //направленность
            }
        }


//---------------------------------------------- КНОПКИ ---------------------------------------------

        private void button1_Click(object sender, EventArgs e) // нажатие кнопки
        {
            if (button1.Text == "Изменить")
            {
                string title = "Изменение записи (Квалификация)";
                string text = "Вы действительно хотите изменить эту запись?";
                DialogResult dialogResult = MessageBox.Show(text, title, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) EditQualification();
            }
            else
            {
                AddQualification();
            }
            Close();
        }


//----------------------------------------------- МЕТОДЫ --------------------------------------------

        void ComboBox(ComboBox cb, TextBox tb) //передача комбобоксам значений из таблицы
        {
            int index = cb.FindString(tb.Text);
            cb.SelectedIndex = index;
        }

        public void AddQualification() //добавление записи Квалификация
        {
            string query = "Insert into Квалификация (ФИО, Специализация, УчебныйГод, "
            + "Четверть, НазваниеКурсов, ОрганизацияОбучения, КоличествоЧасов, Направленность, "
            + "Документ, ПрофессиональныеКонкурсы) Values (@name, @cpec, @year, @quarter, "
            + "@course, @education, @amount, @direction, @doc, @comp)";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", (comboBox1.SelectedValue).ToString());
            command.Parameters.AddWithValue("@cpec", (comboBox2.SelectedValue).ToString());
            command.Parameters.AddWithValue("@year", (comboBox3.SelectedValue).ToString());
            command.Parameters.AddWithValue("@quarter", comboBox4.Text);
            command.Parameters.AddWithValue("@course", richTextBox1.Text);
            command.Parameters.AddWithValue("@education", richTextBox3.Text);
            command.Parameters.AddWithValue("@amount", numericUpDown1.Text);
            command.Parameters.AddWithValue("@direction", (comboBox5.SelectedValue).ToString());
            command.Parameters.AddWithValue("@doc", textBox1.Text);
            command.Parameters.AddWithValue("@comp", richTextBox2.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditQualification() //Изменение записи Квалификация
        {
            string query = "Update Квалификация Set ФИО=@name, Специализация=@cpec, "
+ "УчебныйГод=@year, Четверть=@quarter, НазваниеКурсов=@course, ОрганизацияОбучения=@education, "
+ "КоличествоЧасов=@amount, Направленность=@direction, Документ=@doc, ПрофессиональныеКонкурсы=@comp "
+ "Where Квалификация.Код = @id";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", (comboBox1.SelectedValue).ToString());
            command.Parameters.AddWithValue("@cpec", (comboBox2.SelectedValue).ToString());
            command.Parameters.AddWithValue("@year", (comboBox3.SelectedValue).ToString());
            command.Parameters.AddWithValue("@quarter", comboBox4.Text);
            command.Parameters.AddWithValue("@course", richTextBox1.Text);
            command.Parameters.AddWithValue("@education", richTextBox3.Text);
            command.Parameters.AddWithValue("@amount", numericUpDown1.Text);
            command.Parameters.AddWithValue("@direction", (comboBox5.SelectedValue).ToString());
            command.Parameters.AddWithValue("@doc", textBox1.Text);
            command.Parameters.AddWithValue("@comp", richTextBox2.Text);
            command.Parameters.AddWithValue("@id", textBox7.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
