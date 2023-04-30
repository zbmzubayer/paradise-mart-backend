using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Admin, string, bool> AdminData()
        {
            return new AdminRepo();
        }
        public static IUserRepo<Admin> AdminOthersData()
        {
            return new AdminRepo();
        }
        public static IRepo<Customer, string, bool> CustomerData()
        {
            return new CustomerRepo();
        }
        public static IUserRepo<Customer> CustomerOthersData()
        {
            return new CustomerRepo();
        }
        public static IRepo<Seller, string, bool> SellerData()
        {
            return new SellerRepo();
        }
        public static IUserRepo<Seller> SellerOthersData()
        {
            return new SellerRepo();
        }
        public static IRepo<Product, string, bool> ProductData()
        {
            return new ProductRepo();
        }
        public static IRepo<Category, int, bool> CategoryData()
        {
            return new CategoryRepo();
        }
        public static IRepo<Payment, int, bool> PaymentData()
        {
            return new PaymentRepo();
        }
        public static IRepo<Order, string, Order> OrderData()
        {
            return new OrderRepo();
        }
    }
}
