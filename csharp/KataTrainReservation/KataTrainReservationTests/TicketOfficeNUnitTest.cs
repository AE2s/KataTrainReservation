using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace KataTrainReservation
{
    [TestFixture]
    public class TicketOfficeNUnitTest {
    
        [Test]
        public void Should_return_reservation_valid_when_ticket_office_make_reservation_request_for_one_seat() {

            string trainId="train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetSeats(trainId).Returns(new List<Seat>() { new Seat("01",1) });
            var reservationRequest = new ReservationRequest(trainId, 1);

            var makeReservation = new TicketOffice(seatService).MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }
    }
}
