using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helper
{
    public static class Base64Converter
    {
        public static IFormFile Base64ToIFormFile(string base64String, string fileName = "file")
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(base64String))
            {
                throw new ArgumentException("String base64 não pode ser vazia", nameof(base64String));
            }

            // Separa o cabeçalho MIME dos dados
            var parts = base64String.Split(new[] { ";base64," }, StringSplitOptions.RemoveEmptyEntries);

            // Determina o content type
            var contentType = parts.Length > 1
                ? parts[0].Replace("data:", "")
                : "application/octet-stream";

            // Pega os dados reais
            var cleanBase64 = parts.Length > 1 ? parts[1] : parts[0];

            // Converte para bytes
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(cleanBase64);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Formato base64 inválido", nameof(base64String), ex);
            }

            // Cria o stream e o FormFile
            var stream = new MemoryStream(bytes);

            return new FormFile(
                baseStream: stream,
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "file",
                fileName: SanitizeFileName(fileName, contentType))
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

        private static string SanitizeFileName(string fileName, string contentType)
        {
            var extension = contentType.Split('/').Last();
            var name = Path.GetFileNameWithoutExtension(fileName);

            // Remove caracteres inválidos
            var invalidChars = Path.GetInvalidFileNameChars();
            var cleanName = new string(name
                .Where(c => !invalidChars.Contains(c))
                .ToArray());

            return $"{cleanName}.{extension}";
        }
    }
}
