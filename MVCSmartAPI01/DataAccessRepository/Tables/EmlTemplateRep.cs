using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class EmlTemplateRep : IDataAccessRepository<emlTemplate, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<emlTemplate> Get()
        {
            return ctx.emlTemplates.ToList();
        }
        //Get Specific Data based on Id
        public emlTemplate Get(int id)
        {
            return ctx.emlTemplates.Find(id);
        }

        //Create a new Data
        public void Post(emlTemplate entity)
        {
            ctx.emlTemplates.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisiting Data
        public void Put(int id, emlTemplate entity)
        {
            var myData = ctx.emlTemplates.Find(id);
            if (myData != null)
            {
                myData.TemplateName = entity.TemplateName ;
                myData.TemplateSubject = entity.TemplateSubject;
                myData.TemplateBody = entity.TemplateBody;
                myData.ParameterKeys = entity.ParameterKeys;

                ctx.SaveChanges();
            }
        }
        //Delete Data based on Id
        public void Delete(int id)
        {
            var myData = ctx.emlTemplates.Find(id);
            if (myData != null)
            {
                ctx.emlTemplates.Remove(myData);
                ctx.SaveChanges();
            }
        }
    }
}