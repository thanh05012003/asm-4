using ZestWeb.Context;
using ZestWeb.IService;
using ZestWeb.Models;

namespace ZestWeb.Service
{
    public class UserService:IUserService
    {
        private readonly AsmDbContext _context;

        public UserService()
        {
            _context = new AsmDbContext();
        }
        public bool add(User b)
        {
            try
            {
                _context.Users.Add(b);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(User b)
        {
            if (b != null)
            {
                var User = _context.Users.FirstOrDefault(p => p.Id == b.Id);
                User.Name = b.Name;
                User.Password = b.Password;
                User.IdRole = b.IdRole;
                User.Status = b.Status;
                User.Phone = b.Phone;
                User.Address = b.Address;
                _context.Users.Update(User);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool delete(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public User getUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public List<User> getUserByName(string name)
        {
            return _context.Users.Where(p => p.Name == name).ToList();
        }

        public User getUserByPhone(string name)
        {
            return _context.Users.FirstOrDefault(c => c.Phone == name.Trim());
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}
