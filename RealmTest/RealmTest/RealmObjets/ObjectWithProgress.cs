using RealmLibrary;
using Realms;

namespace RealmTest.RealmObjets
{
    public class ObjectWithProgress : RealmObject
    {
        public ObjectWithProgress()
        {
            Progress = 0;
        }
        
        public int Progress
        {
            get;
            private set;
        }

        public void IncreaseProgress()
        {
            this.Realm.RunInTransaction(() => Progress++);
        }

        public void ResetProgress()
        {
            this.Realm.RunInTransaction(() => Progress = 0);
        }
    }

}
