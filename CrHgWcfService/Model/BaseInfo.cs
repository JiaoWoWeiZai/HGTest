using System;
using CrHgWcfService.Common;

namespace CrHgWcfService.Model
{
    public class BaseInfo
    {
        public override string ToString()
        {
            return JsonHelper.Serialize(this);
        }

        //public BaseInfo() { }

        //public static T GetInfo<T>(int pInter,string resultSet)
        //{
        //    HgEngine.SetResultSet(pInter, resultSet);
        //    var info = Activator.CreateInstance<T>();

        //    foreach (var proInfo in typeof(T).GetProperties())
        //    {
        //        var value = string.Empty;
        //        HgEngine.GetByName(pInter, proInfo.Name, ref value);
        //        proInfo.SetValue(info, value, null);
        //    }
        //    return info;
        //}
    }
}