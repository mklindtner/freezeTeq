using Microsoft.EntityFrameworkCore;

namespace freezeapi.Models
{
    public class FreezeItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}