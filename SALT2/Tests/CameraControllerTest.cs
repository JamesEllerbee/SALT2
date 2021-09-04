using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT2.Tests
{
    /// <summary>
    /// Units tests the <see cref="CameraController"/>.
    /// </summary>
    [Title("Camera Controller should...")]
    public class CameraControllerTest : WAT.Test
    {
        /// <summary>
        /// Verifies that the <see cref="CameraController"/> updates the x value of the translation.
        /// </summary>
        [Test]
        public void UpdateXPostion()
        {
            // Given the default player x pos
            var defaultPlayerXPos = 100f;

            // Given the camera offset
            var cameraXOffset = 3f;

            // Given a camera controller
            var cameraController = new CameraController();

            // When process
            cameraController._Process(0);

            // Then the camera's x pos is reports as 100
            Assert.IsEqual(defaultPlayerXPos + cameraXOffset, cameraController.Translation.x);
        }
    }
}
