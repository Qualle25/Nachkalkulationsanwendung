using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using Library;

namespace Nachkalkulationsanwendung
{
    /// <summary>
    /// Interaktionslogik für edit_Kalkulation.xaml
    /// </summary>
    public partial class edit_Kalkulation : Window
    {
        public delegate void CalculationSave();
        public event CalculationSave OnCalculationSave;
        public edit_Kalkulation()
        {
            InitializeComponent();
            LadenMADTAusschreibung();
            LadenKFZDTAusschreibung();
            LadenMADTNachforderung();
            LadenKFZDTNachforderung();
        }
        

        private void saveKalk_Click(object sender, RoutedEventArgs e)
        {

            KalkModel model = new();
            {
                model.Kunde = tbKunde.Text;
            }
            if (int.TryParse(tbID.Text, out int num))
            {
                model.ID = int.Parse(tbID.Text);
                SQLiteCalc.SaveKalk(model);
                OnCalculationSave?.Invoke();
             
            }
            else
                MessageBox.Show("Bitte Eingabe prüfen");
        }
        
        public void LadenMADTAusschreibung()
        {

            DataTable dt = SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMAAusAnfordern.ItemsSource = dt.DefaultView;
            dgMAAusSichten.ItemsSource=dt.DefaultView;
            dgMAAusBearbeiten.ItemsSource=dt.DefaultView;
            dgMAAusVorOrt.ItemsSource = dt.DefaultView;
            dgMAAusRücksprache.ItemsSource = dt.DefaultView;
            dgMAAusPrüfung.ItemsSource = dt.DefaultView;
            dgMAAusAbgabe.ItemsSource = dt.DefaultView;
        }
        public void LadenKFZDTAusschreibung()
        {
            DataTable dt = SqliteDataAccess.LadenKfzDT(0);
            dgKfzAusAnfordern.ItemsSource = dt.DefaultView;
            dgKfzAusSichten.ItemsSource = dt.DefaultView;
            dgKfzAusBearbeiten.ItemsSource = dt.DefaultView;
            dgKfzAusVorOrt.ItemsSource=dt.DefaultView;
            dgKfzAusRücksprache.ItemsSource = dt.DefaultView;
            dgKfzAusPrüfung.ItemsSource=dt.DefaultView;
            dgKfzAusAbgabe.ItemsSource=dt.DefaultView;
        }
        public void LadenMADTNachforderung()
        {
            DataTable dt=SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMANachfAbklären.ItemsSource = dt.DefaultView;
            dgMANachfBearbeiten.ItemsSource= dt.DefaultView;
            dgMANachfNachfragen.ItemsSource= dt.DefaultView;
            dgMANachfVersenden.ItemsSource= dt.DefaultView;
        }
        public void LadenKFZDTNachforderung()
        {
            DataTable dt=SqliteDataAccess.LadenMitarbeiterDT(0);
            dgKfzNachfAbklären.ItemsSource= dt.DefaultView;
            dgKfzNachfBearbeiten.ItemsSource = dt.DefaultView;
            dgKfzNachfNachfragen.ItemsSource = dt.DefaultView;
            dgKfzNachfVersenden.ItemsSource = dt.DefaultView;
        }

        private void btnEDITAnfAus_Click(object sender, RoutedEventArgs e)
        {
            dataKalkulation win5 = new dataKalkulation();
            win5.Show();
        }
    }
        
}
