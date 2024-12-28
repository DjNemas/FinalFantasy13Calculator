using System.Reflection;

namespace Shared.Utils
{
    public class Mapper
    {
        public static T MapPropertys<T>(object source, bool ignoreNull = true)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = typeof(T).GetProperties();
            var target = Activator.CreateInstance<T>();
            MapPropertys(source, target, sourceProperties, destinationProperties, ignoreNull);
            return target;
        }

        public static IEnumerable<T> MapPropertys<T>(IEnumerable<object> source)
        {
            var destinationList = new List<T>();
            foreach (var item in source)
            {
                destinationList.Add(MapPropertys<T>(item));
            }
            return destinationList;
        }

        public static void MapPropertys<T>(object source, in T target, bool ignoreNull = true)
        {
            if(target == null)
                throw new ArgumentNullException(nameof(target));

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = target.GetType().GetProperties();
            MapPropertys(source, target, sourceProperties, destinationProperties, ignoreNull);
        }

        private static void MapPropertys<T>(object source, T target, PropertyInfo[] sourceProperties, PropertyInfo[] destinationProperties, bool ignoreNull = true)
        {
            foreach (var sourceProperty in sourceProperties)
            {
                if (ignoreNull && sourceProperty.GetValue(source) is null)
                    continue;
                foreach (var destinationProperty in destinationProperties)
                {
                    if (sourceProperty.Name == destinationProperty.Name && sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        destinationProperty.SetValue(target, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}
