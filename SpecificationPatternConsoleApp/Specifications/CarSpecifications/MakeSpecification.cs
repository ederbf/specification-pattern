using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class ManufacturerSpecification : Specification<CarEntity>
    {
        protected string _carManufacturer;

        public ManufacturerSpecification(string carManufacturer)
        {
            _carManufacturer = carManufacturer;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Manufacturer.Equals(_carManufacturer);
        }
    }
}