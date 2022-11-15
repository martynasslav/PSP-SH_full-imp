namespace PoSSapi.Tools
{
    public class RandomGenerator
    {
        public static T GenerateRandom<T>(string? id = null) where T : new()
        {
            var random = new Random();
            var instance = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "Id")
                {
                    property.SetValue(instance, id ?? Guid.NewGuid().ToString());
                }
                else
                {
                    switch(property.PropertyType)
                    {
                        case Type t when t == typeof(string):
                            property.SetValue(instance, Guid.NewGuid().ToString());
                            break;
                        case Type t when t == typeof(int):
                            property.SetValue(instance, random.Next());
                            break;
                        case Type t when t == typeof(double):
                            property.SetValue(instance, random.NextDouble());
                            break;
                        case Type t when t == typeof(DateTime):
                            property.SetValue(instance, DateTime.Now);
                            break;
                        case Type t when t == typeof(bool):
                            property.SetValue(instance, random.Next(0, 2) == 1);
                            break;
                        case Type t when t == typeof(Guid):
                            property.SetValue(instance, Guid.NewGuid());
                            break;
                        case Type t when t == typeof(List<>):
                            property.SetValue(instance, new List<object>());
                            break;
                        case Type t when t == typeof(Enum):
                            property.SetValue(instance, Enum.GetValues(property.PropertyType).GetValue(random.Next(0, Enum.GetValues(property.PropertyType).Length)));
                            break;
                        case Type t when t == typeof(Decimal):
                            property.SetValue(instance, (decimal)random.NextDouble());
                            break;
                        default:
                            property.SetValue(instance, null);
                            break;
                    }
                }
            }

            return instance;
        }
    }
}
