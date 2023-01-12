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
using DataAccess;
using DataAccess.Models;

namespace GUICarDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository<Car> _carManager;

        public MainWindow()
        {
            InitializeComponent();
            _carManager = new CarManager();

            UpdateList();
        }

        private void UpdateList()
        {
            var listCars = _carManager.GetAllCars();

            ListCars.Items.Clear();

            foreach (var car in listCars)
            {
                ListCars.Items.Add(car);
            }
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var carToAdd = new Car();

            var newColor = new DataAccess.Models.Color() {CarColor = Color.Text};
            _carManager.AddColor(newColor);


            //carToAdd.Make = Make.Text; //Här leta upp om färgen redan finns isf lägg till den, annars lägg in ny
            carToAdd.Model = Model.Text;
            carToAdd.Color = newColor;
            carToAdd.HorsePower = int.Parse(HorsePower.Text);

            _carManager.Add(carToAdd);

            UpdateList();

            ClearBoxes();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListCars.SelectedItem is Car car)
            {
                var newCar = new Car()
                {
                    //Make = Make.Text, //Här leta upp om färgen redan finns isf lägg till den, annars lägg in ny.
                    Model = Model.Text,
                    //Color = Color.Text, //Här leta upp om färgen redan finns isf lägg till den, annars lägg in ny
                    HorsePower = int.Parse(HorsePower.Text)
                };

                _carManager.Replace(car.Id, newCar);
            }

            UpdateList();

            ClearBoxes();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListCars.SelectedItem is Car car)
            {
                _carManager.Delete(car.Id);
            }

            UpdateList();

            ClearBoxes();
        }

        private void ClearBoxes()
        {
            Make.Text = string.Empty;
            Model.Text = string.Empty;
            Color.Text = string.Empty;
            HorsePower.Text = string.Empty;
        }

        private void SelectedCar(object sender, SelectionChangedEventArgs e)
        {
            if (ListCars.SelectedItem is Car car)
            {
                Make.Text = car.Make;
                Model.Text = car.Model;
                Color.Text = car.Color;
                HorsePower.Text = car.HorsePower.ToString();
            }
        }
    }
}
