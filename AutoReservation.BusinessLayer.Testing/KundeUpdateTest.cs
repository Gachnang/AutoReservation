using AutoReservation.TestEnvironment;
using AutoReservation.Dal.Entities;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde kunde = Target.GetKunde(1);
            string neuerName = "Nachname";
            kunde.Nachname = neuerName;
            Target.UpdateKunde(kunde);
            Assert.Equal(neuerName, Target.GetKunde(1).Nachname);
        }
    }
}
