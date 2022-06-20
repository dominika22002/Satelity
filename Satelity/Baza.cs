using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Satelity
{
    public class Baza : DbContext
    {
        public virtual DbSet<Satelita> satelity { get; set; }
    }
}
