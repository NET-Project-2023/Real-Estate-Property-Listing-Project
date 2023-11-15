using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features
{
    public static class ValidationMessages
    {
        public const string RequiredMessage = "{0} is required";
        public const string MaxLengthMessage = "{0} must not exceed {1} characters.";
        public const string GreaterThanZeroMessage = "{0} must be greater than 0";
        public const string NotEmptyListMessage = "{0} must not be empty";
        public const string NotValidEnumMessage = "{0} is not a valid status";
        public const string NotEmptyGuidMessage = "{0} must not be empty";
        public const string EmailFormatMessage = "Invalid {0} format.";
        public const string MinLengthMessage = "{0} must be at least {MinLength} characters long.";
        public const string PhoneNumberFormatMessage = "{0} must be in a valid phone number format.";
        public const string NotValidRoleMessage = "{0} is not a valid role.";
    }

}
