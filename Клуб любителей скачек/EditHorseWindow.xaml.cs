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
    /// Логика взаимодействия для EditHorseWindow.xaml
    /// </summary>
   
        public partial class EditHorseWindow : Window
        {
            private horse _currenthorse = new horse();
            RacesclubEntities bd = new RacesclubEntities();
            public EditHorseWindow(horse selecthorse)
            {
                InitializeComponent();
                if (selecthorse == null) return;

                _currenthorse = selecthorse;
                DataContext = _currenthorse;
                ComboBoxEditMembersLastName.ItemsSource = RacesclubEntities.GetContext().members.ToList();
            ComboBoxEditMembersFirstName.ItemsSource = RacesclubEntities.GetContext().members.ToList();
        }
            private void ButtonEdit_Click(object sender, RoutedEventArgs e)
            {
                var flag = true;
                foreach (Control c in GridEdit.Children)
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
                    RacesclubEntities.GetContext().SaveChanges();
                    MessageBox.Show("Ура! Изменилось!");
                    new HorseWindow().Show();
                    Close();
                }
                else MessageBox.Show("Пусто и грустно :( ");
            }
            private void ButtonEditBack_Click(object sender, RoutedEventArgs e)
            {
                new HorseWindow().Show();
                Close();
            }
            private void TextBoxEditAge_PreviewTextInput(object sender,
           TextCompositionEventArgs e)
            {
                if (!char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
            private void TextBoxEditName_PreviewTextInput(object sender,
           TextCompositionEventArgs e)
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
