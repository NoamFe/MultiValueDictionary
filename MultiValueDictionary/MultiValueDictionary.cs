using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionary
{
    public class MultiValueDictionary : IMultiValueDictionary
    {
        public Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
         
      
        public bool Add(string key, string value)
        {
            if (KeyExists(key))
            {
                if (dictionary[key].Contains(value))
                    throw new Exception("ERROR, value already exists");

                dictionary[key].Add(value);
            }
            else
                dictionary.Add(key, new List<string> {value});

            return true;
        }


        public bool Remove(string key, string value)
        {
            if (!KeyExists(key))
                throw new Exception("ERROR, key does not exist");

            if (!dictionary[key].Contains(value))
                throw new Exception("ERROR, value does not exist");

            dictionary[key].Remove(value);
            if (!dictionary[key].Any())
                dictionary.Remove(key);

            return true;
        }

        public bool RemoveAll(string key)
        {
            if (!KeyExists(key))
                throw new Exception("ERROR, key does not exist");

            return dictionary.Remove(key);
        }

        public void Clear() => dictionary.Clear();



        public bool KeyExists(string key) => dictionary.ContainsKey(key);

        public bool ValueExists(string key, string value) => KeyExists(key) && dictionary[key].Contains(value);
      
        public IEnumerable<string> Keys() => dictionary.Keys;


        public IEnumerable<string> Members(string key)
        {
            if (KeyExists(key))
            {
                return dictionary[key];
            }

            throw new Exception("ERROR, key does not exist");
        }

        public IEnumerable<string> AllMembers()
        {
            var results = new List<string>();

            foreach (var key in dictionary)
            {
                results.AddRange(key.Value);
            }

            return results;
        }

        public IEnumerable<(string, string)> Items()
        {
            var results = new List<(string, string)>();

            foreach (var key in dictionary)
            {
                foreach (var value in key.Value)
                {
                    results.Add(new(key.Key, value));
                }
            }

            return results;
        }

    }
}