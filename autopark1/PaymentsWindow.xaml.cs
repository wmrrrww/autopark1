using System;
using System.Linq;
using System.Windows;

namespace autopark1
{
    public partial class PaymentsWindow : Window
    {
        private readonly auto_parkEntities _context;

        public PaymentsWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadPaymentsData();
        }

        private void LoadPaymentsData()
        {
            PaymentsGrid.ItemsSource = _context.Платежи.ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                LoadPaymentsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + (ex.InnerException?.Message ?? ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPayment = PaymentsGrid.SelectedItem as Платежи;

                if (selectedPayment != null)
                {
                    // Проверяем, что все поля заполнены
                    if (string.IsNullOrEmpty(selectedPayment.Сумма) || selectedPayment.Дата_Платежа == null ||
                        string.IsNullOrEmpty(selectedPayment.Номер_Счета) || string.IsNullOrEmpty(selectedPayment.Метод_Оплаты))
                    {
                        MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var newPayment = new Платежи
                    {
                        ID_Сотрудника = selectedPayment.ID_Сотрудника,
                        ID_Клиента = selectedPayment.ID_Клиента,
                        Сумма = selectedPayment.Сумма,
                        Дата_Платежа = selectedPayment.Дата_Платежа,
                        Номер_Счета = selectedPayment.Номер_Счета.ToString(), // Преобразование в строку
                        Метод_Оплаты = selectedPayment.Метод_Оплаты
                    };

                    _context.Платежи.Add(newPayment);
                    _context.SaveChanges();

                    LoadPaymentsData();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите платеж для копирования данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.InnerException?.Message ?? ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPayment = PaymentsGrid.SelectedItem as Платежи;
                if (selectedPayment == null)
                {
                    MessageBox.Show("Пожалуйста, выберите платеж для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот платеж?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Платежи.Remove(selectedPayment);
                    _context.SaveChanges();
                    LoadPaymentsData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + (ex.InnerException?.Message ?? ex.Message), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
