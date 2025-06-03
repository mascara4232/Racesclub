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
    /// Логика взаимодействия для AddHorseWindow.xaml
    /// </summary>
    public partial class AddHorseWindow : Window
    {
        private readonly horse _currenthorse = new horse();
        private readonly RacesclubEntities _bd = new RacesclubEntities();
        public AddHorseWindow()
        {
            InitializeComponent();
            DataContext = _currenthorse;
            ComboBoxMembersLastname.ItemsSource = RacesclubEntities.GetContext().members.ToList();
            ComboBoxMembersFirstname.ItemsSource = RacesclubEntities.GetContext().members.ToList();
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
                RacesclubEntities.GetContext().horse.Add(_currenthorse);
                RacesclubEntities.GetContext().SaveChanges();
                MessageBox.Show("Ура! Добавилось!");
                new HorseWindow().Show();
                Close();
            }
            else MessageBox.Show("Пусто и грустно :( ");
        }
        private void ButtonAddBack_Click(object sender, RoutedEventArgs e)
        {
            new HorseWindow().Show();
            Close();
        }
        private void TextBoxAge_PreviewTextInput(object sender,
          TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TextBoxName_PreviewTextInput(object sender, TextCompositionEventArgs
        e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

    }
}
