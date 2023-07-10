// Operators and Conditionals
// Inside of the RunExercise method of the Exercise class, use Console.WriteLine to print the result of the following operations to the console:

// Add myFirstValue to mySecondValue

// Subtract mySecondValue from myFirstValue

// Multiply myFirstValue by mySecondValue

// Check whether myFirstValue is greater than (>) mySecondValue

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DataContextDapper dapper = new DataContextDapper();
            DataContextEF entityFramework = new DataContextEF();

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWiFi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 1000.00m,
                VideoCard = "Nvidia",
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWiFi,
                HasLTE ,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard
                    + "','" + myComputer.HasWiFi
                    + "','" + myComputer.HasLTE
                    + "','" + myComputer.ReleaseDate
                    + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + myComputer.VideoCard
            + "')";

            // Console.WriteLine(sql);
            // bool result = dapper.ExecuteSql(sql);
            // Console.WriteLine(result);

            string sqlSelect = @"
            SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWiFi,
                Computer.HasLTE ,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach (Computer computer in computers)
            {
                Console.WriteLine("'" + computer.ComputerId
                        + "','" + computer.Motherboard
                        + "','" + computer.HasWiFi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + computer.VideoCard
                + "'");
            }
            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();

            if (computersEF != null)
            {
                foreach (Computer computer in computersEF)
                {
                    Console.WriteLine("'" + computer.ComputerId
                        + "','" + computer.Motherboard
                        + "','" + computer.HasWiFi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + computer.VideoCard
                + "'");
                }
            }
        }
    }
}