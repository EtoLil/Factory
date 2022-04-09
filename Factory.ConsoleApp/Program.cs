// See https://aka.ms/new-console-template for more information
using Factory.Core;
using Factory.Core.Buiders;
using Factory.Core.Creators;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;

Console.WriteLine("Hello, World!");

var engineCreator = new EngineCreator();
var engineWarehouse = new EngineWarehouse(5);

var bodyCreator = new BodyCreator();
var bodyWarehouse = new BodyWarehouse(3);

var accessoriesCreator = new AccessoriesCreator();
var accessoriesWarehouse = new AccessoriesWarehouse(4);

var engineMediator = new DetailsMediator<Engine>(engineWarehouse, engineCreator);
var bodyMediator = new DetailsMediator<Body>(bodyWarehouse, bodyCreator);
var accessoriesMediator = new DetailsMediator<Accessories>(accessoriesWarehouse, accessoriesCreator);

var carBuilder = new CarBuilder(engineWarehouse, bodyWarehouse, accessoriesWarehouse);
var carWarehouse = new CarWarehouse(3);
var carMediator = new CarMediator(carWarehouse, carBuilder);

Task.Run(engineWarehouse.Start);
Task.Run(bodyWarehouse.Start);
Task.Run(accessoriesWarehouse.Start);
Task.Run(carWarehouse.Start);

Console.ReadLine();
