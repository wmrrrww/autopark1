using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class ServicesWindow : Window
    {
        private readonly auto_parkEntities _context;

        public ServicesWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadServicesData();
        }

        private void LoadServicesData()
        {
            //ServicesGrid.ItemsSource = _context.Услуги_По_Обслуживанию.ToList();
            ServicesGrid.ItemsSource = _context.Услуги_По_Обслуживанию.Include("Автомобили").ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                LoadServicesData();
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
                Услуги_По_Обслуживанию selectedService = (Услуги_По_Обслуживанию)ServicesGrid.SelectedItem;

                if (selectedService != null)
                {
                    Услуги_По_Обслуживанию newService = new Услуги_По_Обслуживанию
                    {
                        ID_Услуги = selectedService.ID_Услуги,
                        ID_Автомобиля = selectedService.ID_Автомобиля,
                        Название_Услуги = selectedService.Название_Услуги,
                        Дата_Начала_Работ = selectedService.Дата_Начала_Работ,
                        Дата_Окончания_Работ = selectedService.Дата_Окончания_Работ,
                        Цена = selectedService.Цена,
                    };

                    _context.Услуги_По_Обслуживанию.Add(newService);
                    _context.SaveChanges();

                    LoadServicesData();
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
                var selectedService = ServicesGrid.SelectedItem as Услуги_По_Обслуживанию;
                if (selectedService == null)
                {
                    MessageBox.Show("Пожалуйста, выберите услугу для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту услугу?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Услуги_По_Обслуживанию.Remove(selectedService);
                    _context.SaveChanges();
                    LoadServicesData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
