using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace RealmLibrary
{
    public class ObservableFactory
    {
        public static IObservable<T> CreateSingle<T>(Func<T> action)
        {
            return Observable.Create<T>(obs =>
            {
                try
                {
                    obs.OnNext(action());
                    obs.OnCompleted();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    obs.OnError(e);
                }

                return Disposable.Empty;

            });
        }

    }
}
