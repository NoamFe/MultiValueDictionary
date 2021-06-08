using System.Collections.Generic;

namespace MultiValueDictionary
{
    public interface IMultiValueDictionary
    {
        public bool Add(string key, string value);
        public bool Remove(string key, string value);
        public bool RemoveAll(string key);

        public bool KeyExists(string key);
        public bool ValueExists(string key, string value);
        public IEnumerable<string> Keys();
        public IEnumerable<(string, string)> Items();
        public IEnumerable<string> AllMembers();
        public IEnumerable<string> Members(string key);
        public void Clear();

    }
}