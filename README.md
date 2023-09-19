# Products
ASP.NET MVC project to fetch and display data from a MySQL database.

## Before running the application
1. Set the appropriate connection string to connect with **YOUR** database in appsettings.json:
   ![image](https://github.com/akhilguruprasad22/Products/assets/46602926/5d855904-612a-44e3-a2a0-b9ba50cab383)

2. Create or apply database migrations to enforce a Data Model-first approach through EF Core, which will create the respective database in accordance with the chosen model(s). 
   Feel free to make valid changes in the migration file before applying the migration. Look into the **Migrations** sub-folder for the generated migration files for this project.
   The database schema enforced in this project is of the form:
   ![image](https://github.com/akhilguruprasad22/Products/assets/46602926/6477bd0c-dd2e-4cff-ba16-33f2bce68d4b)


3. Post creation of the table(s), create or add a stored procedure to be called which would enable querying the database to filter entries dynamically on the following fields (criteria):
   a. Product Name
   b. Size
   c. Price
   d. Mfg Date
   e. Category

   The project also supports specifying relationships in the form of AND or OR between these fields.
   The SearchStoredProcedure.sql file under **StoredProcedures** sub-folder creates one such stored procedure (named sp_SearchProducts).

   **NOTE:** If creating your own stored procedure, make sure to change the current name to the name of your stored procedure at ProductsContext.cs line:41 (Call to the stored procedure in FromSqlRaw(...))

4. Seed your database in order to start querying the database.

AND that's it. Now you can run the application and start searching through products.

## Sample runs
- Landing Page:
  ![image](https://github.com/akhilguruprasad22/Products/assets/46602926/728602fc-2f29-4e96-a893-b864f976edd3)

- Querying for product name = tShirt (case-insensitive):
  ![image](https://github.com/akhilguruprasad22/Products/assets/46602926/12af3764-4031-4a24-987e-edc7058de97c)

- Querying for product name = shorts OR size = Small AND price = 275:
  ![image](https://github.com/akhilguruprasad22/Products/assets/46602926/dc8060f3-286c-4558-8554-5ecb702b246c)

