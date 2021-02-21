using Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SynonymService : ISynonymService
    {
        public static Dictionary<string, int> lookup;
        public static List<List<string>> addedSynonyms;
        public List<Task> tasks = new List<Task>();
        public static int index;
        public async Task AddSynonyms(string[] synonyms)
        {

            var sw = new Stopwatch();
            sw.Start();
            if (lookup == null && addedSynonyms == null)
            {
                lookup = new Dictionary<string, int>();
                addedSynonyms = new List<List<string>>();
            }

            foreach (var synonym in synonyms)
                tasks.Add(Task.Run(() => lookup.Add(synonym.ToLower(), index)));

            addedSynonyms.Add(new List<string>(synonyms));


            await Task.WhenAll(tasks);
            index++;

            sw.Stop();
            Console.WriteLine($"Execution Time: {sw.ElapsedMilliseconds} ms");

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
