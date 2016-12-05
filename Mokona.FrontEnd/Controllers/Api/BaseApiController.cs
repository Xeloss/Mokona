namespace Mokona.FrontEnd.Controllers.Api
{
    using Mokona.FrontEnd.Utils;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Core.Security;

    public abstract class BaseApiController : ApiController
    {
        public UserPrincipal UserPrincipal
        {
            get { return (UserPrincipal)this.User; }
        }
    }
}
