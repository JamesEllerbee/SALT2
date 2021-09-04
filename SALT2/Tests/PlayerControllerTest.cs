using Godot;

namespace SALT2.Tests
{
    /// <summary>
    /// Unit tests the <see cref="PlayerController"/>.
    /// </summary>
    [Title("A Player Controller should...")]
    public class PlayerControllerTest : WAT.Test
    {
        /// <summary>
        /// Verifies that the <see cref="PlayerController"/> can be created.
        /// </summary>
        [Test]
        public void InstantiateCorrectly()
        {
            // When player controller instantiated
            var playerController = new PlayerController();

            // Then player controller is not null.
            Assert.IsNotNull(playerController);
        }

        /// <summary>
        /// Verifies that the Flip() method in <see cref="PlayerController"/> works correctly.
        /// </summary>
        public void ReportFacingRight()
        {
            // Given player controller
            var playerController = new PlayerController();

            // Given "move_right" action
            Input.ActionPress("move_right");

            // When player controller processes
            playerController._Process(1);

            // Then the player is facing right.
            Assert.IsTrue(playerController.GetFacingRight);
        }
    }
}
