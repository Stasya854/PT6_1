using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PT6_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Завантаження користувачів з файлу "users.txt"
            List<User> users = LoadUsersFromFile("users.txt");

            // Встановлення джерела даних для DataGrid
            dgSimple.ItemsSource = users;
        }
        private List<User> LoadUsersFromFile(string filePath)
        {
            var users = new List<User>();

            // Перевірка, чи існує файл
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл не знайдено: {filePath}");
                return users;
            }

            // Зчитування всіх рядків з файлу
            foreach (var line in File.ReadAllLines(filePath))
            {
                // Розділення кожного рядка на частини по символу коми
                var parts = line.Split(',');

                // Перевірка, що рядок містить саме 3 частини (Id, Name, Birthday)
                if (parts.Length == 3)
                {
                    try
                    {
                        var user = new User()
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            Birthday = DateTime.Parse(parts[2])
                        };
                        users.Add(user); // Додавання користувача до списку
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при обробці рядка: {line}\n{ex.Message}");
                    }
                }
            }

            return users; // Повертаємо список користувачів
        }
    }

    // Клас користувача з властивостями
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Details
        {
            get
            {
                return String.Format("{0} was born on {1} and this is a long description of the person.", this.Name, this.Birthday.ToString("D", CultureInfo.CreateSpecificCulture("en-US")));
            }
        }
    }
}