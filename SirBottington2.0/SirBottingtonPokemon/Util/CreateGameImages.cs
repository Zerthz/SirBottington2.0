using Microsoft.Extensions.Configuration;
using SkiaSharp;


namespace SirBottingtonPokemon.Util
{
    public class CreateGameImages : ICreateGameImages
    {
        IConfiguration _configuration;
        public CreateGameImages(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Create(int randomPokemon)
        {
            string colorPath = _configuration.GetSection("PokemonPaths")["PROD_Pokemon"];
            colorPath += $"/{randomPokemon}.png";

            string templatePath = _configuration.GetSection("PokemonPaths")["PROD_Template"];

            string blackPath = _configuration.GetSection("PokemonPaths")["PROD_BW"];
            blackPath += $"{randomPokemon}.png";

            if (File.Exists(blackPath) == false)
            {
                CreateBlackImage(colorPath, blackPath);
            }

            var templateImage = SKImage.FromBitmap(SKBitmap.Decode(templatePath));
            var blackImage = SKImage.FromBitmap(SKBitmap.Decode(blackPath));
            var colorImage = SKImage.FromBitmap(SKBitmap.Decode(colorPath));
            DrawGuessImage(randomPokemon, templateImage, blackImage, Path.Combine(_configuration.GetSection("PokemonPaths")["PROD_BW"], randomPokemon + "black.png"));
            DrawGuessImage(randomPokemon, templateImage, colorImage, Path.Combine(_configuration.GetSection("PokemonPaths")["PROD_BW"], randomPokemon + "answer.png"));
        }

        private void DrawGuessImage(int randomPokemon, SKImage templateImage, SKImage drawImage, string path)
        {

            using (SKBitmap bmp = new SKBitmap(1152, 648))
            {
                using (SKSurface surface = SKSurface.Create(new SKImageInfo(1152, 648)))
                {
                    SKCanvas canvas = surface.Canvas;

                    canvas.DrawImage(templateImage,
                                        new SKRect(0, 0, 1152, 648),
                                        new SKRect(0, 0, templateImage.Width, templateImage.Height),
                                        null);
                    canvas.DrawImage(drawImage, (bmp.Width / 5) - (bmp.Height / 4), (bmp.Height / 2) - (bmp.Width / 5));

                    using (var image = surface.Snapshot())
                    using (var data = image.Encode())
                    using (var stream = File.OpenWrite(path))
                    {
                        data.SaveTo(stream);
                    }
                }
            }

        }

        private void CreateBlackImage(string colorPath, string blackPath)
        {
            Console.WriteLine(colorPath);
            Console.WriteLine(blackPath);
            SKBitmap orginalBmp = SKBitmap.Decode(colorPath);
            SKBitmap blackBmp = new SKBitmap(orginalBmp.Width, orginalBmp.Height);

            for (int x = 0; x < orginalBmp.Width; x++)
            {
                for (int y = 0; y < orginalBmp.Height; y++)
                {
                    var c = orginalBmp.GetPixel(x, y);
                    var v = new SKColor(0, 0, 0, c.Alpha);
                    blackBmp.SetPixel(x, y, v);
                }
            }
            try
            {
                var blackImage = SKImage.FromBitmap(blackBmp);
                using (var data = blackImage.Encode())
                {
                    using (var stream = File.OpenWrite(blackPath))
                    {
                        data.SaveTo(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
