using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Connection
/// </summary
/// >
/// 

public class Connectioncls
{

    //private ClsDataAccess objClsDataccess = new ClsDataAccess(@"Server=SYS04;Database=zondaDaily;integrated security=true");

    // public DataSet Countrow;
    //  public SqlDataAdapter adpt;


    //   public SqlConnection connc = new SqlConnection(ConfigurationManager.AppSettings["conc"]);
    //   public SqlCommand cmd_connc = new SqlCommand();[spUpdate_BIXC_Entry_New_Success_Pending]
    //   public SqlDataReader cmd_reader;

    //private ClsDataAccess objClsDataccess = new ClsDataAccess(@"Server=SYS04;Database=zondaDaily;integrated security=true");

    private dal objClsDataccess = new dal(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString());
    //   private ClsDataAccess objClsDataccess = new ClsDataAccess(ConfigurationManager.AppSettings[0].ToString());

    public SqlDataReader GetdataReader(string strSqlSelectCommand)
    {

        SqlDataReader sdr = objClsDataccess.GetSqlDataReader(strSqlSelectCommand);
        return sdr;
    }

    public int ExecuteSqlnonQuery(string strSqldmlCommand)
    {
        return objClsDataccess.ExecuteDML(strSqldmlCommand);
    }

    public string getScalerValue(string strSqldmlCommand)
    {
        return objClsDataccess.ExecuteScalerQuery(strSqldmlCommand);
    }

    #region Notification

    public int Notification(string CusID, string tokenno, string servicecall, string APITitle, out string flag, out string message1, out string ErrCode1)
    {
        return objClsDataccess.Notification(CusID, tokenno, servicecall, APITitle, out flag, out message1, out ErrCode1);
    }

    #endregion

    #region LOGWebApi

    public int LOGWebApi(string CusID, string tokenno, string servicecall, string APITitle, out string flag, out string message1, out string ErrCode1)
    {
        return objClsDataccess.LOGWebApi(CusID, tokenno, servicecall, APITitle, out flag, out message1, out ErrCode1);
    }

    #endregion

    #region Preinserrtnewcustrecords

    public int inserrtnewcustrecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, string Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, string _SkypeID, out string NewID, out string ErrCode)
    {
        int i = objClsDataccess.InsertCustRecords(Cust_SponserID, Email, Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_State, Cust_Country, Cust_Password, Cust_Title, Cust_Name, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_mobileNo, Cust_nominee, Cust_Relation, Cust_Package, Cust_Location, Cust_TempPinID, PayMode, Cust_BankName, Cust_BankAcc, Cust_BankIFSC, Cust_BankBranch, Cust_PanID, _custusername, _SkypeID, out NewID, out ErrCode);
        return i;
    }

    #endregion

    #region verification_byregistration

    public int verification_byregistration(string cusid, out int flag, out string errMsg)
    {
        return objClsDataccess.verification_byregistration(cusid, out flag, out errMsg);
    }

    #endregion

    #region GatewayFiftyFiftyC1

    public int GatewayFiftyFiftyC1(string CustID, string OrderID)
    {
        return objClsDataccess.GatewayFiftyFiftyC1( CustID,  OrderID);
    }
    #endregion


}