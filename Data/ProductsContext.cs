using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Products.Models;

namespace Products.Data
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public ProductsContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<List<Product>> ExecuteSearchSP(ProductQuery requestParams)
        {
            try
            {
                requestParams = CleanValidate(requestParams);
            }
            catch (InvalidOperationException)
            {
                return new List<Product>();
            }

            var Params = new List<MySqlParameter>()
            {
                new MySqlParameter("@field1", requestParams.ProductName),
                new MySqlParameter("@field2", requestParams.Size),
                new MySqlParameter("@field3", requestParams.Price),
                new MySqlParameter("@field4", requestParams.MfgDate==DateTime.MinValue?"": requestParams.MfgDate),
                new MySqlParameter("@field5", requestParams.Category),

                new MySqlParameter("@cond1", requestParams.Condition1),
                new MySqlParameter("@cond2", requestParams.Condition2),
                new MySqlParameter("@cond3", requestParams.Condition3),
                new MySqlParameter("@cond4", requestParams.Condition4)
            };

            var SearchResult = await this.Products.FromSqlRaw("CALL sp_SearchProducts(@field1, @cond1, @field2, @cond2, @field3, @cond3, @field4, @cond4, @field5)", Params.ToArray()).AsNoTracking().ToListAsync();

            return SearchResult;
        }

        private static ProductQuery CleanValidate(ProductQuery queryParams)
        {
            bool fieldSet = false;
            HashSet<string> validConditions = new() { "AND", "OR" };
            HashSet<string> validSizes = new() { "s", "m", "l" };
            HashSet<int> validPrices = new() { 100, 275, 500 };
            HashSet<string> validCategories = new() { "standard", "premium", "economy" };

            if (queryParams.ProductName!=null)
            {
                if(queryParams.ProductName.Trim() != "")
                    fieldSet = true;
                queryParams.ProductName = queryParams.ProductName.Trim();
            }

            //-----------Validate Size-----------------
            if (queryParams.Size != null && queryParams.Size.Trim() != "" && validSizes.Contains(queryParams.Size.ToLower()))
            {
                if(!fieldSet)
                {
                    queryParams.Condition1 = "AND";
                    fieldSet = true;
                }
                queryParams.Size = queryParams.Size.Trim().ToLower();
            }
            else
            {
                queryParams.Size = "";
            }

            //-----------Validate Price-----------------
            if (queryParams.Price > 0 && validPrices.Contains(queryParams.Price))
            {
                if (!fieldSet)
                {
                    queryParams.Condition2 = "AND";
                    fieldSet = true;
                }
            }
            else
            {
                queryParams.Price = -1;
            }

            //-----------Validate MfgDate----------------
            if (queryParams.MfgDate!=DateTime.MinValue)
            {
                if (!fieldSet)
                {
                    queryParams.Condition3 = "AND";
                    fieldSet = true;
                }
            }

            //-----------Validate Category---------------
            if (queryParams.Category != null && queryParams.Category.Trim() != "" && validCategories.Contains(queryParams.Category.ToLower()))
            {
                if (!fieldSet)
                {
                    queryParams.Condition4 = "AND";
                    fieldSet = true;
                }
                queryParams.Category = queryParams.Category.Trim().ToLower();
            }
            else
            {
                queryParams.Category = "";
            }

            if (!fieldSet) {
                throw new InvalidOperationException();
            }
            //------------Validate and Set Conditions-----
            if(queryParams.Condition1==null || !validConditions.Contains(queryParams.Condition1))
            {
                queryParams.Condition1 = "AND";
            }
            if (queryParams.Condition2 == null || !validConditions.Contains(queryParams.Condition2))
            {
                queryParams.Condition2 = "AND";
            }
            if (queryParams.Condition3 == null || !validConditions.Contains(queryParams.Condition3))
            {
                queryParams.Condition3 = "AND";
            }
            if (queryParams.Condition4 == null || !validConditions.Contains(queryParams.Condition4))
            {
                queryParams.Condition4 = "AND";
            }

            return queryParams;
        }
    }
}
