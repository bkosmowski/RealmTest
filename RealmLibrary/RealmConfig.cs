using System;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace RealmLibrary
{
    public class RealmConfig
    {
        private readonly Realm _realmObject;
        public RealmConfiguration Config { get; }

        public RealmConfig()
        {
            Config = new RealmConfiguration("RealmObject.realm") {ShouldDeleteIfMigrationNeeded = true};

            _realmObject = Realm.GetInstance(Config);
        }

        public IQueryable<T> GetAllObjects<T>() where T : RealmObject
        {
            return _realmObject.All<T>();
        }

        //public void AddDogo(string name, int age)
        //{
        //    var person = GetAnyPerson();

        //    _realmObject.Write(() => { _realmObject.Add(new Dog(name, age, person)); });
        //}

        //public void AddDogoWithoutWrite(string name, int age)
        //{
        //    var person = GetAnyPerson();

        //    _realmObject.Add(new Dog(name, age, person));
        //}

        //private Person GetAnyPerson()
        //{
        //    var people = _realmObject.All<Person>();

        //    return people.Any() ? people.First() : new Person("Błażej");
        //}

        public Task<IQueryable<T>> GetAllObjectsAsync<T>() where T : RealmObject
        {
            return Task.Run(() =>   
            {
                var realmThread = Realm.GetInstance(Config);

                var temp = realmThread.All<T>();

                return temp;
            });
        }

        //public Task AddDogoAsync(string name, int age)
        //{
        //    return Task.Run(() =>
        //    {
        //        var realmThread = Realm.GetInstance(Config);

        //        realmThread.Write(() => { realmThread.Add(new Dog(name, age, new Person("New"))); });
        //    });
        //}

        public T WithRealm<T>(Func<Realm, T> action)
        {
            return Realm.GetInstance(Config).Using(action);
        }

        public void ClearDb()
        {
            _realmObject.Write(() =>
            {
                _realmObject.RemoveAll();
            });
        }

        public void RunInTransaction(Action action)
        {
            _realmObject.RunInTransaction(action);
        }

        //public void RemoveAndAdd(Dog dog)
        //{
        //    _realmObject.Write(() =>
        //    {
        //        _realmObject.Remove(dog);
        //        _realmObject.Add(dog);
        //    });
        //}

        //public void UpdateDog(Dog dog)
        //{
        //    _realmObject.Write(() => { _realmObject.Add(dog, true); });
        //}

        public void AddObject<T>(T objectToAdd) where T : RealmObject
        {
            _realmObject.Write(() => { _realmObject.Add(objectToAdd); });
        }
    }
}
