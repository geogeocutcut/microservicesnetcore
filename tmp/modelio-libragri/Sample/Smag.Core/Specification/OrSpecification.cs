using System.Collections.Generic;

namespace Smag.Core.Specification
{
    /// <summary>
    /// Opérateur OR pour les spécifications
    /// </summary>
    public class OrSpecification<TEntity> : AbstractCompositeSpecification<TEntity>
    {
        private ISpecification<TEntity> _conditionLeft;
        private ISpecification<TEntity> _conditionRight;

        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _conditionLeft = left;
            _conditionRight = right;
        }

        public override BusinessResult IsSatisfiedBy(TEntity data)
        {
            BusinessResult resultSpecification = new BusinessResult();
            var br1 = _conditionLeft.IsSatisfiedBy(data);
            var br2 = _conditionRight.IsSatisfiedBy(data);
            if (!br1.IsSuccess && !br2.IsSuccess)
            {
                resultSpecification.Violations.AddRange(br1.Violations);
                resultSpecification.Violations.AddRange(br2.Violations);
            }

            return resultSpecification;
        }
    }
}
