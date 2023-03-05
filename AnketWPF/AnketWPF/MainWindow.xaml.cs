using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AnketWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void btn_addorchange_Click(object sender, RoutedEventArgs e)
        {
            Person p = new Person() { Name = tb_name.Text, Surname = tb_surname.Text, Email = tb_email.Text, Phone = tb_phone.Text, Birthday = datepicker_birthday.SelectedDate.Value };

            People.Add(p);

            tb_name.Text = null;
            tb_surname.Text = null;
            tb_email.Text = null;
            tb_phone.Text = null;
            datepicker_birthday.Text = null;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var people = People.ToArray();
            var json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText("people.txt", json);
        }

        private void listbox_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_addorchange.Content = "Edit";
            foreach (var item in People)
            {
                if (listbox_name.SelectedItem.ToString() == item.Name)
                {
                    tb_name.Text = item.Name;
                    tb_surname.Text = item.Surname;
                    tb_email.Text = item.Email;
                    tb_phone.Text = item.Phone;
                    datepicker_birthday.SelectedDate = item.Birthday.Value;
                    break;
                }
            }
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            var fileName = textbox_filename.Text;
            using (StreamReader r = new StreamReader($"{fileName}.txt"))
            {
                string json = r.ReadToEnd();
                ObservableCollection<Person>? people = JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
                People = people;
                listbox_name.ItemsSource = people;
            }
        }

        private void btn_addorchange_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in People)
            {
                if (listbox_name.SelectedItem.ToString() == item.Name)
                {
                    item.Name = tb_name.Text;
                    item.Surname = tb_surname.Text;
                    item.Email = tb_email.Text;
                    item.Phone = tb_phone.Text;
                    item.Birthday = datepicker_birthday.SelectedDate;
                    break;
                }
            }
        }
    }
}
