using System;
using System.Linq;
using System.Windows;
using autopark1;


namespace autopark1
{
    public partial class CarsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public CarsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadClientsData();
        }

        private void LoadClientsData()
        {
            CarsGrid.ItemsSource = _context.Автомобили.ToList();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Автомобили selectedCar = (Автомобили)CarsGrid.SelectedItem;

                if (selectedCar != null)
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
                Автомобили selectedCar = (Автомобили)CarsGrid.SelectedItem;

                if (selectedCar != null)
                {
                    Автомобили newCar = new Автомобили
                    {
                        ID_Автомобиля = selectedCar.ID_Автомобиля,
                        Марка = selectedCar.Марка,
                        Модель = selectedCar.Модель,
                        Год = selectedCar.Год,
                        Цвет = selectedCar.Цвет,
                        Цена_Продажи = selectedCar.Цена_Продажи,
                        Цена_Аренды = selectedCar.Цена_Аренды,

                    };

                    _context.Автомобили.Add(newCar);
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
                var selectedCar = CarsGrid.SelectedItem as Автомобили;
                if (selectedCar == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Автомобили.Remove(selectedCar);
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
