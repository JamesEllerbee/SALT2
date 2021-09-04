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
        /// Verifies that after the elapsed period, the current direction is repoted
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
            var tokenSource = new CancellationTokenSource();
            var processTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    frogController._Process(0.1F);
                    GD.Print("Process!");
                    Thread.Sleep(100);
                }
            }, tokenSource.Token);


            // When the time
            Thread.Sleep(timeout);

            // Clean up process task
            tokenSource.Cancel();

            // Then verify controller is reporting the correct direction
            Assert.IsEqual(-1, frogController.CurrentFacingDirection, "Frog controller did not report the expected facing direction.");
        }
    }
}
