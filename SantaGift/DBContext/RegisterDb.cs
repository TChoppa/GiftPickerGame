using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SantaGift.Models;
//using System.Data.Entity;

namespace SantaGift.DBContext
{
    public class RegisterDb: DbContext
    {
        public RegisterDb(DbContextOptions<RegisterDb> options) : base(options)
        {

        }
        public DbSet<Register> registers { get; set; }

        public DbSet<Participants> Participants { get; set; }

        public DbSet<ParticipantsView>ParticipantsView { get; set; }
        
        public DbSet<GiftSponsers> GiftSponsers { get; set; }

    }
}
