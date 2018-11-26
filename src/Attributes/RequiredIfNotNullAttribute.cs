using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTab.Tools.Attributes
{
    public class RequiredIfNotNullAttribute : ConditionalValidationAttribute
    {
        public RequiredIfNotNullAttribute(string dependentProperty, object targetValue)
            : base(new RequiredAttribute(), dependentProperty, targetValue) { }

        protected override string ValidationName
        {
            get { return "requiredifnotnull"; }
        }
        
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object> { { "rule", "required" } };
        }
    }
}
