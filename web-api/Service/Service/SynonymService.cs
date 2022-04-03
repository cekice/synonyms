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
        public static Dictionary<string, int> lookup = new Dictionary<string, int>();
        public static List<List<string>> addedSynonyms = addedSynonyms = new List<List<string>>();
        public List<Task> tasks = new List<Task>();
        public static int index = 0;
        public async Task AddSynonyms(string[] synonyms)
        {
            if (synonyms.Distinct().Count() < synonyms.Length)
                throw new Exception("You have duplicate words!");

            var sw = new Stopwatch();
            sw.Start();

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
            var sw = new Stopwatch();
            sw.Start();
            var result = await Task.FromResult(lookup.Where(x => x.Key.Contains(searchTerm.ToLower())).ToList());
            sw.Stop();
            Console.WriteLine($"Execution Time: {sw.ElapsedMilliseconds} ms");
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
