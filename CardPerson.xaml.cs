using HumanResourcesDepartmentWPFApp.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CardPerson.xaml
    /// </summary>
    public partial class CardPerson : Window
    {
        private int idLokal = 0;
        public CardPerson(int idPerson)
        {
            InitializeComponent();
            idLokal = idPerson;
            StartCardPerson();
        }

        private void StartCardPerson()
        {
            using OkContext db = new();

            var getMyPers = from p in db.Personals.Where(p => p.Id == idLokal)
                            join j in db.JobTitles on p.JobTitle equals j.Id
                            join a in db.Areas on p.Area equals a.Id
                            join s in db.SubDivisions on p.SubDivision equals s.Id
                            select new
                            {
                                FIO = $"{p.Family} {p.Name} {p.Lastname}",
                                AreaP = a.NameArea,
                                Adresss = p.Adress,
                                INN = p.Inn,
                                Children = p.ChildrenCount,
                                Podrazdelenie = s.NameDivisions,
                                Dolzhnost = j.NameJobTitle,
                                Oklad = j.Salary
                            };

            if (getMyPers == null)
                MessageBox.Show("Произошла ошибка при загрузке данных\nПожалуйста повторите попытку");
            else if(getMyPers.Count() == 1)
            {
                foreach (var item in getMyPers)
                {
                    FioX.Text = item.FIO;
                    AreaX.Text = item.AreaP;
                    AdresX.Text = item.Adresss;
                    InnX.Text = item.INN;
                    CountX.Text = item.Children.ToString();
                    PodrX.Text = item.Podrazdelenie;
                    DolX.Text = item.Dolzhnost;
                    OkX.Text = item.Oklad.ToString();
                }
            }

        }

    }
}
