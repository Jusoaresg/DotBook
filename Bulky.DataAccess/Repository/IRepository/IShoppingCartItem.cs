using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Bulky.DataAccess;

public interface IShoppingCartItem : IRepository<ShoppingCartItem>
{
    public IEnumerable<ShoppingCartItem> GetAllUserCart(string userId);
    public void Update(ShoppingCartItem obj);
}
