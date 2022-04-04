using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SynonymService : ISynonymService
    {
        private readonly object lookupLock = new object();
        private static Dictionary<string, int> lookup = new Dictionary<string, int>();
        private static List<List<string>> addedSynonyms = new List<List<string>>();
        private List<Task> tasks = new List<Task>();
        private static int index = 0;

        public SynonymService() { }

        public async Task AddSynonyms(string[] synonyms)
        {
            if (synonyms.Distinct().Count() < synonyms.Count())
                throw new Exception("You have duplicate words!");

            foreach (var synonym in synonyms)
                tasks.Add(Task.Run(() => {
                    lock (lookupLock)
                    {
                        lookup.Add(synonym, index);
                    }
                }));

            addedSynonyms.Add(new List<string>(synonyms));

            await Task.WhenAll(tasks);
            index++;
        }

        public async Task<List<KeyValuePair<string, int>>> FindSynonym(string searchTerm)
        {
            var result = await Task.FromResult(lookup.Where(x => x.Key.Contains(searchTerm.ToLower())).ToList());

            if (result == null)
                return null;
            else
                return result;
        }

        public async Task<List<string>> GetSynonyms(int synonymIndex)
        {
            var synonyms = await Task.FromResult(addedSynonyms.ElementAtOrDefault(synonymIndex));
            if (synonyms == null)
                throw new Exception("No Result");
            else
                return synonyms;
        }

    }
}
