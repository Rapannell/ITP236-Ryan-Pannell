using System;
using System.Collections.Generic;
using System.Linq;

namespace Project2
{
    /* ---- ALL OF YOUR CODE FOR PROJECT 2 GOES IN THIS FILE ---- */
    public partial class Customer
    {
        public const string StudentName = "Ryan Pannell"; //--< START HERE WITH YOUR NAME <<<

        /// <summary>
        /// TotalSales is the sum of the SalesOrders' OrderTotal
        /// </summary>
        public decimal TotalSales => SalesOrders.Sum(so => so.OrderTotal);

        /// <summary>
        /// TotalCost is the sum of the SalesOrders' OrderCost
        /// </summary>
        public decimal TotalCost => SalesOrders.Sum(so => so.OrderCost);

        /// <summary>
        /// GrossProfit is the difference between TotalSales and TotalCost
        /// </summary>
        public decimal GrossProfit => TotalSales - TotalCost;

        /// <summary>
        /// ItemsSold is the sum of the SalesOrders' SalesOrderParts Quantities
        /// </summary>
        public int ItemsSold => SalesOrders.Sum(so => so.ItemsSold);

        /// <summary>
        /// LargestSale is the largest sale for the Customer based on OrderTotal
        /// </summary>
        public SalesOrder LargestSale => SalesOrders.OrderByDescending(so => so.OrderTotal).FirstOrDefault();

        /// <summary>
        /// Returns a collection (List) of the items that a Customer has purchased, with the total quantities
        /// Group the SalesOrderParts from the SalesOrders. Group by the Part's PartId and Name
        /// For each group, 
        ///     create a new CustomerItem object summing the 
        ///     Quantites, ExtendedPrices, UnitsShipped and 
        ///     the differences between Quantities and UnitsShipped for the Backorder
        /// </summary>
        public List<CustomerItem> CustomerItems => SalesOrders
            .SelectMany(so => so.SalesOrderParts)
            .GroupBy(sop => new { sop.Part.PartId, sop.Part.Name })
            .Select(g => new CustomerItem(
                CustomerId,
                g.Key.PartId,
                g.Key.Name,
                g.Sum(sop => sop.Quantity),
                g.Sum(sop => sop.ExtendedPrice),
                g.Sum(sop => sop.UnitsShipped),
                g.Sum(sop => sop.Quantity) - g.Sum(sop => sop.UnitsShipped)
            ))
            .ToList();
    }

    public partial class Part
    {
        #region Quantities
        /// <summary>
        /// QuantityOnHand = Units Received - Units Spoiled - Units Shipped
        /// </summary>
        public int QuantityOnHand => ReceivedUnits - SpoiledUnits - ShippedUnits;

        /// <summary>
        /// UnitsSold is the sum of the sales for the Part. Use SalesOrderParts.
        /// </summary>
        public int UnitsSold => SalesOrderParts.Sum(sop => sop.Quantity);

        #endregion
        #region Amounts
        /// <summary>
        /// CurrentValue = Received Value - Spoiled Value - Shipped Value
        /// </summary>
        public decimal CurrentValue => ReceivedValue - SpoiledValue - ShippedValue;

        /// <summary>
        /// Amount Sold is the sum of the extended prices for the SalesOrderParts.
        /// </summary>
        public decimal AmountSold => SalesOrderParts.Sum(sop => sop.ExtendedPrice);

        #endregion
        /// <summary>
        /// Customers is the list of Customers that have purchased this part from us.
        /// Start with Sales Order Parts, find the Sales Order for each one
        /// Then get the Customers for the Sales Orders.
        /// We only want one distinct object for each customer.
        /// Create a List of the Customers.
        /// </summary>
        public List<Customer> Customers => SalesOrderParts
            .Select(sop => sop.SalesOrder.Customer)
            .Distinct()
            .ToList();
    }

    public partial class SalesOrder
    {
        #region Quantities
        /// <summary>
        /// ItemsSold is the sum of the quantities for SalesOrderParts
        /// </summary>
        public int ItemsSold => SalesOrderParts.Sum(sop => sop.Quantity);

        /// <summary>
        /// ItemsShipped is the sum of the SalesOrderParts UnitsShipped Quantities
        /// </summary>
        public int UnitsShipped => SalesOrderParts.Sum(sop => sop.UnitsShipped);

        /// <summary>
        /// BackOrdered is the difference between the Items Sold and the Items Shipped
        /// </summary>
        public int BackOrdered => ItemsSold - UnitsShipped;
        #endregion

        #region Amounts
        /// <summary>
        /// OrderTotal is the sum of the SalesOrderParts' Extended Prices
        /// </summary>
        public decimal OrderTotal => SalesOrderParts.Sum(sop => sop.ExtendedPrice);

        /// <summary>
        /// OrderCost is the sum of the SalesOrderPart's Extended Costs
        /// </summary>
        public decimal OrderCost => SalesOrderParts.Sum(sop => sop.ExtendedCost);

        /// <summary>
        /// GrossProfit is the difference between the Order Total and the Order Cost
        /// </summary>
        public decimal GrossProfit => OrderTotal - OrderCost;

        #endregion
    }

    public partial class SalesOrderPart
    {
        public decimal ExtendedPrice => Quantity * UnitPrice;
        public decimal ExtendedCost => Quantity * UnitCost;
    }
}