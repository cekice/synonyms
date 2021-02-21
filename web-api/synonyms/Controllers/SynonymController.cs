using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace synonyms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SynonymController : ControllerBase
    {
       
        private readonly ISynonymService _synonymService;

        public SynonymController(ISynonymService synonymService)
        {
            _synonymService = synonymService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSynonyms([FromBody] string[] synonyms)
        {
                await _synonymService.AddSynonyms(synonyms);
                return Ok();            
        }

        [HttpGet("search/{synonym}")]
        public async Task<IActionResult> FindSynonym(string synonym)
        {
                var result = await _synonymService.FindSynonym(synonym);
                return Ok(result);
        }

        [HttpGet("{synonymIndex}")]
        public async Task<IActionResult> GetSynonymsByIndex(int synonymIndex)
        {
                var result = await _synonymService.GetSynonyms(synonymIndex);
                return Ok(result);
        }
    }
}
