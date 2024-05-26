using HRDB.dbDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


//------------------------------------------- ИНИЦИАЛИЗАЦИЯ -----------------------------------------

        OleDbConnection connection = new OleDbConnection("Provider = "
        + "Microsoft.ACE.OleDB.12.0; Data Source = "+ Application.StartupPath + "/db.accdb");
        public Form2 form2 = new Form2();
        public Form3 form3 = new Form3();
        public Form4 form4 = new Form4();
        private ExcelExport excelExport = new ExcelExport();

        public string placementQuery = "SELECT Расстановка.Код, ФИО.ФИО, Расстановка.ДатаРождения, "
+ "Образование.Образование, Категория.Категория, Расстановка.СтажРаботыОбщий AS [Стаж (общий)], "
+ "Расстановка.СтажРаботыПед AS [Стаж (пед)], Расстановка.Аттестация, Расстановка.Награды FROM "
+ "Образование INNER JOIN(ФИО INNER JOIN (Категория INNER JOIN Расстановка ON Категория.Код = "
+ "Расстановка.Категория) ON ФИО.Код = Расстановка.ФИО) ON Образование.Код = Расстановка.Образование";
        
        public string qualificationQuery = "SELECT Квалификация.Код, ФИО.ФИО, "
        + "Специализация.Специализация, УчебныйГод.УчебныйГод, Квалификация.Четверть, "
        + "Квалификация.НазваниеКурсов, Квалификация.ОрганизацияОбучения, "
        + "Квалификация.КоличествоЧасов, Направленность.Направленность, "
        + "Квалификация.Документ, Квалификация.ПрофессиональныеКонкурсы FROM "
        + "Направленность INNER JOIN(УчебныйГод INNER JOIN (Специализация INNER JOIN "
        + "(ФИО INNER JOIN Квалификация ON ФИО.Код = Квалификация.ФИО) ON "
        + "Специализация.Код = Квалификация.Специализация) ON УчебныйГод.Код = "
        +"Квалификация.УчебныйГод) ON Направленность.Код = Квалификация.Направленность";

        public string placementTitle = "База данных для работы с педагогическими кадрами - Расстановка";
        public string qualificationTitle = "База данных для работы с педагогическими кадрами - Квалификация";


//---------------------------------------------- ЗАГРУЗКА -------------------------------------------

        private void Form1_Load(object sender, EventArgs e) //загрузка формы
        {
            textBox1.Text = "Поиск по ФИО";
            if (this.Text == placementTitle)
            {
                GetTable(placementQuery);
            }
            else
            {
                GetTable(qualificationQuery);
            }
            int rowCount = dataGridView1.RowCount;
            toolStripStatusLabel1.Text = "Количество записей: " + rowCount.ToString() + "";
        }


//---------------------------------------------- ОПЕРАЦИИ -------------------------------------------

        private void button2_Click(object sender, EventArgs e) // кнопка "добавить"
        {
            if (this.Text == placementTitle)
            {
                form2.Text = "Добавление записи (Расстановка)";
                form2.button1.Text = "Добавить";
                form2.ShowDialog();
            } else
            {
                form4.Text = "Добавление записи (Квалификация)";
                form4.button1.Text = "Добавить";
                form4.ShowDialog();
            }    
        }

        private void button3_Click(object sender, EventArgs e) //кнопка "изменить"
        {
            if (this.Text == placementTitle)
            {
                form2.Text = "Изменение записи (Расстановка)";
                form2.button1.Text = "Изменить";
                form2.textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                form2.textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                form2.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                form2.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                form2.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                form2.textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                form2.textBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                form2.dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                form2.richTextBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                form2.ShowDialog();
            }
            else
            {
                form4.Text = "Изменение записи (Квалификация)";
                form4.button1.Text = "Изменить";
                form4.textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                form4.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                form4.textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                form4.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                form4.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                form4.richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                form4.richTextBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                form4.numericUpDown1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                form4.textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                form4.textBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                form4.richTextBox2.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                form4.ShowDialog();
            }   
        }

        private void button7_Click(object sender, EventArgs e) //добавление с копированием
        {
            if (this.Text == placementTitle)
            {
                form2.Text = "Добавление записи с копированием (Расстановка)";
                form2.button1.Text = "Скопировать";
                form2.textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                form2.textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                form2.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                form2.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                form2.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                form2.textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                form2.textBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                form2.dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                form2.richTextBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                form2.ShowDialog();
            }
            else
            {
                form4.Text = "Добавление записи с копированием (Квалификация)";
                form4.button1.Text = "Скопировать";
                form4.textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                form4.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                form4.textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                form4.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                form4.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                form4.richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                form4.richTextBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                form4.numericUpDown1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                form4.textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                form4.textBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                form4.richTextBox2.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                form4.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e) // кнопка "Удалить"
        {
            Delete();
            Form1_Load(sender, e);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) // кнопка "Выход"
        {
            Application.Exit();
        }


//--------------------------------------------- ПОИСК -----------------------------------------------

        private void button6_Click(object sender, EventArgs e) //поиск по ФИО
        {
            if (this.Text == placementTitle)
            {
                GetTable(placementQuery + " WHERE ФИО.ФИО Like \"" + textBox1.Text + "%\"");
            }
            else
            {
                GetTable(qualificationQuery + " WHERE ФИО.ФИО Like \"" + textBox1.Text + "%\"");
            }

            int rowCount = dataGridView1.RowCount;
            toolStripStatusLabel1.Text = "Количество записей: " + rowCount.ToString() + "";
        }

        private void button5_Click(object sender, EventArgs e) //очистка поиск. поля
        {
            textBox1.Clear();
            Form1_Load(sender, e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Поиск по ФИО") textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox1.Text = "Поиск по ФИО";
        }


//------------------------------------------- ОТКРЫТИЕ ТАБЛИЦ ---------------------------------------

        private void фИОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = placementTitle;
            Form1_Load(sender, e);
        }

        private void квалификацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = qualificationTitle;
            Form1_Load(sender, e);
        }


