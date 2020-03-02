using NUnit.Framework;

namespace KataTrainReservation
{
    [TestFixture]
    public class TicketOfficeNUnitTest {
    
        [Test]
        public void Should_return_reservation_valid_when_ticket_office_make_reservation_request_for_one_seat() {

            var reservationRequest = new ReservationRequest("train1", 1);
            var makeReservation = new TicketOffice().MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }
    }
}
