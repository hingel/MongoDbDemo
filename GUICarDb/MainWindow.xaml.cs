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
        private readonly IRepository<MyColor> _myColorManager;
        private readonly IRepository<Brand> _myBrandManager;

        public MainWindow(IRepository<Car> carManager, IRepository<MyColor> myColorManager, IRepository<Brand> myBrandManager)
        {
            InitializeComponent();
            _carManager = carManager;
            _myColorManager = myColorManager;
            _myBrandManager = myBrandManager;

            UpdateList();
        }

        private void UpdateList()
        {
            var listCars = _carManager.GetAllItems();

            ListCars.Items.Clear();

            foreach (var car in listCars)
            {
                ListCars.Items.Add(car);
            }
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var carToAdd = new Car();

            var newColor = new MyColor() {CarColor = MyColor.Text};

            //Egentligen leta upp om färgen redan finns, isf skippa det.
            _myColorManager.Add(newColor);

            //Kolla egentligen att detta 'Brand' inte redan finns.
            var newBrand = new Brand() {Name = Brand.Text};
            _myBrandManager.Add(newBrand);

            carToAdd.MyColor = newColor;
            carToAdd.Brand = newBrand;
            
            carToAdd.Model = Model.Text;
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
                    //Brand = Brand.Text, //Här leta upp om färgen redan finns isf lägg till den, annars lägg in ny.
                    Model = Model.Text,
                    //MyColor = MyColor.Text, //Här leta upp om färgen redan finns isf lägg till den, annars lägg in ny
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
            Brand.Text = string.Empty;
            Model.Text = string.Empty;
            MyColor.Text = string.Empty;
            HorsePower.Text = string.Empty;
        }

        private void SelectedCar(object sender, SelectionChangedEventArgs e)
        {
            if (ListCars.SelectedItem is Car car)
            {
                Make.Text = car.Brand.MakeName;
                Model.Text = car.Model;
                Color.Text = car.MyColor.CarColor;
                HorsePower.Text = car.HorsePower.ToString();
            }
        }
    }
}
