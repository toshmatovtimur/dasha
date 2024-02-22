using HumanResourcesDepartmentWPFApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace HumanResourcesDepartmentWPFApp
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {

        public static List<Area>? AreaCombobox { get; set; }
        public static List<JobTitle>? JobCombobox { get; set; }
        public static List<SubDivision>? SubCombobox { get; set; }
        public static List<Personal>? MyListPersonal { get; set; }


        public BaseWindow()
        {
            InitializeComponent();
            StartTable();
        }

        // Заполнение таблицы
        private void StartTable()
        {
           using OkContext ok = new();

            MyListPersonal = ok.Personals.ToList();

            dataGrid.ItemsSource = MyListPersonal;


           //Заполнение Comboboxes
           AreaCombobox = ok.Areas.ToList();
           JobCombobox = ok.JobTitles.ToList();
           SubCombobox = ok.SubDivisions.ToList();



        }


        //При закрытии открывается окно авторизации
        private void BaseClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }


        //Обновление записи
        private async void UpdateEntry(object sender, DataGridCellEditEndingEventArgs e)
        {
            Personal? a = e.Row.Item as Personal;

            using OkContext ok = new();

            if (a.Id != 0)
            {
                var upPer = await ok.Database.ExecuteSqlAsync($"UPDATE Personal SET family = {a.Family}, name = {a.Name}, lastname = {a.Lastname}, subDivision = {a.SubDivision}, jobTitle = {a.JobTitle}, adress = {a.Adress}, area = {a.Area}, inn = {a.Inn}, childrenCount = {a.ChildrenCount} WHERE id = {a.Id}");
                if (upPer == 0)
                    MessageBox.Show("Произошла ошибка при обновлении таблицы(Заявитель)\nПовторите попытку");
            }
        }

        #region Обновление ComboBox
        private async void UpdateSub(object sender, EventArgs e)
        {
            if ((dataGrid.SelectedItem as Personal)?.Id == 0 || (dataGrid.SelectedItem as Personal)?.Id == null)
                return;
            else
            {
                try
                {
                    //Меняем подразделение Заявителю
                    using OkContext ok = new();

                    var GetId = await ok.SubDivisions.AsNoTracking().Where(u => u.NameDivisions == (sender as ComboBox).Text).FirstOrDefaultAsync();

                    if (GetId != null)
                        await ok.Database.ExecuteSqlRawAsync("UPDATE Personal SET subDivision = {0} WHERE Id = {1}", GetId.Id, (dataGrid.SelectedItem as Personal)?.Id);
                    else
                        MessageBox.Show("Произошла ошибка при обновлении данных");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка, повторите попытку", ex.Message);
                }
            }
        }

        private async void UpdateJob(object sender, EventArgs e)
        {
            if ((dataGrid.SelectedItem as Personal)?.Id == 0 || (dataGrid.SelectedItem as Personal)?.Id == null)
                return;
            else
            {
                try
                {
                    //Меняем подразделение Заявителю
                    using OkContext ok = new();

                    var GetId = await ok.JobTitles.AsNoTracking().Where(u => u.NameJobTitle == (sender as ComboBox).Text).FirstOrDefaultAsync();

                    if (GetId != null)
                        await ok.Database.ExecuteSqlRawAsync("UPDATE Personal SET jobTitle = {0} WHERE Id = {1}", GetId.Id, (dataGrid.SelectedItem as Personal)?.Id);
                    else
                        MessageBox.Show("Произошла ошибка при обновлении данных");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка, повторите попытку", ex.Message);
                }
            }
        }

        private async void UpdateArea(object sender, EventArgs e)
        {
            if ((dataGrid.SelectedItem as Personal)?.Id == 0 || (dataGrid.SelectedItem as Personal)?.Id == null)
                return;
            else
            {
                try
                {
                    //Меняем подразделение Заявителю
                    using OkContext ok = new();

                    var GetId = await ok.Areas.AsNoTracking().Where(u => u.NameArea == (sender as ComboBox).Text).FirstOrDefaultAsync();

                    if (GetId != null)
                        await ok.Database.ExecuteSqlRawAsync("UPDATE Personal SET area = {0} WHERE Id = {1}", GetId.Id, (dataGrid.SelectedItem as Personal)?.Id);
                    else
                        MessageBox.Show("Произошла ошибка при обновлении данных");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка, повторите попытку", ex.Message);
                }
            }
        }
        #endregion


        // Добавить запись в Таблицу
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using OkContext ok = new();

            dataGrid.CanUserAddRows = true;
            //INSERT INTO Personal(family, name, lastname, subDivision, jobTitle, adress, area, inn, childrenCount) VALUES(NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)

            var AddPer = await ok.Database.ExecuteSqlAsync($"INSERT INTO Personal(family, name, lastname, subDivision, jobTitle, adress, area, inn, childrenCount) VALUES({null}, {null}, {null}, {null}, {null}, {null}, {null}, {null}, {null})");
            if (AddPer == 0)
                MessageBox.Show("Произошла ошибка при обновлении таблицы(Заявитель)\nПовторите попытку");
            else
            {
                StartTable();
                dataGrid.CanUserAddRows = false;
            }

        }


        // Просмотр карточки сотрудника
        private void ViewCardPerson(object sender, RoutedEventArgs e)
        {
            CardPerson cardPerson = new((dataGrid.SelectedItem as Personal).Id);
            cardPerson.ShowDialog();
        }


        // Удалить запись
        private void DeleteEntry(object sender, RoutedEventArgs e)
        {
            using OkContext ok = new();
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                int numberOfRowDeleted = ok.Database.ExecuteSqlRaw("DELETE FROM Personal WHERE id = {0}", (dataGrid.SelectedItem as Personal)?.Id);
                if (numberOfRowDeleted == 1)
                    StartTable();

                else
                    MessageBox.Show("Произошла ошибка при удалении записи\n Повторите попытку");

            }
           
        }


        // Поиск - добить
        private async void SearchTable(object sender, KeyEventArgs e)
        {
            if (sX.Text == "")
                StartTable();
            
            else
            {
                using OkContext db = new();

                var sesese = await db.Personals.ToListAsync();
                dataGrid.ItemsSource = sesese.Where(a => a.Family.ToLower().Contains(sX.Text.ToLower())).ToList();
            }
                
            
        }


        //Выгрузить в Excel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new()
            {
                DefaultExt = "xlsx"

            };

            if (saveFile.ShowDialog() == true)
            {
                new Thread(() => { CreateFile(saveFile.FileName); }) { IsBackground = true }.Start();
            }
        }


        private void CreateFile(string str)
        {
            #region Стили
            //Стиль главного заголовка
            SLStyle titleStyle = new SLStyle();
            titleStyle.Font.FontName = "Arial";
            titleStyle.Font.FontSize = 12;
            titleStyle.Font.Bold = true;
            titleStyle.SetWrapText(true);
            titleStyle.SetVerticalAlignment(DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center);
            titleStyle.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);

            //Стиль месяца
            SLStyle itemRowHeaderStyle = new SLStyle();
            itemRowHeaderStyle.Font.FontName = "Arial";
            itemRowHeaderStyle.Font.FontSize = 14;
            itemRowHeaderStyle.SetWrapText(true);
            titleStyle.Font.Bold = true;
            itemRowHeaderStyle.SetVerticalAlignment(DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center);
            itemRowHeaderStyle.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
            itemRowHeaderStyle.Border.BottomBorder.BorderStyle = itemRowHeaderStyle.Border.TopBorder.BorderStyle = itemRowHeaderStyle.Border.LeftBorder.BorderStyle = itemRowHeaderStyle.Border.RightBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            itemRowHeaderStyle.Border.BottomBorder.Color = itemRowHeaderStyle.Border.TopBorder.Color = itemRowHeaderStyle.Border.LeftBorder.Color = itemRowHeaderStyle.Border.RightBorder.Color = System.Drawing.Color.Black;

            //Стиль значения
            SLStyle strokeStyle = new SLStyle();
            strokeStyle.Font.FontName = "Arial";
            strokeStyle.Font.FontSize = 12;
            strokeStyle.Font.Bold = false;
            strokeStyle.SetWrapText(true);
            strokeStyle.SetVerticalAlignment(DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center);
            strokeStyle.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
            #endregion

            if (str != string.Empty)
            {
                try
                {
                    // Создаю документ
                    using SLDocument doc = new();


                    // Генерация колонок в зависимости от выбора Месяцев
                    // Создаю объкт таблицы
                    DataTable dt = new();

                    List<string> strings = new() { "Id", "Фамилия", "Имя", "Отчество", "ИНН", "Подразделение", "Должность", "Выплаты", "Адрес", "Район", "Дети" };


                    //Затем в цикле надо задать Заголовки
                    foreach (var item in strings)
                    {
                        dt.Columns.Add(item, typeof(string));

                    }

                    // Задать стиль района Главного Заголовка
                    doc.SetRowHeight(1, 35);

                    // Задать стили заголовков месяцев колонок
                    for (int j = 1; j < strings.Count + 1; j++)
                    {
                        doc.SetColumnWidth(j, 20);
                        doc.SetColumnStyle(j, titleStyle);
                    }

                    //Задаю стиль заголовка
                    doc.ImportDataTable(1, 1, dt, true);

                    //Запрос
                    using OkContext db = new();

                    var getMyPers = from p in db.Personals
                                    join j in db.JobTitles on p.JobTitle equals j.Id
                                    join a in db.Areas on p.Area equals a.Id
                                    join s in db.SubDivisions on p.SubDivision equals s.Id
                                    select new
                                    {
                                        p.Id,
                                        p.Family,
                                        p.Name,
                                        p.Lastname,
                                        a.NameArea,
                                        p.Adress,
                                        p.Inn,
                                        p.ChildrenCount,
                                        s.NameDivisions,
                                        j.NameJobTitle,
                                        j.Salary
                                    };


                    int rowIndex = 2;

                    
                        foreach (var j in getMyPers)
                        {
                            doc.SetCellValue(rowIndex, 1, j.Id);
                            doc.SetCellValue(rowIndex, 2, j.Family);
                            doc.SetCellValue(rowIndex, 3, j.Name);
                            doc.SetCellValue(rowIndex, 4, j.Lastname);
                            doc.SetCellValue(rowIndex, 5, j.Inn);
                            doc.SetCellValue(rowIndex, 6, j.NameDivisions);
                            doc.SetCellValue(rowIndex, 7, j.NameJobTitle);
                            doc.SetCellValue(rowIndex, 8, j.Salary.ToString());
                            doc.SetCellValue(rowIndex, 9, j.Adress);
                            doc.SetCellValue(rowIndex, 10, j.NameArea);
                            doc.SetCellValue(rowIndex, 11, j.ChildrenCount.ToString());
                            doc.SetCellStyle(2, 1, getMyPers.Count() + 1, 11, strokeStyle);
                            doc.SetRowHeight(2, getMyPers.Count(), 40);
                            rowIndex++;
                        }

                    // Сохранение документа
                    doc.SaveAs(str);


                    // Открыть файл
                    Process.Start(new ProcessStartInfo { FileName = str, UseShellExecute = true });

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }

        }
    }
}
 