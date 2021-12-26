using Discord;
using SirBottington.Models;
using SirBottington.Services.API;
using SirBottington.Services.DataAccess;
using System.Text.RegularExpressions;

namespace SirBottington.Utilities
{
    public class XKCDUtil
    {
        private readonly EmbedBuilder _embedBuilder;
        private readonly GetXKCDAPI _api;
        private readonly XKCDDataAccess _db;

        public XKCDUtil(EmbedBuilder embedBuilder, GetXKCDAPI api, XKCDDataAccess db)
        {
            _embedBuilder = embedBuilder;
            _api = api;
            _db = db;
        }
        public Embed XKCDEmbedBuilder(XKCDModel comic)
        {
            _embedBuilder.WithTitle(comic.title)
                      .WithDescription(comic.link)
                      .WithFooter($"Alt: {comic.alt}")
                      .WithImageUrl(comic.img)
                      .WithColor(new Color(151, 168, 201));
            return _embedBuilder.Build();
        }

        public Embed XKCDSearchEmbedBuilder(XKCDModel comic, string arg, int score)
        {
            _embedBuilder.WithTitle(comic.title)
                      .WithDescription($"Comic matching the search: {arg}\nIt got a score of: {score}")
                      .WithFooter($"Alt: {comic.alt}")
                      .WithImageUrl(comic.img)
                      .WithColor(new Color(151, 168, 201));
            return _embedBuilder.Build();
        }
    
        public async Task Update()
        {
            // Hämta den senaste så vi har något att loopa upp till
            var latestComic = await _api.GetLatest();
            Console.WriteLine("Starting the update");

            for (int i = 1; i < latestComic.num; i++)
            {
                // Skippa 404
                if (i == 404)
                    continue;

                var comic = await _api.GetSpecific(i);
                if (comic is not null)
                {
                     // Hämta och stoppa in i DB
                    await _db.UpdateComic(comic);
                }
                Console.WriteLine("Done with comic: " + i);
            }
            Console.WriteLine("Done with update");
        }
        
        public async Task<Embed> Search(string arg)
        {
            // have this search been done before?
            var searchHistory = await _db.GetHistory();
            if (searchHistory.ContainsKey(arg))
            {
                return XKCDEmbedBuilder(searchHistory[arg]);
            }

            var allComics = await _db.GetAllComics();

            Regex rgxPattern = new Regex(arg, RegexOptions.IgnoreCase);
            XKCDModel result = new();
            int maxScore = 0;
            foreach (var comic in allComics)
            {
                int currentScore = 0;
                if(comic.title.Contains(arg, StringComparison.OrdinalIgnoreCase))
                {
                    currentScore += 3;
                }
                if(comic.alt.Contains(arg, StringComparison.OrdinalIgnoreCase))
                {
                    currentScore += 1;
                }
                foreach(Match match in rgxPattern.Matches(comic.transcript))
                {
                    currentScore += 2;
                }


                if(currentScore > maxScore)
                {
                    maxScore = currentScore;
                    result = comic;
                }
            }

            if(maxScore > 0)
            {
                await _db.InsertComicHistory(new XKCDSearchModel { SearchTerm = arg, Comic = result });
                return XKCDSearchEmbedBuilder(result, arg, maxScore);
            }
            return null;
        }
    }
}
