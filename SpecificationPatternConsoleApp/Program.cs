// See https://aka.ms/new-console-template for more information

using SpecificationPatternConsoleApp.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SpecificationPatternConsoleApp.Specifications;
using SpecificationPatternConsoleApp.Specifications.CarSpecifications;

var serviceProvider = new ServiceCollection()
    .AddTransient<ICarRepository, CarRepository>()
    .BuildServiceProvider();

var carsRepository = serviceProvider.GetService<ICarRepository>();

//ALL CARS
Console.WriteLine("*********************All Cars**********************");
foreach (var car in await carsRepository.GetAllAsync(null))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}

//BMW CARS
Console.WriteLine();
Console.WriteLine("*********************BMW Cars**********************");
var spec1 = new ManufacturerSpecification("BMW");
foreach (var car in await carsRepository.GetAllAsync(spec1))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}

//At least 150hp
Console.WriteLine();
Console.WriteLine("*********************At least 150hp**********************");
var spec2 = new MinHorsepowerSpecification(150);
foreach (var car in await carsRepository.GetAllAsync(spec2))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}

//At least 150hp AND not older thanrom 1990
Console.WriteLine();
Console.WriteLine("*********************At least 150hp and not older than 1990**********************");
var spec3 = new MinHorsepowerSpecification(150)
            .And(new MinYearSpecification(1990));
foreach (var car in await carsRepository.GetAllAsync(spec3))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}

//At least 150hp AND not older than 1990 AND NOT diesel
Console.WriteLine();
Console.WriteLine("*********************At least 150hp and not older than 1990**********************");
var spec4 = new MinHorsepowerSpecification(150)
            .And(new MinYearSpecification(1990))
            .And(new FuelSpecification("diesel").Not());
foreach (var car in await carsRepository.GetAllAsync(spec4))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}

//At least 150hp OR BMW
Console.WriteLine();
Console.WriteLine("*********************At least 150hp or BMW**********************");
var spec5 = new MinHorsepowerSpecification(150)
            .Or(new ManufacturerSpecification("BMW"));
foreach (var car in await carsRepository.GetAllAsync(spec5))
{
    Console.WriteLine($"{car.Id} - {car.Year} {car.Manufacturer} {car.Model} {car.Horsepower}hp");
}