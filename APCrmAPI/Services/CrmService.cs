using System;
using System.Diagnostics;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace APCrmAPI.Services
{
    public class CrmService
    {
        public static OrganizationServiceProxy GetOrganizationServiceProxy()
        {
            try
            {
                return CreateConnectionWithCRM();
            }
            catch (TimeoutException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private static OrganizationServiceProxy CreateConnectionWithCRM()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            OrganizationServiceProxy _proxy = null;
            try
            {
                Uri oUri = GetOrganizationUri();
                ClientCredentials clientCredentials = GetClientCredentials();
                _proxy = new OrganizationServiceProxy(oUri, null, clientCredentials, null);
            }
            catch (Exception ex)
            {
            }

            stopwatch.Stop();
            return _proxy;
        }

        private static Uri GetOrganizationUri()
        {
            Uri oUri = null;
            string ServerName = "", OrganizationName = "", Protocol = "";
            try
            {
                ServerName = "crmdev.gridsystems.pk";
                OrganizationName = "GlobalRescueLLC";
                Protocol = "https";
                if (!String.IsNullOrWhiteSpace(ServerName) && !String.IsNullOrWhiteSpace(OrganizationName) && !String.IsNullOrWhiteSpace(Protocol))
                {
                    ServerName = ServerName.Contains("http://") || ServerName.Contains("https://") ? ServerName.Replace("http://", "").Replace("https://", "") : ServerName;
                    //if ("https".Equals(Protocol, StringComparison.OrdinalIgnoreCase) && !ServerName.Trim().EndsWith("globalrescue.com"))
                    //    ServerName += ".globalrescue.com";
                    oUri = new Uri(Protocol + "://" + ServerName + "/" + OrganizationName + "/XRMServices/2011/Organization.svc");
                }
            }
            catch (TimeoutException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return oUri;
        }

        private static ClientCredentials GetClientCredentials()
        {

            ClientCredentials clientCredentials = null;
            string Login = "", Password = "", Domain = "", EncrytedPassword = "", SecurityKey = "";
            try
            {
                //            < add key = "Login" value = "crmwsvcs" />
                //< add key = "Password" value = "khO400Q4v1rU" />
                Login = "crmwsvcs";
                Password = "khO400Q4v1rU";
                Domain = "GRIDSYSTEMS";

                if (!String.IsNullOrWhiteSpace(Domain) && !String.IsNullOrWhiteSpace(Login))
                {
                    clientCredentials = new ClientCredentials();
                    clientCredentials.UserName.UserName = Domain + "\\" + Login;
                    clientCredentials.UserName.Password = Password;
                }
                else
                {
                }
            }
            catch (TimeoutException ex)
            {
               
            }
            catch (Exception ex)
            {
                
            }
            return clientCredentials;
        }
    }
}