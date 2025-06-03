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
    /// Логика взаимодействия для AddBettingWindow.xaml
    /// </summary>
    public partial class AddBettingWindow : Window
    {
        private readonly betting _currentbetting = new betting();
        private readonly RacesclubEntities _bd = new RacesclubEntities();
        public AddBettingWindow()
        {
            InitializeComponent();
            DataContext = _currentbetting;
            ComboBoxHorse.ItemsSource = RacesclubEntities.GetContext().horse.ToList();
            ComboBoxMembers.ItemsSource = RacesclubEntities.GetContext().members.ToList();
            ComboBoxRaces.ItemsSource = RacesclubEntities.GetContext().races.ToList();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var flag = true;
            foreach (Control c in GridAdd.Children)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (((TextBox)c).Text == String.Empty)
                    {
                        flag = false;
                    }
                }
                if (c.GetType() == typeof(ComboBox))
                {
                    if (((ComboBox)c).Text == String.Empty)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                RacesclubEntities.GetContext().betting.Add(_currentbetting);
                RacesclubEntities.GetContext().SaveChanges();
                MessageBox.Show("Ура! Добавилось!");
                new BettingWindow().Show();
                Close();
            }
            else MessageBox.Show("Пусто и грустно :( ");
        }
        private void ButtonAddBack_Click(object sender, RoutedEventArgs e)
        {
            new BettingWindow().Show();
            Close();
        }
        private void TextBoxNum_PreviewTextInput(object sender,
          TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
