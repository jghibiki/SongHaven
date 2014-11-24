using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NUnit.Framework;
using SongHaven.Controllers;
using System.Net;

namespace SongHaven.Tests.Controllers
{
    [TestFixture]
    public class CommandsControllerTest
    {
        [Test]
        public void Test()
        {
            // I do nothing YAY!
        }

        [Test]
        public void Index()
        {
            // Arrange
            CommandsController controller = new CommandsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [Test]
        public void DetailsBadRequest()
        {
            // Arrange
            CommandsController controller = new CommandsController();
          
            // Act
            ViewResult result = controller.Details(null) as ViewResult;  // look for null, should return BR

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, HttpStatusCode.BadRequest.GetType());


        }

        [Test]
        public void DetailsNotFound()
        {
            // Arrange
            CommandsController controller = new CommandsController();

            // Act
            ViewResult result = controller.Details(180) as ViewResult;  // try to retrieve a command that isn't there

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, HttpStatusCode.NotFound.GetType());

        }

        [Test]
        public void DetailsCommand()
        {
            // Arrange
            CommandsController controller = new CommandsController();
            Command cmd = new Command();
            cmd.int_id = 180;
            cmd.int_command = 4;
            controller.Create(cmd);    //create a test command

            // Act
            ViewResult result = controller.Details(180) as ViewResult;  // retrieve the command we created

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, new Command().GetType());

            controller.Delete(180); // cleanup the test command
        }
    }
}
