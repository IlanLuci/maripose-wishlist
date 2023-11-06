namespace wishlist.handlers;

using maripose;

class NameHandler : RequestHandler
{
    public override void Handle()
    {
        Write(Config.name);
    }
}