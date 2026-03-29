using System;
using BCrypt.Net;
namespace BE_CRUDMascotas
{
    public class GeneradorHash
    {
        public static void Main()
        {
            string password = "admin123"; // la contraseña que quieras hashear
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            Console.WriteLine("Hash generado:");
            Console.WriteLine(hash);
        }

    }
}
