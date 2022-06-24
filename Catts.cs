using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrchestrationLayerDemo
{
    public class Catts
    {
        public Catts()
        {

        }

        [XmlRoot(ElementName = "InputDataItem")]
        public class CattsTcaServiceObjMain
        {
            public CattsTcaServiceRequest CattsTcaServiceRequest { get; set; }
        }
        public class CattsTcaServiceRequest
        {
            public string ApplicationName { get; set; }
            public string UserId { get; set; }
            public string SubOperationName { get; set; }
            public string TraceLevel { get; set; }
            public string TraceType { get; set; }
            public string InputType { get; set; }
            public string AdditionalReports { get; set; }
            public InputData InputData { get; set; }
        }


        public class InputData
        {
            [XmlElement(ElementName = "InputDataItem")]
            public List<InputDataItem> InputDataItem { get; set; }
        }


        public class InputDataItem
        {
            [XmlAttribute("type")]
            public string type { get; set; }

            [XmlTextAttribute()]
            public string text { get; set; }

        }
        public class InputReport
        {
            public string TRACE_LEVEL { get; set; }
            public string INPUT_TYPE { get; set; }
            public string VALIDATION_STATUS { get; set; }
            public string TRACE_RESULT { get; set; }
            public string MTRL_ID { get; set; }
            public string EPM_ID { get; set; }
            public string SPEC_TYPE { get; set; }
            public string SPEC_CODE { get; set; }
            public string FINISH_DATE { get; set; }
            public string BATCH { get; set; }
            public string FPO_LOT { get; set; }
            public string BOX_ID { get; set; }
            public string BOX_TYPE { get; set; }
            public string VISUAL_ID { get; set; }
            public string TRACKING_ID { get; set; }

        }

        public class BatchReport
        {

            public string BATCH { get; set; }
            public string MTRL_ID { get; set; }
            public string SKU { get; set; }
            public string SPEC_TYPE { get; set; }
            public string SPEC_CODE { get; set; }
            public string WHS_ID { get; set; }
            public string PLANT { get; set; }
            public string DN { get; set; }
            public string DN_TYPE { get; set; }
            public string AGID_DATE { get; set; }
            public string DN_BATCH_QTY { get; set; }
            public string REMAINDER_QTY { get; set; }
            public string SLS_ORG_CD { get; set; }
            public string SLD_CUST_ID { get; set; }
            public string SLD_CUST_NM { get; set; }
            public string SLD_CUST_CITY_NM { get; set; }
            public string SLD_CUST_COUNTRY_NM { get; set; }
            public string SLD_CUST_GEO_CD { get; set; }
            public string SHP_CUST_ID { get; set; }
            public string SHP_CUST_NM { get; set; }
            public string SHP_CUST_CITY_NM { get; set; }
            public string SHP_CUST_COUNTRY_NM { get; set; }
            public string SHP_CUST_GEO_CD { get; set; }
            public string SLS_DOC_ID { get; set; }
            public string SLS_DOC_LINE_NBR { get; set; }
            public string CUST_PO_NBR { get; set; }
            public string CUST_MTRL_ID { get; set; }
            public string SLS_ORD_LINE_DSC { get; set; }
            public string PROCESS_CODE { get; set; }
            public string PRODUCT_GRADE { get; set; }
            public string CODE_NAME { get; set; }
            public string MARKET_CODE_NAME { get; set; }
            public string FMLY_NM { get; set; }
            public string IC_CATEGORY { get; set; }
            public string SHP_MEDIA_TYPE { get; set; }
            public string BASE_NM { get; set; }
            public string DIV_NM { get; set; }
            public string SBS_NM { get; set; }
            public string OPR_BUSNS_UN_NM { get; set; }
            public string OPR_BUSNS_UN_CD { get; set; }
            public string MOVEMENT_CODE { get; set; }
            public string DNLINE_CREATE_DATE { get; set; }
            public string DSTRB_CHNL_CD { get; set; }
            public string DLVR_DT { get; set; }
            public string EPM_ID { get; set; }
            public string VRTCL_SEG_CD { get; set; }
            public string PCSR_NBR { get; set; }
            public string SPEED { get; set; }
            public string CACHE_SIZE { get; set; }
            public string PKG_TECH_CD { get; set; }
            public string THRML_DSGN_POWER { get; set; }
            public string SLS_DOC_TYPE_CD { get; set; }
            public string PLANT_ORIG { get; set; }
            public string SHP_CUST_ID_ORIG { get; set; }
            public string SLD_CUST_COUNTRY_CD { get; set; }
            public string SHP_CUST_COUNTRY_CD { get; set; }
        }

        public class BoxReport
        {
            public string BATCH { get; set; }
            public string MTRL_ID { get; set; }
            public string SKU { get; set; }
            public string SPEC_TYPE { get; set; }
            public string SPEC_CODE { get; set; }
            public string WHS_ID { get; set; }
            public string PLANT { get; set; }
            public string DN { get; set; }
            public string DN_TYPE { get; set; }
            public string AGID_DATE { get; set; }
            public string DN_BATCH_QTY { get; set; }
            public string OVERPACK_ID { get; set; }
            public string SHP_BOX_ID { get; set; }
            public string SHP_BOX_IND { get; set; }
            public string DCULT_EXPECTED { get; set; }
            public string DCULT_PERCENT { get; set; }
            public string SHP_BOX_BATCH_QTY { get; set; }
            public string FACTORY_BOX_ID { get; set; }
            public string FULFIL_BOX_ID { get; set; }
            public string FACTORY_BOX_QTY { get; set; }
            public string REMAINDER_QTY { get; set; }
            public string BATCH_REMAINDER_QTY { get; set; }
            public string SLS_ORG_CD { get; set; }
            public string SLD_CUST_ID { get; set; }
            public string SLD_CUST_NM { get; set; }
            public string SLD_CUST_CITY_NM { get; set; }
            public string SLD_CUST_COUNTRY_NM { get; set; }
            public string SLD_CUST_GEO_CD { get; set; }
            public string SHP_CUST_ID { get; set; }
            public string SHP_CUST_NM { get; set; }
            public string SHP_CUST_CITY_NM { get; set; }
            public string SHP_CUST_COUNTRY_NM { get; set; }
            public string SHP_CUST_GEO_CD { get; set; }
            public string SLS_DOC_ID { get; set; }
            public string SLS_DOC_LINE_NBR { get; set; }
            public string CUST_PO_NBR { get; set; }
            public string CUST_MTRL_ID { get; set; }
            public string SLS_ORD_LINE_DSC { get; set; }
            public string PROCESS_CODE { get; set; }
            public string PRODUCT_GRADE { get; set; }
            public string CODE_NAME { get; set; }
            public string MARKET_CODE_NAME { get; set; }
            public string FMLY_NM { get; set; }
            public string IC_CATEGORY { get; set; }
            public string SHP_MEDIA_TYPE { get; set; }
            public string BASE_NM { get; set; }
            public string DIV_NM { get; set; }
            public string SBS_NM { get; set; }
            public string OPR_BUSNS_UN_NM { get; set; }
            public string OPR_BUSNS_UN_CD { get; set; }
            public string MOVEMENT_CODE { get; set; }
            public string TO_NUM { get; set; }
            public string WHS_DOC_NUM { get; set; }
            public string WHS_DOC_LINE { get; set; }
            public string ORIG_IBDN { get; set; }
            public string TO_DATE { get; set; }
            public string DNLINE_CREATE_DATE { get; set; }
            public string DSTRB_CHNL_CD { get; set; }
            public string DLVR_DT { get; set; }
            public string EPM_ID { get; set; }
            public string VRTCL_SEG_CD { get; set; }
            public string PCSR_NBR { get; set; }
            public string SPEED { get; set; }
            public string CACHE_SIZE { get; set; }
            public string PKG_TECH_CD { get; set; }
            public string THRML_DSGN_POWER { get; set; }
            public string ULT_COUNT { get; set; }
            public string SLS_DOC_TYPE_CD { get; set; }
            public string PLANT_ORIG { get; set; }
            public string SHP_CUST_ID_ORIG { get; set; }
            public string SLD_CUST_COUNTRY_CD { get; set; }
            public string SHP_CUST_COUNTRY_CD { get; set; }

        }

        public class UnitTrace
        {

            public string VISUAL_ID { get; set; }
            public string FUSE_PART_ID { get; set; }
            public string BATCH { get; set; }
            public string FPO_BATCH { get; set; }
            public string MTRL_ID { get; set; }
            public string SKU { get; set; }
            public string SPEC_TYPE { get; set; }
            public string SPEC_CODE { get; set; }
            public string WHS_ID { get; set; }
            public string PLANT { get; set; }
            public string DN { get; set; }
            public string DN_TYPE { get; set; }
            public string AGID_DATE { get; set; }
            public string DN_BATCH_QTY { get; set; }
            public string OVERPACK_ID { get; set; }
            public string SHP_BOX_ID { get; set; }
            public string SHP_BOX_IND { get; set; }
            public string DCULT_EXPECTED { get; set; }
            public string DCULT_PERCENT { get; set; }
            public string SHP_BOX_BATCH_QTY { get; set; }
            public string FACTORY_BOX_ID { get; set; }
            public string FULFIL_BOX_ID { get; set; }
            public string FACTORY_BOX_QTY { get; set; }
            public string REMAINDER_QTY { get; set; }
            public string BATCH_REMAINDER_QTY { get; set; }
            public string SLS_ORG_CD { get; set; }
            public string SLD_CUST_ID { get; set; }
            public string SLD_CUST_NM { get; set; }
            public string SLD_CUST_CITY_NM { get; set; }
            public string SLD_CUST_COUNTRY_NM { get; set; }
            public string SLD_CUST_GEO_CD { get; set; }
            public string SHP_CUST_ID { get; set; }
            public string SHP_CUST_NM { get; set; }
            public string SHP_CUST_CITY_NM { get; set; }
            public string SHP_CUST_COUNTRY_NM { get; set; }
            public string SHP_CUST_GEO_CD { get; set; }
            public string SLS_DOC_ID { get; set; }
            public string SLS_DOC_LINE_NBR { get; set; }
            public string CUST_PO_NBR { get; set; }
            public string CUST_MTRL_ID { get; set; }
            public string SLS_ORD_LINE_DSC { get; set; }
            public string PROCESS_CODE { get; set; }
            public string PRODUCT_GRADE { get; set; }
            public string CODE_NAME { get; set; }
            public string MARKET_CODE_NAME { get; set; }
            public string FMLY_NM { get; set; }
            public string IC_CATEGORY { get; set; }
            public string SHP_MEDIA_TYPE { get; set; }
            public string BASE_NM { get; set; }
            public string DIV_NM { get; set; }
            public string SBS_NM { get; set; }
            public string OPR_BUSNS_UN_NM { get; set; }
            public string OPR_BUSNS_UN_CD { get; set; }
            public string MOVEMENT_CODE { get; set; }
            public string TO_NUM { get; set; }
            public string WHS_DOC_NUM { get; set; }
            public string WHS_DOC_LINE { get; set; }
            public string ORIG_IBDN { get; set; }
            public string TO_DATE { get; set; }
            public string DNLINE_CREATE_DATE { get; set; }
            public string DSTRB_CHNL_CD { get; set; }
            public string DLVR_DT { get; set; }
            public string EPM_ID { get; set; }
            public string VRTCL_SEG_CD { get; set; }
            public string PCSR_NBR { get; set; }
            public string SPEED { get; set; }
            public string CACHE_SIZE { get; set; }
            public string PKG_TECH_CD { get; set; }
            public string THRML_DSGN_POWER { get; set; }
            public string ULT_COUNT { get; set; }
            public string SLS_DOC_TYPE_CD { get; set; }
            public string PLANT_ORIG { get; set; }
            public string SHP_CUST_ID_ORIG { get; set; }
            public string SLD_CUST_COUNTRY_CD { get; set; }
            public string SHP_CUST_COUNTRY_CD { get; set; }
            public string CANDIDATE_UNIT { get; set; }
        }

        public class transactiondetails
        {
            public string IsTransactionSuccess { get; set; }
            public string ErrorMessage { get; set; }
        }
        public class CattsTcaOutputDataSet
        {
            public List<transactiondetails> TransactionDetails { get; set; }
            public List<InputReport> InputReport { get; set; }
            public List<BatchReport> BatchReport { get; set; }
            public List<BoxReport> BoxReport { get; set; }
            public List<UnitTrace> UnitTrace { get; set; }

        }


        public class ResultCATT
        {
            public CattsTcaOutputDataSet CattsTcaOutputDataSet { get; set; }

        }



    }
}
