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
            LadenMADT();
            LadenKFZDT();
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
        public void LadenMADT()
        {

            DataTable dt = SqliteDataAccess.LadenMitarbeiterDT(0);
            dgMAAnfordern.ItemsSource = dt.DefaultView;
        }
        public void LadenKFZDT()
        {
            DataTable dt = SqliteDataAccess.LadenKfzDT(0);
                dgKfzAnfordern.ItemsSource = dt.DefaultView;
        }

        private void btnEDITAnfAus_Click(object sender, RoutedEventArgs e)
        {
            dataKalkulation win5 = new dataKalkulation();
            win5.Show();
        }
    }
        
}
