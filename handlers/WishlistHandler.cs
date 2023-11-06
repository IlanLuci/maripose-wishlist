namespace wishlist.handlers;

using maripose;

class WishlistHandler : RequestHandler
{
    public override void Handle()
    {
        string wishlistStr = Wishlist.GetAll();

        Write(wishlistStr);
    }
}