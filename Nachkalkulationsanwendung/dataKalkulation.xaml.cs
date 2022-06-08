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

namespace Nachkalkulationsanwendung
{
    /// <summary>
    /// Interaktionslogik für dataKalkulation.xaml
    /// </summary>
    public partial class dataKalkulation : Window
    {
        public dataKalkulation()
        {
            InitializeComponent();
            AuswahlKfz();
        }
        public class checkedBoxKfz
        {
            public string Kfz { get; set; }
            public bool MyBool { get; set; }
        }
        public void AuswahlKfz()
        {
            
            checkedBoxKfz k = new();
            k.Kfz = ToString();
            
            DataTable dt = SqliteDataAccess.LadenKfzDT(0);
            dgKfz.ItemsSource = dt.DefaultView;
        }

    }
    
}
