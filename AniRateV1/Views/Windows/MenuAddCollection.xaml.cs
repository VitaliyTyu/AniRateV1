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

namespace AniRateV1.Views.Windows
{
    /// <summary>
    /// Interaction logic for MenuAddCollection.xaml
    /// </summary>
    public partial class MenuAddCollection : Window
    {
        public MenuAddCollection()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string NewCollectionName
        {
            get { return AddCollectionBox.Text; }
        }
    }
}
