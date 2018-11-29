using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneProject;
using CapstoneProject.Controllers;
using CapstoneProject.Models;
using System.Web;
using Moq;
using System.Web.Routing;
using CapstoneProject.UnitTests.Support;

namespace CapstoneProject.Tests.Controllers
{
	[TestClass]
	public class WorkSpaceTest
	{
		/// <summary>
		/// Purpose of TC: 
		/// - Validate whether with valid input data, a workspace is created and saved into database, 
		///     and then the user is redirected to the Index action
		/// </summary>
		[TestMethod]
		public void CreateWorkSpace()
		{
			//Setup a fake HttpRequest
			Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
			Mock<HttpRequestBase> moqRequest = new Mock<HttpRequestBase>();
			Mock<HttpPostedFileBase> moqPostedFile = new Mock<HttpPostedFileBase>();

			moqRequest.Setup(r => r.Files.Count).Returns(0);
			moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

			//arrange 
			var controller = new GroupController();
			var workspace = new WorkSpace()
			{
				WorkSpaceName = "WorkSpace TestCorrect",
				Bilimail = "vobichto1997@gmail.com",
				Description = "Demo test is correct",
				ImageWS = "name.jpg"
			};

			controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller);
			var validationResults = TestModelHelper.ValidateModel(controller, workspace);

			//act
			var redirectRoute = controller.CreateGroup(workspace) as RedirectToRouteResult;

			//assert
			Assert.IsNotNull(redirectRoute);
			Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
			//Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
			Assert.AreEqual(0, validationResults.Count);
		}

		/// <summary>
		/// Purpose of TC:
		/// - Validate whether with invalid input data (required field 'Title' is null), 
		///     the WorkSpace cannot be created and an error message should be shown.
		/// </summary>
		[TestMethod]
		public void CreateWorkSpace_WithWorkSpaceNameIsNull_ExpectValidationError()
		{
			Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
			Mock<HttpRequestBase> moqRequest = new Mock<HttpRequestBase>();
			Mock<HttpPostedFileBase> moqPostedFile = new Mock<HttpPostedFileBase>();

			moqRequest.Setup(r => r.Files.Count).Returns(0);
			moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

			//arrange
			var controller = new HomeController();
			var workspace = new WorkSpace()
			{
				WorkSpaceName = "",
				Bilimail = "vobichto1997@gmail.com",
				Description = "Demo test is correct",
				ImageWS = "name.jpg"
			};

			controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller);
			var validationResults = TestModelHelper.ValidateModel(controller, workspace);

			//act
			var viewResult = controller.CreateWorkSpace(workspace) as ViewResult;

			//Assert
			Assert.IsNull(viewResult);
			Assert.IsTrue(viewResult.ViewData.ModelState.IsValid);
			Assert.AreEqual(1, validationResults.Count);
			Assert.IsTrue(validationResults[0].ErrorMessage.Equals("The WorkSpaceName field is required."));
		}
		[TestMethod]
		public void AddMemberWS(int id)
		{
			//Setup a fake HttpRequest
			Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
			Mock<HttpRequestBase> moqRequest = new Mock<HttpRequestBase>();
			Mock<HttpPostedFileBase> moqPostedFile = new Mock<HttpPostedFileBase>();

			moqRequest.Setup(r => r.Files.Count).Returns(0);
			moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

			//arrange 
			var controller = new HomeController();
			var ws_user_roles = new WS_User_Roles()
			{
				WorkSpace_ID = 1,
				Role_Admin = true,
				User_ID = "",
			};

			controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller);
			var validationResults = TestModelHelper.ValidateModel(controller, ws_user_roles);

			//act
			var redirectRoute = controller.AddMemberWS(id) as RedirectToRouteResult;

			//assert
			Assert.IsNotNull(redirectRoute);
			Assert.AreEqual("ShowWorkSpace", redirectRoute.RouteValues["action"]);
			//Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
			Assert.AreEqual(0, validationResults.Count);
		}
		[TestMethod]
		public void DeleteWorkSpace(WorkSpace ws)
		{
			//Setup a fake HttpRequest
			Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
			Mock<HttpRequestBase> moqRequest = new Mock<HttpRequestBase>();
			Mock<HttpPostedFileBase> moqPostedFile = new Mock<HttpPostedFileBase>();

			moqRequest.Setup(r => r.Files.Count).Returns(0);
			moqContext.Setup(x => x.Request).Returns(moqRequest.Object);

			//arrange 
			var controller = new HomeController();
            PMSEntities db = new PMSEntities();
			ws.ID = 1;
			var work_id = ws.ID;
			WorkSpace workspace = db.WorkSpaces.Find(work_id);
			db.WorkSpaces.Remove(workspace);

			controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), controller);
			var validationResults = TestModelHelper.ValidateModel(controller, workspace);

			//act
			var redirectRoute = controller.Index() as RedirectToRouteResult;

			//assert
			Assert.IsNotNull(redirectRoute);
			Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
			//Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
			Assert.AreEqual(0, validationResults.Count);
		}
	}
}
