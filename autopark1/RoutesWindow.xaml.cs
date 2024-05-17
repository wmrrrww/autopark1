using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class RoutesWindow : Window
    {
        private readonly auto_parkEntities _context;

        public RoutesWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadRoutesData();
        }

        private void LoadRoutesData()
        {
            //RoutesGrid.ItemsSource = _context.Маршруты.ToList();
            RoutesGrid.ItemsSource = _context.Маршруты.Include("Клиенты").Include("Остановки").ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                Маршруты selectedRoute = (Маршруты)RoutesGrid.SelectedItem;

                if (selectedRoute != null)
                {
                    Маршруты newRoute = new Маршруты
                    {
                        ID_Маршрута = selectedRoute.ID_Маршрута,
                        ID_Клиента = selectedRoute.ID_Клиента,
                        ID_Остановки = selectedRoute.ID_Остановки,
                        Название_Маршрута = selectedRoute.Название_Маршрута,
                        Начальная_Точка = selectedRoute.Начальная_Точка,
                        Конечная_Точка = selectedRoute.Конечная_Точка,
                        Расстояние = selectedRoute.Расстояние,
                    };

                    _context.Маршруты.Add(newRoute);
                    _context.SaveChanges();

                    LoadRoutesData();
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
                var selectedRoute = RoutesGrid.SelectedItem as Маршруты;
                if (selectedRoute == null)
                {
                    MessageBox.Show("Пожалуйста, выберите маршрут для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот маршрут?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Маршруты.Remove(selectedRoute);
                    _context.SaveChanges();
                    LoadRoutesData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
