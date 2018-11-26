using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace xTab.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ConditionalValidationAttribute : ValidationAttribute, IClientValidatable
    {
        protected readonly ValidationAttribute innerAttribute;
        protected readonly string dependentProperty;
        protected readonly object targetValue;

        protected ConditionalValidationAttribute(ValidationAttribute innerAttribute, string dependentProperty, object targetValue)
        {
            this.innerAttribute = innerAttribute;
            this.dependentProperty = dependentProperty;
            this.targetValue = targetValue;
        }

        protected abstract string ValidationName { get; }

        protected virtual IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.dependentProperty);

            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);

                if ((dependentValue == null && this.targetValue == null) || (dependentValue != null && dependentValue.Equals(this.targetValue)))
                {
                    if (!innerAttribute.IsValid(value))
                    {
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string dependentPropertyId = BuildDependentPropertyId(metadata, context as ViewContext);
            string targetValue = (this.targetValue ?? String.Empty).ToString();
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = ValidationName
            };
            
            if (this.targetValue.GetType() == typeof(bool))
                targetValue = targetValue.ToLower();
          
            rule.ValidationParameters.Add("dependentproperty", dependentPropertyId);
            rule.ValidationParameters.Add("targetvalue", targetValue);

            foreach (var param in GetExtraValidationParameters())
            {
                rule.ValidationParameters.Add(param);
            }

            yield return rule;
        }


        //public string DependentProperty { get { return this.dependentProperty; } }
        //public object TargetValue { get { return this.targetValue; } }
       
        
        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            string dependentPropertyId = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.dependentProperty);
            string thisField = metadata.PropertyName + "_";

            if (dependentPropertyId.StartsWith(thisField))
            {
                dependentPropertyId = dependentPropertyId.Substring(thisField.Length);
            }
            return dependentPropertyId;
        }
    }
}
