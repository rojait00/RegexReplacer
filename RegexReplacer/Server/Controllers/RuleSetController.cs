using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegexReplacer.Shared;

namespace RegexReplacer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleSetController : ControllerBase
    {
        IList<RuleSet> ruleSets = new List<RuleSet>();
        private readonly ILogger<RuleSetController> _logger;

        public RuleSetController(ILogger<RuleSetController> logger)
        {
            _logger = logger;

            var demoRule = RuleSet.GetDemoRuleSet();

            ruleSets.Add(demoRule);
        }


        [HttpGet]
        public IEnumerable<RuleSet> Get()
        {
            return ruleSets;
        }

        [HttpPost]
        public IEnumerable<RuleSet> Put(RuleSet ruleSet)
        {
            if (ruleSet.Id != Guid.Empty)
            {
                if(ruleSets.Any(x=> x.Id == ruleSet.Id))
                {
                    Delete(ruleSet);
                }

                ruleSets.Add(ruleSet);
            }
            return ruleSets;
        }

        [HttpDelete]
        public IEnumerable<RuleSet> Delete(RuleSet ruleSet)
        {
            if (ruleSet.Id != Guid.Empty)
            {
                ruleSets.Remove(ruleSets.First(x => ruleSet.Name == x.Name));
            }
            return ruleSets;
        }
    }
}