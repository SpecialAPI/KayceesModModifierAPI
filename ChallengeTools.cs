using System;
using System.Collections.Generic;
using System.Text;

namespace KayceesModModifierAPI
{
    public static class ChallengeTools
    {
        public static List<T> Repeat<T>(this T toRepeat, int times)
        {
            List<T> repeated = new List<T>();
            if(times > 0)
            {
                for(int i = 0; i < times; i++)
                {
                    repeated.Add(toRepeat);
                }
            }
            return repeated;
        }

        public static bool AllEqual<T>(this List<T> list, T comparison)
        {
            foreach(T l in list)
            {
                if(!l.Equals(comparison))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
