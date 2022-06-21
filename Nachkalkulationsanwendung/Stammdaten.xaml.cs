using Library;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xaml;

namespace Nachkalkulationsanwendung
{

    public partial class Stammdaten : Window
    {

        public Stammdaten()
        {
            InitializeComponent();

            LadenMitarbeiterDT();
            LadenKfzDT();
        }

        public void LadenMitarbeiterDT()
        {
            int maID = 0;
            DataTable dt = SqliteStammdatenMAKFZ.LadenMitarbeiterDT(maID);
            dgMA.ItemsSource = dt.DefaultView;
        }
  
        private void LadenKfzDT()
        {
            int kfzID = 0;
            DataTable dtk = SqliteStammdatenMAKFZ.LadenKfzDT(kfzID);
            dgKfz.ItemsSource = dtk.DefaultView;
        }

        private void AddMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            Mitarbeiter m = new()
            {
                Vorname = Vorname.Text,
                Nachname = Nachname.Text
            };

            if (decimal.TryParse(Kfaktor.Text, out decimal num))
            {
                m.Kostenfaktor = decimal.Parse(Kfaktor.Text);
                SqliteStammdatenMAKFZ.SaveMitarbeiter(m);
                LadenMitarbeiterDT();
                
                Vorname.Text = "";
                Nachname.Text = "";
                Kfaktor.Text = "";
            }
            else
                MessageBox.Show("Bitte geben Sie bei Faktor eine Zahl ein!");
        }

        private void AddKfz_Click(object sender, RoutedEventArgs e)
        {
            Kfz f = new()
            {
                Kennzeichen = Kennzeichen.Text
            };

            if (decimal.TryParse(Faktor.Text, out decimal num))
            {
                f.Faktor = decimal.Parse(Faktor.Text);
                SqliteStammdatenMAKFZ.SaveKfz(f);
                LadenKfzDT();

                Kennzeichen.Text = "";
                Faktor.Text = "";
            }
            else
                MessageBox.Show("Bitte geben Sie bei Faktor eine Zahl ein!");
        }     

        private void DelMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dgMA.SelectedItem;

            if (dataRowView != null)
            {
                SqliteStammdatenMAKFZ.delMitarbeiter(Convert.ToInt32(dataRowView.Row["IDMA"]));
                LadenMitarbeiterDT();
                Vorname.Clear();
                Nachname.Clear();
                Kfaktor.Clear();
            }
            else
                MessageBox.Show("Bitte Mitarbeiter aus Liste auswählen");
        }

        private void dgMA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dgMA.SelectedItem;

            if (dataRowView != null)
            {
                Vorname.Text = dataRowView.Row["Vorname"].ToString();
                Nachname.Text = dataRowView.Row["Nachname"].ToString();
                Kfaktor.Text = dataRowView.Row["Kostenfaktor"].ToString();
            }
        }

        private void UpdateMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dgMA.SelectedItem;

            if (dataRowView != null)
            {
                if (decimal.TryParse(Kfaktor.Text, out decimal faktor))
                {
                    Mitarbeiter m = new()
                    {
                        Id = Convert.ToInt32(dataRowView.Row["IDMA"]),
                        Vorname = Vorname.Text,
                        Nachname = Nachname.Text,
                        Kostenfaktor = faktor
                    };

                    SqliteStammdatenMAKFZ.updateMitarbeiter(m);
                    LadenMitarbeiterDT();

                    Vorname.Text = "";
                    Nachname.Text = "";
                    Kfaktor.Text = "";
                    dgMA.SelectedIndex = -1;
                }
                else
                    MessageBox.Show("Bitte geben Sie bei Faktor eine Zahl ein!");
            }
            else
                MessageBox.Show("Bitte Mitarbeiter aus Liste auswählen");
        }

        private void UpdateKfz_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)dgKfz.SelectedItem;
            if (dataView != null)
            {
                if (decimal.TryParse(Faktor.Text, out decimal faktor))
                {
                    Kfz f = new()
                    {
                        ID = Convert.ToInt32(dataView.Row["IDKfz"]),
                        Kennzeichen = Kennzeichen.Text,
                        Faktor=faktor
                    };
                    SqliteStammdatenMAKFZ.updateKfz(f);
                    LadenKfzDT();
                    Kennzeichen.Text = "";
                    Faktor.Text = "";
                    dgKfz.SelectedIndex = -1;
                }
                else
                    MessageBox.Show("Bitte geben Sie bei Faktor eine Zahl ein!");
            }
            else
                MessageBox.Show("Bitte Kfz aus Liste auswählen");
        }

        private void DelKfz_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)dgKfz.SelectedItem;

            if (dataView != null)
            {
                SqliteStammdatenMAKFZ.delKfz(Convert.ToInt32(dataView.Row["IDKfz"]));
                LadenKfzDT();
                Vorname.Clear();
                Nachname.Clear();
                Kfaktor.Clear();
            }

            else
                MessageBox.Show("Bitte Kfz aus Liste auswählen");
        }

        private void dgKfz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dgKfz.SelectedItem;

            if (dataRowView != null)
            {
                Kennzeichen.Text = dataRowView.Row["Kennzeichen"].ToString();
                Faktor.Text = dataRowView.Row["Faktor"].ToString();
            }
        }
    }
}
