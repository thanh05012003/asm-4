using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;

namespace ZestWeb.Service
{
    public class RoleService:IRoleService
    {
        private readonly AsmDbContext _context;
        public RoleService()
        {
            _context = new AsmDbContext();
        }
        public bool add(Role b)
        {
            throw new NotImplementedException();
        }

        public bool update(Role b)
        {
            throw new NotImplementedException();
        }

        public bool delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Role getRoleById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Role> getRoleByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
    }
}
