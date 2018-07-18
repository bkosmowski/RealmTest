using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using Realms;

namespace RealmLibrary
{
    public static class RealmExtension
    {
        public static T Using<T>(this Realm realm, Func<Realm, T> action)
        {
            try
            {
                return action(realm);
            }
            finally
            {
                realm.Dispose();
            }
        }

        public static void RunInTransaction(this Realm realm, Action action)
        {
            realm.Write(action);
        }
    }
}
