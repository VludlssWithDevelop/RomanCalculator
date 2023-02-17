using System;
using System.ComponentModel;
using System.Reflection;

namespace RomanCalculator.Core.Utils
{
    public static class AttributeUtils
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttribute is null)
                throw new ArgumentException($"Description attribute missed for enum value: {value}");

            return descriptionAttribute.Description;
        }
    }
}
