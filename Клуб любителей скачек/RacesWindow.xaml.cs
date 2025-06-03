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
    /// Логика взаимодействия для RacesWindow.xaml
    /// </summary>
    public partial class RacesWindow : Window
    {
        private races _currentrace = new races();
        public RacesWindow()
        {
            InitializeComponent();
            DataContext = _currentrace;
            DataGridRaces.ItemsSource = RacesclubEntities.GetContext().races.ToList();
            DataGridRaces.ItemsSource = RacesclubEntities.GetContext().races.OrderBy(x => x.id).ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddRacesWindow().Show();
            Close();
        }
        private void ButtonDeleteGrid_Click(object sender, RoutedEventArgs e)
        {
            var racesRemoving = DataGridRaces.SelectedItems.Cast<races>().ToList();
            if (MessageBox.Show($"Подтвердить удаление?", "!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) !=
               MessageBoxResult.Yes) return;

            try
            {
                var context = RacesclubEntities.GetContext();

                foreach (var race in racesRemoving)
                {
                    var relatedbetting = context.betting.Where(b => b.race_id == race.id).ToList();
                    if (relatedbetting.Any())
                    {
                        context.betting.RemoveRange(relatedbetting);
                    }
                    context.races.Remove(race);
                }

                context.SaveChanges();
                MessageBox.Show("Данные успешно удалены!");
                DataGridRaces.ItemsSource = context.races.OrderBy(x => x.id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
        private void ButtonEditGrid_Click(object sender, RoutedEventArgs e)
        {
            var item = (races)DataGridRaces.SelectedItem;
            new EditRacesWindow(item).Show();
            Close();
        }
        private void BtnToBetting_Click(object sender, RoutedEventArgs e)
        {
            new BettingWindow().Show();
            Close();
        }
        private void BtnToHorse_Click(object sender, RoutedEventArgs e)
        {
            new HorseWindow().Show();
            Close();
        }
    }
}
