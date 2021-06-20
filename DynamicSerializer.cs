using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AplOkien
{
    public static class DynamicSerializer
    {
        public static void Save<T>(T ob, StreamWriter sw)
        {
            Type t = ob.GetType();
            sw.WriteLine($"[[{t.FullName}]]");
            foreach (var p in t.GetProperties().Where(prop => prop.CanWrite))
            {
                sw.WriteLine($"[{p.Name}]");
                sw.WriteLine(p.GetValue(ob));
            }
            sw.WriteLine($"[[]]");
        }

        public static T Load<T>(StreamReader sr) where T : new()
        {
            T ob = default(T);
            Type tob = null;
            PropertyInfo property = null;

            while (!sr.EndOfStream)
            {
                var ln = sr.ReadLine();
                if (ln == "[[]]")
                    return ob;
                if (ln.StartsWith("[["))
                {
                    tob = Type.GetType(ln.Trim('[', ']'));
                    if (tob == null)
                    {
                        return default(T);
                    }
                    if (!typeof(T).IsAssignableFrom(tob))
                    {
                        return default(T);
                    }
                    ob = (T)Activator.CreateInstance(tob);
                }
                else if (ln.StartsWith("[") && ob != null)
                {
                    property = tob.GetProperty(ln.Trim('[', ']'));
                    if (property == null)
                    {
                        return default(T);
                    }
                }
                else if (ob != null && property != null)
                {
                    property.SetValue(ob, Convert.ChangeType(ln, property.PropertyType));
                }
                else
                {
                    return default(T);
                }
            }

            return default(T);

        }
    }
}
