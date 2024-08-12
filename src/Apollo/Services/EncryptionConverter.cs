using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Apollo.Services
{
    public class EncryptionConverter : ValueConverter<string, string>
    {
        public EncryptionConverter(IDataProtector protector)
            : base(
                plainText => protector.Protect(plainText), // Encrypts data before storing it
                cipherText => protector.Unprotect(cipherText)) // Decrypts data when retrieving it
        {
        }
    }
}
