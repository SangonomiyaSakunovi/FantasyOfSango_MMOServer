//Developer : SangonomiyaSakunovi
//Discription:

using FantasyOfSango.Services;

namespace FantasyOfSango.Bases
{
    public class BaseSystem
    {
        protected ResourceService resourceService = null;

        public virtual void InitSystem()
        {
            resourceService = ResourceService.Instance;
        }
    }
}
