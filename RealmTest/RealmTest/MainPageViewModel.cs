using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using RealmLibrary;
using Realms;
using RealmTest.RealmObjets;

namespace RealmTest
{
    public class MainPageViewModel : BindableBase
    {
        private readonly SerialDisposable _progressUpdate;
        private readonly RealmConfiguration _config;
        public MainPageViewModel()
        {
            _progressUpdate = new SerialDisposable();
            var realmConfig = new RealmConfig();
            _config = realmConfig.Config;

            ObjectWithProgress = new ObjectWithProgress();

            realmConfig.AddObject(ObjectWithProgress);

            StartProgressCommand = new DelegateCommand(StartProgress);
        }

        private void StartProgress()
        {
            ObjectWithProgress.ResetProgress();

            string key = ObjectWithProgress.Key;
            
            _progressUpdate.Disposable = Observable.Interval(TimeSpan.FromMilliseconds(100))
                .ObserveOn(TaskPoolScheduler.Default)
                .Subscribe(_ => IncreaseProgress(key));
        }

        public void IncreaseProgress(string key)
        {
            var otherRealmInstance = Realm.GetInstance(_config);

            var foundRealmObject = otherRealmInstance.Find(nameof(RealmObjets.ObjectWithProgress), key) as ObjectWithProgress;

            using (var transaction = otherRealmInstance.BeginWrite())
            {
                if (foundRealmObject != null)
                {
                    foundRealmObject.Progress++;
                }

                transaction.Commit();
            }

            if (foundRealmObject?.Progress >= 100)
            {
                _progressUpdate.Disposable = Disposable.Empty;
            }

            otherRealmInstance.Dispose();
        }

        public ObjectWithProgress ObjectWithProgress { get; set; }

        public ICommand StartProgressCommand { get; }
    }
}
