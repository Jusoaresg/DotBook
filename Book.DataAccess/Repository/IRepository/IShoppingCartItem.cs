using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Book.DataAccess;

public interface IShoppingCartItem : IRepository<ShoppingCartItem>
{
    public IEnumerable<ShoppingCartItem> GetAllUserCart(string userId);
    public void Update(ShoppingCartItem obj);
}
