using System.Collections.Generic;
using UnityEngine;

namespace KooFrame.ReadExcel
{
    public class AbstractIReadExcel_SO<T> : ScriptableObject
    {
        public List<T> dataList = new List<T>();

        public void Init(List<T> list)
        {
            Clear();
            foreach (var data in list)
            {
                dataList.Add(data);
            }
        }

        public void Clear()
        {
            dataList.Clear();
        }
    }
}