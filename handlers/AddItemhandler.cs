namespace wishlist.handlers;

using maripose;

class AddItemHandler : RequestHandler
{
    public override void Handle()
    {
        string password = _GET["password"];
        string name = _GET["name"];
        float goal = float.Parse(_GET["goal"]);
        string link = _GET["link"];

        if (password == Config.password)
        {
            // name or link containing '|' char cannot be permitted
            // it would break wishlist saving system because
            // it is used as a seperator between different fields
            if (name.Contains('|') || link.Contains('|'))
            {
                // TODO: alert user of why there item was not created
                Context.Response.Redirect("/settings?password=" + Config.password);
                Context.Response.Close();
            }

            Wishlist.AddItem(name, goal, link);
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