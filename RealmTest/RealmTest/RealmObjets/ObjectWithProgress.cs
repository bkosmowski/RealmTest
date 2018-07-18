using System;
using RealmLibrary;
using Realms;

namespace RealmTest.RealmObjets
{
    public class ObjectWithProgress : RealmObject
    {
        public ObjectWithProgress()
        {
            Key = Guid.NewGuid().ToString();
            Progress = 0;
        }
        
        [PrimaryKey]
        public string Key { get; set; }

        public int Progress
        {
            get;
            set;
        }

        public void ResetProgress()
        {
            this.Realm.RunInTransaction(() => Progress = 0);
        }
    }

}
