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
using System.Collections;

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
        [System.Web.Http.Route("api/crm/GetLogoAnnotationByRpCodeName")]
        public Note_RpCode GetLogoAnnotationByRpCodeName(string rpCodeName)
        {
            Note_RpCode logoData = new Note_RpCode();
            //Guid rpCodeGuid = Guid.Empty;
            try
            {
                //rpCodeGuid = GetRpCodeGuidByRpCodeName(rpCodeName);
                QueryExpression query = new QueryExpression("annotation");
                //query.Criteria.AddCondition("objectid", ConditionOperator.Equal, rpCodeId);
                query.Criteria.AddCondition("mimetype", ConditionOperator.In, ImageTypes.ToArray());
                query.Criteria.AddCondition("subject", ConditionOperator.Equal, "LogoImage");
                query.ColumnSet = new ColumnSet(new string[] { "subject", "notetext", "objectid", "mimetype", "documentbody", "filename" });

                LinkEntity rpCodeLink = new LinkEntity("annotation", "gr_cpc", "objectid", "gr_cpcid", JoinOperator.Inner);
                rpCodeLink.LinkCriteria.AddCondition("gr_cpccode", ConditionOperator.Equal, rpCodeName);
                rpCodeLink.Columns = new ColumnSet("gr_referralurl", "gr_cpccode");
                rpCodeLink.EntityAlias = "rp";

                query.LinkEntities.Add(rpCodeLink);
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
                if (entity.Attributes.ContainsKey("rp.gr_referralurl"))
                {
                    logoData.PartnerUrl =  ((AliasedValue)entity.Attributes["rp.gr_referralurl"]).Value.ToString();
                }
            }
            catch (Exception ex) 
            {
            
            }
            return logoData;    
        }

        public Guid GetRpCodeGuidByRpCodeName(string rpCodeName)
        {
            Guid rPCodeGuid = new Guid();
            try
            {
                QueryExpression rpCodeQuery = new QueryExpression("gr_cpc");
                rpCodeQuery.Criteria.AddCondition("gr_cpccode", ConditionOperator.Equal, rpCodeName);
                rpCodeQuery.ColumnSet = new ColumnSet("gr_cpcid");

                EntityCollection rpCodeEntities = CrmService.GetOrganizationServiceProxy().RetrieveMultiple(rpCodeQuery);
                if (rpCodeEntities != null && rpCodeEntities.Entities != null && rpCodeEntities.Entities.Count > 0)
                {
                    Entity rpCodeEntity = rpCodeEntities[0];
                    rPCodeGuid = rpCodeEntity.Id;
                }

            }
            catch (Exception ex) 
            {
            }
            return rPCodeGuid;
        }
    }
}