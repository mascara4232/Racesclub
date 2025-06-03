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

namespace Клуб_любителей_скачек
{
    /// <summary>
    /// Логика взаимодействия для BettingWindow.xaml
    /// </summary>
    public partial class BettingWindow : Window
    {
        private betting _currentbetting = new betting();
        public BettingWindow()
        {
            InitializeComponent();
            DataContext = _currentbetting;
            DataGridBetting.ItemsSource = RacesclubEntities.GetContext().betting.ToList();
            DataGridBetting.ItemsSource = RacesclubEntities.GetContext().betting.OrderBy(x => x.id).ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddBettingWindow().Show();
            Close();
        }
        private void ButtonDeleteGrid_Click(object sender, RoutedEventArgs e)
        {
            var bettingRemoving = DataGridBetting.SelectedItems.Cast<betting>().ToList();
            if (MessageBox.Show($"Подтвердить удаление?", "!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) !=
           MessageBoxResult.Yes) return;

            RacesclubEntities.GetContext().betting.RemoveRange(bettingRemoving);
            RacesclubEntities.GetContext().SaveChanges();
            MessageBox.Show("Ура! Удалилось!");
            DataGridBetting.ItemsSource = RacesclubEntities.GetContext().betting.OrderBy(x =>
           x.id).ToList();
        }
        private void ButtonEditGrid_Click(object sender, RoutedEventArgs e)
        {
            var item = (betting)DataGridBetting.SelectedItem;
            new EditBettingWindow(item).Show();
            Close();
        }
        private void BtnToMembers_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
        private void BtnToRaces_Click(object sender, RoutedEventArgs e)
        {
            new RacesWindow().Show();
            Close();
        }
    }
}
