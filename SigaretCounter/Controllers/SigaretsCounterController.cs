using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SigaretCounter.Data;
using SigaretCounter.Models;
using System.Diagnostics.Metrics;

namespace SigaretCounter.Controllers;

public class SigaretsCounterController : Controller
{
    private readonly XgbRackotpgContext _context;
    private readonly ILogger<SigaretsCounterController> _logger;
    public SigaretsCounterController(ILogger<SigaretsCounterController> logger, XgbRackotpgContext context)
    {
        _logger = logger;
        _context = context;
    }
    //Command for create model
    //-f Overwrite existing files
    ////Scaffold-DbContext “Host=postgres81.1gb.ru;Database=xgb_rackotpg;Username=xgb_rackotpg;Password=X-NVF-vbP5mG” Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -f
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<RsSigaretsGetCount> GetCount(int userid = 0, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (startDate == null)
            startDate = DateTime.MinValue;
        if (endDate == null)
            endDate = DateTime.MaxValue;
        if (userid == 0)
            throw new Exception("User not found.");
        List<CounterSigaret> counterSigaretsLst = await _context.CounterSigarets
            .Where(x => x.Userid == userid && x.CurrentDate >= startDate.Value.Date && x.CurrentDate <= endDate.Value.Date)
            .ToListAsync();//
        if (counterSigaretsLst == null)
            throw new Exception("Data not found.");

        return new RsSigaretsGetCount()
        {
            UserId = userid,
            CountSigarets = counterSigaretsLst.Sum(x => x.SigaretsCount),
            StartDate = startDate,
            EndDate = endDate
        };
    }

    [HttpGet]
    public async Task<RsSigaretsList> GetAll(int userid = 0, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (startDate == null)
            startDate = DateTime.MinValue;
        if (endDate == null)
            endDate = DateTime.MaxValue;
        if (userid == 0)
            throw new Exception("User not found.");
        List<CounterSigaret> counterSigaretsLst = await _context.CounterSigarets
            .Where(x => x.Userid == userid && x.CurrentDate >= startDate.Value.Date && x.CurrentDate <= endDate.Value.Date)
            .ToListAsync();//
        if (counterSigaretsLst == null)
            throw new Exception("Data not found.");
        return new RsSigaretsList()
        {
            UserId = 1,
            //CounterSigaretList = counterSigaretsLst
        };
    }

    [HttpGet]
    public async Task<RsSigaretsPutCount> PutCount(int userid = 0, int count = 0, DateTime? date = null)
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

        return new RsSigaretsPutCount()
        {
            UserId = userid,
            CountSigarets = counterSigarets.SigaretsCount,
            CurrentDate = date
        };
    }
}
