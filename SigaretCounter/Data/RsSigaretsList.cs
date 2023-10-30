using SigaretCounter.Models;

namespace SigaretCounter.Data
{
    public class RsSigaretsList
    {
        public int UserId { get; set; }
        public List<CounterSigaret> CounterSigaretList { get; set; }

        public RsSigaretsList()
        {
            
        }
    }
}
