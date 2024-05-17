using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class RentalsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public RentalsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadClientsData();
        }

        private void LoadClientsData()
        {
            //RentalsGrid.ItemsSource = _context.Аренда.ToList();
            RentalsGrid.ItemsSource = _context.Аренда.Include("Клиенты").ToList();
            RentalsGrid.ItemsSource = _context.Аренда.Include("Автомобили").ToList();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Аренда selectedRental = (Аренда)RentalsGrid.SelectedItem;

                if (selectedRental != null)
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
                Аренда selectedRental = (Аренда)RentalsGrid.SelectedItem;

                if (selectedRental != null)
                {
                    Аренда newRental = new Аренда
                    {
                        ID_Аренды = selectedRental.ID_Аренды,
                        ID_Клиента = selectedRental.ID_Клиента,
                        ID_Автомобиля = selectedRental.ID_Автомобиля,
                        Дата_Начала_Аренды = selectedRental.Дата_Начала_Аренды,
                        Дата_Конца_Аренды = selectedRental.Дата_Конца_Аренды,
                        Арендная_Плата = selectedRental.Арендная_Плата,
                        Количество_Часов = selectedRental.Количество_Часов,
                        Способ_Оплаты = selectedRental.Способ_Оплаты,

                    };

                    _context.Аренда.Add(newRental);
                    _context.SaveChanges();

                    LoadClientsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.InnerException?.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRental = RentalsGrid.SelectedItem as Аренда;
                if (selectedRental == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Аренда.Remove(selectedRental);
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
