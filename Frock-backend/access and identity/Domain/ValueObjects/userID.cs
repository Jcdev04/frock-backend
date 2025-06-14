namespace Frock_backend.access_and_identity.Domain.ValueObjects
{
    public class userID
    {
        public string Value { get; private set; }

        public userID(string firstName, string lastName)
        {
            Value = GenerateUserId(firstName, lastName);
        }

        public userID(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("UserId cannot be null or empty");

            Value = value;
        }

        private string GenerateUserId(string firstName, string lastName)
        {
            var firstLetter = firstName.ToUpper()[0];
            var lastLetter = lastName.ToUpper()[0];
            var randomDigits = new Random().Next(10000, 99999);

            return $"{firstLetter}{lastLetter}{randomDigits}";
        }

        public override string ToString() => Value;

        public static implicit operator string(userID userId) => userId.Value;
        public static implicit operator userID (string value) => new userID (value);
    }
}

