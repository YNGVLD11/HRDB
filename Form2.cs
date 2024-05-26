using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRDB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

//------------------------------------------- ИНИЦИАЛИЗАЦИЯ -----------------------------------------

        public Form1 form1;
        OleDbConnection connection = new OleDbConnection("Provider = "
        + "Microsoft.ACE.OleDB.12.0; Data Source = " + Application.StartupPath + "/db.accdb");


//---------------------------------------------- ЗАГРУЗКА -------------------------------------------

        private void Form2_Load(object sender, EventArgs e) //загрузка формы
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Категория". При необходимости она может быть перемещена или удалена.
            this.категорияTableAdapter.Fill(this.dbDataSet.Категория);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Образование". При необходимости она может быть перемещена или удалена.
            this.образованиеTableAdapter.Fill(this.dbDataSet.Образование);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.ФИО". При необходимости она может быть перемещена или удалена.
            this.фИОTableAdapter.Fill(this.dbDataSet.ФИО);
            
            if (button1.Text == "Изменить" | button1.Text == "Скопировать")
            {
                СomboBox(comboBox1, textBox3);
                СomboBox(comboBox2, textBox4);
                СomboBox(comboBox3, textBox5);
            }
            textBox7.Text = dateTimePicker2.Text;
        }


//---------------------------------------------- ЧЕКБОКС --------------------------------------------

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //переключение флажков у чекбокса
        {
            if (checkBox1.Checked == false)
            {
                dateTimePicker2.Enabled = false;
            }
            else
            {
                dateTimePicker2.Enabled = true;
            }
        }


//---------------------------------------------- КНОПКА ---------------------------------------------

        private void button1_Click(object sender, EventArgs e) // нажатие кнопки
        {
            if (button1.Text == "Изменить")
            {
                string title = "Изменение записи - Расстановка";
                string text = "Запись будет изменена";
                DialogResult dialogResult = MessageBox.Show(text, title, MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK) EditPlaceMent();
            }
            else
            {
                AddPlacement();
            }
            Close(); 
        }


//----------------------------------------------- МЕТОДЫ --------------------------------------------

        void СomboBox(ComboBox cb, TextBox tb) //передача комбобоксам значений из таблицы
        {
            int index = cb.FindString(tb.Text);
            cb.SelectedIndex = index;
        }

        private void AddPlacement() //добавление записи Расстановка
        {
            if (checkBox1.Checked == false)
            {
                textBox7.Text = "";
            }
            else
            {
                textBox7.Text = dateTimePicker2.Text;
            }
            string query = "Insert into Расстановка (ФИО, ДатаРождения, Образование, "
            + "Категория, СтажРаботыОбщий, СтажРаботыПед, Аттестация, Награды) Values "
            + "(@name, @birthday, @knowledge, @category, @totalExp, @pedExp, "
            + "@certification, @Awards)";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", (comboBox1.SelectedValue).ToString());
            command.Parameters.AddWithValue("@birthday", dateTimePicker1.Text);
            command.Parameters.AddWithValue("@knowledge", (comboBox2.SelectedValue).ToString());
            command.Parameters.AddWithValue("@category", (comboBox3.SelectedValue).ToString());
            command.Parameters.AddWithValue("@totalExp", textBox1.Text);
            command.Parameters.AddWithValue("@pedExp", textBox2.Text);
            command.Parameters.AddWithValue("@certification", textBox7.Text);
            command.Parameters.AddWithValue("@Awards", richTextBox1.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        void EditPlaceMent() //изменение записи Расстановка
        {
            if (checkBox1.Checked == false)
            {
                textBox7.Text = "";
            }
            else
            {
                textBox7.Text = dateTimePicker2.Text;
            }
            string query = "Update Расстановка Set ФИО=@name, ДатаРождения=@birthday, "
            + "Образование=@knowledge, Категория=@category, СтажРаботыОбщий=@totalExp, "
            + "СтажРаботыПед=@pedExp, Аттестация=@certification, Награды=@awards Where "
            + "Расстановка.Код = @id";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", comboBox1.SelectedValue.ToString());
            command.Parameters.AddWithValue("@birthday", dateTimePicker1.Text);
            command.Parameters.AddWithValue("@knowledge", (comboBox2.SelectedValue).ToString());
            command.Parameters.AddWithValue("@category", (comboBox3.SelectedValue).ToString());
            command.Parameters.AddWithValue("@totalExp", textBox1.Text);
            command.Parameters.AddWithValue("@pedExp", textBox2.Text);
            command.Parameters.AddWithValue("@certification", textBox7.Text);
            command.Parameters.AddWithValue("@awards", richTextBox1.Text);
            command.Parameters.AddWithValue("@id", textBox6.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
