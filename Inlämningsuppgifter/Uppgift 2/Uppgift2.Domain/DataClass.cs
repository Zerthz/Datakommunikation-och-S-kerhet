using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uppgift2.Domain
{
    // Jag hade ingen större lust att skapa upp exakt samma klass på båda sidor av connectionen, så det här projektet & den här klassen
    // är endast här för att göra saker lite simplare för en uppgift. 
    public class DataClass
    {
        public int NoTimesRead { get; set; } = 1;
        public string InitialMessage { get; init; } = string.Empty;
        public string? ResponseMessage { get; set; }
    }
}
