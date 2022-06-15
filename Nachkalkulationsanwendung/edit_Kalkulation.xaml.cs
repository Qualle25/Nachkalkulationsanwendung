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
        List <Erträge> listert=new ();
        List<Aufwände> listauf = new();
        public Erträge erträge1;
        
        public edit_Kalkulation(string ID)
        {
            
            InitializeComponent();
            tbID.Text = ID;
            LadenMADTAusschreibung();
            LadenKFZDTAusschreibung();
            LadenMADTNachforderung();
            LadenKFZDTNachforderung();
            LadenMADTNachtragsangebot();
            LadenKFZDTNachtragsangebot();
            LadenMADTAuftrag();
            LadenKFZDTAuftrag();
            LadenMADTLager();
            LadenKFZDTLager();
            LadenMADTTechnik();
            LadenKFZDTTechnik();
            LadenMADTBuchhaltung();
            LadenKFZBuchhaltung();
            LadenErtragsListe();
            LadenAufwandsliste();
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
                MessageBox.Show("Bitte geben Sie eine Zahl bei Auftragsnummer ein");
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
            DataTable dt=SqliteDataAccess.LadenKfzDT(0);
            dgKfzNachfAbklären.ItemsSource= dt.DefaultView;
            dgKfzNachfBearbeiten.ItemsSource = dt.DefaultView;
            dgKfzNachfNachfragen.ItemsSource = dt.DefaultView;
            dgKfzNachfVersenden.ItemsSource = dt.DefaultView;
        }
        public void LadenMADTNachtragsangebot()
        {
            DataTable dt=SqliteDataAccess.LadenMitarbeiterDT(0); 
            dgMANachtrAbklären.ItemsSource=dt.DefaultView;
            dgMANachtrErstellen.ItemsSource= dt.DefaultView;
            dgMANachtrPrüfen.ItemsSource= dt.DefaultView;
            dgMANachtrVersenden.ItemsSource= dt.DefaultView;
        }
        public void LadenKFZDTNachtragsangebot()
        {
            DataTable dt = SqliteDataAccess.LadenKfzDT(0);
            dgKfzNachtrAbklären.ItemsSource = dt.DefaultView;
            dgKfzNachtrErstellen.ItemsSource = dt.DefaultView;
            dgKfzNachtrPrüfem.ItemsSource= dt.DefaultView;
            dgKfzNachtrVersenden.ItemsSource = dt.DefaultView;
        }
        public void LadenMADTAuftrag()
        {
            DataTable dt = SqliteDataAccess.LadenMitarbeiterDT(0); 
            dgMAAuftrErstellen.ItemsSource= dt.DefaultView;
            dgMAAuftrVorOrt.ItemsSource= dt.DefaultView;
            dgMAAuftrÄnderung.ItemsSource= dt.DefaultView;
            dgMAAuftrPreisfindung.ItemsSource= dt.DefaultView;
            dgMAAuftrBestellen.ItemsSource= dt.DefaultView;
            dgMAAuftrRücksprache.ItemsSource= dt.DefaultView;
        }
        public void LadenKFZDTAuftrag()
        {
            DataTable dt=SqliteDataAccess.LadenKfzDT(0);
            dgKfzAuftrErstellen.ItemsSource = dt.DefaultView;
            dgKfzAuftrVorOrt.ItemsSource= dt.DefaultView;
            dgKfzAuftrÄnderung.ItemsSource= dt.DefaultView;
            dgKfzAuftrPreisfindung.ItemsSource= dt.DefaultView;
            dgKfzAuftrBestellen.ItemsSource= dt.DefaultView;
            dgKfzAuftrRücksprache.ItemsSource= dt.DefaultView;
        }
        public void LadenMADTLager()
        {
            DataTable dt = SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMALaWarenannahme.ItemsSource= dt.DefaultView;
            dgMALaWareneinlagerung.ItemsSource= dt.DefaultView;
            dgMALaKommissionieren.ItemsSource= dt.DefaultView;
            dgMALaWarenbereitstellung.ItemsSource= dt.DefaultView;
            dgMALaZwischenlagerung.ItemsSource = dt.DefaultView;
            dgMALaVersand.ItemsSource= dt.DefaultView;
        }
        public void LadenKFZDTLager()
        {
            DataTable dt = SqliteDataAccess.LadenKfzDT(0);
            dgKfzLaWarenannahme.ItemsSource = dt.DefaultView;
            dgKfzLaWareneinlagerung.ItemsSource = dt.DefaultView;
            dgKfzLaKommissionieren.ItemsSource = dt.DefaultView;
            dgKfzLaWarenbereitstellung.ItemsSource=dt.DefaultView;
            dgKfzLaZwischenlagerung.ItemsSource = dt.DefaultView;
            dgKfzLaVersand.ItemsSource= dt.DefaultView;
        }
        public void LadenMADTTechnik()
        {
            DataTable dt=SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMATeRücksprache.ItemsSource = dt.DefaultView;
            dgMATeAuspacken.ItemsSource = dt.DefaultView;
            dgMATeDLintern.ItemsSource = dt.DefaultView;
            dgMATeAbfall.ItemsSource = dt.DefaultView;
            dgMATeLieferung.ItemsSource = dt.DefaultView;
            dgMATeDLextern.ItemsSource = dt.DefaultView;
        }
        public void LadenKFZDTTechnik()
        {
            DataTable dt=SqliteDataAccess.LadenKfzDT(0);
            dgKfzTeRücksprache.ItemsSource= dt.DefaultView;
            dgKfzTeAuspacken.ItemsSource= dt.DefaultView;
            dgKfzTeDLintern.ItemsSource= dt.DefaultView;
            dgKfzTeAbfall.ItemsSource= dt.DefaultView;  
            dgKfzTeLieferung.ItemsSource= dt.DefaultView;
            dgKfzTeDLextern.ItemsSource= dt.DefaultView;    
        }
        public void LadenMADTBuchhaltung()
        {
            DataTable dt = SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMABuEinpflegen.ItemsSource= dt.DefaultView;
            dgMABuPrüfung.ItemsSource= dt.DefaultView;
            dgMABuArchivierung.ItemsSource=dt.DefaultView;
            dgMABuNachkalkualtion.ItemsSource= dt.DefaultView;
        }
        public void LadenKFZBuchhaltung()
        {
            DataTable dt=SqliteDataAccess.LadenKfzDT(0);
            dgKfzBuEinpflegen.ItemsSource = dt.DefaultView;
            dgKfzBuPrüfung.ItemsSource = dt.DefaultView;
            dgKfzBuArchivierung.ItemsSource = dt.DefaultView;
            dgKfzBuNachkalkualtion.ItemsSource = dt.DefaultView;
        }
        //private void btnEDITAnfAus_Click(object sender, RoutedEventArgs e)
        //{
        //    dataKalkulation win5 = new dataKalkulation();
        //    win5.Show();
        //}

        public void LadenErtragsListe()
        {
            if (int.TryParse(tbID.Text, out int num0))
            {
                listert = SqliteErträgeAufwände.LadenErtragsListe(num0);
            }         
            lbErträge.Items.Clear();
            foreach (Erträge erträge in listert)
            {
                lbErträge.Items.Add(erträge);
            }
        }

        private void btAddErlös_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(tbErlösBetrag.Text, out decimal num)&& int.TryParse(tbErtragsID.Text,out int num2)&& int.TryParse(tbID.Text, out int num3))
            {
                Erträge er = new();
                er.IDErtrag = num2;
                er.ID = num3;
                er.Ertrag_Wert = num;
                er.Ertrag =tbErlösPosition.Text.ToString();
                SqliteErträgeAufwände.SaveErtrag(er);
                LadenErtragsListe();
            }
            else
                MessageBox.Show("Bitte geben Sie eine Zahl bei Betrag ein");

        }

        private void btUpdateErträge_Click(object sender, RoutedEventArgs e)
        {
            if (lbErträge.SelectedItem != null)
            {
                if (decimal.TryParse(tbErlösBetrag.Text, out decimal num) && int.TryParse(tbErtragsID.Text, out int num2) && int.TryParse(tbID.Text, out int num3))
                {
                    Erträge ert = new();
                    ert.IDErtrag = num2;
                    ert.ID = num3;
                    ert.Ertrag_Wert = num;
                    SqliteErträgeAufwände.UpdateErtrag((Erträge)lbErträge.SelectedItem);
                    LadenErtragsListe();
                }
                else
                    MessageBox.Show("Bitte geben Sie eine Zahl bei Betrag ein");
            }
            else
            {
                MessageBox.Show("Bitte Ertrag aus Liste auswählen");
            }
        }

        private void btDelErträge_Click(object sender, RoutedEventArgs e)
        {
            if (lbErträge.SelectedItem != null)
            {
                SqliteErträgeAufwände.DelErtrag((Erträge)lbErträge.SelectedItem);
                LadenErtragsListe();
                tbErlösPosition.Clear();
                tbErtragsID.Clear();
                tbErlösBetrag.Clear();

            }
            else
            {
                MessageBox.Show("Bitte Kalkualtions aus Liste auswählen");
            }
        }

        private void lbErträge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbErträge.SelectedItem!=null)
            {
                Erträge er = (Erträge)lbErträge.SelectedItem;
                tbErtragsID.Text=er.IDErtrag.ToString();
                tbErlösBetrag.Text=er.Ertrag_Wert.ToString();
                tbErlösPosition.Text=er.Ertrag.ToString();
            }
        }
        public void LadenAufwandsliste()
        {
            if(int.TryParse(tbID.Text, out int num0))
            {
                listauf = SqliteErträgeAufwände.LadenAufwandsListe(num0);
            }
            lbAufwände.Items.Clear();
            foreach (Aufwände aufwände in listauf)
            {
                lbAufwände.Items.Add(aufwände);
            }
        }

        private void btAddAufwand_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(tbAufwandBetrag.Text, out decimal num) && int.TryParse(tbAufwandsID.Text, out int num2) && int.TryParse(tbID.Text, out int num3))
            {
                Aufwände auf = new();
                auf.IDAufwand = num2;
                auf.ID = num3;
                auf.Wert_Aufwand = num;
                auf.Aufwand = tbErlösPosition.Text.ToString();
                SqliteErträgeAufwände.SaveAufwand(auf);
                LadenAufwandsliste();
            }
            else
                MessageBox.Show("Bitte geben Sie eine Zahl bei Betrag ein");

        }

        private void btUpdateAufwand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelAufwand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbAufwände_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAufwände.SelectedItem != null)
            {
                Aufwände auf = (Aufwände)lbAufwände.SelectedItem;
                tbAufwandsID.Text = auf.IDAufwand.ToString();
                tbAufwandBetrag.Text = auf.Wert_Aufwand.ToString();
                tbAufwandPosition.Text = auf.Aufwand.ToString();
            }
        }


        //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

    }
        
}
