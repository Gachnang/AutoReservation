using AutoReservation.TestEnvironment;
using AutoReservation.Dal.Entities;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.GetReservation(1);
            int alteId = reservation.KundeId;
            int neueId = 1;
            if (alteId < 2)
            {
                neueId = alteId++;
            }
            else
            {
                neueId = alteId--;
            }
            reservation.KundeId = neueId;
            Target.UpdateReservation(reservation);
            Assert.Equal(neueId, Target.GetReservation(1).KundeId);
        }
    }
}
