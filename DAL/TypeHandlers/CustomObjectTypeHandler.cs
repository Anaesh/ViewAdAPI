using System;
using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace ViewAdAPI.DAL.TypeHandlers
{
    public class CustomObjectTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = value.ToString();
        }
    }
}

