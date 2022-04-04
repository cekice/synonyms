using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISynonymService
    {
        Task AddSynonyms(string[] synonyms);
        Task<List<KeyValuePair<string, int>>> FindSynonym(string searchTerm);
        Task<List<string>> GetSynonyms(int synonymIndex);
    }
}
