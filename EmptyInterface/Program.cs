using System.Collections.Generic;

namespace EmptyInterface;

public static class Program
{
    public static void Main(string[] args)
    {
        var ctx = new Dictionary<string,string>();

        var arr = new object[]{
            "hello",
            new data{name="Bolo"},
        };

        foreach(var item in arr)
        {
            ctx = authorize(item,ctx);
            System.Console.WriteLine("Name is {0}",ctx["Name"]);
        }


    }

    private static Dictionary<string,string> authorize(object obj, Dictionary<string,string> ctx){
        if (obj is INameGetter nameGetter )
        {
            ctx["Name"] = nameGetter.GetName();
            return ctx;
        }

        ctx["Name"] = "Undefined";
        return ctx;
    }
}

interface INameGetter{
    string GetName();
}

class data : INameGetter
{
    public string name { get; set; }

    public string GetName() => name;
}

