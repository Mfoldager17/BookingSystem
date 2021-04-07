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
        private int udlejningsIdTilOpdater = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            BookingSystemDbEntities db = new BookingSystemDbEntities();

            Kunde kundeFraMail = db.Kunde.ToList().Find(k => k.Email == tbMail.Text.ToLower().Trim());

            // Her et bevis på brug af LINQ
            gridUdlejninger.ItemsSource = (from u in db.Udlejning
                                           where u.KundeId == kundeFraMail.KundeId
                                           select u
                                           ).ToList();
        }

        private void gridUdlejninger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingSystemDbEntities db = new BookingSystemDbEntities();

            if (gridUdlejninger.SelectedItem != null)
            {
                Udlejning ul = (Udlejning)gridUdlejninger.SelectedItem;
                Værktøj værktøj = db.Værktøj.ToList().Find(v => v.VærktøjId == ul.VærktøjId);
                Kunde kunde = db.Kunde.ToList().Find(k => k.KundeId == ul.KundeId);

                udlejningsIdTilOpdater = ul.UdlejningsId;

                tbUdlejningId.Text = ul.UdlejningsId.ToString();
                combobox.Text = ul.Status;
                tbKundeId.Text = ul.KundeId.ToString();
                tbVærktøjsId.Text = ul.VærktøjId.ToString();
                tbFraDato.Text = ul.FraDato.Day + "-" + ul.FraDato.Month + "-" + ul.FraDato.Year;
                tbTilDato.Text = ul.TilDato.Day + "-" + ul.TilDato.Month + "-" + ul.TilDato.Year;

                tbKundeIdFraKunde.Text = kunde.KundeId.ToString();
                tbKundeNavn.Text = kunde.Navn;
                tbAdresse.Text = kunde.Adresse;
                tbEmail.Text = kunde.Email;

                tbVærktøjsIdFraVærktøj.Text = værktøj.VærktøjId.ToString();
                tbVærktøjsNavn.Text = værktøj.Værktøjsnavn;
                tbBeskrivelseVærktøj.Text = værktøj.Beskrivelse;
                tbDepositumVærktøj.Text = værktøj.depositum + ",-";
                tbDøgnPrisVærktøj.Text = værktøj.døgnpris.ToString();

                tbtotalPris.Text = ul.beregnFuldPris() + ",-";
            }
        }

        private void btnOpdaterStatus_Click(object sender, RoutedEventArgs e)
        {
            BookingSystemDbEntities db = new BookingSystemDbEntities();

            if (gridUdlejninger.SelectedItem != null)
            {
                Udlejning ul = db.Udlejning.ToList().Find(u => u.UdlejningsId == udlejningsIdTilOpdater);
                Kunde kunde = db.Kunde.ToList().Find(k => k.KundeId == ul.KundeId);

                ul.Status = combobox.Text;

                db.SaveChanges();
                gridUdlejninger.ItemsSource = db.Udlejning.ToList().FindAll(u => u.KundeId == ul.KundeId);
            }
            else
            {
                MessageBox.Show("Du skal have valgt en udlejning");
            }
        }
    }
}