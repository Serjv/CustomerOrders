using CustomerOrders.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CustomerOrders.DAL
{
    public class SampleContext : DbContext
    {

        public SampleContext()
        : base("DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
        public DbSet<OrderInfo> OrderInfos { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
    class MyContextInitializer : System.Data.Entity.CreateDatabaseIfNotExists<SampleContext>
    {
        protected override void Seed(SampleContext db)
        {

            var sqlcom = db.Database.ExecuteSqlCommand(@"CREATE PROCEDURE ProductSummary AS
BEGIN

DECLARE @number INT;
            SET @number = 1;
            WHILE(@number < 101)
 BEGIN
    INSERT INTO Customer
VALUES('Petya' + CAST(@number as nvarchar), 'Main str.', 'VIP')
SET @number = @number + 1

    INSERT INTO Customer
VALUES('Petya' + CAST(@number as nvarchar), 'Main str.', 'Top')
SET @number = @number + 1

    INSERT INTO Customer
VALUES('Petya' + CAST(@number as nvarchar), 'Main str.', 'Middle')
SET @number = @number + 1

    INSERT INTO Customer
VALUES('Petya' + CAST(@number as nvarchar), 'Main str.', 'Std')
SET @number = @number + 1
IF @number > 101
      BREAK
   ELSE
      CONTINUE
    END


    DECLARE @numberproduct INT;
            SET @numberproduct = 1;
            WHILE(@numberproduct < 5001)
 BEGIN
    INSERT INTO Product
VALUES('Product' + CAST(@numberproduct as nvarchar))
SET @numberproduct = @numberproduct + 1
IF @numberproduct > 5001
      BREAK
   ELSE
      CONTINUE
    END



    DECLARE @random INT;
            DECLARE @numbercustomer INT;
            DECLARE @startvalue INT;
            SET @numbercustomer = 1;

            SET @startvalue = 0;
            WHILE(@numbercustomer < 101)

    BEGIN
    SET @random = FLOOR(RAND() * (50 - 5) + 5);
            WHILE(@startvalue < @random)

    BEGIN
    INSERT INTO OrderList
VALUES(@numbercustomer, '03.04.2019')
SET @startvalue = @startvalue + 1
IF @startvalue > @random
      BREAK
   ELSE
      CONTINUE
    END


    SET @startvalue = 0;
            SET @numbercustomer = @numbercustomer + 1
IF @numbercustomer > 101
      BREAK
   ELSE
      CONTINUE
    END



    DECLARE @orderlistid INT;
            DECLARE @random2 INT;
            DECLARE @startvalue2 INT;
            DECLARE @orderlistcount INT;
            SET @orderlistid = 1;
            SET @startvalue2 = 0;
            SET @orderlistcount = (select count(*) from OrderList) +@orderlistid;
            WHILE(@orderlistid < @orderlistcount)

    BEGIN
    SET @random2 = FLOOR(RAND() * (100 - 1) + 1);
            WHILE(@startvalue2 < @random2)

    BEGIN
    INSERT INTO OrderInfo
VALUES(@orderlistid, FLOOR(RAND() * (5000 - 1) + 1), 300, 2, 600)
SET @startvalue2 = @startvalue2 + 1
IF @startvalue2 > @random2
      BREAK
   ELSE
      CONTINUE
    END


    SET @startvalue2 = 0;
            SET @orderlistid = @orderlistid + 1
IF @orderlistid > @orderlistcount
      BREAK
   ELSE
      CONTINUE
    END

END; ");
            var sqlcom2 = db.Database.ExecuteSqlCommand("EXEC ProductSummary");
            var sqlcom3 = db.Database.ExecuteSqlCommand(@"CREATE TRIGGER MyTrig
ON OrderList
AFTER  UPDATE, DELETE
AS

BEGIN
DECLARE @CustID int

SELECT @CustID = (SELECT CustomerID FROM deleted)
 
   update customer
 set category='STD'
 where Id= @CustID
 and (select
count(a.customerID) 
	from orderList a
	where customerId= @CustID
group by a.customerId
 )<5
 
 update customer
 set category='MIDDLE'
 where Id= @CustID
 and (select
count(a.customerID) 
	from orderList a
	where customerId= @CustID
group by a.customerId
 ) <30

 update customer
 set category='TOP'
 where Id= @CustID
 and (select
count(a.customerID) 
	from orderList a
	where customerId= @CustID
group by a.customerId
 ) between 30 and 39



  update customer
 set category='VIP'
 where Id= @CustID
 and (select
count(a.customerID) 
	from orderList a
	where customerId= @CustID
group by a.customerId
 )>=40

 END");
        }
    }



}