using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gambling.Common.Utilities
{
    public static class EnumExtensions
    {
        public static List<string> ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));
            List<string> messages = new List<string>();

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return messages;

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            messages.Add(propValue.ToString());
            return messages;
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
