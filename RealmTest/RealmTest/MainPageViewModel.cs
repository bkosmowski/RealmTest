using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using RealmLibrary;
using RealmTest.RealmObjets;

namespace RealmTest
{
    public class MainPageViewModel : BindableBase
    {

        private readonly SerialDisposable _progressUpdate;
        public MainPageViewModel()
        {
            _progressUpdate = new SerialDisposable();
            var realmConfig = new RealmConfig();

            ObjectWithProgress = new ObjectWithProgress();

            realmConfig.AddObject(ObjectWithProgress);

            StartProgressCommand = new DelegateCommand(StartProgress);
        }

        private void StartProgress()
        {
            ObjectWithProgress.ResetProgress();
            _progressUpdate.Disposable = Observable.Interval(TimeSpan.FromMilliseconds(100))
                .SubscribeOn(TaskPoolScheduler.Default)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => IncreaseProgress());
        }

        public void IncreaseProgress()
        {
            ObjectWithProgress.IncreaseProgress();

            if (ObjectWithProgress.Progress >= 100)
            {
                _progressUpdate.Disposable = Disposable.Empty;
            }
        }

        public ObjectWithProgress ObjectWithProgress { get; set; }

        public ICommand StartProgressCommand { get; }
    }
}
