using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigaretCounter.Data;
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
        //-f Overwrite existing files
        ////Scaffold-DbContext “Host=postgres;Database=xgb_pg;Username=xgb_pg;Password=***” Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -f
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<RsSigaretsCount> GetSigaretsCountAsync(int userid = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                if (startDate == null)
                    startDate = DateTime.Now;
                if (endDate == null)
                    endDate = DateTime.Now.AddDays(1);
                if (userid == 0)
                    throw new Exception("User not found.");
                List<CounterSigaret> counterSigaretsLst = await _context.CounterSigarets
                    .Where(x => x.Userid == userid && x.CurrentDate >= startDate.Value.Date && x.CurrentDate <= endDate.Value.Date)
                    .ToListAsync();//
                if (counterSigaretsLst == null)
                    throw new Exception("Data not found.");
                return new RsSigaretsCount() { UserId = 1, CountSigarets = counterSigaretsLst.Sum(x => x.SigaretsCount) };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RsSigaretsCount() { UserId = 1, CountSigarets = 0, ErrorMessage = ex.Message };
            }
        }

        [HttpGet]
        public async Task<RsSigaretsCount> PutSigaretsCountAsync(int userid = 0, int count = 0, DateTime? date = null)
        {
            try
            {
                if (date == null)
                    date = DateTime.Now;
                if (userid == 0)
                    throw new Exception("User not found.");

                CounterSigaret counterSigarets = new CounterSigaret() { Userid = userid, CurrentDate = (DateTime)date, SigaretsCount = count };
                counterSigarets.SigaretsCount = count == 0 ? counterSigarets.SigaretsCount + 1 : counterSigarets.SigaretsCount;
                counterSigarets.CurrentDate = ((DateTime)(date != null ? date : DateTime.Now));
                _context.CounterSigarets.Add(counterSigarets);
                _context.SaveChanges();

                return new RsSigaretsCount() { UserId = userid, CountSigarets = counterSigarets.SigaretsCount };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RsSigaretsCount() { UserId = userid, CountSigarets = 0, ErrorMessage = ex.Message };
            }
        }
    }
}
