using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HRDB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


//------------------------------------------- ИНИЦИАЛИЗАЦИЯ -----------------------------------------

        OleDbConnection connection = new OleDbConnection("Provider = "
        + "Microsoft.ACE.OleDB.12.0; Data Source = " + Application.StartupPath + "/db.accdb");


//---------------------------------------------- ЗАГРУЗКА -------------------------------------------

        private void Form3_Load(object sender, EventArgs e) //загрузка формы
        {
            string query = "";
            switch (this.Text)
            {
                case "Справочники - ФИО": query = "ФИО";
                    break;
                case "Справочники - Образование": 
                    query = "Образование";
                    break;
                case "Справочники - Квалификационная категория":
                    query = "Категория";
                    break;
                case "Справочники - Специализация":
                    query = "Специализация";
                    break;
                case "Справочники - Учебный год":
                    query = "УчебныйГод";
                    break;
                case "Справочники - Направленность":
                    query = "Направленность";
                    break;
            }
            GetTable("Select * from " + query + " order by "+ query +";");
            dataGridView1.Columns[0].Visible = false;
        }


//-------------------------------------------- ТАБЛИЦА ----------------------------------------------

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridView1.SelectedCells[0].Value.ToString();
            textBox1.Text = name;
        }


//---------------------------------------------- КНОПКИ ---------------------------------------------

        private void button1_Click(object sender, EventArgs e) //кнопка "Добавить"
        {
            if (textBox1.Text != "")
            {
                switch (this.Text)
                {
                    case "Справочники - ФИО":
                        AddName("ФИО");
                        break;
                    case "Справочники - Образование":
                        AddName("Образование");
                        break;
                    case "Справочники - Квалификационная категория":
                        AddName("Категория");
                        break;
                    case "Справочники - Специализация":
                        AddName("Специализация");
                        break;
                    case "Справочники - Учебный год":
                        AddName("УчебныйГод");
                        break;
                    case "Справочники - Направленность":
                        AddName("Направленность");
                        break;
                }
                Form3_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Для добавления наименования необходимо заполнить "
                + "текстовое поле!", "Ошибка (добавление наименования)");
                textBox1.Focus();
            } 
        }

        private void button2_Click(object sender, EventArgs e) //кнопка "Изменить"
        {
            if (textBox1.Text != "")
            {
                switch (this.Text)
                {
                    case "Справочники - ФИО":
                        EditName("ФИО");
                        break;
                    case "Справочники - Образование":
                        EditName("Образование");
                        break;
                    case "Справочники - Квалификационная категория":
                        EditName("Категория");
                        break;
                    case "Справочники - Специализация":
                        EditName("Специализация");
                        break;
                    case "Справочники - Учебный год":
                        EditName("УчебныйГод");
                        break;
                    case "Справочники - Направленность":
                        EditName("Направленность");
                        break;
                }
                Form3_Load(sender, e);
            }
            else
            {
                string text = "Для изменения наименования необходимо заполнить текстовое поле!";
                string title = "Изменение наименования - Ошибка";
                MessageBox.Show(text, title);
                textBox1.Focus();
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //кнопка "Удалить"
        {
            switch (this.Text)
            {
                case "Справочники - ФИО":
                    Delete("ФИО");
                    break;
                case "Справочники - Образование":
                    Delete("Образование");
                    break;
                case "Справочники - Квалификационная категория":
                    Delete("Категория");
                    break;
                case "Справочники - Специализация":
                    Delete("Специализация");
                    break;
                case "Справочники - Учебный год":
                    Delete("УчебныйГод");
                    break;
                case "Справочники - Направленность":
                    Delete("Направленность");
                    break;
            }
            Form3_Load(sender, e);
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e) // выход
        {
            Close();
        }


//---------------------------------------------- СПРАВКА --------------------------------------------

        private void добавлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "Справка - добавление";
            string text = "Для добавления нового наименования необходимо записать его имя "
            +" в текстовом поле, после чего нажать на кнопку 'Добавить'.\nСохранение "
            +" изменений происходит автоматически.\nЕсли оставить поле пустым, "
            +" пользователя встретит соответствующее предупреждение об ошибке.";
            MessageBox.Show(text, title);
        }

        private void изменениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "Справка - изменение";
            string text = "Для изменения существующего наименования необходимо навестись "
            +" на нужную для правки запись с помощью клика по таблице, а также ввести "
            +" новое имя в текстовое поле, после чего нажать на кнопку 'Изменить'.\n"
            +" Сохранение изменений происходит автоматически.\nЕсли оставить поле пустым, "
            + " пользователя встретит соответствующее предупреждение об ошибке.";
            MessageBox.Show(text, title);
        }

        private void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "Справка - удаление";
            string text = "Для удаления существующего наименования необходимо навестись "
            +" на нужную для правки запись с помощью клика по таблице, после чего нажать "
            +" на кнопку 'Удалить'.\n"
            +" Сохранение изменений происходит автоматически.\nПеред удалением пользователя "
            +" встретит диалоговое окно с предупреждением.";
            MessageBox.Show(text, title);
        }

        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = "Справка - Справочники";
            string text = "Справочники - объекты конфигурации, позволяющие хранить "
            +" в информационной базе данные, имеющие одинаковую структуру и списочный "
            + "характер. Каждый элемент справочника характеризуется кодом и "
            + "наименованием.";
            MessageBox.Show(text, title);
        }


//----------------------------------------------- МЕТОДЫ --------------------------------------------

        public void GetTable(string query) //формирование таблиц
        {
            connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void AddName(string tableName) //добавление наименования
        {
            string query = "Insert into " + tableName + " (" + tableName + ") Values (@name)";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", textBox1.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void EditName(string tableName) //изменение наименования
        {
            string query = "Update " + tableName + " Set " + tableName + "=@name Where " + tableName + ".Код = @id";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Parameters.AddWithValue("@name", textBox1.Text);
            command.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void Delete(string tableName) // удаление наименования
        {
            DialogResult dialogResult = MessageBox.Show("Вы действительно хотите удалить это наименование?", "Удаление записи", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string query = "Delete * from " + tableName + " Where " + tableName + ".Код = @id";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }  
    }
}
