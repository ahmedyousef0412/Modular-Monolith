using Identity.Domain.Entity;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("Users");

         builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
             .HasConversion(
                 email => email.Value,           
                 value => Email.Create(value)   // Convert string from DB back to Email Value Object
             )
             .IsRequired()
             .HasMaxLength(200);



        
        builder.OwnsMany(u => u.RefreshTokens, rt =>
        {
            rt.ToTable("RefreshTokens");

            rt.WithOwner().HasForeignKey("UserId");


            #region Problom that I faced

            //rt.HasKey("Id"); // Shadow property as primary key and this is int not Guid

            rt.Property<Guid>("Id");
            rt.HasKey("Id");

            #endregion

            rt.Property(x => x.Token)
              .IsRequired();

            rt.HasIndex(x => x.Token)
              .IsUnique();

            rt.Property(x => x.ExpiresOn)
              .IsRequired();
        });


    }
}


#region Explanation for Email Value Object Configuration


/*

 Email is a value object that encapsulates the email value,
 ensuring validation and immutability.
 in the User entity, the Email property is of type Email, not a simple string.
 

EFCore only know how to persist primitive types (like string, int, etc.) directly.


So EF asks:

How do I save this Email object into a database column?

To solve this, we use a Value Converter in EF Core.
email => email.Value
email is of type Email (Value Object)

email.Value is a string

this means:

When EF saves a User, store Email.Value (string) in the database.

Email.Create("Omer@gmail.com")

Stored in DB as:

"Omer@gmail.com"
 */


#endregion