namespace QuestionBank.Utility;

public class CryptoHelper
{
    public static string GetPasswordHash(string password)
    {

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password,string storedPasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedPasswordHash);
    }


    
}