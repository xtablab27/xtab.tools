using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTab.Tools.Attributes
{
    public class RequiredIfAttribute : ConditionalValidationAttribute
    {
        public RequiredIfAttribute(string dependentProperty, object targetValue)
            : base(new RequiredAttribute(), dependentProperty, targetValue) { }

        protected override string ValidationName
        {
            get { return "requiredif"; }
        }
        
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object> { { "rule", "required" } };
        }
    }
}
