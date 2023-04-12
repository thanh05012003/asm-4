using ZestWeb.Models;


namespace ZestWeb.IService
{
    public interface ITypeProductsService
    {
        public bool add(TypeProduct b);
        public bool update(TypeProduct b);
        public bool delete(Guid id);
        public TypeProduct getById(Guid id);
        public List<TypeProduct> getByName(string name);
        public List<TypeProduct> GetAll();
    }
}
