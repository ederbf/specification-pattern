using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class MaxYearSpecification : Specification<CarEntity>
    {
        protected int _maxYear;

        public MaxYearSpecification(int maxYear)
        {
            _maxYear = maxYear;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Year <= _maxYear;
        }
    }
}