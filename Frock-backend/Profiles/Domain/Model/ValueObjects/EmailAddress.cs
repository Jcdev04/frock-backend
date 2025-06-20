namespace Frock_backend.Profiles.Domain.Model.ValueObjects
{
    public class EmailAddress
    {
        public string email;
        public string Address;

        public EmailAddress(string email) { 
            this.email = email;
            this.Address = email;
        }
        public EmailAddress() {
            email = "";
            Address = "";
        }
    }
}
