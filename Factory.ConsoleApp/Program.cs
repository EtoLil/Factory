// See https://aka.ms/new-console-template for more information
using Factory.Core.Buiders;
using Factory.Core.Creators;
using Factory.Core.Entities;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;

Console.WriteLine("Hello, World!");

var engineCreator = new EngineCreator();
var engineWarehouse = new EngineWarehouse(3);

var bodyCreator = new BodyCreator();
var bodyWarehouse = new BodyWarehouse(3);

var accessoriesCreator = new AccessoriesCreator();
var accessoriesWarehouse = new AccessoriesWarehouse(3);

var engineMediator = new DetailsMediator<Engine>(engineWarehouse, engineCreator);
var bodyMediator = new DetailsMediator<Body>(bodyWarehouse, bodyCreator);
var accessoriesMediator = new DetailsMediator<Accessories>(accessoriesWarehouse, accessoriesCreator);

var carBuilder1 = new CarBuilder(engineWarehouse, bodyWarehouse, accessoriesWarehouse);
var carWarehouse1 = new CarWarehouse(2);
var carMediator1 = new CarMediator(carWarehouse1, carBuilder1);

var carBuilder2 = new CarBuilder(engineWarehouse, bodyWarehouse, accessoriesWarehouse);
var carWarehouse2 = new CarWarehouse(2);
var carMediator2 = new CarMediator(carWarehouse2, carBuilder2);

Task.Run(engineWarehouse.Start);
Task.Run(bodyWarehouse.Start);
Task.Run(accessoriesWarehouse.Start);

Task.Run(carWarehouse1.Start);
Task.Run(carWarehouse2.Start);

Console.ReadLine();

//TODO: Abstract mediator
