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
    }
}
