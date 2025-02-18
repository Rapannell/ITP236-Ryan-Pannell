using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI_RyanPannell
{
    public static class Extension
    {
        public static string ProperName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            string[] parts = name.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (!string.IsNullOrEmpty(parts[i]))
                {
                    parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", parts);
        }
    }
}
