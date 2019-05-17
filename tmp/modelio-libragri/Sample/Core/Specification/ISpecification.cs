using System.Collections.Generic;

namespace Core.Specification
{

    /// <summary>
    /// Définit un comportement général pour la définition d'une règle de gestion
    /// https://en.wikipedia.org/wiki/Specification_pattern
    //  http://blog.xebia.fr/2009/12/29/le-pattern-specification-pour-la-gestion-de-vos-regles-metier/
    /// </summary>
    public interface ISpecification<TEntity>
    {
        /// <summary>
        /// // Détermine si la règle métier est respectée
        /// </summary>
        BusinessResult IsSatisfiedBy(TEntity data);

        /// <summary>
        /// Chainage de règles en ET
        /// </summary>
        ISpecification<TEntity> And(ISpecification<TEntity> otherSpecification);

        /// <summary>
        /// Chainage de règles en OU
        /// </summary>
        ISpecification<TEntity> Or(ISpecification<TEntity> otherSpecification);

        /// <summary>
        /// Contraire d'une règle
        /// </summary>
        ISpecification<TEntity> Not();
    }
}