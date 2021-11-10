using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class issModel : DbContext
    {
        public issModel()
            : base("name=ISSModel")
        {
        }
        public virtual DbSet<Item> GetItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Customer> Customers { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.RoomStatus> RoomStatus { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.RawMeteriyal> RawMeteriyals { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Staff> Staffs { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Vehicle> Vehicles { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.TripSalesEntry> TripSalesEntries { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.RawMateriyalPurchase> RawMateriyalPurchases { get; set; }
        public DbSet<Sales> Sales { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.Company> Companies { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.ReciptEntry> ReciptEntries { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.PaymentEntry> PaymentEntries { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.UnitConvertion> UnitConvertions { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.AccountGroup> AccountGroups { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.AcLedger> AcLedger { get; set; }
        //public System.Data.Entity.DbSet<issBlueMetal.Models.Item> Items { get;  set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.customerLedger> customerLedgers { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.supplierLedger> supplierLedgers { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.errorLog> errorLogs { get; set; }
        public System.Data.Entity.DbSet<issBlueMetal.Models.Hotel> Hotels { get; set; }

        public System.Data.Entity.DbSet<issBlueMetal.Models.TblGRC> TblGRCs { get; set; }
    }
}