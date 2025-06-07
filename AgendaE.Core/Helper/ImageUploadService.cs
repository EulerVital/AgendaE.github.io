using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace AgendaE.Core.Helper
{
    public class ImageUploadService
    {
        private readonly Cloudinary _cloudinary;

        public ImageUploadService(IConfiguration configuration)
        {
            // Obtém as configurações das variáveis de ambiente ou do appsettings.json
            var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")
                ?? configuration["Cloudinary:CloudName"];

            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")
                ?? configuration["Cloudinary:ApiKey"];

            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")
                ?? configuration["Cloudinary:ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException("Configurações do Cloudinary não encontradas. " +
                    "Defina as variáveis de ambiente CLOUDINARY_CLOUD_NAME, CLOUDINARY_API_KEY e CLOUDINARY_API_SECRET");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }


        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            await using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream),
                PublicId = $"api_uploads/{Guid.NewGuid()}",
                Overwrite = true,
                Transformation = new Transformation()
                    .Quality("auto")
                    .FetchFormat("auto")
                    .Width(2000).Height(2000).Crop("limit") // Limita o tamanho máximo
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Erro no upload: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUrl.ToString();
        }

        public async Task<bool> DeleteImageAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("O Url da imagem não pode ser nulo ou vazio", nameof(url));
            }

            string publicId = ExtractPublicIdFromUrl(url);

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image,
                Invalidate = true // Invalida a imagem na CDN
            };

            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Error != null)
            {
                throw new Exception($"Erro ao excluir imagem: {deletionResult.Error.Message}");
            }

            return deletionResult.Result == "ok";
        }

        public string ExtractPublicIdFromUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentException("A URL da imagem não pode ser nula ou vazia", nameof(imageUrl));
            }

            try
            {
                var uri = new Uri(imageUrl);
                var segments = uri.Segments;

                // Encontra o índice do segmento "upload/"
                int uploadIndex = Array.FindIndex(segments, s => s.Equals("upload/", StringComparison.OrdinalIgnoreCase));

                if (uploadIndex < 0 || uploadIndex >= segments.Length - 1)
                {
                    throw new ArgumentException("URL do Cloudinary inválida - padrão não reconhecido");
                }

                // Pega todos os segmentos após "upload/"
                string publicIdWithVersion = string.Concat(segments[(uploadIndex + 1)..]);

                // Remove a extensão do arquivo se existir
                int lastDotIndex = publicIdWithVersion.LastIndexOf('.');
                string publicId = lastDotIndex > 0
                    ? publicIdWithVersion.Substring(0, lastDotIndex)
                    : publicIdWithVersion;

                // Remove a parte da versão (v\d+/)
                int versionSlashIndex = publicId.IndexOf('/');
                if (versionSlashIndex > 0)
                {
                    publicId = publicId.Substring(versionSlashIndex + 1);
                }

                return publicId;
            }
            catch (UriFormatException)
            {
                throw new ArgumentException("URL fornecida não é uma URL válida");
            }
        }

        // Versão alternativa recebendo Stream diretamente
        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                PublicId = $"api_uploads/{Guid.NewGuid()}",
                Overwrite = true,
                Transformation = new Transformation()
                    .Quality("auto")
                    .FetchFormat("auto")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}
