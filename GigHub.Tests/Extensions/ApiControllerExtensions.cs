using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GigHub.Tests.Extensions
{
	public static class ApiControllerExtensions
	{
		public static void MockCurrentUser(this ApiController controller, string userId, string userName)
		{
			var identity = new GenericIdentity(userName);
			identity.AddClaim(new Claim(ClaimTypes.Name, userName));
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));

			var principal = new GenericPrincipal(identity, null);

			controller.User = principal;
		}
	}
}
