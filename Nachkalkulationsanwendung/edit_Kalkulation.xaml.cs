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
        List<Positionen> listpos = new();

        public int AuftragsID { get { return int.TryParse(tbID.Text,out int ID)?ID:0; }} 
        public enum WindowModus
        {
            Hinzufügen,Aktualisieren
        }
        public WindowModus Modus { get; set; }
        public edit_Kalkulation(string ID,WindowModus modus)
        {
            Modus= modus;
            InitializeComponent();
            tbID.Text = ID;
            ErstellenMADT();
            ErstellenKfzDT();
            LadenErtragsListe();
            LadenAufwandsListe();
            LadenPositionsListe();
            LadenMAAufwandAuschreibungAnfordern();

            if (Modus==WindowModus.Aktualisieren)
            {
                saveKalk.Content = "Kalkulation aktualisieren";
            }
        }
        

        private void saveKalk_Click(object sender, RoutedEventArgs e)
        {
            if (Modus==WindowModus.Hinzufügen)
            {
                Kalkulation model = new();
                {
                    model.Kunde = tbKunde.Text;
                }
                if (int.TryParse(tbID.Text, out int num))
                {
                    model.ID = int.Parse(tbID.Text);
                    SqliteKalkulation.SaveKalk(model);
                    OnCalculationSave?.Invoke();
                    SaveMAAufwandAuschreibungAnfordern(AuftragsID);

                }
                else
                    MessageBox.Show("Bitte geben Sie eine Zahl bei Auftragsnummer ein");
            }
            else
            {
                Kalkulation kalkulation = new()
                {
                    ID = AuftragsID,
                    Kunde = tbKunde.Text
                };
                SqliteKalkulation.UpdateKalkulation(kalkulation);
                OnCalculationSave?.Invoke();
                SaveMAAufwandAuschreibungAnfordern(AuftragsID);
            }

            Close();
        }
        
        public void ErstellenMADT()
        {
            DataTable dt = SqliteStammdatenMAKFZ.LadenMitarbeiterDT(0);
            
            DataColumn colHours = dt.Columns.Add("Stunden", typeof(decimal));
            colHours.SetOrdinal(0); // position as first column

            List<string> inputColumns = new List<string>() { "Stunden", "..." }; // extenable

            foreach (DataColumn col in dt.Columns)
            {
                if(!inputColumns.Contains(col.ColumnName))
                    col.ReadOnly = true;
            }
            foreach (DataRow row in dt.Rows)
                row["Stunden"] = 0.00; // set default value
            dgMAAusAnfordern.ItemsSource = dt.DefaultView;
            dgMAAusSichten.ItemsSource=dt.DefaultView;
            dgMAAusBearbeiten.ItemsSource=dt.DefaultView;
            dgMAAusVorOrt.ItemsSource = dt.DefaultView;
            dgMAAusRücksprache.ItemsSource = dt.DefaultView;
            dgMAAusPrüfung.ItemsSource = dt.DefaultView;
            dgMAAusAbgabe.ItemsSource = dt.DefaultView;
            dgMANachfAbklären.ItemsSource = dt.DefaultView;
            dgMANachfBearbeiten.ItemsSource = dt.DefaultView;
            dgMANachfNachfragen.ItemsSource = dt.DefaultView;
            dgMANachfVersenden.ItemsSource = dt.DefaultView;
            dgMANachtrAbklären.ItemsSource = dt.DefaultView;
            dgMANachtrErstellen.ItemsSource = dt.DefaultView;
            dgMANachtrPrüfen.ItemsSource = dt.DefaultView;
            dgMANachtrVersenden.ItemsSource = dt.DefaultView;
            dgMAAuftrErstellen.ItemsSource = dt.DefaultView;
            dgMAAuftrVorOrt.ItemsSource = dt.DefaultView;
            dgMAAuftrÄnderung.ItemsSource = dt.DefaultView;
            dgMAAuftrPreisfindung.ItemsSource = dt.DefaultView;
            dgMAAuftrBestellen.ItemsSource = dt.DefaultView;
            dgMAAuftrRücksprache.ItemsSource = dt.DefaultView;
            dgMALaWarenannahme.ItemsSource = dt.DefaultView;
            dgMALaWareneinlagerung.ItemsSource = dt.DefaultView;
            dgMALaKommissionieren.ItemsSource = dt.DefaultView;
            dgMALaWarenbereitstellung.ItemsSource = dt.DefaultView;
            dgMALaZwischenlagerung.ItemsSource = dt.DefaultView;
            dgMALaVersand.ItemsSource = dt.DefaultView;
            dgMATeRücksprache.ItemsSource = dt.DefaultView;
            dgMATeAuspacken.ItemsSource = dt.DefaultView;
            dgMATePositionen.ItemsSource = dt.DefaultView;
            dgMATeDLintern.ItemsSource = dt.DefaultView;
            dgMATeAbfall.ItemsSource = dt.DefaultView;
            dgMATeLieferung.ItemsSource = dt.DefaultView;
            dgMATeDLextern.ItemsSource = dt.DefaultView;
            dgMABuEinpflegen.ItemsSource = dt.DefaultView;
            dgMABuPrüfung.ItemsSource = dt.DefaultView;
            dgMABuArchivierung.ItemsSource = dt.DefaultView;
            dgMABuNachkalkualtion.ItemsSource = dt.DefaultView;
        }

        public void ErstellenKfzDT()
        {
            DataTable dt = SqliteStammdatenMAKFZ.LadenKfzDT(0);
            DataColumn colDistance = dt.Columns.Add("Distanz", typeof(decimal));
            colDistance.SetOrdinal(0);
            List<string> inputColumns = new List<string>() { "Distanz", "..." }; // extenable

            foreach (DataColumn col in dt.Columns)
            {
                if (!inputColumns.Contains(col.ColumnName))
                    col.ReadOnly = true;
            }
            foreach (DataRow row in dt.Rows)
                row["Distanz"] = 0.00; // set default value
            dgKfzAusAnfordern.ItemsSource = dt.DefaultView;
            dgKfzAusSichten.ItemsSource = dt.DefaultView;
            dgKfzAusBearbeiten.ItemsSource = dt.DefaultView;
            dgKfzAusVorOrt.ItemsSource=dt.DefaultView;
            dgKfzAusRücksprache.ItemsSource = dt.DefaultView;
            dgKfzAusPrüfung.ItemsSource=dt.DefaultView;
            dgKfzAusAbgabe.ItemsSource=dt.DefaultView;
            dgKfzNachfAbklären.ItemsSource = dt.DefaultView;
            dgKfzNachfBearbeiten.ItemsSource = dt.DefaultView;
            dgKfzNachfNachfragen.ItemsSource = dt.DefaultView;
            dgKfzNachfVersenden.ItemsSource = dt.DefaultView;
            dgKfzNachtrAbklären.ItemsSource = dt.DefaultView;
            dgKfzNachtrErstellen.ItemsSource = dt.DefaultView;
            dgKfzNachtrPrüfem.ItemsSource = dt.DefaultView;
            dgKfzNachtrVersenden.ItemsSource = dt.DefaultView;
            dgKfzAuftrErstellen.ItemsSource = dt.DefaultView;
            dgKfzAuftrVorOrt.ItemsSource = dt.DefaultView;
            dgKfzAuftrÄnderung.ItemsSource = dt.DefaultView;
            dgKfzAuftrPreisfindung.ItemsSource = dt.DefaultView;
            dgKfzAuftrBestellen.ItemsSource = dt.DefaultView;
            dgKfzAuftrRücksprache.ItemsSource = dt.DefaultView;
            dgKfzLaWarenannahme.ItemsSource = dt.DefaultView;
            dgKfzLaWareneinlagerung.ItemsSource = dt.DefaultView;
            dgKfzLaKommissionieren.ItemsSource = dt.DefaultView;
            dgKfzLaWarenbereitstellung.ItemsSource = dt.DefaultView;
            dgKfzLaZwischenlagerung.ItemsSource = dt.DefaultView;
            dgKfzLaVersand.ItemsSource = dt.DefaultView;
            dgKfzTeRücksprache.ItemsSource = dt.DefaultView;
            dgKfzTeAuspacken.ItemsSource = dt.DefaultView;
            dgKfzTePositionen.ItemsSource = dt.DefaultView;
            dgKfzTeDLintern.ItemsSource = dt.DefaultView;
            dgKfzTeAbfall.ItemsSource = dt.DefaultView;
            dgKfzTeLieferung.ItemsSource = dt.DefaultView;
            dgKfzTeDLextern.ItemsSource = dt.DefaultView;
            dgKfzBuEinpflegen.ItemsSource = dt.DefaultView;
            dgKfzBuPrüfung.ItemsSource = dt.DefaultView;
            dgKfzBuArchivierung.ItemsSource = dt.DefaultView;
            dgKfzBuNachkalkualtion.ItemsSource = dt.DefaultView;
        }

       


        public void LadenErtragsListe()
        {
            if (int.TryParse(tbID.Text, out int num0))
            {
                listert = SqliteErträgeAufwände.LadenErtragsListe(num0);
            }         
            lbErträge.Items.Clear();
            foreach (Erträge erträge in listert)
            {
                if(lbErträge.Items.Cast<Erträge>().Count(x => x.IDErtrag == erträge.IDErtrag) > 0)
                {
                    MessageBox.Show("Bitte vergeben Sie unterschiedliche Positionsnummern");
                }
                else
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
                er.Ertrag =tbErlösPosition.Text;
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
                    ert.Ertrag = tbErlösPosition.Text;
                    SqliteErträgeAufwände.UpdateErtrag(ert);
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
                MessageBox.Show("Bitte Position aus Liste auswählen");
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
        public void LadenAufwandsListe()
        {
            if (int.TryParse(tbID.Text, out int num0))
            {
                listauf = SqliteErträgeAufwände.LadenAufwandsListe(num0);
            }
            lbAufwände.Items.Clear();
            foreach (Aufwände aufwände in listauf)
            {
                //if (lbErträge.Items.Contains(erträge.IDErtrag)) <- funktioniert nicht
                if (lbAufwände.Items.Cast<Aufwände>().Count(x => x.IDAufwand == aufwände.IDAufwand) > 0)
                {
                    MessageBox.Show("Bitte vergeben Sie unterschiedliche Positionsnummern");
                }
                else
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
                auf.Aufwand = tbAufwandPosition.Text.ToString();
                SqliteErträgeAufwände.SaveAufwand(auf);
                LadenAufwandsListe();
            }
            else
                MessageBox.Show("Bitte geben Sie eine Zahl bei Betrag ein");

        }

        private void btUpdateAufwand_Click(object sender, RoutedEventArgs e)
        {
            if (lbAufwände.SelectedItem != null)
            {
                if (decimal.TryParse(tbAufwandBetrag.Text, out decimal num) && int.TryParse(tbAufwandsID.Text, out int num2) && int.TryParse(tbID.Text, out int num3))
                {
                    Aufwände auf = new();
                    auf.IDAufwand = num2;
                    auf.ID = num3;
                    auf.Wert_Aufwand = num;
                    auf.Aufwand = tbAufwandPosition.Text;
                    SqliteErträgeAufwände.UpdateAufwand(auf);
                    LadenAufwandsListe();
                }
                else
                    MessageBox.Show("Bitte geben Sie eine Zahl bei Betrag ein");
            }
            else
            {
                MessageBox.Show("Bitte Ertrag aus Liste auswählen");
            }
        }

        private void btDelAufwand_Click(object sender, RoutedEventArgs e)
        {
            if (lbAufwände.SelectedItem != null)
            {
                SqliteErträgeAufwände.DelAufwände((Aufwände)lbAufwände.SelectedItem);
                LadenAufwandsListe();
                tbAufwandPosition.Clear();
                tbAufwandsID.Clear();
                tbAufwandBetrag.Clear();

            }
            else
            {
                MessageBox.Show("Bitte Position aus Liste auswählen");
            }
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
        public void LadenPositionsListe()
        {
            if (int.TryParse(tbID.Text, out int num0))
            {
                listpos = SqlitePositionen.LadenPositionsListe(num0);
            }
            lbPositionen.Items.Clear();
            foreach (Positionen positionen in listpos)
            {
                if (lbPositionen.Items.Cast<Positionen>().Count(x => x.PositionID == positionen.PositionID) > 0)
                {
                    MessageBox.Show("Bitte vergeben Sie unterschiedliche Positionsnummern");
                }
                else
                    lbPositionen.Items.Add(positionen);

            }
        }

        private void btAddPosition_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(tbPositionErtrag.Text, out decimal num) && int.TryParse(tbPositionID.Text, out int num2) && int.TryParse(tbID.Text, out int num3)&& decimal.TryParse(tbPositionAufwand.Text, out decimal num4))
            {
                Positionen p = new();
                p.PositionID = num2;
                p.ID = num3;
                p.PErtrag = num;
                p.PAufwand = num4;
                p.Position = tbPositionText.Text.ToString();
                SqlitePositionen.SavePosition(p);
                LadenPositionsListe();
                
            }
            else
                MessageBox.Show("Bitte geben Sie eine Zahl bei Aufwand, Ertrag und Positionsnummer ein");

        }

        private void btUpdatePosition_Click(object sender, RoutedEventArgs e)
        {
            if (lbPositionen.SelectedItem != null)
            {
                if (decimal.TryParse(tbPositionErtrag.Text, out decimal num) 
                    && int.TryParse(tbPositionID.Text, out int num2) 
                    && int.TryParse(tbID.Text, out int num3) 
                    && decimal.TryParse(tbPositionAufwand.Text, out decimal num4))
                {
                    Positionen p = new();
                    p.PositionID = num2;
                    p.ID = num3;
                    p.PErtrag = num;
                    p.PAufwand = num4;
                    p.Position = tbPositionText.Text.ToString();
                    SqlitePositionen.UpdatePosition(p);
                    LadenPositionsListe();
                }
                else
                    MessageBox.Show("Bitte geben Sie eine Zahl bei Aufwand, Ertrag und Positionsnummer ein");
            }
            else
            {
                MessageBox.Show("Bitte Position aus Liste auswählen");
            }
        }

        private void btDelPosition_Click(object sender, RoutedEventArgs e)
        {
            if (lbPositionen.SelectedItem != null)
            {
                SqlitePositionen.DelPosition((Positionen)lbPositionen.SelectedItem);
                LadenPositionsListe();
                tbPositionText.Clear();
                tbPositionID.Clear();
                tbPositionAufwand.Clear();
                tbPositionErtrag.Clear();
            }
            else
            {
                MessageBox.Show("Bitte Position aus Liste auswählen");
            }
        }

        private void lbPositionen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPositionen.SelectedItem != null)
            {
                Positionen p = (Positionen)lbPositionen.SelectedItem;
                tbPositionID.Text = p.PositionID.ToString();
                tbPositionText.Text = p.Position.ToString();
                tbPositionErtrag.Text = p.PErtrag.ToString();
                tbPositionAufwand.Text=p.PAufwand.ToString();
            }
        }
        public void SaveMAAufwandAuschreibungAnfordern(int auftragsID)
        {
            List<long> mitarbeiterIDs = new List<long>();

            if(Modus == WindowModus.Aktualisieren)
            {
                DataTable dt = SqliteKalkulation.LadenMAAufwandAuschreibungAnfordern(dgMAAusAnfordern.Uid, auftragsID);
                mitarbeiterIDs = dt.AsEnumerable()
                    .Where(x => x.Field<double>("Stunden") > 0)
                    .Select(x => x.Field<long>("IDMA"))
                    .ToList();
            }

            foreach (DataRowView row in dgMAAusAnfordern.ItemsSource)
            {
                decimal stunden = decimal.Parse(row.Row["Stunden"].ToString());
                if (stunden > 0)
                {
                    int Id = int.Parse(row.Row["IDMA"].ToString());
                    Mitarbeiter ma = SqliteStammdatenMAKFZ.GetMitarbeiterByID(Id);
                    ma.Arbeitszeit = decimal.Parse(row.Row["Stunden"].ToString());
                    ma.Abschnitt= dgMAAusAnfordern.Uid;

                    if(mitarbeiterIDs.Contains(ma.Id))
                        SqliteMAKFZ.UpdateMitarbeiterArbeitszeit(ma, auftragsID,ma.Abschnitt);
                    else
                        SqliteMAKFZ.SaveMitarbeiterArbeitszeit(ma, auftragsID, ma.Abschnitt);
                }
            }
        }
       public void LadenMAAufwandAuschreibungAnfordern()
        {
            DataTable dt = SqliteKalkulation.LadenMAAufwandAuschreibungAnfordern(dgMAAusAnfordern.Uid,AuftragsID);
            dgMAAusAnfordern.ItemsSource = dt.DefaultView;
        }


    }
}
