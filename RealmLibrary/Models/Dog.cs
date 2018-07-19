using System;
using Realms;

namespace RealmLibrary.Models
{
    public class Dog : RealmObject
    {
        public Dog()
        {
            Key = Guid.NewGuid().ToString();
        }
        [PrimaryKey]
        public string Key { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
