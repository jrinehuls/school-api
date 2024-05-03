namespace SchoolAPI.Services
{
    public interface IEntityFinder<T, I>
    {
        Task<T> FindEntityByIdOrThrow(I id);
    }
}
