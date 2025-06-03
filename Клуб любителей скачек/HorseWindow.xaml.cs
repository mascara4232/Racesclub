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
    /// Логика взаимодействия для HorseWindow.xaml
    /// </summary>
    public partial class HorseWindow : Window
    {
        private horse _currenthorse = new horse();
        public HorseWindow()
        {
            InitializeComponent();
            DataContext = _currenthorse;
            DataGridHorse.ItemsSource = RacesclubEntities.GetContext().horse.ToList();
            DataGridHorse.ItemsSource = RacesclubEntities.GetContext().horse.OrderBy(x => x.id).ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddHorseWindow().Show();
            Close();
        }
        private void ButtonDeleteGrid_Click(object sender, RoutedEventArgs e)
        {
            var horseRemoving = DataGridHorse.SelectedItems.Cast<horse>().ToList();
            if (MessageBox.Show($"Подтвердить удаление?", "!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) !=
               MessageBoxResult.Yes) return;

            try
            {
                var context = RacesclubEntities.GetContext();

                foreach (var horse in horseRemoving)
                {
                    var relatedbetting = context.betting.Where(b => b.horse_id == horse.id).ToList();
                    if (relatedbetting.Any())
                    {
                        context.betting.RemoveRange(relatedbetting);
                    }
                    context.horse.Remove(horse);
                }

                context.SaveChanges();
                MessageBox.Show("Данные успешно удалены!");
                DataGridHorse.ItemsSource = context.horse.OrderBy(x => x.id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
        private void ButtonEditGrid_Click(object sender, RoutedEventArgs e)
        {
            var item = (horse)DataGridHorse.SelectedItem;
            new EditHorseWindow(item).Show();
            Close();
        }
        private void BtnToRaces_Click(object sender, RoutedEventArgs e)
        {
            new RacesWindow().Show();
            Close();
        }
        private void BtnToMembers_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
