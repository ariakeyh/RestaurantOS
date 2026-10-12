using System.Security.Cryptography;
using System.Text;

namespace Common.Utils;

public static class PasswordEncryptor
{
    public static string EncryptPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();

        foreach (var byteValue in bytes)
        {
            builder.Append(byteValue.ToString("x2"));
        }

        return builder.ToString();
    }

    public static bool VerifyPassword(string plainPassword, string encryptedPassword)
    {
        var hashedPassword = EncryptPassword(plainPassword);
        return hashedPassword == encryptedPassword;
    }
}