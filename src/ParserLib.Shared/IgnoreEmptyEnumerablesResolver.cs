using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Reflection;

namespace SheepReaper.GameSaves
{
    public class IgnoreEmptyEnumerablesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) &&
                typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    IEnumerable enumerable = null;

                    switch (member.MemberType)
                    {
                        // this value could be in a public field or public property
                        case MemberTypes.Property:
                            enumerable = instance
                                .GetType()
                                .GetProperty(member.Name)
                                ?.GetValue(instance, null) as IEnumerable;
                            break;

                        case MemberTypes.Field:
                            enumerable = instance
                                .GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as IEnumerable;
                            break;

                        default:
                            break;
                    }

                    return enumerable == null || enumerable.GetEnumerator().MoveNext();
                };
            }

            return property;
        }

        public static readonly IgnoreEmptyEnumerablesResolver Instance = new IgnoreEmptyEnumerablesResolver();
    }
}