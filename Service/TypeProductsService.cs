using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;

namespace ZestWeb.Service
{
    public class TypeProductsService:ITypeProductsService
    {
        private readonly AsmDbContext _context;

        public TypeProductsService()
        {
            _context = new AsmDbContext();
        }
        public bool add(TypeProduct b)
        {
            if (b == null) return false;

            _context.TypeProducts.Add(b);
            _context.SaveChanges();
            return true;
        }

        public bool update(TypeProduct b)
        {
            throw new NotImplementedException();
        }

        public bool delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public TypeProduct getById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TypeProduct> getByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<TypeProduct> GetAll()
        {
            return _context.TypeProducts.ToList();
        }
    }
}
