using System;
using System.Linq;
using System.Windows;
using autopark1;

namespace autopark1
{
    public partial class EmployeesWindow : Window
    {
        private readonly auto_parkEntities _context;

        public EmployeesWindow()
        {
            InitializeComponent();
            _context = new auto_parkEntities();
            LoadData();
        }

        private void LoadData()
        {
            EmployeesGrid.ItemsSource = _context.Сотрудники.ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
                LoadData();
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
                Сотрудники selectedEmployee = (Сотрудники)EmployeesGrid.SelectedItem;

                if (selectedEmployee != null)
                {
                    using (var context = new auto_parkEntities())
                    {
                        Сотрудники newEmployee = new Сотрудники
                        {
                            ФИО = selectedEmployee.ФИО,
                            Должность = selectedEmployee.Должность,
                            Телефон = selectedEmployee.Телефон,
                            Почта = selectedEmployee.Почта,
                        };

                        context.Сотрудники.Add(newEmployee);
                        context.SaveChanges();
                    }

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEmployee = EmployeesGrid.SelectedItem as Сотрудники;
                if (selectedEmployee == null)
                {
                    MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Сотрудники.Remove(selectedEmployee);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

