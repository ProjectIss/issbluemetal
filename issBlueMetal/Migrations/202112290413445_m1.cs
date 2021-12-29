namespace issBlueMetal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountGroups",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        accountGrouop = c.String(),
                        parentGroup = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AcLedgers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        accountledger = c.String(),
                        accountGroupID = c.Int(nullable: false),
                        openingBal = c.String(),
                        type = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AccountGroups", t => t.accountGroupID, cascadeDelete: true)
                .Index(t => t.accountGroupID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        address = c.String(),
                        cellNo = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.customerLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dateOfPurchages = c.DateTime(nullable: false),
                        customerId = c.Int(nullable: false),
                        debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        type = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.customerId)
                .Index(t => t.companyId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        address = c.String(),
                        cellNo = c.String(),
                        openingBalance = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.errorLogs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ErrorDate = c.DateTime(nullable: false),
                        controllerName = c.String(),
                        MethodName = c.String(),
                        ErrorMessage = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        uom = c.String(),
                        purchasePrize = c.String(),
                        salcePrize = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tranNo = c.Int(nullable: false),
                        income = c.Int(nullable: false),
                        expence = c.Int(nullable: false),
                        flag = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        roomNo = c.String(),
                        particular = c.String(),
                        status = c.String(),
                        user = c.String(),
                        mode = c.String(),
                        invNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PaymentEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        PaymentType = c.String(),
                        acLedgerId = c.Int(nullable: false),
                        description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        supplierId = c.Int(nullable: false),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcLedgers", t => t.acLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.supplierId, cascadeDelete: true)
                .Index(t => t.acLedgerId)
                .Index(t => t.supplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        name = c.String(),
                        address = c.String(),
                        cellNo = c.String(),
                        openingBalance = c.String(),
                        cstNo = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RawMateriyalPurchases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        dateTime = c.DateTime(),
                        vehicleId = c.Int(nullable: false),
                        staffId = c.Int(nullable: false),
                        type = c.String(),
                        departurePlaceId = c.Int(nullable: false),
                        supplierId = c.Int(nullable: false),
                        arrivalPlaceId = c.Int(nullable: false),
                        materialNameId = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rawMaterialDept = c.Decimal(nullable: false, precision: 18, scale: 2),
                        salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JcbSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        noofLoad = c.Int(nullable: false),
                        productionDept = c.Decimal(nullable: false, precision: 18, scale: 2),
                        salaryPerLoad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ratePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        driverSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vehicleRent = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Locations", t => t.arrivalPlaceId, cascadeDelete: true)
             //   .ForeignKey("dbo.Locations", t => t.departurePlaceId, cascadeDelete: true)
                .ForeignKey("dbo.RawMeteriyals", t => t.materialNameId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.staffId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.supplierId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.vehicleId, cascadeDelete: true)
                .Index(t => t.vehicleId)
                .Index(t => t.staffId)
                .Index(t => t.departurePlaceId)
                .Index(t => t.supplierId)
                .Index(t => t.arrivalPlaceId)
                .Index(t => t.materialNameId);
            
            CreateTable(
                "dbo.RawMeteriyals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        uom = c.String(),
                        discription = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        name = c.String(),
                        address = c.String(),
                        cellNo = c.String(),
                        licenceNo = c.String(),
                        badgeno = c.String(),
                        licenceExDate = c.DateTime(),
                        salary = c.String(),
                        salaryType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        vehicleNo = c.String(),
                        shortNo = c.String(),
                        vehicleName = c.String(),
                        registrationAddress = c.String(),
                        mfdYear = c.DateTime(),
                        insuranceExDate = c.DateTime(),
                        taxExDate = c.DateTime(),
                        permitExDate = c.DateTime(),
                        fcExDate = c.DateTime(),
                        oillService = c.DateTime(),
                        greecePacking = c.DateTime(),
                        coolentOill = c.DateTime(),
                        gearBoxOill = c.DateTime(),
                        grownOill = c.DateTime(),
                        salary = c.String(),
                        vehicleDetail = c.String(),
                        noofUnit = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ReciptEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        PaymentType = c.String(),
                        acLedgerId = c.Int(nullable: false),
                        description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        companyId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcLedgers", t => t.acLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.acLedgerId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.RoomStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        booking = c.Int(nullable: false),
                        vacancy = c.Int(nullable: false),
                        housKeeping = c.Int(nullable: false),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        billNo = c.Int(nullable: false),
                        Date = c.DateTime(),
                        vehicle = c.String(),
                        driver = c.String(),
                        type = c.String(),
                        itemId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                        address = c.String(),
                        phoneNo = c.String(),
                        deliveryPlace = c.String(),
                        noOfUnit = c.Decimal(precision: 18, scale: 2),
                        ratePerUnit = c.Decimal(precision: 18, scale: 2),
                        driverSalary = c.Decimal(precision: 18, scale: 2),
                        rentAmount = c.Decimal(precision: 18, scale: 2),
                        netAmount = c.Decimal(precision: 18, scale: 2),
                        advanceAmount = c.Decimal(precision: 18, scale: 2),
                        paidAmount = c.Decimal(precision: 18, scale: 2),
                        balanceAmount = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.itemId, cascadeDelete: true)
                .Index(t => t.itemId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.supplierLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dateOfPurchages = c.DateTime(nullable: false),
                        supplierId = c.Int(nullable: false),
                        debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        type = c.String(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.supplierId, cascadeDelete: true)
                .Index(t => t.supplierId)
                .Index(t => t.companyId);
            
            CreateTable(
                "dbo.TblGRCs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        grcNo = c.Int(nullable: false),
                        grcDate = c.DateTime(nullable: false),
                        name = c.String(),
                        phone = c.String(),
                        age = c.String(),
                        adult = c.String(),
                        child = c.String(),
                        arrtime = c.DateTime(nullable: false),
                        roomType = c.String(),
                        roomOption = c.String(),
                        roomNo = c.String(),
                        tariff = c.String(),
                        billNo = c.Int(nullable: false),
                        noofDay = c.Int(nullable: false),
                        total = c.Int(nullable: false),
                        chkdate = c.DateTime(nullable: false),
                        chkTime = c.DateTime(nullable: false),
                        taxAmt = c.String(),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TripSalesEntries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        date = c.DateTime(),
                        vehicleId = c.Int(nullable: false),
                        staffId = c.Int(nullable: false),
                        type = c.String(),
                        materialNameId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        address = c.String(),
                        phoneNo = c.String(),
                        deliveryPlaceId = c.Int(nullable: false),
                        noofUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        driverSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        paidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rateperUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        advanceAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        balanceAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.deliveryPlaceId, cascadeDelete: true)
                .ForeignKey("dbo.RawMeteriyals", t => t.materialNameId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.staffId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.vehicleId, cascadeDelete: true)
                .Index(t => t.vehicleId)
                .Index(t => t.staffId)
                .Index(t => t.materialNameId)
                .Index(t => t.CustomerId)
                .Index(t => t.deliveryPlaceId);
            
            CreateTable(
                "dbo.UnitConvertions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(nullable: false),
                        mainUnitName = c.String(),
                        subUnitName = c.String(),
                        factor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        username = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        role = c.String(nullable: false, maxLength: 50),
                        status = c.String(maxLength: 50),
                        lastLogin = c.DateTime(),
                        companyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .Index(t => t.companyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "companyId", "dbo.Companies");
            DropForeignKey("dbo.TripSalesEntries", "vehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.TripSalesEntries", "staffId", "dbo.Staffs");
            DropForeignKey("dbo.TripSalesEntries", "materialNameId", "dbo.RawMeteriyals");
            DropForeignKey("dbo.TripSalesEntries", "deliveryPlaceId", "dbo.Locations");
            DropForeignKey("dbo.TripSalesEntries", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.supplierLedgers", "supplierId", "dbo.Suppliers");
            DropForeignKey("dbo.supplierLedgers", "companyId", "dbo.Companies");
            DropForeignKey("dbo.Sales", "itemId", "dbo.Items");
            DropForeignKey("dbo.Sales", "customerId", "dbo.Customers");
            DropForeignKey("dbo.ReciptEntries", "customerId", "dbo.Customers");
            DropForeignKey("dbo.ReciptEntries", "acLedgerId", "dbo.AcLedgers");
            DropForeignKey("dbo.RawMateriyalPurchases", "vehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RawMateriyalPurchases", "supplierId", "dbo.Suppliers");
            DropForeignKey("dbo.RawMateriyalPurchases", "staffId", "dbo.Staffs");
            DropForeignKey("dbo.RawMateriyalPurchases", "materialNameId", "dbo.RawMeteriyals");
            DropForeignKey("dbo.RawMateriyalPurchases", "departurePlaceId", "dbo.Locations");
            DropForeignKey("dbo.RawMateriyalPurchases", "arrivalPlaceId", "dbo.Locations");
            DropForeignKey("dbo.PaymentEntries", "supplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PaymentEntries", "acLedgerId", "dbo.AcLedgers");
            DropForeignKey("dbo.customerLedgers", "customerId", "dbo.Customers");
            DropForeignKey("dbo.customerLedgers", "companyId", "dbo.Companies");
            DropForeignKey("dbo.AcLedgers", "accountGroupID", "dbo.AccountGroups");
            DropIndex("dbo.User", new[] { "companyId" });
            DropIndex("dbo.TripSalesEntries", new[] { "deliveryPlaceId" });
            DropIndex("dbo.TripSalesEntries", new[] { "CustomerId" });
            DropIndex("dbo.TripSalesEntries", new[] { "materialNameId" });
            DropIndex("dbo.TripSalesEntries", new[] { "staffId" });
            DropIndex("dbo.TripSalesEntries", new[] { "vehicleId" });
            DropIndex("dbo.supplierLedgers", new[] { "companyId" });
            DropIndex("dbo.supplierLedgers", new[] { "supplierId" });
            DropIndex("dbo.Sales", new[] { "customerId" });
            DropIndex("dbo.Sales", new[] { "itemId" });
            DropIndex("dbo.ReciptEntries", new[] { "customerId" });
            DropIndex("dbo.ReciptEntries", new[] { "acLedgerId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "materialNameId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "arrivalPlaceId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "supplierId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "departurePlaceId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "staffId" });
            DropIndex("dbo.RawMateriyalPurchases", new[] { "vehicleId" });
            DropIndex("dbo.PaymentEntries", new[] { "supplierId" });
            DropIndex("dbo.PaymentEntries", new[] { "acLedgerId" });
            DropIndex("dbo.customerLedgers", new[] { "companyId" });
            DropIndex("dbo.customerLedgers", new[] { "customerId" });
            DropIndex("dbo.AcLedgers", new[] { "accountGroupID" });
            DropTable("dbo.User");
            DropTable("dbo.UnitConvertions");
            DropTable("dbo.TripSalesEntries");
            DropTable("dbo.TblGRCs");
            DropTable("dbo.supplierLedgers");
            DropTable("dbo.Sales");
            DropTable("dbo.RoomStatus");
            DropTable("dbo.ReciptEntries");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Staffs");
            DropTable("dbo.RawMeteriyals");
            DropTable("dbo.RawMateriyalPurchases");
            DropTable("dbo.Suppliers");
            DropTable("dbo.PaymentEntries");
            DropTable("dbo.Locations");
            DropTable("dbo.Hotels");
            DropTable("dbo.Items");
            DropTable("dbo.errorLogs");
            DropTable("dbo.Customers");
            DropTable("dbo.customerLedgers");
            DropTable("dbo.Companies");
            DropTable("dbo.AcLedgers");
            DropTable("dbo.AccountGroups");
        }
    }
}
