using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class StopsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public StopsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadStopsData();
        }

        private void LoadStopsData()
        {
            //StopsGrid.ItemsSource = _context.Остановки.ToList();
            StopsGrid.ItemsSource = _context.Остановки.Include("Клиенты").ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                LoadStopsData();
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
                Остановки selectedStop = (Остановки)StopsGrid.SelectedItem;

                if (selectedStop != null)
                {
                    Остановки newStop = new Остановки
                    {
                        ID_Остановки = selectedStop.ID_Остановки,
                        ID_Клиента = selectedStop.ID_Клиента,
                        Название_Остановки = selectedStop.Название_Остановки,
                        Местоположение = selectedStop.Местоположение,
                        Время_Прибытия = selectedStop.Время_Прибытия,
                        Время_Отправления = selectedStop.Время_Отправления,
                    };

                    _context.Остановки.Add(newStop);
                    _context.SaveChanges();

                    LoadStopsData();
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
                var selectedStop = StopsGrid.SelectedItem as Остановки;
                if (selectedStop == null)
                {
                    MessageBox.Show("Пожалуйста, выберите остановку для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту остановку?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Остановки.Remove(selectedStop);
                    _context.SaveChanges();
                    LoadStopsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
