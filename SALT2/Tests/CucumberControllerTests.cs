using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT2.Tests
{
    /// <summary>
    /// Unit tests the <see cref="CucumberController"/>.
    /// </summary>
    [Title("Cucumber Controller should...")]
    public class CucumberControllerTests : WAT.Test
    {
        /// <summary>
        /// Verfies that the CucumberController reacts as expected when the controller determines the player is touching it.
        /// </summary>
        [Test]
        public void BehaveWhenTouchedByPlayer()
        {
            // Given Cucumber controller.
            var cucumberController = new CucumberController();

            // When physics process
            cucumberController._Process(0.1f);

            // Then the cucumber behaves as expected
            Assert.IsFalse(cucumberController.IsActive);
            Assert.IsFalse(cucumberController.Visible);
        }
    }
}
