using System.Text.Json;

namespace FantasyOfSango.Bases
{
    public abstract class BaseThreads
    {
        public abstract void Run();
        public abstract void Update();
        public abstract void Stop();

        protected virtual string SetJsonString(object ob)
        {
            string jsonString = JsonSerializer.Serialize(ob);
            return jsonString;
        }

        protected T_obj DeJsonString<T_obj>(string str)
        {
            T_obj obj = JsonSerializer.Deserialize<T_obj>(str);
            return obj;
        }
    }
}
