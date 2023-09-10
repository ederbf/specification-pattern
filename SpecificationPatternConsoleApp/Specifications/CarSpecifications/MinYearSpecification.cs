using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class MinYearSpecification : Specification<CarEntity>
    {
        protected int _minYear;

        public MinYearSpecification(int minYear)
        {
            _minYear = minYear;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Year >= _minYear;
        }
    }
}