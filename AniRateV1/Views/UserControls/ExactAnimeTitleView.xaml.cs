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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AniRateV1.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ExactAnimeTitleView.xaml
    /// </summary>
    public partial class ExactAnimeTitleView : UserControl
    {
        public ExactAnimeTitleView()
        {
            InitializeComponent();
        }

        public void ChooseingCollectionsListBox_Unselected(object sender, RoutedEventArgs e)
        {
            ChooseingCollectionsListBox.UnselectAll();
        }
    }
}
