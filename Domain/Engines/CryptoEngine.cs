namespace Domain.Engines; 

public class CryptoEngine {
    public static string Encrypt(string password) {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes("qwertzuiopüasdfghjklöäyxcvbnm");
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream()) {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (var streamWriter = new StreamWriter(cryptoStream)) {
                        streamWriter.Write(password);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }
}