using System;
using System.ComponentModel;
using System.Linq;

namespace Stocknize.Crosscutting.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if(fieldInfo is not null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                if(attributes is not null)
                {
                    description = ((DescriptionAttribute)attributes).Description;
                }
            }
            return description;
        }
    }
}
