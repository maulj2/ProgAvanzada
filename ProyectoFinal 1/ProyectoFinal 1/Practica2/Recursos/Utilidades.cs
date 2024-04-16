using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoFinal.Recursos
{
    public class Utilidades
    {
        public static string EncriptarClave(string clave)
        {

            StringBuilder sb = new StringBuilder();
            Encoding enc = Encoding.UTF8;

            byte[] result = SHA256.HashData(enc.GetBytes(clave));

            foreach (byte b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();

        }
    }
}
