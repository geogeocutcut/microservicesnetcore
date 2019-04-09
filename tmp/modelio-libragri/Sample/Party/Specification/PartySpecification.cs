using System;
using System.Collections.Generic;
using Smag.Core.Specification;
using Smag.PartyDomain.Model;

namespace Smag.PartyDomain.Specification
{
    public class PartySpecification
    {
        private static volatile ISpecification<Party> _specification;
        private static readonly object _obj = new object();
        private const string EntityName = "Party";
        private class PartyNameSpecification : AbstractCompositeSpecification<Party>
        {
            
            public override BusinessResult IsSatisfiedBy(Party party)
            {
                var br = new BusinessResult();


                if (string.IsNullOrEmpty(party?.Name))
                    br.Violations.Add(new BusinessViolation
                    {
                        Level = BusinessLevel.Error,
                        Message = "Specification " + Name + " isn't satisfied : " + Message
                    });

                return br;
            }
        }
        
        public static BusinessResult IsSatisfiedBy(Party party)
        {
            if (_specification == null)
                lock (_obj)
                    if (_specification == null)
                        _specification = new PartyNameSpecification { Message = ValidationErrorMessage.MandatoryCheckError };
           

            return _specification.IsSatisfiedBy(party);
        }
    }
}
