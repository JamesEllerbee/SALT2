using Godot;

namespace SALT2.Tests
{
    /// <summary>
    /// Unit tests the <see cref="PlayerController"/>.
    /// </summary>
    [Title("Player Controller")]
    public class PlayerControllerTest : WAT.Test
    {
        /// <summary>
        /// Verifies that the <see cref="PlayerController"/> can be created.
        /// </summary>
        [Test]
        public void TestPlayerControllerCreation()
        {
            // Given player controller
            var playerController = new PlayerController();

            // When player controller does...

            // Then player controller behaves.
            Assert.IsNotNull(playerController);
        }

        /// <summary>
        /// Verifies that the Flip() method in <see cref="PlayerController"/> works correctly.
        /// </summary>
        public void TestFacingRight()
        {
            // Given player controller
            var playerController = new PlayerController();

            // Given "move_right" action
            Input.ActionPress("move_right");

            // When controller process...
            playerController._Process(1);

            // Then the player is facing right.
            Assert.IsTrue(playerController.GetFacingRight);
        }
    }
}
