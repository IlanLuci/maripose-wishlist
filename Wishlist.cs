using System;
using System.Collections;
using System.Collections.Concurrent;
using System.IO;
using wishlist;

class Wishlist
{
    private static ConcurrentDictionary<string, Item> _list = new();
    private static string _filePath;

    static Wishlist()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string wishlistDirectory = Path.Join(workingDirectory, "/wishlist");
        _filePath = Path.Join(wishlistDirectory, $"{Config.name}.wishlist");

        if (Directory.Exists(wishlistDirectory))
        {
            IEnumerable readLines = File.ReadLines(_filePath);

            // each line will be a new item
            // items will be formatted as "name | goal | completed | link"
            foreach (string item in readLines)
            {
                string[] split = item.Split('|');

                string itemName = split[0];
                float goal = float.Parse(split[1]);
                float completed = float.Parse(split[2]);
                string link = split[3];

                Item wishlistItem = new Item
                {
                    goal = goal,
                    completed = completed,
                    link = link
                };

                _list[itemName] = wishlistItem;
            }
        }
        else
        {
            Directory.CreateDirectory(wishlistDirectory);

            File.Create(_filePath);
        }
    }

    public static string GetAll()
    {
        string wishlistStr = "";

        foreach (var kvp in _list)
        {
            string[] item = { kvp.Key, kvp.Value.goal.ToString(), kvp.Value.completed.ToString(), kvp.Value.link };
            string itemStr = string.Join('|', item);

            wishlistStr += itemStr + System.Environment.NewLine;
        }

        return wishlistStr;
    }

    public static void AddItem(string name, float goal, string link)
    {
        Item wishlistItem = new Item
        {
            goal = goal,
            completed = 0,
            link = link
        };

        _list[name] = wishlistItem;
    }
    public static void RemoveItem(string name)
    {
        _list.TryRemove(name, out _);
    }

    public static void UpdateCompleted(string name, float completed)
    {
        _list[name].completed = completed;
    }
    public static void UpdateGoal(string name, float goal)
    {   
        _list[name].goal = goal;
    }

    public static void Save()
    {
        StreamWriter writer = new StreamWriter(_filePath);

        foreach (var kvp in _list)
        {
            string[] item = { kvp.Key, kvp.Value.goal.ToString(), kvp.Value.completed.ToString(), kvp.Value.link };
            string itemStr = string.Join('|', item);
            
            writer.WriteLine(itemStr);
        }

        writer.Flush();
        writer.Close();
    }
}