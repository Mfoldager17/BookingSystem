using BookingSystemEF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookingSystemDbEntities db = new BookingSystemDbEntities();

            Kunde kundeFraMail = db.Kunde.ToList().Find(k => k.Email == tbMail.Text);

            // var udlejningerFraEmail = from u in db.Udlejning
            //                         join k in db.Kunde
            //                       on u.KundeId equals kundeFromMail.KundeId
            //                      where u.KundeId == kundeFromMail.KundeId
            //
            //join v in db.Værktøj
            //on u.VærktøjId equals v.VærktøjId
            //where u.VærktøjId == v.VærktøjId

            //            select new
            //{
            //  Navn = k.Navn,
            //Værktøj = v.Værktøjsnavn,
            //Udlejningsdato = u.FraDato.ToString(),
            //Afleveringsdato = u.TilDato.ToString(),
            //Pris = (u.TilDato.Day - u.FraDato.Day) * v.døgnpris
            //};
            //gridUdlejninger.ItemsSource = udlejningerFraEmail.ToList().OrderBy(d => d.Afleveringsdato);

            gridUdlejninger.ItemsSource = (from u in db.Udlejning
                                           where u.KundeId == kundeFraMail.KundeId
                                           select new
                                           {
                                               u.UdlejningsId,
                                               u.KundeId,
                                               u.Status,
                                               fraDato = u.FraDato.Date.ToString(),
                                               tilDato = u.TilDato.Date.ToString()
                                           }).ToList();
        }

        private void gridUdlejninger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(gridUdlejninger.SelectedItem);
        }
    }
}