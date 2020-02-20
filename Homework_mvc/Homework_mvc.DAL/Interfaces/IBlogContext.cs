using Homework_mvc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_mvc.DAL
{
    public interface IBlogContext : IDisposable
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Profile> Profiles { get; set; }
    }
}
