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
            return customers.Where(x => x.PostalCode != null || x.Region == null || !x.Phone.Contains('('));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException();
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();
            var a = customers.Select(x => x.Country).Distinct();

            return null;
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