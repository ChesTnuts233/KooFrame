using UnityEngine;

namespace KooFrame.KooTools.Math_Utils
{
    public static partial class KooGetRandom
    {
        /// <summary>
        /// 随机返回一个int类型值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int GetRandomValueFrom(int[] values)
        {
            return values[Random.Range(0, values.Length)];
        }


        public static object GetRandomObjFrom(object[] values)
        {
            return values[Random.Range(0, values.Length)];
        }


        /// <summary>
        /// 随机返回一个泛型值
        /// </summary>
        /// <param name="values">泛型字段集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }
    }
}