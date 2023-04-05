namespace Ascend_Server.api.IServices;

    public interface IGenericService<T> where T : class
    { 
        Task Add(T entity);
        void Update(T enity); 
    }

