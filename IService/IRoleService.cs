using ZestWeb.Models;


namespace ZestWeb.IService
{
    public interface IRoleService
    {
        public bool add(Role b);
        public bool update(Role b);
        public bool delete(Guid id);
        public Role getRoleById(Guid id);
        public List<Role> getRoleByName(string name);
        public List<Role> GetAll();
    }
}
