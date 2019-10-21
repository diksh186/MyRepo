using Mufc.CelumConnector.Constants;
using Mufc.CelumConnector.Controllers;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web;
using Mufc.CelumConnector.Models.CelumConnector.CelumProperties;
using System.Web.Routing;
using Mufc.CelumConnector.Tests.TestBaseClasses;
using Sitecore.FakeDb;

namespace Mufc.CelumConnector.Tests
{
    /// <summary>
    /// Test class is used to test alll the functions defined in celum connector controller. 
    /// </summary>
    public class CelumConnectorTest : CelumConnectorTestBase
    {

        /// <summary>
        /// Test case for published images on celum. Checking preview URL with Test case URL. 
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="expectedURL"></param>
        [TestCase("1093", "http://celum-dev.mufcplatform.in/AssetPicker/images/0/0/0/4/1093/9c72c387-d9b0-4aca-97c9-179646eef439.jpg")]
        public void CelumImageAssetPicker(string assetId, string expectedURL)
        {
            using (Db db = new Sitecore.FakeDb.Db())
            {

                // set the setting value in unit test using db instance
                db.Configuration.Settings[CelumConstant.CelumAssetServiceURL] = AssetServiceURL;
                db.Configuration.Settings[CelumConstant.CelumWebServiceURL] = WebServiceURL;
                db.Configuration.Settings[CelumConstant.CelumAssetServiceActionURL] = AssetServiceActionURL;
                db.Configuration.Settings[CelumConstant.CelumAssetsURL] = AssetURL;
                db.Configuration.Settings[CelumConstant.CelumApiKeyValue] = ApiKeyValue;
                db.Configuration.Settings[CelumConstant.MediaTypeImage] = MediaTypeImage;
                db.Configuration.Settings[CelumConstant.ErrorImageURL] = ErrorImageURL;
                db.Configuration.Settings[CelumConstant.CelumCustomFieldServiceURL] = CustomFieldServiceURL;
                db.Configuration.Settings[CelumConstant.CelumSoapUsername] = SoapUsername;
                db.Configuration.Settings[CelumConstant.CelumSoapPassword] = SoapPassword;

                HttpContext.Current = FakeHttpContext;
                HttpContext.Current.Session[CelumConstant.CelumImageID] = FieldId;
                var wrapper = new HttpContextWrapper(HttpContext.Current);

                CelumConnectorController celumConnector = new CelumConnectorController();
                celumConnector.ControllerContext = new ControllerContext(wrapper, new RouteData(), celumConnector);
                //TODO:Need to update with new implementation
                JsonResult json = celumConnector.CelumImageAssetPicker(assetId,null);
                string url = ((CelumSitecoreAssetDetails)(json.Data)).CelumAssetData[2].CelumValue;
                Assert.AreEqual(url, expectedURL);
                HttpContext.Current = null;

            }

        }

        /// <summary>
        /// Test case for published video on celum. Checking Ooyala Video Id with Test case Video Id. 
        /// </summary>
        /// <param name="assetId"></param>
        /// <param name="expectedURL"></param>
        [TestCase("64", "I1cW8xNDE6vTy2xqDYarbd2eKtvQeCRW")]
        public void CelumVideoAssetPicker(string assetId, string expectedURL)
        {
           
            using (Db db = new Sitecore.FakeDb.Db())
            {

                // set the setting value in unit test using db instance
                db.Configuration.Settings[CelumConstant.CelumAssetServiceURL] = AssetServiceURL;
                db.Configuration.Settings[CelumConstant.CelumWebServiceURL] = WebServiceURL;
                db.Configuration.Settings[CelumConstant.CelumAssetServiceActionURL] = AssetServiceActionURL;
                db.Configuration.Settings[CelumConstant.CelumAssetsURL] = AssetURL;
                db.Configuration.Settings[CelumConstant.CelumApiKeyValue] = ApiKeyValue;
                db.Configuration.Settings[CelumConstant.MediaTypeVideo] = MediaTypeVideo;
                db.Configuration.Settings[CelumConstant.ErrorImageURL] = ErrorImageURL;
                db.Configuration.Settings[CelumConstant.CelumCustomFieldServiceURL] = CustomFieldServiceURL;
                db.Configuration.Settings[CelumConstant.CelumSoapUsername] = SoapUsername;
                db.Configuration.Settings[CelumConstant.CelumSoapPassword] = SoapPassword;

                HttpContext.Current = FakeHttpContext;
                HttpContext.Current.Session[CelumConstant.CelumVideoID] = FieldId;
                var wrapper = new HttpContextWrapper(HttpContext.Current);

                CelumConnectorController celumConnector = new CelumConnectorController();
                celumConnector.ControllerContext = new ControllerContext(wrapper, new RouteData(), celumConnector);
                JsonResult json = celumConnector.CelumVideoAssetPicker(assetId);
                string url = ((CelumSitecoreAssetDetails)(json.Data)).CelumAssetData[2].CelumValue;
                Assert.AreEqual(url, expectedURL);
                HttpContext.Current = null;

            }
        }
    }
}
