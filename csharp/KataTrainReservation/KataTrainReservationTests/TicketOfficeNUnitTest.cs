using NFluent;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace KataTrainReservation
{
    [TestFixture]
    public class TicketOfficeNUnitTest
    {

        [Test]
        public void Should_return_reservation_valid_when_ticket_office_make_reservation_request_for_one_seat()
        {

            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2) });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 1);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Check.That(reservationRequest.TrainId).IsEqualTo(makeReservation.TrainId);
            Check.That(makeReservation.Seats.Count).IsEqualTo(reservationRequest.SeatCount);
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

            Check.That(reservationRequest.TrainId).IsEqualTo(makeReservation.TrainId);
            Check.That(makeReservation.Seats.Count).IsEqualTo(reservationRequest.SeatCount);
        }

        [Test]
        public void Should_not_reserve_when_seat_count_of_reservation_is_greather_than_available_seats()
        {
            string trainId = "train1";
            ISeatService seatService = Substitute.For<ISeatService>();
            seatService.GetAvailableSeats(trainId).Returns(
                new List<Seat>() { new Seat(new Coach("01"), 1), new Seat(new Coach("01"), 2) });
            IBookingService bookingService = Substitute.For<IBookingService>();
            bookingService.GetBookingId().Returns("RES1");
            var reservationRequest = new ReservationRequest(trainId, 5);

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Check.That(makeReservation).IsNull();
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

            var makeReservation = new TicketOffice(seatService, bookingService).MakeReservation(reservationRequest);

            Check.That(reservationRequest.TrainId).IsEqualTo(makeReservation.TrainId);
            Check.That(makeReservation.Seats.Count).IsEqualTo(reservationRequest.SeatCount);
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

            Check.That(makeReservation).IsNull();
        }


        [Test]
        public void Should_make_reservation_twice_even_if_coach_over_70()
        {
            string trainId = "train2";
            var reservedSeatsForFirstReservation = new List<Seat>() {new Seat(new Coach("02"), 1), new Seat(new Coach("02"), 2)
                , new Seat(new Coach("02"), 3), new Seat(new Coach("02"), 4), new Seat(new Coach("02"), 5), new Seat(new Coach("02"), 6) };
            List<Seat> reservedSeatsForSecondReservation = new List<Seat>() {new Seat(new Coach("03"), 1), new Seat(new Coach("03"), 2)
                , new Seat(new Coach("03"), 3), new Seat(new Coach("03"), 4) };
            ISeatService seatService = new SeatService(new TrainSeatsMock());
            IBookingService bookingService = new BookingService();
            var firstReservationRequest = new ReservationRequest(trainId, 6);
            var secondReservationRequest = new ReservationRequest(trainId, 4);

            var makeFirstReservation = new TicketOffice(seatService, bookingService).MakeReservation(firstReservationRequest);

            var makeSecondReservation = new TicketOffice(seatService, bookingService).MakeReservation(secondReservationRequest);
                                   
            Check.That(makeFirstReservation.Seats).ContainsExactly(reservedSeatsForFirstReservation);
            Check.That(makeSecondReservation.Seats).ContainsExactly(reservedSeatsForSecondReservation);
           
            Check.That(10).IsEqualTo(seatService.GetAvailableSeats(trainId).Count);
        }
    }
}



