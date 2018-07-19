using System;
using System.Linq;
using System.Threading.Tasks;
using RealmLibrary.Models;
using Realms;

namespace RealmLibrary
{
    public class RealmConfig
    {
        public RealmConfiguration Config { get; }

        public RealmConfig(string dbName)
        {
            Config = new RealmConfiguration(dbName) {ShouldDeleteIfMigrationNeeded = true, ShouldCompactOnLaunch = (bytes, used) => true};
        }

        
        public Task<IQueryable<T>> GetAllObjectsAsync<T>() where T : RealmObject
        {
            return Task.Run(() =>   
            {
                var realmThread = Realm.GetInstance(Config);

                var temp = realmThread.All<T>();

                return temp;
            });
        }
        
        public T WithRealm<T>(Func<Realm, T> action)
        {
            return Realm.GetInstance(Config).Using(action);
        }
    }
}
