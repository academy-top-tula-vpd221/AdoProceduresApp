using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");
    
    SqlCommand command = connection.CreateCommand();

    /*
    string procedureInsert = @"CREATE PROCEDURE InsertAuthor
                            @last_name NVARCHAR(50),
                            @first_name NVARCHAR(50),
                            @birth_date DATE
                        AS
                            INSERT INTO authors
                            (last_name, first_name, birth_date)
                            VALUES(@last_name, @first_name, @birth_date)

                            SELECT SCOPE_IDENTITY()
                         GO";
    string procedureSelectAll = @"CREATE PROCEDURE SelectAll
                                  AS
                                    SELECT * FROM authors
                                  GO";

    
    command.CommandText = procedureInsert;
    await command.ExecuteNonQueryAsync();

    command.CommandText = procedureSelectAll;
    await command.ExecuteNonQueryAsync();
    */

    /*
    string last_name, first_name, birth_date;

    Console.Write("Input last name: ");
    last_name = Console.ReadLine();

    Console.Write("Input first name: ");
    first_name = Console.ReadLine();

    Console.Write("Input birth date: ");
    birth_date = Console.ReadLine();

    SqlParameter firstNameParameter
        = new SqlParameter("@first_name", first_name);
    SqlParameter lastNameParameter
        = new SqlParameter("@last_name", last_name);
    SqlParameter birthDateParameter
        = new SqlParameter("@birth_date", DateTime.Parse(birth_date!));

    command.Parameters.AddRange(new[] { firstNameParameter,
                                        lastNameParameter,
                                        birthDateParameter});

    command.CommandText = "InsertAuthor";
    command.CommandType = System.Data.CommandType.StoredProcedure;

    var id = await command.ExecuteScalarAsync();

    Console.WriteLine($"Author add to table with id: {id}");
    */

    /*
    command.CommandText = "SelectAll";
    command.CommandType = System.Data.CommandType.StoredProcedure;

    using(var reader = await command.ExecuteReaderAsync())
    {
        if (reader.HasRows)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.Write($"\t{reader.GetName(i)}");
            Console.WriteLine();

            while (await reader.ReadAsync())
            {
                Console.Write($"\t{reader.GetInt32(0)}\t{reader.GetString(1)}{((reader.GetString(1).Length < 8) ? "\t" : "")}\t{reader.GetString(2)}{((reader.GetString(2).Length < 8) ? "\t" : "")}\t{((reader.GetValue(3)?.ToString()?.Length == 0) ? "" : reader.GetDateTime(3).ToShortDateString())}");
                Console.WriteLine();
            }

        }
    }
    */


    /*
    string procedurePricesRanges = @"CREATE PROCEDURE PriceRange
                                        @priceMin DECIMAL (9, 2) OUT,
                                        @priceMax DECIMAL (9, 2) OUT,
                                        @priceAvg DECIMAL (9, 2) OUT
                                     AS
                                        SELECT 
                                            @priceMin = MIN(price),
                                            @priceMax = MAX(price),
                                            @priceAvg = AVG(price)
                                        FROM products
                                     GO";

    command.CommandText = procedurePricesRanges;
    await command.ExecuteNonQueryAsync();
    */

    command.CommandText = "PriceRange";
    command.CommandType = System.Data.CommandType.StoredProcedure;

    SqlParameter priceMin = new SqlParameter()
    {
        ParameterName = "@priceMin",
        SqlDbType = System.Data.SqlDbType.Decimal,
        Direction = System.Data.ParameterDirection.Output
    };

    SqlParameter priceMax = new SqlParameter()
    {
        ParameterName = "@priceMax",
        SqlDbType = System.Data.SqlDbType.Decimal,
        Direction = System.Data.ParameterDirection.Output
    };

    SqlParameter priceAvg = new SqlParameter()
    {
        ParameterName = "@priceAvg",
        SqlDbType = System.Data.SqlDbType.Decimal,
        Direction = System.Data.ParameterDirection.Output
    };

    command.Parameters.AddRange(new[] 
    {
        priceMax,
        priceMin,
        priceAvg
    });

    await command.ExecuteNonQueryAsync();
    Console.WriteLine($"Min price: {command.Parameters["@priceMin"].Value}");
    Console.WriteLine($"Max price: {priceMax.Value}");
    Console.WriteLine($"Avg price: {priceAvg.Value}");

}