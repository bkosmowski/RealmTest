using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RealmLibrary;
using RealmLibrary.Models;
using Realms;

namespace RealmConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Console.ReadKey();
        }

        private static void RunRealmInManyThreads()
        {
            var realmConfig = new RealmConfig("RealmObject.realm");

            var threads = new List<Thread>();
            for (int index = 0; index < 10; index++)
            {
                threads.Add(new Thread(() =>
                {
                    var realm = Realm.GetInstance(realmConfig.Config);

                    realm.RunInTransaction(() =>
                    {
                        for (int i = 0; i < 10000; i++)
                        {
                            realm.Add(new Dog {Name = "temp", Age = 10});
                        }
                    });

                    realm.All<Dog>().ToList();
                }));
            }

            foreach (var thread in threads)
            {
                thread.Start();
                thread.Join();
            }

            var realmWithoutUsing = Realm.GetInstance(realmConfig.Config);

            var dogsWithoutUsing = realmWithoutUsing.All<Dog>();

            realmWithoutUsing.RunInTransaction(() =>
            {
                foreach (var dog in dogsWithoutUsing)
                {
                    dog.Age = 20;
                }
            });

            var disposableRealmConfig = new RealmConfig("RealmObject_disposable.realm");

            var disposabledThreads = new List<Thread>();
            for (int index = 0; index < 10; index++)
            {
                disposabledThreads.Add(new Thread(() =>
                {
                    Realm.GetInstance(disposableRealmConfig.Config).Using(realm =>
                    {
                        realm.RunInTransaction(() =>
                        {
                            for (int i = 0; i < 10000; i++)
                            {
                                realm.Add(new Dog
                                {
                                    Name = "temp",
                                    Age = 10
                                });
                            }
                        });

                        realm.All<Dog>().ToList();
                    });
                }));
            }

            foreach (var disposabledThread in disposabledThreads)
            {
                disposabledThread.Start();
                disposabledThread.Join();
            }

            Realm.GetInstance(realmConfig.Config).Using(realm =>
            {
                var dogWithUsing = realm.All<Dog>();

                realm.RunInTransaction(() =>
                {
                    foreach (var dog in dogWithUsing)
                    {
                        dog.Age = 20;
                    }
                });
            });
        }
    }
}