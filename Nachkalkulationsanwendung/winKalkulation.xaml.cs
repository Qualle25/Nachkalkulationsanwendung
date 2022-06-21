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

namespace Nachkalkulationsanwendung
{
    /// <summary>
    /// Interaktionslogik für winKalkulation.xaml
    /// </summary>
    public partial class winKalkulation : Window
    {

        
        List<Kalkulation> Kalk = new();
        public winKalkulation()
        {
            InitializeComponent();
            LadenKalkulationsListe();

        }
        public void LadenKalkulationsListe()
        {
            int kID = 0;
            Kalk = SqliteKalkulation.LadenKalkListe(kID);
            lbKalk.Items.Clear();
            foreach (Kalkulation model in Kalk)
            {
                lbKalk.Items.Add(model);
            }
        }
        private void btn_newCalc_Click(object sender, RoutedEventArgs e)
        {
            edit_Kalkulation win4 = new(string.Empty, edit_Kalkulation.WindowModus.Hinzufügen);
            win4.Owner = this;
            win4.Show();
        }

        private void btn_delCalc_Click(object sender, RoutedEventArgs e)
        {
            if (lbKalk.SelectedItem != null)
            {
                SqliteKalkulation.delKalk((Kalkulation)lbKalk.SelectedItem);
                LadenKalkulationsListe();
            }
            else
            {
                MessageBox.Show("Bitte Kalkualtions aus Liste auswählen");
            }
        }

        private void lbKalk_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbKalk.SelectedItem!=null)
            {
                Kalkulation model = (Kalkulation)lbKalk.SelectedItem; 
                edit_Kalkulation win4 = new(model.ID.ToString(),edit_Kalkulation.WindowModus.Aktualisieren);
                win4.Show();
                win4.tbKunde.Text= model.Kunde.ToString();
                win4.tbID.Text= model.ID.ToString();

            }
        }
    }
}
