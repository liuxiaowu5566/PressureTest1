using DemoService.Services.Data;
using System.Diagnostics;
using Xunit;

namespace DemoTest.DataAdd
{
    public class SpeedTest
    {
        [Fact]
        public void SelectTest()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            PreparationService service = new PreparationService();
            var result = service.SelectMatch().Result;
            sp.Stop();
            var time = sp.Elapsed;
        }
    }
}
