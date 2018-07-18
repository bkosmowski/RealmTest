using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using RealmLibrary;
using Realms;

namespace RealmConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var realmConfig = new RealmConfig();

            realmConfig.RunInTransaction(() =>
            {
                for (int index = 0; index < 10000; index++)
                {
                //    realmConfig.AddDogoWithoutWrite("Temp dogo", 10);
                }
            });
            
            UseObservableWithRealm();

            Console.ReadKey();
        }

        public static void UseObservableWithRealm()
        {
            //var realmConfig = new RealmConfig();

            //realmConfig.AddDogo("Wafel", 10);

            //var allDogs = realmConfig.GetAllObjects<Dog>();

            //Console.WriteLine($"{allDogs.Count()} dogs in db");

            //var allPeople = realmConfig.GetAllObjects<Person>();

            //Console.WriteLine($"{allPeople.Count()} people in db");


            //ObservableFactory.CreateSingle(() => realmConfig.WithRealm(realm =>
            //    {
            //        realm.Write(() => { realm.Add(new Dog("observable dog", 5, new Person("observer"))); });

            //        return realm.All<Dog>().ToList();
            //    })).SubscribeOn(TaskPoolScheduler.Default)
            //    .ObserveOn(Scheduler.Default)
            //    .Subscribe(result =>
            //    {
            //        var realmInSubscription = Realm.GetInstance(realmConfig.Config);

            //        var updatedDogos = realmInSubscription.All<Dog>();
            //        foreach (var dog in updatedDogos)
            //        {
            //            Console.WriteLine(dog.Name);
            //        }

            //        Console.WriteLine($"{updatedDogos.Count()} dogs in db");
            //    });
        }
        
        //var sw = new Stopwatch();

        //realmConfig.AddDogo("temp", 1);

        //var dogo = realmConfig.GetAllObjects<Dog>().First();

        //sw.Start();

        ////for (int index = 0; index < 10000; index++)
        ////{
        //    realmConfig.RemoveAndAdd(dogo);
        ////}

        //sw.Stop();

        //Console.WriteLine($"Remove and add elapsed in {sw.ElapsedMilliseconds} ms");

        //sw.Reset();

        //sw.Start();

        ////for (int index = 0; index < 10000; index++)
        ////{
        //    realmConfig.UpdateDog(dogo);
        ////}

        //sw.Stop();

        //Console.WriteLine($"Update elapsed in {sw.ElapsedMilliseconds} ms");
    }
}