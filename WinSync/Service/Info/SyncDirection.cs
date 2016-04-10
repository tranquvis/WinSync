using System;
using System.Collections.Generic;
using System.Linq;

namespace WinSync.Service
{
    public class SyncDirection
    {
        public static SyncDirection 
            TwoWay = new SyncDirection(0, "Two Way"), 
            To1 = new SyncDirection(1, "To Folder 1"),
            To2 = new SyncDirection(2, "To Folder 2");

        private static readonly SyncDirection[] List = { TwoWay, To1, To2 };

        private readonly string _name;
        public int Id { get; private set; }

        private SyncDirection(int id, string name)
        {
            Id = id;
            _name = name;
        }

        /// <summary>
        /// parse SyncDirection from string
        /// </summary>
        /// <param name="name">direction name</param>
        /// <returns>synchronisation direction</returns>
        /// <exception cref="InvalidOperationException">thrown when name is not valid</exception>
        public static SyncDirection Parse(string name)
        {
            return List.First(x => x._name.Equals(name));
        }

        /// <summary>
        /// get SyncDirection from value
        /// </summary>
        /// <param name="value">direction value</param>
        /// <returns>synchronisation direction</returns>
        /// <exception cref="InvalidOperationException">thrown when value is not valid</exception>
        public static SyncDirection FromValue(int value)
        {
            return List.First(x => x.Id == value);
        }

        public override string ToString()
        {
            return _name;
        }

        public static List<string> NameList => List.Select(syncDirection => syncDirection._name).ToList();
    }
}
