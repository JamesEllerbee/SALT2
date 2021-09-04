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
    }
}
