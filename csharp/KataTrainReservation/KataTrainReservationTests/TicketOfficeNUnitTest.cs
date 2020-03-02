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
            seatService.GetAvailableSeats(trainId).Returns(new List<Seat>() { new Seat("01",1), new Seat("01", 2) });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 1);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }

        [Test]
        public void Should_reserve_seats_when_train_is_empty()
        {
            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(
                new List<Seat>() { new Seat("01", 1), new Seat("01", 2)
                , new Seat("01", 3), new Seat("01", 4), new Seat("01", 5), new Seat("01", 6)});
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 3);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }

        [Test]
        public void Should_not_reserve_when_seat_count_of_reservation_is_greather_than_available_seats()
        {
            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(
                new List<Seat>() { new Seat("01", 1), new Seat("01", 2)});
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 5);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);
            
            Assert.IsNull(makeReservation);
        }

        [Test]
        public void Should_return_reservation_of_train_if_seat_count_under_70_percent_seat_train_count()
        {

            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(
                 new List<Seat>() { new Seat("01", 1), new Seat("01", 2)
                , new Seat("01", 3), new Seat("01", 4), new Seat("01", 5), new Seat("01", 6)
                , new Seat("01", 7), new Seat("01", 8), new Seat("01", 9), new Seat("01", 10)
                 });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 7);

            var makeReservation = new TicketOffice(seatService,bookingService).MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }
    }
}
