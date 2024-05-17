using autopark1;
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

namespace autopark1
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, соответствуют ли введенные учетные данные заданным
            if (UsernameTextBox.Text == "Администратор" && PasswordTextBox.Password == "1289q")
            {
                // Если учетные данные верны, открываем главное окно приложения
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрываем окно аутентификации
                this.Close();
            }
            else
            {
                // Если учетные данные неверны, выводим сообщение об ошибке
                MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

