﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework8
{
    //Михаил Анкудинов 1. homework8 

    //а) Создать приложение, показанное на уроке, добавив в него защиту от возможных ошибок
    //(не создана база данных, обращение к несуществующему вопросу, открытие слишком большого файла и т.д.).
    //б) Изменить интерфейс программы, увеличив шрифт, поменяв цвет элементов и добавив другие 
    //«косметические» улучшения на свое усмотрение.
    //в) Добавить в приложение меню «О программе» с информацией о программе(автор, версия, авторские права и др.).
    //г) 
    //д) Добавить пункт меню Save As, в котором можно выбрать имя для сохранения базы данных(элемент SaveFileDialog).

    public partial class Form1 : Form
    {
        //База данных с вопросами
        TrueFalse database;
        public Form1()
        {
            InitializeComponent();
        }

        //Кнопка Выход
        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Метод, отвечающий за ввод данных пользователя
        private void New_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(sfd.FileName);
                database.Add("Вопрос: ", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
            };
        }
        //Обработка события изменений значения numericUpDown
        private void NumberValueChanged(object sender, EventArgs e)
        {
            try
            {
                tboxQuestion.Text = database[(int)nudNumber.Value - 1].Text;
                cboxTrue.Checked = database[(int)nudNumber.Value - 1].TrueFalse;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"Подробности: {ex.Message}", "Данный запрос отсутствует");
            }
        }

        //Кнопка Добавить
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (database == null)
            {
                MessageBox.Show("Создайте новую базу данных", "Сообщение");
                return;
            }
            database.Add((database.Count + 1).ToString(), true);
            nudNumber.Maximum = database.Count;
            nudNumber.Value = database.Count;
        }

        //Кнопка Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (nudNumber.Maximum == 1 || database == null) return;
            database.Remove((int)nudNumber.Value);
            nudNumber.Maximum--;
            if (nudNumber.Value > 1) nudNumber.Value = nudNumber.Value;
        }

        // Кнопка Сохранить
        private void miSave_Click(object sender, EventArgs e)
        {
            if (database != null) database.Save();
            else MessageBox.Show("Файл не найден!");
        }

        //Кнопка Открыть
        private void OpenClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                database = new TrueFalse(ofd.FileName);
                database.Load();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = database.Count;
                nudNumber.Value = 1;
            }
        }

        //Кнопка Обновить
        private void SaveQuestClick(object sender, EventArgs e)
        {
            try
            {
                database[(int)nudNumber.Value - 1].Text = tboxQuestion.Text;

                database[(int)nudNumber.Value - 1].TrueFalse = cboxTrue.Checked;
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show($"Подробности: {ex.Message}", "Откройте или создайте файл с вопросами");
            }
        }

        //О программе
        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор программы:\nМихаил Анкудинов\n© Copyright \n Версия 1.0.00", "О программе");
        }


        //Метод, отвечающий за сохрание файла
        private void SaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            

            if(database == null)
            {
                database = new TrueFalse(saveFileDialog1.FileName);
                database.Add("Вопрос: ", true);
                database.Save();
                nudNumber.Minimum = 1;
                nudNumber.Maximum = 1;
                nudNumber.Value = 1;
                MessageBox.Show("Сохранено");
            }
            else
            {
                database.FileName = saveFileDialog1.FileName;
                database.Save();
                MessageBox.Show("Сохранено");
            }
        }


        #region xml и данные в привате
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tboxQuestion_TextChanged(object sender, EventArgs e)
        {

        }

        private void form1_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
