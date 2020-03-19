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
            seatService.GetAvailableSeats(trainId).Returns(new List<Seat>() { new Seat(new Coach("01"),1), new Seat(new Coach("01"), 2) });
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
                new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2)
                , new Seat(new Coach("01"), 3), new Seat(new Coach("01"), 4), new Seat(new Coach("01"), 5), new Seat(new Coach("01"), 6)});
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
                new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2)});
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
                 new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2)
                , new Seat(new Coach("01"), 3), new Seat(new Coach("01"), 4), new Seat(new Coach("01"), 5), new Seat(new Coach("01"), 6)
                , new Seat(new Coach("01"), 7), new Seat(new Coach("01"), 8), new Seat(new Coach("01"), 9), new Seat(new Coach("01"), 10)
                 });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 7);

            var makeReservation = new TicketOffice(seatService,bookingService).MakeReservation(reservationRequest);

            Assert.AreEqual(reservationRequest.TrainId, makeReservation.TrainId);
            Assert.AreEqual(makeReservation.Seats.Count, reservationRequest.SeatCount);
        }

        [Test]
        public void Should_not_return_reservation_of_train_if_seat_count_greater_than_70_percent_seat_train_count()
        {

            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(
                 new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2)
                , new Seat(new Coach("01"), 3), new Seat(new Coach("01"), 4), new Seat(new Coach("01"), 5), new Seat(new Coach("01"), 6)
                , new Seat(new Coach("01"), 7), new Seat(new Coach("01"), 8), new Seat(new Coach("01"), 9), new Seat(new Coach("01"), 10)
                 });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 8);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Assert.IsNull(makeReservation);
        }

        [Test]
        public void Should_reserve_twice_when_seats_is_available()
        {
            string trainId = "train1";
            ISeatService seatService = new SeatService(new TrainSeatsMock());           
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var firstReservationRequest = new ReservationRequest(trainId, 3);
            var secondReservationRequest = new ReservationRequest(trainId, 4);

            var makeFirstReservation = new TicketOffice(seatService, bookingService).MakeReservation(firstReservationRequest);

            var makeSecondReservation = new TicketOffice(seatService, bookingService).MakeReservation(secondReservationRequest);

            Assert.AreEqual(3, seatService.GetAvailableSeats(trainId).Count);
        }

        [Test]
        public void Should_generate_different_booking_id_when_two_reservation_was_confirmed()
        {
            string trainId = "train1";
            ISeatService seatService = new SeatService(new TrainSeatsMock());
            IBookingService bookingService = new BookingService();           
            var firstReservationRequest = new ReservationRequest(trainId, 3);
            var secondReservationRequest = new ReservationRequest(trainId, 4);

            var makeFirstReservation = new TicketOffice(seatService, bookingService).MakeReservation(firstReservationRequest);
            var makeSecondReservation = new TicketOffice(seatService, bookingService).MakeReservation(secondReservationRequest);

           
            Assert.AreNotEqual(makeFirstReservation.BookingId, makeSecondReservation.BookingId);
        }

        [Test]
        public void Should_make_reservation_twice_even_if_coach_over_70()
        {
            string trainId = "train2";
            ISeatService seatService = new SeatService(new TrainSeatsMock());
            IBookingService bookingService = new BookingService();           
            var firstReservationRequest = new ReservationRequest(trainId, 6);
            var secondReservationRequest = new ReservationRequest(trainId, 4);

            var makeFirstReservation = new TicketOffice(seatService, bookingService).MakeReservation(firstReservationRequest);

            var makeSecondReservation = new TicketOffice(seatService, bookingService).MakeReservation(secondReservationRequest);

            Assert.AreEqual(10, seatService.GetAvailableSeats(trainId).Count);
        }
    }
}



