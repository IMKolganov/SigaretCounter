using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigaretCounter.Models;
using System.Diagnostics.Metrics;

namespace SigaretCounter.Controllers
{
    public class SigaretsCounterController : Controller
    {
        private readonly XgbRackotpgContext _context; 
        public SigaretsCounterController(XgbRackotpgContext context)
        {
            _context = context;
        }
        //Command for create model
        //Scaffold-DbContext “Host=postgres;Database=xgb_pg;Username=xgb_pg;Password=***” Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<int> GetSigaretsCountAsync(int userid = 0)
        {
            if (userid == 0)
                throw new Exception("User not found.");
            List<CounterSigaret> counterSigaretsLst = await _context.CounterSigarets.Where(x => x.Userid == userid).ToListAsync();//
            if (counterSigaretsLst == null)
                throw new Exception("Data not found.");
            return counterSigaretsLst.Sum(x => x.SigaretsCount);
        }

        [HttpGet]
        public async Task<int> PutSigaretsCountAsync(int userid = 0, int count = 0)
        {
            if (userid == 0)
                throw new Exception("User not found.");
            List<CounterSigaret> counterSigaretsLst = await _context.CounterSigarets.Where(x => x.Userid == userid).ToListAsync();
            CounterSigaret counterSigarets = counterSigaretsLst.First();
            if (counterSigaretsLst == null || counterSigarets == null)
                throw new Exception("Data not found.");

            counterSigarets.SigaretsCount = count == 0 ? counterSigarets.SigaretsCount++ : counterSigarets.SigaretsCount + count;
            _context.CounterSigarets.Update(counterSigarets);
            _context.SaveChanges();
            return counterSigarets.SigaretsCount;
        }
    }
}
