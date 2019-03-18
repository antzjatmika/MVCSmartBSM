using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class TrxRekananDocumentTrackRep : IDataAccessRepository<trxRekananDocumentTrack, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<trxRekananDocumentTrack> Get()
        {
            return ctx.trxRekananDocumentTracks.ToList();
        }
        //Get Specific Data based on Id
        public trxRekananDocumentTrack Get(int id)
        {
            return ctx.trxRekananDocumentTracks.Find(id);
        }

        //Create a new Data
        public void Post(trxRekananDocumentTrack entity)
        {
            ctx.trxRekananDocumentTracks.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, trxRekananDocumentTrack entity)
        {
            var myData = ctx.trxRekananDocumentTracks.Find(id);
            if (myData != null)
            {
                myData.IdDocument = entity.IdDocument;
                myData.IdRekanan = entity.IdRekanan;
                myData.IdTypeOfDocument = entity.IdTypeOfDocument;
                myData.IdTypeOfStatusDoc = entity.IdTypeOfStatusDoc;
                myData.VerificationNote = entity.VerificationNote;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.trxRekananDocumentTracks.Find(id);
            if (myData != null)
            {
                ctx.trxRekananDocumentTracks.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}