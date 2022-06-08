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

        
        List<KalkModel> Kalk = new();
        public winKalkulation()
        {
            InitializeComponent();
            LadenKalkulationsListe();

        }
        public void LadenKalkulationsListe()
        {
            int kID = 0;
            Kalk = SQLiteCalc.LadenKalkListe(kID);
            lbKalk.Items.Clear();
            foreach (KalkModel model in Kalk)
            {
                lbKalk.Items.Add(model);
            }
        }
        private void btn_newCalc_Click(object sender, RoutedEventArgs e)
        {
            edit_Kalkulation win4 = new();
            win4.Owner = this;
            win4.Show();
        }

        private void btn_delCalc_Click(object sender, RoutedEventArgs e)
        {
            if (lbKalk.SelectedItem != null)
            {
                SQLiteCalc.delKalk((KalkModel)lbKalk.SelectedItem);
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
                ((edit_Kalkulation)Application.Current.MainWindow).tbID.Text = lbKalk.SelectedValuePath;
            }
        }
    }
}
