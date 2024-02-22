using HumanResourcesDepartmentWPFApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HumanResourcesDepartmentWPFApp
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            StartAdminWindow();
        }


        private void StartAdminWindow()
        {
            using OkContext db = new();

            AreaX.ItemsSource = db.Areas.ToList();

            SubX.ItemsSource = db.SubDivisions.ToList();

            JobX.ItemsSource = db.JobTitles.ToList();


        }


        private void AdminClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }

        // Добавить Район
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using OkContext db = new();
            AreaX.CanUserAddRows = true;
            await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Areas(nameArea) VALUES({null})");
            AreaX.ItemsSource = db.Areas.ToList();
            AreaX.CanUserAddRows = false;
        }


        // Добавить подразделение
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using OkContext db = new();
            SubX.CanUserAddRows = true;
            await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO SubDivisions(nameDivisions) VALUES({null})");
            SubX.ItemsSource = db.SubDivisions.ToList();
            SubX.CanUserAddRows = false;

        }


        // Добавить должность и оклад
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using OkContext db = new();
            JobX.CanUserAddRows = true;
            await db.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO JobTitles(nameJobTitle, salary) VALUES({null}, {null})");
            JobX.ItemsSource = db.JobTitles.ToList();
            JobX.CanUserAddRows = false;

        }






        // Редактировать запись в таблице Районы
        private async void AreaCell(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Считывание строки
            Area? a = e.Row.Item as Area;

            using OkContext db = new();

            if (a.Id != 0)
            {
                //Обновление таблицы Населенный пункт
                await db.Database.ExecuteSqlRawAsync("UPDATE Areas SET nameArea = {0} WHERE Id = {1}", a.NameArea, a.Id);
            }
        }

        // Редактировать записи в таблице Подразделение
        private async void SubCell(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Считывание строки
            SubDivision? a = e.Row.Item as SubDivision;

            using OkContext db = new();

            if (a.Id != 0)
            {
                //Обновление таблицы Населенный пункт
                await db.Database.ExecuteSqlRawAsync("UPDATE SubDivisions SET nameDivisions = {0} WHERE Id = {1}", a.NameDivisions, a.Id);
            }
        }

        // Редактировать записи в таблице Должности и оклады
        private async void JobCell(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Считывание строки
            JobTitle? a = e.Row.Item as JobTitle;

            using OkContext db = new();

            if (a.Id != 0)
            {
                //Обновление таблицы Населенный пункт
                await db.Database.ExecuteSqlRawAsync("UPDATE JobTitles SET nameJobTitle = {0}, salary = {1} WHERE Id = {2}", a.NameJobTitle, a.Salary, a.Id);
            }
        }

        private void DeleteArea(object sender, RoutedEventArgs e)
        {
            using OkContext ok = new();
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                int numberOfRowDeleted = ok.Database.ExecuteSqlRaw("DELETE FROM Areas WHERE id = {0}", (AreaX.SelectedItem as Area)?.Id);
                if (numberOfRowDeleted == 1)
                    StartAdminWindow();

                else
                    MessageBox.Show("Произошла ошибка при удалении записи\n Повторите попытку");

            }

        }

        private void DeleteSub(object sender, RoutedEventArgs e)
        {
            using OkContext ok = new();
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                int numberOfRowDeleted = ok.Database.ExecuteSqlRaw("DELETE FROM SubDivisions WHERE id = {0}", (SubX.SelectedItem as SubDivision)?.Id);
                if (numberOfRowDeleted == 1)
                    StartAdminWindow();

                else
                    MessageBox.Show("Произошла ошибка при удалении записи\n Повторите попытку");

            }

        }

        private void DeleteJobAndSalary(object sender, RoutedEventArgs e)
        {
            using OkContext ok = new();
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                int numberOfRowDeleted = ok.Database.ExecuteSqlRaw("DELETE FROM JobTitles WHERE id = {0}", (JobX.SelectedItem as JobTitle)?.Id);
                if (numberOfRowDeleted == 1)
                    StartAdminWindow();

                else
                    MessageBox.Show("Произошла ошибка при удалении записи\n Повторите попытку");

            }

        }
    }
}
