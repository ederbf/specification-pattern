using SpecificationPatternConsoleApp.Database.Entites;
using System;
using System.Linq;

namespace SpecificationPatternConsoleApp.Database
{
    public static class SampleData
      {
          static object synchlock = new object();
          static volatile bool seeded = false;

          public static void EnsureSeedData(this Context context)
          {
              if (!seeded && context.Cars.Count() == 0)
              {
                  lock (synchlock)
                  {
                      if (!seeded)
                      {
                          var cars = GenerateCars();

                          context.Cars.AddRange(cars);

                          context.SaveChanges();
                          seeded = true;
                      }
                  }
              }
          }

          #region Data
          public static CarEntity[] GenerateCars()
          {
              return new CarEntity[] {
                  new CarEntity
                  {
                      Manufacturer = "Opel",
                      Model = "Astra G",
                      Year = 1998,
                      Fuel  = "gasoline",
                      Horsepower = 150
                  },
                  new CarEntity
                  {
                      Manufacturer = "BMW",
                      Model = "325i e36",
                      Year = 1992,
                      Fuel  = "gasoline",
                      Horsepower = 192
                  },
                  new CarEntity
                  {
                      Manufacturer = "BMW",
                      Model = "116d f21",
                      Year = 2013,
                      Fuel  = "diesel",
                      Horsepower = 116
                  },
                  new CarEntity
                  {
                      Manufacturer = "Porsche",
                      Model = "944",
                      Year = 1982,
                      Fuel  = "gasoline",
                      Horsepower = 163
                  },
                  new CarEntity
                  {
                      Manufacturer = "Audi",
                      Model = "A1",
                      Year = 2010,
                      Fuel  = "gasoline",
                      Horsepower = 90
                  },
                  new CarEntity
                  {
                      Manufacturer = "Toyota",
                      Model = "Land Cruiser J120",
                      Year = 2010,
                      Fuel  = "diesel",
                      Horsepower = 164
                  }
              };
          }

          #endregion
      }
}
