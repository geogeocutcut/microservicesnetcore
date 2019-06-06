using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NHibernate;
using NHibernate.Collection;
using NHibernate.Proxy;

namespace Smag.PartyDomain.Webapi
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

    public class NHibernateContractResolver : DefaultContractResolver
    {
        private static readonly MemberInfo[] NHibernateProxyInterfaceMembers = typeof(INHibernateProxy).GetMembers();

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);

            members.RemoveAll(memberInfo =>
                            (IsMemberPartOfNHibernateProxyInterface(memberInfo)) ||
                            (IsMemberDynamicProxyMixin(memberInfo)) ||
                            (IsMemberMarkedWithIgnoreAttribute(memberInfo, objectType)) ||
                            (IsMemberInheritedFromProxySuperclass(memberInfo, objectType)));

            var actualMemberInfos = new List<MemberInfo>();

            foreach (var memberInfo in members)
            {
                var infos = memberInfo.DeclaringType.BaseType.GetMember(memberInfo.Name);
                actualMemberInfos.Add(infos.Length == 0 ? memberInfo : infos[0]);
            }

            return actualMemberInfos;
        }

        private static bool IsMemberDynamicProxyMixin(MemberInfo memberInfo)
        {
            return memberInfo.Name == "__interceptors";
        }

        private static bool IsMemberInheritedFromProxySuperclass(MemberInfo memberInfo, Type objectType)
        {
            return memberInfo.DeclaringType.Assembly == typeof(INHibernateProxy).Assembly;
        }

        private static bool IsMemberMarkedWithIgnoreAttribute(MemberInfo memberInfo, Type objectType)
        {
            var infos = typeof(INHibernateProxy).IsAssignableFrom(objectType)
                        ? objectType.BaseType.GetMember(memberInfo.Name)
                        : objectType.GetMember(memberInfo.Name);

            return infos[0].GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Length > 0;
        }

        private static bool IsMemberPartOfNHibernateProxyInterface(MemberInfo memberInfo)
        {
            return Array.Exists(NHibernateProxyInterfaceMembers, mi => memberInfo.Name == mi.Name);
        }


        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(INHibernateProxy).IsAssignableFrom(objectType))
            {
                var oType = objectType.GetInterfaces().FirstOrDefault(i => i.FullName.StartsWith("Your.Domain.Namespace"));
                return oType != null ? base.CreateContract(oType) : base.CreateContract(objectType.BaseType);
            }
            return base.CreateContract(objectType);
        }
    }
}
