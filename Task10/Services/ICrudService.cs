namespace Task10.Services;

public interface ICrudService<T>
{
    public Task<List<T>> List();
    public Task<T> Create();
    public Task<T> Update();
    public Task Delete();
}