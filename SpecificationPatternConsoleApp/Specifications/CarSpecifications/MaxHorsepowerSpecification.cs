using System;
using System.Linq.Expressions;
using SpecificationPatternConsoleApp.Database.Entites;

namespace SpecificationPatternConsoleApp.Specifications.CarSpecifications
{
    public class MaxHorsepowerSpecification : Specification<CarEntity>
    {
        protected int _maxHP;

        public MaxHorsepowerSpecification(int maxHP)
        {
            _maxHP = maxHP;
        }
        public override Expression<Func<CarEntity,bool>> ToExpression()
        {
            return x => x.Horsepower <= _maxHP;
        }
    }
}