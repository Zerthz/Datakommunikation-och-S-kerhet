namespace Server.State
{
    public interface IAppState
    {
        string Get(string key);
        void Store(string key, string value);
    }
}
