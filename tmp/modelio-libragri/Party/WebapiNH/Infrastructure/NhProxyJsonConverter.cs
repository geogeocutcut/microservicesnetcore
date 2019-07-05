using System;
using System.Collections;
using Newtonsoft.Json;
using NHibernate.Collection;
using NHibernate.Proxy;

namespace Libragri.PartyDomain.Webapi
{
    public class NhProxyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(INHibernateProxy).IsAssignableFrom(objectType) || typeof(AbstractPersistentCollection).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is AbstractPersistentCollection && ((AbstractPersistentCollection)value).WasInitialized) {
                writer.WriteStartArray();
                foreach(var item in ((IEnumerable)value)){
                    serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
                return;
            }
            writer.WriteNull();
        }
    }
}
