using PokeApiNet;

namespace SirBottingtonPokemon.Util
{
    public class Evolutions
    {
        public static async Task<string> GetEvolutions(PokemonSpecies species, PokeApiClient client)
        {
            
            var evos = await client.GetResourceAsync(species.EvolutionChain);
            var list = Evos(evos.Chain, new List<string>());
            string output = "";
            foreach (var item in list)
            {
                output += $"{ char.ToUpper(item[0]) + item.Substring(1)}, ";
            }
            output = output.TrimEnd(',', ' ');
            return output;
           
        }

        private static List<string> Evos(ChainLink chain, List<string> output)
        {            
            output.Add(chain.Species.Name);
            for (int i = 0; i < chain.EvolvesTo.Count; i++)
            {
                if (chain.EvolvesTo.Count > 0)
                    Evos(chain.EvolvesTo[i], output);
            }

            return output;
        }
    }
}
