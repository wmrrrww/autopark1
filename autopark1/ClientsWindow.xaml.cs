using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using autopark1;

namespace autopark1
{
    public partial class ClientsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public ClientsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadClientsData();
        }

        private void LoadClientsData()
        {
            ClientsGrid.ItemsSource = _context.Клиенты.ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Клиенты selectedClient = (Клиенты)ClientsGrid.SelectedItem;

                if (selectedClient != null)
                {
                    _context.SaveChanges();
                    LoadClientsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Клиенты selectedClient = (Клиенты)ClientsGrid.SelectedItem;

                if (selectedClient != null)
                {
                    Клиенты newClient = new Клиенты
                    {
                        ФИО = selectedClient.ФИО,
                        Почта = selectedClient.Почта,
                        Телефон = selectedClient.Телефон,
                        Адрес = selectedClient.Адрес,
                    };

                    _context.Клиенты.Add(newClient);
                    _context.SaveChanges();

                    LoadClientsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedClient = ClientsGrid.SelectedItem as Клиенты;
                if (selectedClient == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Клиенты.Remove(selectedClient);
                    _context.SaveChanges();
                    LoadClientsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
