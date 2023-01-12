
using DataAccess;
using DataAccess.Models;

var carManager = new CarManager();

Console.WriteLine("Enter model:");
var model = Console.ReadLine();
Console.WriteLine("Enter make:");
var make = Console.ReadLine();
Console.WriteLine("Enter color:");
var color = Console.ReadLine();
Console.WriteLine("Enter horsepower");
var hp = int.Parse(Console.ReadLine());

carManager.Add(new Car(){Model = model, Make = make, Color = color, HorsePower = hp});

var carCollection = carManager.GetAll();

foreach (var car in carCollection)
{
    Console.WriteLine(car);
}

