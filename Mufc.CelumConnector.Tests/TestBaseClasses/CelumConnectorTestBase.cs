using System.IO;
using System.Web;
using System.Web.SessionState;

namespace Mufc.CelumConnector.Tests.TestBaseClasses
{
    /// <summary>
    /// base class for celum connector
    /// </summary>
    public class CelumConnectorTestBase
    {

        public string AssetServiceURL { get { return "http://www.celum.com/schema/api/AssetService"; } }

        public string WebServiceURL { get { return "http://ec2-52-208-13-206.eu-west-1.compute.amazonaws.com/appserver/ws"; } }

        public string AssetServiceActionURL { get { return "http://ec2-52-208-13-206.eu-west-1.compute.amazonaws.com/appserver/ws/apiAssetService.wsdl"; } }

        public string AssetURL { get { return "http://ec2-52-208-13-206.eu-west-1.compute.amazonaws.com/appserver/cora/Assets({0})"; } }

        public string ApiKeyValue { get { return "nr2s9l4tvu1ka6qiovnfcb0td3"; } }

        public string FieldId { get { return "Field123"; } }

        public string MediaTypeImage { get { return "1000"; } }

        public string MediaTypeVideo { get { return "3000"; } }

        public string ErrorImageURL { get { return "/picker/ooops.png"; } }

        public string CustomFieldServiceURL { get { return "http://www.celum.com/schema/api/CustomFieldService"; } }

        public string SoapUsername { get { return "admin"; } }

        public string SoapPassword { get { return "_Celum123_"; } }

        /// <summary>
        /// Set the fake HTTP context & Need to null this object after use
        /// </summary>
        public HttpContext FakeHttpContext
        {
            get
            {
                var httpRequest = new HttpRequest("", "http://CelumConnector/", "");
                var stringWriter = new StringWriter();
                var httpResponse = new HttpResponse(stringWriter);
                var httpContext = new HttpContext(httpRequest, httpResponse);

                var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                        new HttpStaticObjectsCollection(), 10, true,
                                                        HttpCookieMode.AutoDetect,
                                                        SessionStateMode.InProc, false);

                SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

                return httpContext;
            }

        }

    }
}