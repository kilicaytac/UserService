
namespace UserService.Domain.Kernel
{
    public abstract class Entity<TId>
    {
       
        TId _Id;
        public virtual TId Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }
       
        protected Entity()
        {
        }
    }
}
