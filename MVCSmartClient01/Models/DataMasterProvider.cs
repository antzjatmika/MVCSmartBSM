using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Newtonsoft.Json;

namespace MVCSmartClient01.Models
{
    public class DataMasterProvider
    {
        private IEnumerable<mstTypeOfRekanan> _TypeOfRekananColls { get; set; }
        private IEnumerable<mstTypeOfRegion> _TypeOfRegionColls { get; set; }
        private IEnumerable<mstSubRegion> _SubRegionColls { get; set; }
        private IEnumerable<mstSegmentasi> _TypeOfSegmentasi3Colls { get; set; }
        private IEnumerable<mstSegmentasi> _TypeOfSegmentasi5Colls { get; set; }
        private IEnumerable<mstReference> _TypeTotalAsetColls { get; set; }
        private IEnumerable<mstReference> _TriwulanColls { get; set; }
        private IEnumerable<mstReference> _TingkatResikoColls { get; set; }
        private IEnumerable<mstReference> _LbgPemeringkatColls { get; set; }
        /// <summary>
        /// Configuration values dictionary
        /// </summary>
        public IEnumerable<mstTypeOfRekanan> TypeOfRekananColls
        {
            get { return _TypeOfRekananColls; }
        }
        public IEnumerable<mstTypeOfRegion> TypeOfRegionColls
        {
            get { return _TypeOfRegionColls; }
        }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi3Colls
        {
            get { return _TypeOfSegmentasi3Colls; }
        }
        public IEnumerable<mstSegmentasi> TypeOfSegmentasi5Colls
        {
            get { return _TypeOfSegmentasi5Colls; }
        }
        public IEnumerable<mstReference> TypeTotalAsetColls
        {
            get { return _TypeTotalAsetColls; }
        }
        public IEnumerable<mstReference> TriwulanColls
        {
            get { return _TriwulanColls; }
        }
        public IEnumerable<mstReference> TingkatResikoColls
        {
            get { return _TingkatResikoColls; }
        }
        public IEnumerable<SimpleRef> JenisPermohonanColls
        {
            get {
                return new List<SimpleRef>() { 
                    new SimpleRef { RefId = 1, RefDescription = "Pendaftaran Baru" }
                    , new SimpleRef { RefId = 2, RefDescription = "Perpanjangan" }
                    , new SimpleRef { RefId = 3, RefDescription = "Perubahan Data" } };
            }
        }
        public IEnumerable<mstReference> LbgPemeringkatColls
        {
            get { return _LbgPemeringkatColls; }
        }
        private static DataMasterProvider instance;
        public static DataMasterProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataMasterProvider();
                }
                return instance;
            }
        }

        // private constructor
        private DataMasterProvider() 
        {
            string SmartAPIUrl = ConfigurationManager.AppSettings["SmartAPIUrl"];
            string urlMaster = string.Format("{0}/api/MstDatamaster", SmartAPIUrl);

            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(urlMaster);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 

            mstDataMaster myDataMaster = new mstDataMaster();
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage = client.GetAsync(urlMaster).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                myDataMaster = JsonConvert.DeserializeObject<mstDataMaster>(responseData);
                _TypeOfRekananColls = myDataMaster.TypeOfRekananColls;
                _TypeOfRegionColls = myDataMaster.TypeOfRegionColls;
                _SubRegionColls = myDataMaster.SubRegionColls;
                _TypeOfSegmentasi3Colls = myDataMaster.TypeOfSegmentasi3Colls;
                _TypeOfSegmentasi5Colls = myDataMaster.TypeOfSegmentasi5Colls;
                _TypeTotalAsetColls = myDataMaster.TypeTotalAsetColls;
                _TriwulanColls = myDataMaster.TriwulanColls;
                _TingkatResikoColls = myDataMaster.TingkatResikoColls;
                _LbgPemeringkatColls = myDataMaster.LbgPemeringkatColls;
            }
        }

    }
}