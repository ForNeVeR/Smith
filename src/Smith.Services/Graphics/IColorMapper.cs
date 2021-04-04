namespace Smith.Services.Graphics
{
    public interface IColorMapper
    {
        string this[long id]
        {
            get;
        }
    }
}