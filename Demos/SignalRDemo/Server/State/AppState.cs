using System.Collections.Concurrent;

namespace Server.State
{
    public class AppState : IAppState
    {
        // Den här låser i bakgrunden, så att man inte skriver över ngt, den använder
        // lock
        private readonly ConcurrentDictionary<string, string> _data = new();
       
        public string Get(string key)
        {
            return _data.GetValueOrDefault(key) ?? string.Empty;
        }

        public void Store(string key, string value)
        {
            // en func där vi inte bryr oss om variablar så vi sätter de som discard "_"
            // Vad detta gör är att vi skapar eller uppdaterar. funcen kollar om det finns ett värde sparat 
            // och vad vi gör med det är vad vi sätter efter => 
            // vi säger att vi ska ta det nya value

            _data.AddOrUpdate(key, value, (_, _) => value);
        }
    }
}
