using System.Text;
using NUnit.Framework;
using Mufc.CelumConnector.IServices;
using Mufc.CelumConnector.Services;
using Mufc.CelumConnector.Services.Builder;
using Mufc.CelumConnector.Constants;
using System.Xml;
using Mufc.CelumConnector.Tests.TestBaseClasses;
using System.Collections.Generic;
using System.Xml.Linq;
using Mufc.CelumConnector.MockClasses;
using NSubstitute;

namespace Mufc.CelumConnector.Tests
{
    /// <summary>
    /// Class is used to test all the functions defined in service class
    /// </summary>
    public class CelumServiceTest : CelumServiceTestBase
    {

        [TestCase("1093", "http://celum-dev.mufcplatform.in/AssetPicker/images/0/0/0/4/1093/9c72c387-d9b0-4aca-97c9-179646eef439.jpg")]
        public void GetAssetMetadata(string assetId, string expectedURL)
        {
            using (Sitecore.FakeDb.Db db = new Sitecore.FakeDb.Db())
            {
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

                //TODO: Need to create mock object for service
                //AssetRequestBuilder requestBuilder = new AssetRequestBuilder(new XmlDocument(), new StringBuilder());
                //XmlDocument soapEnvelopeXml = requestBuilder.SetAssetId(assetId).Build();
                //XDocument xdoc = new SoapInstance().GetAssetMetadata();
                //ICelumTouchPoint touchPoint = Substitute.For<ICelumTouchPoint>();
                //touchPoint.MakeRequest(soapEnvelopeXml).Returns(xdoc);

                ICelumTouchPoint touchPoint = new CelumTouchPoint();
                AssetCustomFieldBuilder customBuilder = new AssetCustomFieldBuilder(new XmlDocument(), new StringBuilder());
                AssetRequestBuilder metaRequestBuilder = new AssetRequestBuilder(new XmlDocument(), new StringBuilder());

                ICelumService objService = new CelumService(touchPoint, customBuilder, metaRequestBuilder);

                string metadata = objService.GetAssetMetadata(assetId, MediaTypeImage);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(metadata);
                string actualURL = string.Empty;
                for (int i = 0; i < json.Count; i++)
                {
                    if (json[i]["Key"].Value == CelumConstant.BackstageId)
                    {
                        actualURL = json[i]["Value"].Value;
                        break;
                    }
                }
                Assert.AreEqual(actualURL, expectedURL);
            }
        }

        //[TestCase("20034")]
        //public void GetPublishInstance(string expectedInstanceId)
        //{

        //    List<string> lstAssetId = new List<string>();
        //    lstAssetId.Add("47");
        //    lstAssetId.Add("48");
        //    lstAssetId.Add("49");
        //    lstAssetId.Add("50");
        //    using (Sitecore.FakeDb.Db db = new Sitecore.FakeDb.Db())
        //    {
        //        // set the setting value in unit test using db instance
        //        db.Configuration.Settings[CelumConstant.CelumPublishServiceURL] = "http://www.celum.com/schema/api/PublishService";
        //        db.Configuration.Settings[CelumConstant.CelumAssetPublishActionURL] = "http://10.92.176.80:8080/ws/apiPublishService.wsdl";
        //        db.Configuration.Settings[CelumConstant.CelumWebServiceURL] = "http://10.92.176.80:8080/ws";
        //        db.Configuration.Settings[CelumConstant.ImageMasterStageId] = "1";
        //        db.Configuration.Settings[CelumConstant.WorkspaceNodeId] = "20";



        //        PublishAssetBuilder requestBuilder = new PublishAssetBuilder(new XmlDocument(), new StringBuilder());
        //        XmlDocument soapEnvelopeXml = requestBuilder.PublishAsset(lstAssetId, CelumConstant.ImageMasterStageId).Build();//todo:fixing build
        //        XDocument xdoc = new SoapInstance().GetMockInstance();
        //        ICelumTouchPoint touchPoint = Substitute.For<ICelumTouchPoint>();
        //        touchPoint.MakeRequest(soapEnvelopeXml).Returns(xdoc);

        //        ICelumService objService = new CelumService(touchPoint, requestBuilder);
        //        string instanceId = objService.GetPublishInstance(lstAssetId, CelumConstant.ImageMasterStageId);//todo:fixing build
        //        Assert.AreEqual(instanceId, expectedInstanceId);
        //    }
        //}


        //[TestCase("20034", "47")]
        //public void GetPublishAsset(string instanceId, string expectedAssetId)
        //{

        //    using (Sitecore.FakeDb.Db db = new Sitecore.FakeDb.Db())
        //    {
        //        // set the setting value in unit test using db instance
        //        db.Configuration.Settings[CelumConstant.CelumPublishServiceURL] = "http://www.celum.com/schema/api/PublishService";
        //        db.Configuration.Settings[CelumConstant.CelumAssetPublishActionURL] = "http://10.92.176.80:8080/ws/apiPublishService.wsdl";
        //        db.Configuration.Settings[CelumConstant.CelumWebServiceURL] = "http://10.92.176.80:8080/ws";
        //        db.Configuration.Settings[CelumConstant.AssetPublishMaxValue] = "100";

        //        AssetInstanceBuilder requestBuilder = new AssetInstanceBuilder(new XmlDocument(), new StringBuilder());
        //        XmlDocument soapEnvelopeXml = requestBuilder.SetInstanceId(instanceId).Build();

        //        XDocument xdoc = new SoapInstance().GetAssetPublishURL();
        //        ICelumTouchPoint touchPoint = Substitute.For<ICelumTouchPoint>();
        //        touchPoint.MakeRequest(soapEnvelopeXml).Returns(xdoc);

        //        ICelumService objService = new CelumService(touchPoint, requestBuilder);
        //        List<CelumPublishAsset> lstPublishAsset = objService.PublishAssetRequest(instanceId,"");//todo: fixing build

        //        string assetId = (from f in lstPublishAsset where f.AssetResponseId == expectedAssetId select f.AssetResponseId).SingleOrDefault();
        //        Assert.AreEqual(assetId, expectedAssetId);
        //    }
        //}
    }
}
