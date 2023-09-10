using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class MinHorsepowerSpecification : Specification<CarEntity>
    {
        protected int _minHP;

        public MinHorsepowerSpecification(int minHP)
        {
            _minHP = minHP;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Horsepower >= _minHP;
        }
    }
}