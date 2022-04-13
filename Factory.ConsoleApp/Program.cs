// See https://aka.ms/new-console-template for more information
using Factory.Core.Buiders;
using Factory.Core.Creators;
using Factory.Core.Entities;
using Factory.Core.Mediators;
using Factory.Core.Warehouse;

//Console.WriteLine("Hello, World!");

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

//var dealer1 = new Dealer(carWarehouse1);
//var dealer2 = new Dealer(carWarehouse1);
//var dealer3 = new Dealer(carWarehouse1);


engineWarehouse.Run();
bodyWarehouse.Run();
accessoriesWarehouse.Run();

carWarehouse1.Run();

//Thread.Sleep(21000);

//Task.Run(dealer1.Start);
//Thread.Sleep(100);

//Task.Run(dealer2.Start);
//Thread.Sleep(100);

//Task.Run(dealer3.Start);
//Thread.Sleep(100);

//Task.Run(dealer1.Start);
//Thread.Sleep(100);

//Task.Run(dealer2.Start);
//Thread.Sleep(100);

//Task.Run(dealer3.Start);




// ManualResetEvent _event = new ManualResetEvent(true);
// void P()
//{

//    Console.WriteLine("hihi");
//    Thread.Sleep(5000);
//    _event.WaitOne();
//    Console.WriteLine("hihi x2");
//}

//Task task = new Task(P);
//task.Start();

//Thread.Sleep(4000);
//Console.WriteLine("ccc");
//_event.Reset();

//Console.WriteLine("ccc1");
//Thread.Sleep(4000);
//_event.Set();

Console.ReadLine();

//TODO: Abstract mediator



