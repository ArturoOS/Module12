using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(x => x.Orders.Select(y => y.Total).Sum() > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null || suppliers == null)
                throw new   ArgumentNullException();
            List<(Customer customer, IEnumerable<Supplier> suppliers)> list = new List<(Customer customer, IEnumerable<Supplier> suppliers)>();
            foreach (var customer in customers)
            {
                var suppliersList = suppliers.Where(x => customer.City == x.City && customer.Country == x.Country);
                list.Add((customer, suppliersList));
            }

            return list;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null || suppliers == null)
                throw new ArgumentNullException();
            List<(Customer customer, IEnumerable<Supplier> suppliers)> list = new List<(Customer customer, IEnumerable<Supplier> suppliers)>();
            foreach (var customer in customers)
            {
                var suppliersList = suppliers.Where(x => customer.City == x.City && customer.Country == x.Country);
                list.Add((customer, suppliersList));
            }

            return list;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
                throw new ArgumentNullException();
            return customers.Where(x => x.Orders.Select(y => y.Total).Sum() > limit); ;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();
            List<(Customer, DateTime)> list = new List<(Customer, DateTime)>();
            foreach (var item in customers)
            {
                if (item.Orders.Length!=0)
                {
                    list.Add((item, item.Orders.Min(x => x.OrderDate)));
                }
            }
            return list;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();
            List<(Customer, DateTime)> list = new List<(Customer, DateTime)>();
            foreach (var item in customers)
            {
                if (item.Orders.Length != 0)
                {
                    list.Add((item, item.Orders.Min(x => x.OrderDate)));
                }
            }
            return list.OrderBy(x => x.Item2);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException();
            return customers.Where(x => int.TryParse(x.PostalCode, out _) == false || x.Region == null || !x.Phone.Contains('('));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException();

            List<Linq7CategoryGroup> list = new List<Linq7CategoryGroup>();
            
            var orderByCategory = products.OrderBy(x => x.Category).GroupBy(x => x.Category).Select(x=>x.First());
            foreach ( var category in orderByCategory) 
            {
                var orderByStock = products.Where(x => x.Category == category.Category).OrderByDescending(x => x.UnitsInStock).GroupBy(x => x.UnitsInStock).Select(x => x.First());
                var unitsInStockGroupList = new List<Linq7UnitsInStockGroup>();
                foreach (var stock in orderByStock)
                {
                    var unitsInStockGroup = new Linq7UnitsInStockGroup()
                    {
                        UnitsInStock = stock.UnitsInStock,
                        Prices = products.Where(x => x.Category == category.Category && x.UnitsInStock==stock.UnitsInStock).Select(x => x.UnitPrice).ToList()
                    };
                    unitsInStockGroupList.Add(unitsInStockGroup);
                }


                list.Add(new Linq7CategoryGroup() 
                { 
                    Category = category.Category, 
                    UnitsInStockGroup = unitsInStockGroupList
                });
            }
            return list ;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null)
                throw new ArgumentNullException();
            var list = new List<(decimal category, IEnumerable<Product> products)>();
            
            var cheapProducts = products
                .GroupBy(x => x.UnitPrice >= 0 && x.UnitPrice <= cheap)
                .Where(x => x.Key == true)
                .Select(x => x.ToList());
            var middleProducts = products
                .GroupBy(x => x.UnitPrice > cheap && x.UnitPrice <= middle)
                .Where(x => x.Key == true)
                .Select(x => x.ToList());
            var expensiveProducts = products
                .GroupBy(x => x.UnitPrice > middle && x.UnitPrice <= expensive)
                .Where(x => x.Key == true)
                .Select(x => x.ToList());

            list.Add((cheap,cheapProducts.FirstOrDefault()));
            list.Add((middle, middleProducts.FirstOrDefault()));
            list.Add((expensive, expensiveProducts.FirstOrDefault()));

            return list;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();
            var list = new List<(string,int,int)>();
            var cities = customers.Select(x => x.City).Distinct();
            foreach ( var city in cities) 
            {
                int averageIncome = 0;
                
                var income = customers.Where(x => x.City == city).Select(x => x.Orders);
                if (income.Count() > 0)
                {
                    decimal sum = 0;
                    foreach (var item in income)
                    {
                        sum += item.Sum(x=>x.Total);
                    }
                    averageIncome = (int)Math.Round(sum/ customers.Where(x => x.City == city).ToList().Count,0);
                }

                var averageIntensity = (int)decimal.Round(
                    customers
                    .Where(x => x.City == city)
                    .Select(x => x.Orders).Sum(x=>x.Length)/ 
                    customers.Where(x => x.City == city)
                    .ToList().Count,0);

                list.Add((city,averageIncome,averageIntensity));
            }

            return list;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                throw new ArgumentNullException();
            var a = suppliers.OrderBy(x => x.Country.Length).ThenBy(x => x.Country).Select(x=>x.Country).Distinct();
            string b="";
            foreach (var item in a)
            {
                b +=item;
            }
            return b;
        }
    }
}