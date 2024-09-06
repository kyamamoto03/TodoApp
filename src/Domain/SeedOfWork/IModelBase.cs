namespace Domain.SeedOfWork;

public abstract class IModelBase
{
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }

    public void Created(DateTime now)
    {
        CreateDate = now;
        UpdateDate = now;
    }

    public void Updated(DateTime now)
    {
        UpdateDate = now;
    }
}