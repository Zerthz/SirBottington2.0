using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using PokeApiNet;
namespace SirBottingtonPokemon.Util
{
    public class PokemonEmbedBuilder
    {
        public static Embed GetEmbed(Pokemon pokemon, PokemonSpecies species, string evolutions)
        {
            var flavortext = species.FlavorTextEntries.FirstOrDefault(x => x.Language.Name == "en").FlavorText;

            var evolutionField = new EmbedFieldBuilder()
                .WithName("Family")
                .WithValue(evolutions)
                .WithIsInline(true);

            var typesField = new EmbedFieldBuilder()
                .WithName("Type")
                .WithValue(GetTypes(pokemon))
                .WithIsInline(true);

            var generationField = new EmbedFieldBuilder()
                .WithName("Their First Gen")
                .WithValue("Generation " + Generations.GetGeneration(pokemon.Id))
                .WithIsInline(true);

            var heightField = new EmbedFieldBuilder()
                .WithName("Height")
                .WithValue($"{pokemon.Height * 10}cm")
                .WithIsInline(true);

            var weightField = new EmbedFieldBuilder()
                .WithName("Weight")
                .WithValue($"{pokemon.Weight / 10.0}kg")
                .WithIsInline(true);

            var legendaryField = new EmbedFieldBuilder()
                .WithName("Is Legendary")
                .WithValue(species.IsLegendary ? "Yes" : "No")
                .WithIsInline(true);

            var flavorField = new EmbedFieldBuilder()
                .WithName("Flavor Text")
                .WithValue(flavortext)
                .WithIsInline(false);



            var embedPokemon = new EmbedBuilder()
                .AddField(evolutionField)
                .AddField(typesField)
                .AddField(generationField)
                .AddField(heightField)
                .AddField(weightField)
                .AddField(legendaryField)
                .AddField(flavorField)
                .WithThumbnailUrl(pokemon.Sprites.FrontDefault);
            return embedPokemon.Build();
        }

        private static string GetTypes(Pokemon pokemon)
        {
            string types = "";

            foreach (var item in pokemon.Types)
            {
                types += $"{char.ToUpper(item.Type.Name[0]) + item.Type.Name.Substring(1)}, ";
                
            }
            types = types.TrimEnd(',', ' ');

            return types;
        }
    }
}
