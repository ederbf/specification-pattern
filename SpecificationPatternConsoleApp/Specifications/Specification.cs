using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPatternConsoleApp.Specifications
{
    public abstract class Specification<T> 
    {
        public abstract Expression<Func<T, bool>> ToExpression();        
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public bool IsSatisfiedBy(T entity) {            
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> And(Specification<T> specification) {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification) {
            return new OrSpecification<T>(this, specification);
        }   

        public Specification<T> Not() {
            return new NotSpecification<T>(this);
        } 

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }        
    }

    public class NotSpecification<T> : Specification<T> 
    {
        private readonly Specification<T> _nested;        

        public NotSpecification(Specification<T> nested) {
            _nested = nested;            
        }

        public override Expression<Func<T, bool>> ToExpression() {            
            Expression<Func<T, bool>> nestedExpression = _nested.ToExpression();
            var exprBody = Expression.Not(nestedExpression.Body);
            return Expression.Lambda<Func<T, bool>>(exprBody, nestedExpression.Parameters);            
        }
    }

    public class AndSpecification<T> : Specification<T> 
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var visitor = new ParameterReplaceVisitor()
            {
                Target = rightExpression.Parameters[0],
                Replacement = leftExpression.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(rightExpression.Body);
            var andExpression = Expression.AndAlso(leftExpression.Body, rewrittenRight);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters);          
        }
    }

    public class OrSpecification<T> : Specification<T> 
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var visitor = new ParameterReplaceVisitor()
            {
                Target = rightExpression.Parameters[0],
                Replacement = leftExpression.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(rightExpression.Body);
            var andExpression = Expression.OrElse(leftExpression.Body, rewrittenRight);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters);
        }
    }    

    internal class ParameterReplacer : ExpressionVisitor {

        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node) 
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter) {
            _parameter = parameter;
        }
    }

    public class ParameterReplaceVisitor : ExpressionVisitor
    {
        public ParameterExpression Target { get; set; }
        public ParameterExpression Replacement { get; set; }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == Target ? Replacement : base.VisitParameter(node);
        }
    }   
}