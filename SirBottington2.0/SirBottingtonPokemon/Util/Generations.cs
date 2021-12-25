
namespace SirBottingtonPokemon.Util
{
    public static class Generations
    {
        public static int GetGeneration(int id)
        {
            if (id > 0 && id <= 151) return 1;
            else if (id > 151 && id <= 251) return 2;
            else if (id > 251 && id <= 386) return 3;
            else if (id > 386 && id <= 493) return 4;
            else if (id > 493 && id <= 649) return 5;
            else if (id > 649 && id <= 721) return 6;
            else if (id > 721 && id <= 809) return 7;
            else return 8;
        }
    }
}
