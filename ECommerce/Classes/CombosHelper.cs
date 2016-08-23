using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ECommerce.Classes
{
    public class CombosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();

        public static List<Departament> GetDepartments()
        {
            var departments = db.Departaments.ToList();
            departments.Add(new Departament
            {
                DepartamentId = 0,
                Name = "[Seleccione un estado...]",
            });
            return departments.OrderBy(d => d.Name).ToList();
        }

        public static List<Product> GetProducts(int companyId)
        {
            var products = db.Products.Where(p => p.CompanyId == companyId).ToList();
            products.Add(new Product
            {
                ProductId = 0,
                Description = "[Seleccione un producto...]",
            });
            return products.OrderBy(p => p.Description).ToList();
        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                DepartamentId = 0,
                Name = "[Seleccione una ciudad...]",
            });
            return cities.OrderBy(d => d.Name).ToList();
        }
        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0,
                Name = "[Seleccione una empresa...]",
            });
            return companies.OrderBy(d => d.Name).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories.Where(c => c.CompanyId == companyId).ToList();
            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Seleccione una categoria...]",
            });
            return categories.OrderBy(d => d.Description).ToList();
        }

        public static List<Customer> GetCustomers(int companyId)
        {
            var customers = db.Customers.Where(c => c.CompanyId == companyId).ToList();
            customers.Add(new Customer
            {
                CustomerId = 0,
                FirstName = "[Seleccione una cliente...]",
            });

            return customers.OrderBy(d => d.FirstName). ThenBy(c => c.LastName).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes.Where(c => c.CompanyId == companyId).ToList();
            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Seleccione una tasa...]",
            });
            return taxes.OrderBy(d => d.Description).ToList();
        }
    }
}