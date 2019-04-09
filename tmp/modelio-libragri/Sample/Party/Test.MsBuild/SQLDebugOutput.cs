using NHibernate;

namespace Smag.PartyDomain.Test.Mock
{
    public class SQLDebugOutput : EmptyInterceptor, IInterceptor
    {
        public override NHibernate.SqlCommand.SqlString
           OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine("NH: " + sql);

            return base.OnPrepareStatement(sql);
        }
    }
}