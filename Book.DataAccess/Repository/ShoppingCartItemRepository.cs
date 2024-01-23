using System.Data.Common;
using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;

namespace Book.DataAccess;

public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItem
{
    private readonly ApplicationDbContext _db;

    public ShoppingCartItemRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<ShoppingCartItem> GetAllUserCart(string userId)
    {
        IQueryable<ShoppingCartItem> query = dbSet;
        var items = query.Where(u=>u.UserId==userId);
        return items;
    }

    public void Update(ShoppingCartItem obj)
    {
        //var itemFromDb = _db.ShoppingCartItems.FirstOrDefault(u=>u.Id==obj.Id);
        _db.ShoppingCartItems.Update(obj);
    }
}
