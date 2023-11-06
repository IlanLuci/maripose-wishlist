namespace wishlist.handlers;

using maripose;

class RemoveItemHandler : RequestHandler
{
    public override void Handle()
    {
        string password = _GET["password"];
        string name = _GET["name"];

        if (password == Config.password)
        {
            Wishlist.RemoveItem(name);
            Wishlist.Save();

            Context.Response.Redirect("/settings?password=" + Config.password);
            Context.Response.Close();
        }
        else
        {
            Context.Response.Redirect("/");
            Context.Response.Close();
        }
    }
}