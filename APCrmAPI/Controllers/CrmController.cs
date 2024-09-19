using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Xrm.Sdk.Query;
using APCrmAPI.Services;
using Microsoft.Xrm.Sdk;
using APCrmAPI.Models.RPCode;

namespace APCrmAPI.Controllers
{

    public class CrmController : ApiController
    {
        private CrmService _crmService;
        public static List<string> ImageTypes = new List<string>() { "image/x-png", "image/pjpeg", "image/png", "image/jpeg", "image/jpg", "image/gif" };

        public CrmController()
        {
            _crmService = new CrmService();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/crm/getRpCodeById")]
        public Note_RpCode GetLogoAnnotationByRpCodeId(string rpCodeId)
        {
            Note_RpCode logoData = new Note_RpCode();

            try
            {
                QueryExpression query = new QueryExpression("annotation");
                query.Criteria.AddCondition("objectid", ConditionOperator.Equal, rpCodeId);
                query.Criteria.AddCondition("mimetype", ConditionOperator.In, ImageTypes.ToArray());
                query.Criteria.AddCondition("subject", ConditionOperator.Equal, "LogoImage");
                query.ColumnSet = new ColumnSet(new string[] { "subject", "notetext", "objectid", "mimetype", "documentbody", "filename" });
                query.AddOrder("createdon", OrderType.Ascending);

                EntityCollection logoImageEntities = CrmService.GetOrganizationServiceProxy().RetrieveMultiple(query);
                if (logoImageEntities != null && logoImageEntities.Entities!=null && logoImageEntities.Entities.Count > 0)
                {
                    Entity logoImageEntity = logoImageEntities[0];
                    logoData = MapLogoImageToModel(logoImageEntity);
                }
            }
            catch(Exception ex) 
            {
                
            }
            return logoData;
        }

        private Note_RpCode MapLogoImageToModel(Entity entity)
        {
            Note_RpCode logoData = new Note_RpCode();

            try
            {
                logoData.Guid = entity.Id.ToString();
                if (entity.Attributes.ContainsKey("subject"))
                {
                    logoData.Subject = entity.Attributes["subject"].ToString();
                }
                if (entity.Attributes.ContainsKey("notetext"))
                {
                    logoData.Description = entity.Attributes["notetext"].ToString();
                }
                if (entity.Attributes.ContainsKey("objectid"))
                {
                    logoData.ObjectId = ((EntityReference)entity.Attributes["objectid"]).Id.ToString();
                }
                if (entity.Attributes.ContainsKey("mimetype"))
                {
                    logoData.Mimetype = entity.Attributes["mimetype"].ToString();
                } 
                if (entity.Attributes.ContainsKey("documentbody"))
                {
                    logoData.DocumentBody = entity.Attributes["documentbody"].ToString();
                }
                if (entity.Attributes.ContainsKey("filename"))
                {
                    logoData.FileName = entity.Attributes["filename"].ToString();
                }
            }
            catch (Exception ex) 
            {
            
            }
            return logoData;    
        }
    }
}