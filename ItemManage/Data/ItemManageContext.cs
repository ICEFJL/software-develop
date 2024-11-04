using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ItemManage.Models;

namespace ItemManage.Data
{
    public class ItemManageContext : DbContext
    {
        public ItemManageContext()
        {

        }
        public ItemManageContext (DbContextOptions<ItemManageContext> options)
            : base(options)
        {
            
        }

        public DbSet<ItemManage.Models.User> User { get; set; } = default!;
        public DbSet<ItemManage.Models.Item> Item { get; set; } = default!;
        public DbSet<ItemManage.Models.Outbound> Outbound { get; set; } = default!;
        public DbSet<ItemManage.Models.Inbound> Inbound { get; set; } = default!;
    }
}
