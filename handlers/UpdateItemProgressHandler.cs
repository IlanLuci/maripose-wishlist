namespace wishlist.handlers;

using maripose;

class UpdateItemProgressHandler : RequestHandler
{
    public override void Handle()
    {
        string password = _GET["password"];
        string name = _GET["name"];
        float completed = float.Parse(_GET["completed"]);

        if (password == Config.password)
        {

            Wishlist.UpdateCompleted(name, completed);
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