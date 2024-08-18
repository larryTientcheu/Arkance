namespace Arkance.Interface
{
    public class Helpers
    {
        public string SetAppreciations(double? valeur)
        {
            switch (valeur)
            {
                case >= 0 and <= 10:
                    return "Peut faire mieux";
                    
                case >= 11 and <= 16:
                    return "Bien";

                case > 16 and <= 20:
                    return "Très Bien";

                default:
                    return "Pas de note disponible";
            }
        }
    }
}
