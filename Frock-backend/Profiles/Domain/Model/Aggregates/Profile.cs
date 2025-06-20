using Frock_backend.Profiles.Domain.Model.Commands;
using Frock_backend.Profiles.Domain.Model.ValueObjects;
using System.Net.Mail;

namespace Frock_backend.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Profile Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Profile aggregate root.
/// It contains the properties and methods to manage the profile information.
/// </remarks>
public partial class Profile
{
    public int Id { get; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public StreetAddress Address { get; private set; }
    
    public string FullName => Name.FullName;
    public string EmailAddress => Email.email;
    public string StreetAddress => Address.FullAddress;

    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
    }
    
    public Profile(string firstName, string lastName, string email, string street, string city, string state, string postalCode, string country, string number)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Address = new StreetAddress(street, city, state, postalCode, country, number);
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Address = new StreetAddress(command.Street, command.City,command.City,  command.PostalCode, command.Country, command.Number);
    }
}