using System;

namespace Core.Common.Model
{
    public abstract class AuditableEntity<TId> : BaseEntity<TId>
    {
        /// <summary>
        /// Date à laquelle l'objet a été créé.
        /// </summary>
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ayant créé l'objet actuel.
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Date à laquelle l'objet a été supprimé.
        /// </summary>
        public virtual DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ayant supprimé l'objet actuel.
        /// </summary>
        public virtual string DeletedBy { get; set; }

        /// <summary>
        /// Date à laquelle l'objet a été mis à jour.
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur ayant modifié l'objet actuel.
        /// </summary>
        public virtual string UpdatedBy { get; set; }

    }
}