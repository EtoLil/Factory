namespace Factory.Core
{
    public abstract class Details : IDetails
    {
        public Guid Id { get; set; }

        public Details(Guid id)
        {
            Id = id;
        }
    }
}
