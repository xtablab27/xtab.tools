using System;
using System.ComponentModel;
using System.Reflection;

namespace xTab.Tools.Attributes
{
    public class LocalDescriptionAttribute : DescriptionAttribute
    {
        private PropertyInfo nameProperty;
        private Type resourceType;

        public LocalDescriptionAttribute(string displayNameKey) : base(displayNameKey) { }

        public Type NameResourceType
        {
            get
            {
                return resourceType;
            }
            set
            {
                resourceType = value;
                nameProperty = resourceType.GetProperty(this.Description, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string Description
        {
            get
            {
                if (nameProperty == null)
                {
                    return base.Description;
                }

                return (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }
        }
    }
}
