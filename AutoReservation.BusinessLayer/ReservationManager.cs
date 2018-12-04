using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        // TODO: Check if Auto is available
        public List<Reservation> GetAllReservations()
        {
            /*
            * TODO: Get List of All Reservations
            */
            throw new NotImplementedException();
        }

        public Reservation GetReservation(int id)
        {
            /*
             * TODO: Get Reservation
             */
            throw new NotImplementedException();
        }

        public void Insert(Reservation reservation)
        {
            /*
             * TODO: Add new Reservation
             */
        }

        public void Update(Reservation reservation)
        {
            /*
             * TODO: Update Reservation
             */
        }

        public void Delete(Reservation reservation)
        {
            /*
             * TODO: Delete Reservation
             */
        }
    }
}