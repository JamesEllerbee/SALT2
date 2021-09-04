using System.Threading;
using System.Threading.Tasks;
using Godot;
using Thread = System.Threading.Thread;

namespace SALT2.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="BasicGroundFrogControllerTest"/> class.
    /// </summary>
    [Title("A Basic Ground Frog Controller should...")]
    public class BasicGroundFrogControllerTest : WAT.Test
    {
        /// <summary>
        /// Verifies that after the elapsed period, the current direction is repoted.
        /// </summary>
        [Test]
        public void ChangeDirectionProperly()
        {
            // Given an amount of time to wait
            var timeout = 4000;

            // Given a basic ground frog controller
            var frogController = new BasicGroundFrogController();

            // Given controller ready
            frogController._Ready();

            // Given thread to call _Process
            var processTask = Task.Factory.StartNew(() =>
            {
                int count = 40;
                while (count >= 0)
                {
                    frogController._Process(0.1F);
                    Thread.Sleep(100);
                    count--;
                }
            });

            // When the time period for the entity to turn around passes
            Thread.Sleep(timeout);

            // Then verify controller is reporting the correct direction
            Assert.IsEqual(-1, frogController.CurrentFacingDirection, "Frog controller did not report the expected facing direction.");
        }
    }
}
