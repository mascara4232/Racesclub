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
    /// Логика взаимодействия для MembersWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private members _currentmember = new members();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _currentmember;
            DataGridMembers.ItemsSource = RacesclubEntities.GetContext().members.ToList();
            DataGridMembers.ItemsSource = RacesclubEntities.GetContext().members.OrderBy(x => x.id).ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddMembersWindow().Show();
            Close();
        }
        private void ButtonDeleteGrid_Click(object sender, RoutedEventArgs e)
        {
            var memberRemoving = DataGridMembers.SelectedItems.Cast<members>().ToList();
            if (MessageBox.Show($"Подтвердить удаление?", "!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) !=
           MessageBoxResult.Yes) return;

            RacesclubEntities.GetContext().members.RemoveRange(memberRemoving);
            RacesclubEntities.GetContext().SaveChanges();
            MessageBox.Show("Ура! Удалилось!");
            DataGridMembers.ItemsSource = RacesclubEntities.GetContext().members.OrderBy(x =>
           x.id).ToList();
        }
        private void BtnToHorse_Click(object sender, RoutedEventArgs e)
        {
            new HorseWindow().Show();
            Close();
        }
        private void BtnToBetting_Click(object sender, RoutedEventArgs e)
        {
            new BettingWindow().Show();
            Close();
        }
        private void ButtonEditGrid_Click(object sender, RoutedEventArgs e)
        {
            var item = (members)DataGridMembers.SelectedItem;
            new EditMembersWindow(item).Show();
            Close();
        }

    }
}
