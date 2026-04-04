using LINQTask.Data;
using LINQTask.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _context = new ApplicationDbcontext();
            //            LINQ Task Using Linq &C# on BikeStoreDB:
            //1 - List all customers' first and last names along with their email addresses.
            var coustomer = _context.Customers.
              Select(e =>
              new { FirstName = $"{e.FirstName}", LastName = e.LastName, EmailAdress = e.Email })
              .ToList();

            //2 - Retrieve all orders processed by a specific staff member(e.g., staff_id = 3).
            var orders = _context.Orders.Where(e => e.StaffId == 3).ToList();
            //orders.ForEach(e => Console.WriteLine($"{e.OrderId}"));
            //3 - Get all products that belong to a category named "Mountain Bikes".
            var allPuroduct = _context.Products.Join(
                _context.Categories,
                e => e.CategoryId,
                c => c.CategoryId,
                (e, c) => new
                {
                    e.ProductName,
                    c.CategoryName
                }).ToList();
            //4 - Count the total number of orders per store.
            var count_TotalNumberoFOrders = _context.Orders.Include(e => e.Store)
                .GroupBy(e => e.Store.StoreName)
                .Select(g => new { StoreName = g.Key, count = g.Count() }).ToList();
            //foreach (var item in count_TotalNumberoFOrders)
            //{
            //    Console.WriteLine($"StoreName:{item.StoreName},Count:{item.count}");
            //}
            //5 - List all orders that have not been shipped yet(shipped_date is null).
            var all_orders = _context.Orders.Where(e => e.ShippedDate == null).ToList();
            //foreach (var item in all_orders)
            //{
            //    Console.WriteLine($"{item.OrderId} , {item.ShippedDate}");
            //}
            //6 - Display each customer’s full name and the number of orders they have placed.
            var Diplay_Customer = _context.Customers.Join(
               _context.Orders,
               e => e.CustomerId,
               o => o.CustomerId,
               (e, o) => new
               {
                   FullName = $"{e.FirstName} {e.LastName}",
                   Numbre = $"{o.OrderId}"
               }).ToList();
            foreach (var item in Diplay_Customer)
                Console.WriteLine($"Fullname: {item.FullName} , Number Of Palced: {item.Numbre}");
            //7 - List all products that have never been ordered(not found in order_items).
            var all_products = _context.Products.Join(
                _context.OrderItems,
                e => e.ProductId,
                c => c.ProductId,
                (e, c) => new
                {
                    e.ProductName,
                    itemProductID = c.ProductId,
                    productID = e.ProductId
                }
                ).Where(e => e.productID != 0).ToList();
            //8 - Display products that have a quantity of less than 5 in any store stock.
            var dispayProduct = _context.Products.Join(
                _context.Stocks,
                e => e.ProductId,
                s => s.ProductId,
                (e, s) => new
                {
                    e.ProductName,
                    s.Quantity
                }
               ).Where(Q => Q.Quantity < 5)
               .ToList();
            foreach (var item in dispayProduct)
            {
                Console.WriteLine($"{item.ProductName}  :  {item.Quantity}");
            }
            //9 - Retrieve the first product from the products table.
            var first_product = _context.Products.First();
            Console.WriteLine(first_product.ProductId);
            //10 - Retrieve all products from the products table with a certain model year.
            var All_product = _context.Products.Where(e => e.ModelYear == 2016);
            //11 - Display each product with the number of times it was ordere
            var _Display_each_product = _context.Products.Join(
                _context.OrderItems,
                e => e.ProductId,
                o => o.ProductId,
                (e, o) => new
                {
                    ProductName = e.ProductName,
                    OrderId = o.OrderId
                }
                ).GroupBy(e => e.ProductName)
                .Select(e =>
                new
                {
                    ProductName = e.Key,
                    count = e.Count()
                })
                .ToList();
            foreach (var item in _Display_each_product)
            {
                Console.WriteLine($"ProductName = {item.ProductName}  : Count: {item.count} ");
            }

            //12 - Count the number of products in a specific category.
            var count = _context.Products
                .Include(e => e.Category
           ).GroupBy(e => e.Category.CategoryName)
           .Select(o => new { CategoryName = o.Key, countPoduct = o.Count() }).ToList();
            foreach (var item in count)
            {
                Console.WriteLine($"CategoryName: {item.CategoryName} : countPoduct {item.countPoduct}");
            }
            //13 - Calculate the average list price of products.
            var Calculate_the_averag = _context.Products.Average(e => e.ListPrice);

            Console.WriteLine("======================================================");

            //14 - Retrieve a specific product from the products table by ID.
            int ID = 3;
            var specificProduct = _context.Products.Find(ID);
            //15 - List all products that were ordered with a quantity greater than 3 in any order.
            var products_ = _context.Products.Join(_context.OrderItems,
                p => p.ProductId,
                e => e.ProductId,
                (p, e) => new
                {
                    ProductName = p.ProductName,
                    e.Quantity
                }).Where(e => e.Quantity > 3).ToList();
            foreach (var item in products_)
            {
                Console.WriteLine($"ProductName:{item.ProductName} => Quantity : {item.Quantity}");
            }
            //16 - Display each staff member’s name and how many orders they processed.
            var Display = _context.Orders.Include(e => e.Staff)
                .GroupBy(e => new
                {
                    e.Staff.FirstName,
                    e.Staff.LastName
                }).Select(p =>
                new { fullname = $"{p.Key.FirstName} {p.Key.LastName}", count = p.Count() }).ToList();
            for (int i = 0; i < Display.Count; i++)
            {
                Console.WriteLine(Display[i].fullname + "==>" + Display[i].count);
            }
            //17 - List active staff members only(active = true) along with their phone numbers.
            var active_staff = _context.Staffs.Select(e => new
            {
                FullName = $"{e.FirstName} {e.LastName}",
                PhoneNumber = $"{e.Phone}",
                Active = e.Active
            }).Where(e => e.Active == 1).ToList();
            foreach (var item in active_staff)
            {
                Console.WriteLine($"FullName: {item.FullName} | Active: {item.Active} | PhoneNumber: {item.PhoneNumber}");
            }
            //18 - List all products with their brand name and category name.
            var all_products_ = _context.Products.Include(e => e.Brand)
                .Include(e => e.Category)
                .Select(
                e => new
                {
                    ProductName = e.ProductName,
                    BrandName = e.Brand.BrandName,
                    categoryName = e.Category.CategoryName
                }
                ).ToList();
            foreach (var item in all_products_)
            {
                Console.WriteLine($"ProductName: {item.ProductName} |BrandName: {item.BrandName} | categoryName: {item.categoryName} ");
            }
            //19 - Retrieve orders that are completed.
            var completOrder = _context.Orders.Where(e => e.ShippedDate == null);
            var _completOrder = _context.Orders.Where(e => e.OrderStatus == 0);
            //20 - List each product with the total quantity sold(sum of quantity from order_items).
            var totalQuantitySold = _context.Products.Include(e => e.OrderItems)
                .Select(
                    _out => new
                    {
                        ProductName = _out.ProductName,
                        quantitySold = _out.OrderItems.Sum(oi => oi.Quantity)
                    }).ToList();
          
            //var listProduct = _context.Products.Include(
            //    e => e.OrderItems)
            //    .GroupBy(e => e.ProductName)
            //    .Select(_out => new { _out.Key, _out. });

        }
    }
}