//----------------------------------------- ОТКРЫТИЕ СПРАВОЧНИКОВ -----------------------------------

        private void фИОToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - ФИО");
        }

        private void образованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - Образование");
        }

        private void специализацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - Специализация");
        }

        private void фИОToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - Квалификационная категория");
        }

        private void учебныйГодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - Учебный год");
        }

        private void направленностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(form3, "Справочники - Направленность");
        }


//--------------------------------------------- ВЫГРУЗКА --------------------------------------------

        private void excelToolStripMenuItem_Click(object sender, EventArgs e) //выгрузка
        {
            string text = "Вы уверены, что хотите выгрузить текущую таблицу в новую книгу Excel?";
            string title = "Выгрузка - предупреждение";
            DialogResult result = MessageBox.Show(text, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                excelExport.Export(dataGridView1);
            }
        }


//------------------------------------------ КОНТЕКСТНОЕ МЕНЮ ---------------------------------------

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e) //вызов контекстного меню
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) // копирование выбранной ячейки таблицы в буфер обмена
        {
            string cell = dataGridView1.CurrentCell.Value.ToString();
            Clipboard.SetText(cell);
        }


//---------------------------------------------- СПРАВКА --------------------------------------------

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Программа: База данных для работы с педагогическими кадрами\n\nРазработал: "
+ "Плешков В.В\n\nНаучный преподаватель: Абильмажинова Н.Н\n\nБратск, 2024 год";
            string title = "Справка - О программе";
            MessageBox.Show(text, title);
        }

        private void операцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Операции позволяют выполнять основные действия с таблицами (записями) или справочниками (наименованиями). Операции можно активировать нажатием кнопок или вызовом в панели меню.\r\n\r\nОбновление: перерисовка и заполнение таблицы / справочника данными из информационной базы. Операцию необходимо выполнять вручную при каждом добавлении, добавлении копированием и изменении записей / наименований.\r\n\r\nДобавление: создание в информационной базе новой записи / наименования. Для выполнения операции необходимо на появившейся карточке добавления заполнить нужные поля данными, после чего нажать на кнопку. После добавления обновление обязательно.\r\n\r\nИзменение: правка существующей записи / наименования в информационной базе. Для выполнения операции необходимо на появившейся карточке изменения изменить нужные поля новыми данными, после чего нажать на кнопку. После изменения обновление обязательно.\r\n\r\nДобавление копированием: создание в информационной базе новой записи / наименования, заполненной данными другой записи / наменования. Для выполнения операции необходимо выделить нужную строку (любую ее ячейку) и нажать на кнопку. В открывшейся карточке можно нажать на кнопку сразу или, при необходимости, внести правки. После добавления копированием обновление не обязательно.\r\n\r\nУдаление: очищение информационной базы от конкретной записи / наименования. Для выполнения операции необходимо выделить нужную строку (любую ее ячейку) и нажать на кнопку.\r\n\r\nВыход: закрытие программы с освобождением ресурсом и сохранение информационной базы.";
            string title = "Помощь - Операции";
            MessageBox.Show(text, title);
        }

        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Таблицы - основные объекты конфигурации. Они позволяют хранить в себе факт совершенных хозяйственных операций, имеющихся в организации / предприятии событий и так далее. Это могут быть, например, приходные накладные, приказы о приеме на работу, счета, платежные поручения и т.д.\nКаждая строка таблицы называется записью.";
            string title = "Помощь - Таблицы";
            MessageBox.Show(text, title);
        }

        private void справочникиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string title = "Справка - Справочники";
            string text = "Справочники - объекты конфигурации, позволяющие хранить "
            + " в информационной базе данные, имеющие одинаковую структуру и списочный "
            + "характер. Каждый элемент справочника характеризуется кодом и "
            + "наименованием.";
            MessageBox.Show(text, title);
        }

        private void выгрузкаБазыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text = "Выгрузка - перенос данных таблицы в другую информационную среду. При этом текущая база данных сохраняется. Осуществляется перенос текущей таблицы, поэтому перед выгрузкой можно выполнить нужные операции выборки и фильтрации.\r\n\r\nExcel: позволяет выполнить выгрузку в программу Microsoft Office. При этом создается новая книга, с которой затем можно будет работать отдельно.";
            string title = "Помощь - Выгрузка";
            MessageBox.Show(text, title);
        }


//----------------------------------------------- МЕТОДЫ --------------------------------------------

        public void GetTable(string query) //формирование таблиц
        {
            connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
            DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[0].Width = 50;
        }

        void Delete() // удаление записи
        {
            string text = "Выбранная запись будет удалена";
            string title = "Удаление записи";
            DialogResult dialogResult = MessageBox.Show(text, title, MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string deleteQuery = "Delete * from Расстановка Where Расстановка.Код = " + id + "";
                OleDbCommand command = new OleDbCommand(deleteQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }  
        }

        void OpenForm(Form form, string title) //открытие формы справочников
        {
            form.Text = title;
            form.ShowDialog();
        }    
    }
}
