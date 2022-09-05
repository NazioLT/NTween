using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.NTween
{
    public static class ListExtension
    {
        public static T First<T>(this List<T> _list) => _list[0];

        private static void test()
        {
            List<string> a = new List<string>();
            
            string b = a.First();
        }
    }
}