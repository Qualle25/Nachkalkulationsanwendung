using Library;
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
using System.Xaml;



namespace Nachkalkulationsanwendung
{

    public partial class Stammdaten : Window
    {
        List<MitarbeiterModel> Mitarbeiter = new List<MitarbeiterModel>();
        List<KfzModel> Kfz = new List<KfzModel>();
        public Stammdaten()
        {
            InitializeComponent();

            LadenMitarbeiterListe();
            LadenKfzListe();
        }
        private void LadenMitarbeiterListe()
        {
            Mitarbeiter = SqliteDataAccess.LadenMitarbeiterListe();
            VerbindenMitarbeiterListe();
        }
        private void LadenKfzListe()
        {
            Kfz = SqliteDataAccess.LadenKfzListe();
            VerbindenKfzListe();
        }
        private void VerbindenMitarbeiterListe()
        {

            listboxMitarbeiter.Items.Clear();
                foreach (MitarbeiterModel model in Mitarbeiter)
			    {
                    listboxMitarbeiter.Items.Add(model);
			    }

        }
        private void VerbindenKfzListe()
        {
            listboxKfz.Items.Clear();
            foreach (KfzModel model in Kfz)
            {
                listboxKfz.Items.Add(model);
            }
        }
        public static void SaveMitarbeiter(MitarbeiterModel mitarbeiter)
        {
            string Text = "insert into Mitarbeiter (Vorname,Nachname,Kostenfaktor)values(" +Vorname.Text+") "
        }

        private void addMitarbeiter_Click(object sender, RoutedEventArgs e)
        {

            MitarbeiterModel m = new MitarbeiterModel();
            m.Vorname = Vorname.Text;
            m.Nachname = Nachname.Text;

            decimal num = 0;

            bool success = decimal.TryParse(Kfaktor.Text, out num);
            if (success)
            {
                m.Kostenfaktor = decimal.Parse(Kfaktor.Text);
                SqliteDataAccess.SaveMitarbeiter(m);
                LadenMitarbeiterListe();
                
                Vorname.Text = "";
                Nachname.Text = "";
                Kfaktor.Text = "";
            }
            else
            {
                MessageBox.Show("Bitte Eingabe prüfen");
            }

            
        }

        private void addKfz_Click(object sender, RoutedEventArgs e)
        {
            KfzModel f = new KfzModel();
            f.Kennzeichen = Kennzeichen.Text;

            decimal num = 0;

            bool success = decimal.TryParse(Faktor.Text, out num);
            if (success)
            {
                f.Faktor = decimal.Parse(Faktor.Text);
                SqliteDataAccess.SaveKfz(f);

                Kennzeichen.Text = "";
                Faktor.Text = "";
            }
            else
            {
                MessageBox.Show("Bitte Eingabe prüfen");
            }

            
        }

        private void AktualisiereKfz_Click(object sender, RoutedEventArgs e)
        {
            LadenKfzListe();
        }
        

        private void delMitarbeiter_Click(object sender, RoutedEventArgs e)
        {
            if (listboxMitarbeiter.SelectedItem != null)
            {
               
                //listboxMitarbeiter.Items.Remove(listboxMitarbeiter.SelectedItem);
                SqliteDataAccess.delmitarbeiter((MitarbeiterModel)listboxMitarbeiter.SelectedItem);
                LadenMitarbeiterListe();
                
            }
            else
            {
                MessageBox.Show("Bitte Mitarbeiter aus Liste auswählen");
            }


        }

        private void listboxMitarbeiter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox lb)
            {
                if ((e.AddedItems.Count > 0) && (e.AddedItems[0] != null))
                {
                    MitarbeiterModel mmo = (MitarbeiterModel)e.AddedItems[0];
                    Vorname.Text = mmo.Vorname;
                    Nachname.Text = mmo.Nachname;
                    Kfaktor.Text = mmo.Kostenfaktor.ToString();
                }
            }




            /*
            MitarbeiterModel mmo = (MitarbeiterModel)listboxMitarbeiter.SelectedItem;
            if (listboxMitarbeiter.SelectedItem != null)
            {
                Vorname.Text = mmo.Vorname;
                Nachname.Text = mmo.Nachname;
                Kfaktor.Text = mmo.Kostenfaktor.ToString();
            }
            else
            {

                MessageBox.Show("Mitarbeiter auswählen");
            }
            */

        }
    }

}
