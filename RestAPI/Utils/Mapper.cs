namespace RestAPI.Utils
{
    public class Mapper
    {
        public static T MapPropertys<T>(object source)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = typeof(T).GetProperties();
            var destination = Activator.CreateInstance<T>();
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationProperties)
                {
                    if (sourceProperty.Name == destinationProperty.Name)
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
            return destination;
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

        public static void MapPropertys<T>(object source, in T target)
        {
            if(target == null)
                throw new ArgumentNullException(nameof(target));

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = target.GetType().GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationProperties)
                {
                    if (sourceProperty.Name == destinationProperty.Name)
                    {
                        destinationProperty.SetValue(target, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}
