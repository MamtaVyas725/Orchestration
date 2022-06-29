using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchestrationLayerDemo.Controllers
{
    //Input
    public class SntraceItem
    {
        public string[] SN { get; set; }
      public SyntraceRootObject SyntraceRootObject { get; set; }
    }
    public class SyntraceRootObject
    {
        //public SntraceItem SN { get; set; }
       public string[] SN { get; set; }
       // public List<SyntraceOutputColumn> OutPutColumn { get; set; }
        public string[] OutPutColumn { get; set; }

    }
    public class SyntraceOutputColumn
    {
        string Batch_no { get; set; }
        string SN { get; set; }
        string Type { get; set; }
        string Site { get; set; }
        string Fcst_prd_nm { get; set; }
        string Work_Order { get; set; }
        string Cust_sn { get; set; }
        string BUILD_DATE { get; set; }
        string Coo_flag { get; set; }
        string Mat_id { get; set; }
        string Version { get; set; }
        string Carton_ID { get; set; }
        string Pallet_ID { get; set; }
        string Firmware { get; set; }
        string Receipt_ID { get; set; }
        string intel_pn { get; set; }
        string vendor { get; set; }
        string mfg_pn { get; set; }
        string PART_DESC { get; set; }
        string SN_FLAG { get; set; }
        string SYSTEM_ASSEMBLY_SITE { get; set; }
        string PART_NO { get; set; }
        string DATE_CODE { get; set; }
        string LOT_CODE { get; set; }
        string VENDOR_CODE { get; set; }
        string VENDOR_NAME { get; set; }
        string PPID { get; set; }
        string PART { get; set; }
        string LOCATION { get; set; }
        string MAKER_DESC { get; set; }
        string MAKER_PART_NO { get; set; }
        string BOARD_ASSEMBLY_SITE { get; set; }
        string REEL_CODE { get; set; }
        string SLOT { get; set; }
        string UNITQTY { get; set; }
        string CUSTOMER_PN { get; set; }
        string PPPN { get; set; }
        string Online_Offline { get; set; }
        string PROGRAM_STATUS { get; set; }
        string PROGRAM_TIME { get; set; }
        string CHECKSUM { get; set; }
        string PROGRAMED_FILE_NAME { get; set; }
        string PROGRAM_LOCATION { get; set; }
        string dlv_doc_id { get; set; }
        string lineitem { get; set; }
        string shipsite { get; set; }
        string shipdate { get; set; }
        string trackingnumber { get; set; }
        string sls_ord_id { get; set; }
        string Sales_Org { get; set; }
        string Dist_Channel { get; set; }
        string soldto_id { get; set; }
        string shipto_id { get; set; }
        string Ship_To_Country { get; set; }
        string Last_Update_Date_Time { get; set; }
        string Product_Operation_Code { get; set; }
        string Base_Product_Name { get; set; }
        string Sales_Document_Type_Cd { get; set; }
        string Sales_Order_Reason_Code { get; set; }
        string Family_Code { get; set; }
        string SBS_Code { get; set; }
        string Product_Division_Code { get; set; }
        string Group_Code { get; set; }
        string Material_Type_Code { get; set; }
    }

    //Output
    public class SyntraceOutputResponse
    {
        public List<Result> Result { get; set; }
    }

    public class Result
    {
        string VENDOR_NAME { get; set; }
        string CHECKSUM { get; set; }
        string PART_NO { get; set; }
        string Mat_id { get; set; }
        string MAKER_DESC { get; set; }
        string sls_ord_id { get; set; }
        string Version { get; set; }
        string Base_Product_Name { get; set; }
        string SYSTEM_ASSEMBLY_SITE { get; set; }
        string VENDOR_CODE { get; set; }
        string Firmware { get; set; }
        string Group_Code { get; set; }
        string Pallet_ID { get; set; }
        string DATE_CODE { get; set; }
        string MAKER_PART_NO { get; set; }
        string mfg_pn { get; set; }
        string PART_DESC { get; set; }
        string UNITQTY { get; set; }
        string BOARD_ASSEMBLY_SITE { get; set; }
        string SBS_Code { get; set; }
        string LOT_CODE { get; set; }
        string PROGRAMED_FILE_NAME { get; set; }
        string Ship_To_Country { get; set; }
        string soldto_id { get; set; }
        string Carton_ID { get; set; }
        string PPID { get; set; }
        string shipsite { get; set; }
        string Site { get; set; }
        string LOCATION { get; set; }
        string REEL_CODE { get; set; }
        string PROGRAM_TIME { get; set; }
        string PROGRAM_LOCATION { get; set; }
        string SN_FLAG { get; set; }
        string CUSTOMER_PN { get; set; }
        string dlv_doc_id { get; set; }
        string Product_Division_Code { get; set; }
        string Cust_sn { get; set; }
        string vendor { get; set; }
        string PART { get; set; }
        string SLOT { get; set; }
        string trackingnumber { get; set; }
        string Sales_Document_Type_Cd { get; set; }
        string SN { get; set; }
        string PPPN { get; set; }
        string Coo_flag { get; set; }
        string Receipt_ID { get; set; }
        string shipdate { get; set; }
        string PROGRAM_STATUS { get; set; }
        string Product_Operation_Code { get; set; }
       string Fcst_prd_nm { get; set; }
        string BUILD_DATE { get; set; }
        string lineitem { get; set; }
        string Sales_Order_Reason_Code { get; set; }
        string Online_Offline { get; set; }
        string Work_Order { get; set; }
        string Dist_Channel { get; set; }
        string Sales_Org { get; set; }
        string Family_Code { get; set; }
        string Type { get; set; }
        string shipto_id { get; set; }
        string intel_pn { get; set; }
        string Last_Update_Date_Time { get; set; }
    }
}
