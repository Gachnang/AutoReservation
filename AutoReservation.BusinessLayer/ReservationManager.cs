using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
	public class ReservationManager
		: ManagerBase
	{
		// TODO: Check if Auto is available
		public List<Reservation> List
		{
			get
			{
				using (AutoReservationContext context = new AutoReservationContext())
				{
					return context.Reservationen.ToList();
				}
			}
		}

		public Reservation GetReservation(int resNr)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				return context.Reservationen.Include(r => r.Auto).Include(r => r.Kunde)
					.Single(res => res.ReservationsNr == resNr);
			}
		}

		public void InsertReservation(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				if (RangeCheck(reservation))
				{
					context.Entry(reservation).State = EntityState.Added;
					context.SaveChanges();
				}
				else
				{
					throw new InvalidDateRangeException("Error while inserting", reservation);
				}
			}
		}

		public void UpdateReservation(Reservation reservation)
		{
			//TODO RangeCheck
			using (AutoReservationContext context = new AutoReservationContext())
			{

				if (RangeCheck(reservation))
				{
					context.Entry(reservation).State = EntityState.Modified;
					try
					{
						context.SaveChanges();
					}
					catch (DbUpdateConcurrencyException)
					{
						throw CreateOptimisticConcurrencyException(context, reservation);
					}
				}
				else
				{
					throw new InvalidDateRangeException("Error while inserting", reservation);
				}
			}
		}

		public void DeleteReservation(Reservation reservation)
		{
			using (AutoReservationContext context = new AutoReservationContext())
			{
				context.Entry(reservation).State = EntityState.Deleted;
				try
				{
					context.SaveChanges();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}

		public bool RangeCheck(Reservation reservation)
		{
			return (reservation.Von.AddHours(24).CompareTo(reservation.Bis) < 0);
		}
	}
}