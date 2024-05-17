using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class SalesWindow : Window
    {
        private readonly auto_parkEntities _context;

        public SalesWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadSalesData();
        }

        private void LoadSalesData()
        {
            //SalesGrid.ItemsSource = _context.Продажи.ToList();
            SalesGrid.ItemsSource = _context.Продажи.Include("Клиенты").ToList();
            SalesGrid.ItemsSource = _context.Продажи.Include("Автомобили").ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                LoadSalesData();
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
                Продажи selectedSale = (Продажи)SalesGrid.SelectedItem;

                if (selectedSale != null)
                {
                    Продажи newSale = new Продажи
                    {
                        ID_Цены = selectedSale.ID_Цены,
                        ID_Клиента = selectedSale.ID_Клиента,
                        ID_Автомобиля = selectedSale.ID_Автомобиля,
                        Дата_Продажи = selectedSale.Дата_Продажи,
                        Цена = selectedSale.Цена,
                        Способ_Оплаты = selectedSale.Способ_Оплаты,
                    };

                    _context.Продажи.Add(newSale);
                    _context.SaveChanges();

                    LoadSalesData();
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
                var selectedSale = SalesGrid.SelectedItem as Продажи;
                if (selectedSale == null)
                {
                    MessageBox.Show("Пожалуйста, выберите продажу для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту продажу?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Продажи.Remove(selectedSale);
                    _context.SaveChanges();
                    LoadSalesData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
