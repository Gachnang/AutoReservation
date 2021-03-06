﻿using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {

        public List<Kunde> ListOfKunden
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                   return context.Kunden.ToList();
                }
            }
        }

        public Kunde GetKunde(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.Where(k => k.Id == id).FirstOrDefault();
            }
        }
        
        public bool DeleteKunde (Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if(context.Entry(kunde) != null)
                {
                    context.Entry<Kunde>(kunde).State = EntityState.Deleted;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int InsertKunde (Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry<Kunde>(kunde).State = EntityState.Added;
                context.SaveChanges();
                return kunde.Id;
            }
        }

        public bool UpdateKunde (Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if(context.Entry(kunde) != null)
                {
                    context.Entry<Kunde>(kunde).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw CreateOptimisticConcurrencyException<Kunde>(context, kunde);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}