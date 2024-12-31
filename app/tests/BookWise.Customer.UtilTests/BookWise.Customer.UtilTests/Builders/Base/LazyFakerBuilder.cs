using Bogus;

namespace BookWise.Customer.UtilTests.Builders.Base;

public abstract class LazyFakerBuilder<TEntity> 
    where TEntity : class, new()
{
    private readonly Lazy<Faker<TEntity>> _lazyFaker;

    protected Faker<TEntity> Faker => _lazyFaker.Value;

    public virtual TEntity Build() => Faker.Generate();

    protected abstract Faker<TEntity> Factory();

    protected LazyFakerBuilder()
    {
        _lazyFaker = new Lazy<Faker<TEntity>>(
            Factory, isThreadSafe: true);
    }

    public ICollection<TEntity> BuildCollection(int? count = null)
    {
        count ??= Random.Shared.Next(3, 100);

        return Enumerable.Range(0, count.Value)
            .Select(_ => Build())
            .ToArray(); 
    }
}
