using ZestWeb.Models;


namespace ZestWeb.IService
{
    public interface IUserService
    {
        public bool add(User b);
        public bool update(User b);
        public bool delete(Guid id);
        public User getUserById(Guid id);
        public List<User> getUserByName(string name);
        public User getUserByPhone(string name);
        public List<User> GetAll();
    }
}
