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

namespace DinerMax3000WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var index = cboAllMenus.SelectedIndex;
            DinerMax3000.WPFClient.DinerMax3000ViewModel currentViewModel = (DinerMax3000.WPFClient.DinerMax3000ViewModel)DataContext;
            foreach(var currentMenuItem in currentViewModel.NewMenuItems)
            {
                currentViewModel.SelectedMenu.SaveNewMenuItem(currentMenuItem.Title, currentMenuItem.Description, currentMenuItem.Price);
            }
            currentViewModel.NewMenuItems = new List<DinerMax3000.Business.MenuItem>();
            BindingOperations.GetBindingExpressionBase(cboAllMenus, ComboBox.ItemsSourceProperty).UpdateTarget();
            cboAllMenus.SelectedIndex = index;
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DinerMax3000.Business.MenuItem newMenuItem = e.Row.Item as DinerMax3000.Business.MenuItem;
            if (newMenuItem != null && e.EditAction == DataGridEditAction.Commit && e.Row.IsNewItem)
            {
                DinerMax3000.WPFClient.DinerMax3000ViewModel currentViewModel = (DinerMax3000.WPFClient.DinerMax3000ViewModel)DataContext;
                currentViewModel.NewMenuItems.Add(newMenuItem);
            }
        }
    }
}
