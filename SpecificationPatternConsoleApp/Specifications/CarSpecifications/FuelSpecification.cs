using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class FuelSpecification : Specification<CarEntity>
    {
        protected string _fuel;

        public FuelSpecification(string fuel)
        {
            _fuel = fuel;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Fuel.Equals(_fuel);
        }
    }
}