﻿using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        
        public List<Auto> ListOfAutos
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

        public Auto GetAuto(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.SingleOrDefault(a => a.Id == id);
            }
        }

        public int InsertAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Added;

                context.SaveChanges();

                return auto.Id;
            }
        }

        public bool UpdateAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if(context.Entry(auto) != null)
                {
                    context.Entry(auto).State = EntityState.Modified;
                
                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw CreateOptimisticConcurrencyException(context, auto);
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if (context.Entry(auto) != null)
                {
                    context.Entry(auto).State = EntityState.Deleted;
                
                    context.SaveChanges();

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