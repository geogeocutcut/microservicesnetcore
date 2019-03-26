namespace Core.Common.Model
{
    public abstract class Entity<TId>
    {
        protected TId _id;

        public abstract TId GetId();

    }
}