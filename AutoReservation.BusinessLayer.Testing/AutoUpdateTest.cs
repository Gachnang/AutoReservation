using AutoReservation.TestEnvironment;
using AutoReservation.Dal.Entities;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            Auto auto = Target.GetAuto(1);
            string neueMarke = "ACDC";
            auto.Marke = neueMarke;
            Target.UpdateAuto(auto);
            Assert.Equal(neueMarke, Target.GetAuto(1).Marke);
        }
    }
}
