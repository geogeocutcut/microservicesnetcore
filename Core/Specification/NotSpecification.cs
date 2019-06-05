using System.Collections.Generic;

namespace Core.Specification
{
    public class NotSpecification<TEntity> : AbstractCompositeSpecification<TEntity>
    {
        private ISpecification<TEntity> _condition;
        public NotSpecification(ISpecification<TEntity> spec)
        {
            _condition = spec;
        }
        public override BusinessResult IsSatisfiedBy(TEntity data)
        {
            BusinessResult br = new BusinessResult();
            if(_condition.IsSatisfiedBy(data).IsSuccess)
            {
                br.Violations.Add(new BusinessViolation {
                    Level = BusinessLevel.Error,
                    Message = "Specification Not " + Name + " isn't satisfied"
                });
            }
            return br;
        }
    }
}
