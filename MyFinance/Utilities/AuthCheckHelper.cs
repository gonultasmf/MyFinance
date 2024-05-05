using System.Text;

namespace MyFinance.Utilities;

public static class AuthCheckHelper
{
    public static string BasicAuth(string username, string password, DateTime expireTime)
    {
        string credentials = $"{username}:{password}:{expireTime.ToShortDateString()}";
        byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
        string base64Credebtials = Convert.ToBase64String(credentialsBytes);

        return base64Credebtials;
    }

    public static (string?, string?, DateTime?) ParseBasicAuthToken(string authToken)
    {
        if (string.IsNullOrEmpty(authToken))
            return (null, null, null);

        byte[] bytes = Convert.FromBase64String(authToken);
        string credentials = Encoding.UTF8.GetString(bytes);
        var userInfo = credentials.Split(':', StringSplitOptions.TrimEntries);

        return (userInfo[0], userInfo[1], Convert.ToDateTime(userInfo[2]));
    }
}

