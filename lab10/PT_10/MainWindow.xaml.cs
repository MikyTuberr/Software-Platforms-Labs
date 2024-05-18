using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PT_10
{
    public partial class MainWindow : Window
    {
        private BindingList<Car> myCarsBindingList;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCarData();
            InitializeDataGridView();
            //LINQQueryManager.ExecuteLINQQuery(myCarsBindingList, resultTextBlock);
            //Ex2();
            //DemonstrateSortableSearchableBindingList();
        }

        private void Ex2()
        {
            Comparison<Car> arg1 = delegate (Car car1, Car car2)
            {
                return car2.Motor.HorsePower.CompareTo(car1.Motor.HorsePower);
            };

            Predicate<Car> arg2 = delegate (Car car)
            {
                return car.Motor.Model == "TDI";
            };

            Action<Car> arg3 = delegate (Car car)
            {
                MessageBox.Show(car.ToString());
            };

            List<Car> myCarsList = new List<Car>(myCarsBindingList);

            myCarsList.Sort(arg1);
            List<Car> foundCars = myCarsList.FindAll(arg2);
            foundCars.ForEach(arg3);

            myCarsBindingList = new BindingList<Car>(myCarsList);
        }

        public void DemonstrateSortableSearchableBindingList()
        {
            SortableSearchableBindingList<Car> myCars = new SortableSearchableBindingList<Car>
            {
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "CGI"), 2011),
            };

            myCars.ApplySort(new CarComparer());

            StringBuilder sb = new StringBuilder();
            foreach (var car in myCars)
            {
                sb.AppendLine(car.ToString());
            }
            MessageBox.Show(sb.ToString());

            int index = myCars.Find(TypeDescriptor.GetProperties(typeof(Car))["MotorModel"], "TDI");

            if (index != -1)
            {
                MessageBox.Show(myCars[index].ToString());
            }
        }

        private void InitializeCarData()
        {
            myCarsBindingList = new BindingList<Car> {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };
        }

        private void InitializeDataGridView()
        {
            dataGrid.ItemsSource = myCarsBindingList;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzanie poprawności wprowadzonych danych
            if (!string.IsNullOrWhiteSpace(carModelTextBox.Text) &&
                !string.IsNullOrWhiteSpace(motorModelTextBox.Text) &&
                !string.IsNullOrWhiteSpace(displacementTextBox.Text) &&
                !string.IsNullOrWhiteSpace(horsePowerTextBox.Text) &&
                !string.IsNullOrWhiteSpace(yearTextBox.Text))
            {
                double horsePower;
                if (double.TryParse(horsePowerTextBox.Text, out horsePower))
                {
                    myCarsBindingList.Add(new Car(
                        carModelTextBox.Text,
                        new Engine(double.Parse(displacementTextBox.Text), horsePower, motorModelTextBox.Text),
                        int.Parse(yearTextBox.Text)
                    ));
                }
                else
                {
                    MessageBox.Show("Nieprawidłowa wartość dla mocy silnika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być uzupełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = myCarsBindingList;
        }

        private void RemoveCarButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCar = dataGrid.SelectedItem as Car;
            if (selectedCar != null)
            {
                myCarsBindingList.Remove(selectedCar);
            }
            else
            {
                MessageBox.Show("Nie wybrano samochodu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = myCarsBindingList;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            var filteredList = myCarsBindingList.Where(car => car.Model.ToLower().Contains(searchText)).ToList();
            dataGrid.ItemsSource = filteredList;
        }

        private void SortByHorsePowerButton_Click(object sender, RoutedEventArgs e)
        {
            myCarsBindingList = new BindingList<Car>(myCarsBindingList.OrderBy(car => car.Motor.HorsePower).ToList());
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = myCarsBindingList;
        }


    }
}
