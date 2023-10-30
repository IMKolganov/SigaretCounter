namespace SigaretCounter.Data
{
    public class RsSigaretsPutCount
    {
        public int UserId { get; set; }
        public int CountSigarets { get; set; }
        public DateTime? CurrentDate { get; set; } = DateTime.MinValue;

        public RsSigaretsPutCount()
        {
            
        }
    }
}
