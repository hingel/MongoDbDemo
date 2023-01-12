
using DataAccess;
using DataAccess.Models;

var carManager = new CarManager();

//Console.WriteLine("Enter model:");
//var model = Console.ReadLine();
//Console.WriteLine("Enter make:");
//var make = Console.ReadLine();
//Console.WriteLine("Enter color:");
//var color = Console.ReadLine();
//Console.WriteLine("Enter horsepower");
//var hp = int.Parse(Console.ReadLine());


//var brown = new Color() { CarColor = "Brown" };
//var saab = new Make() { MakeName = "Saab" };

//carManager.AddColor(brown);
//carManager.AddMake(saab);

//carManager.Add(new Car(){Model = "90", Make = saab, Color = brown, HorsePower = 131});

var colorCollection = carManager.GetAllColors();

foreach (var color in colorCollection)
{
    Console.WriteLine(color.CarColor);
}

var carCollection = carManager.GetAllCars();

foreach (var car in carCollection)
{
    Console.WriteLine(car);
}

