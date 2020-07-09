using TimePlanner.Interfaces;
using TimePlanner.Models;

namespace TimePlanner.Repositories
{
    public class EntryRepository : BaseRepository<Entry>, IEntryRepository
    {
        public EntryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
