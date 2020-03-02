﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KataTrainReservation
{
    public interface ISeatService
    {
        List<Seat> GetAvailableSeats(string trainId);
    }
}