using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace GigHub.IntegrationTests.Extensions
{
	public static class ControllerExtensions
	{
		public static void MockCurrentUser(this Controller controller, string userId, string userName)
		{
			var identity = new GenericIdentity(userName);
			identity.AddClaim(new Claim(ClaimTypes.Name, userName));
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));

			var principal = new GenericPrincipal(identity, null);

			//these 5 lines are equivalent to code below
			//var mockHttpContext = new Mock<HttpContextBase>();
			//mockHttpContext.SetupGet(c => c.User).Returns(principal);

			//var mockControllerContext = new Mock<ControllerContext>();
			//mockControllerContext.SetupGet(c => c.HttpContext).Returns(mockHttpContext.Object);

			//controller.ControllerContext = mockControllerContext.Object;

			controller.ControllerContext = Mock.Of<ControllerContext>(ctr =>
				ctr.HttpContext == Mock.Of<HttpContextBase>(http => 
				http.User == principal));
		}
	}
}
