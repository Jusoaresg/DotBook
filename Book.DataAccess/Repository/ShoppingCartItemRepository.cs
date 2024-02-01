using System.Data.Common;
using System.Linq.Expressions;
using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess;

public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItem
{
    private readonly ApplicationDbContext _db;

    public ShoppingCartItemRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(ShoppingCartItem obj)
    {

        var updatedItem = _db.ShoppingCartItems.
                                        Include(item => item.Product).
                                        FirstOrDefault(item => item.Id == obj.Id);
        if (updatedItem != null)
        {
            double price = obj.Amount > 100 ? obj.Product.Price100 :
                            obj.Amount > 50 ? obj.Product.Price50 :
                            obj.Product.Price;
        }

        _db.ShoppingCartItems.Update(obj);
    }
}
