using System.ComponentModel.DataAnnotations;

namespace Frock_backend.access_and_identity.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty");

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format");

            Value = email.ToLower();
        }

        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new Email(value);
    }
}
