using System.Text;
using UnityEngine;

namespace NetLib.Extensions
{
    public static class JsonExtensions
    {
        public static byte[] ToJsonBinary<T>(this T data) where T : new()
        {
            string json = JsonUtility.ToJson(data);
            return Encoding.ASCII.GetBytes(json);
        }

        public static T FromJsonBinary<T>(this byte[] data) where T : new()
        {
            string json = Encoding.ASCII.GetString(data);
            return JsonUtility.FromJson<T>(json);
        }
    }
}