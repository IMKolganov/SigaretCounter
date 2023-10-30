namespace SigaretCounter.Data
{
    public class RsSigaretsGetCount
    {
        public int UserId { get; set; }
        public int CountSigarets { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MinValue;

        public RsSigaretsGetCount()
        {
            
        }
    }
}
