namespace Frock_backend.Profiles.Domain.Model.ValueObjects
{
    public class PersonName
    {
        public string FirstName;
        public string LastName;
        public string FullName { get; private set; }

        public PersonName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = $"{firstName} {lastName}";
        }
        public PersonName() {
            this.FirstName = "";
            this.LastName = "";
            this.FullName = "";
        }
    }
}
