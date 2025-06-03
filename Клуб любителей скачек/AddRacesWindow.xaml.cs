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
    /// Логика взаимодействия для AddRacesWindow.xaml
    /// </summary>
    public partial class AddRacesWindow : Window
    {
        private readonly races _currentrace = new races();
        private readonly RacesclubEntities _bd = new RacesclubEntities();
        public AddRacesWindow()
        {
            InitializeComponent();
            DataContext = _currentrace;
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
                RacesclubEntities.GetContext().races.Add(_currentrace);
                RacesclubEntities.GetContext().SaveChanges();
                MessageBox.Show("Ура! Добавилось!");
                new RacesWindow().Show();
                Close();
            }
            else MessageBox.Show("Пусто и грустно :( ");
        }
        private void ButtonAddBack_Click(object sender, RoutedEventArgs e)
        {
            new RacesWindow().Show();
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
