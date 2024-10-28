using System.ComponentModel;
using System.Reflection;

namespace Arc.Common.Extentions
{

    /// <summary>
    /// Global Extentions.
    /// </summary>
    public static class Extentions
    {

        /// <summary>
        /// Use for get "Description" of enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
#pragma warning disable CS8600
            FieldInfo field = value.GetType().GetField(value.ToString());
#pragma warning restore CS8600
#pragma warning disable CS8600
#pragma warning disable CS8604
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
#pragma warning restore CS8604
#pragma warning restore CS8600
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }

}