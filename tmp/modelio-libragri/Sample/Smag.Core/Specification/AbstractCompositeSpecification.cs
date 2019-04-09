
using System.Collections.Generic;

namespace Smag.Core.Specification
{
    public abstract class AbstractCompositeSpecification<TEntity> : ISpecification<TEntity>
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public AbstractCompositeSpecification()
        {
            Name = this.GetType().ToString();
        }

        public abstract BusinessResult IsSatisfiedBy(TEntity entity);
        public ISpecification<TEntity> And(ISpecification<TEntity> other) => new AndSpecification<TEntity>(this, other);
        public ISpecification<TEntity> Or(ISpecification<TEntity> other) => new OrSpecification<TEntity>(this, other);
        public ISpecification<TEntity> Not() => new NotSpecification<TEntity>(this);
    }
}