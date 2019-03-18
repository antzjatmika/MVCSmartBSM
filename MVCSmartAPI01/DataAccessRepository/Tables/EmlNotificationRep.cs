using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class EmlNotificationRep : IDataAccessRepository<emlNotification, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<emlNotification> Get()
        {
            return ctx.emlNotifications.ToList();
        }
        //Get Specific Data based on Id
        public emlNotification Get(int id)
        {
            return ctx.emlNotifications.Find(id);
        }

        //Create a new Data
        public void Post(emlNotification entity)
        {
            ctx.emlNotifications.Add(entity);
            ctx.SaveChanges();
        }
        //Update Existing Data
        public void Put(int id, emlNotification entity)
        {
            var myData = ctx.emlNotifications.Find(id);
            if (myData != null)
            {
                myData.TemplateId = entity.TemplateId;
                myData.ParameterKeys = entity.ParameterKeys;
                myData.ParameterValues = entity.ParameterValues;
                myData.EmailSubject = entity.EmailSubject;
                myData.EmailBody = entity.EmailBody;
                myData.AddressEmailIdTo = entity.AddressEmailIdTo;
                myData.AddressEmailIdCc = entity.AddressEmailIdCc;
                myData.AddressEmailTo = entity.AddressEmailTo;
                myData.AddressEmailCc = entity.AddressEmailCc;
                myData.StatusDelivery = entity.StatusDelivery;
                myData.MailCreationDate = entity.MailCreationDate;
                myData.SentDate = entity.SentDate;
                myData.FlagActivate = entity.FlagActivate;
                myData.SatuanPeriode = entity.SatuanPeriode;
                myData.CacahPeriode = entity.CacahPeriode;
                myData.TanggalJatuhTempo = entity.TanggalJatuhTempo;
                myData.TanggalNotifikasi = entity.TanggalNotifikasi;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.emlNotifications.Find(id);
            if (myData != null)
            {
                ctx.emlNotifications.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}