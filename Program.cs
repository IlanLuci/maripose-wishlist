global using LogManager;

using maripose;
using maripose.caching;
using maripose.defaultHandlers;
using maripose.logging;

using wishlist.handlers;

WebMaripose web = new WebMaripose();

web.MapDefault(typeof(StaticFileHandler));

web.MapGet("/index.html", typeof(StaticFileHandler), CacheTime.Startup);

web.MapGet("/name", typeof(NameHandler));
web.MapGet("/wishlist", typeof(WishlistHandler));
web.MapGet("/settings", typeof(SettingsHandler));
web.MapGet("/add", typeof(AddItemHandler));
web.MapGet("/remove", typeof(RemoveItemHandler));
web.MapGet("/progress", typeof(ProgressItemHandler));

StaticFileHandler.EnableIncludes("/includes", true);

Logger.SetLog(LogMan.Info);

web.Start(numThreads: 4);

string command = null;

while ((command = LogMan.ReadLine()) != "stop")
{
    if (command == "boop")
    {
        LogMan.Info("beep");
    }
}

web.Stop();