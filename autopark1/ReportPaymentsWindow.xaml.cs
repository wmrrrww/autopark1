using autopark1;
using System;
using System.Linq;
using System.Windows;

namespace autopark1
{
    public partial class ReportPaymentsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public ReportPaymentsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные пользователем даты
            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.MaxValue;

            try
            {
                // Выполняем запрос с использованием оператора JOIN и фильтруем данные по выбранным датам
                var query = from payment in _context.Платежи
                            join client in _context.Клиенты on payment.ID_Клиента equals client.ID_Клиента
                            where payment.Дата_Платежа >= startDate && payment.Дата_Платежа <= endDate
                            select new
                            {
                                Клиент = client.ФИО,
                                Сумма = payment.Сумма
                            };

                // Отобразим результаты запроса в DataGrid
                reportDataGrid.ItemsSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании отчета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}