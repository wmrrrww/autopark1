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
using System.Windows.Navigation;
using System.Windows.Shapes;
using autopark1;

namespace autopark1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие нового окна для клиентов
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие нового окна для сотрудников
            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.ShowDialog();
        }

        private void PaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание и открытие нового окна для платежей
            PaymentsWindow paymentsWindow = new PaymentsWindow();
            paymentsWindow.ShowDialog();
        }

        // Добавьте обработчики для остальных кнопок здесь

        // Обработчики для остальных кнопок
        private void RoutesButton_Click(object sender, RoutedEventArgs e)
        {
            RoutesWindow routesWindow = new RoutesWindow();
            routesWindow.ShowDialog();
        }

        private void StopsButton_Click(object sender, RoutedEventArgs e)
        {
            StopsWindow stopsWindow = new StopsWindow();
            stopsWindow.ShowDialog();
        }

        private void DriversButton_Click(object sender, RoutedEventArgs e)
        {
            DriversWindow driversWindow = new DriversWindow();
            driversWindow.ShowDialog();
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow salesWindow = new SalesWindow();
            salesWindow.ShowDialog();
        }

        private void RentalsButton_Click(object sender, RoutedEventArgs e)
        {
            RentalsWindow rentalsWindow = new RentalsWindow();
            rentalsWindow.ShowDialog();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            CarsWindow carsWindow = new CarsWindow();
            carsWindow.ShowDialog();
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            ServicesWindow servicesWindow = new ServicesWindow();
            servicesWindow.ShowDialog();
        }

        private void LoadData()
        {
            // Уберите этот блок из конструктора MainWindow и оставьте только вызов LoadData
            //using (var context = new auto_parkEntities())
            //{
            //    MyGrid.ItemsSource = auto_parkEntities.GetContext().Автомобили.ToList();
            //}
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //добавление записи в базу данных
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //обновление записи в базе данных
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //удаление записи из базы данных
        }

        private void SalesReportButton_Click(object sender, RoutedEventArgs e)
        {
            ReportPaymentsWindow reportPaymentsWindow = new ReportPaymentsWindow();
            reportPaymentsWindow.ShowDialog();
        }
    }
}
