namespace Smith.Services.Persistance
{
    public interface IDatabaseContextFactory
    {
        DatabaseContext CreateDbContext();
    }
}