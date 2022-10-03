using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for dal
/// </summary>
/// 


public class dal
{
    #region Public Constructor , takes one argument as sqlconnection string

    /// <summary>
    /// Public Constructor , takes one argument as sqlconnection string
    /// </summary>
    /// <param name="SQLconnectionString">SQLconnectionString</param>
    /// 
    public string ConnectionString1 = "";
    private string message;
    private string ErrCode;
    public dal(string SQLconnectionString)
    {
        ConnectionString1 = SQLconnectionString;
    }
    #endregion

    public SqlDataReader GetSqlDataReader(string SqlSelectCommand)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            SqlCommand cmd = new SqlCommand(SqlSelectCommand, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }
        catch (SqlException ex)
        {
            conc.Close();
            return null;
        }
    }

    #region Executes Non-Query Statements

    public int ExecuteDML(string SqlDMLcmd)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            int returnval = 0;
            SqlCommand cmd = new SqlCommand(SqlDMLcmd, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            returnval = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conc.Close();
            return returnval;
        }
        catch (SqlException ex)
        {
            return ex.ErrorCode;
        }
        finally
        {
            conc.Close();
        }
    }
    #endregion

    #region ExecuteScalerQuery(string strSqlSelectCommand)

    public string ExecuteScalerQuery(string strSqlSelectCommand)
    {

        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            string returnval = string.Empty;
            SqlCommand cmd = new SqlCommand(strSqlSelectCommand, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            returnval = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            conc.Close();
            return returnval;
        }
        catch (SqlException ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Notification

    public int Notification(string CusID, string tokenno, string servicecall, string @APITitle, out string errflag, out string message1, out string ErrCode1)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            SqlCommand cmd = new SqlCommand("LOGWebApi", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOGWebApi";
            cmd.Parameters.AddWithValue("@CusID", CusID);
            cmd.Parameters.AddWithValue("@tokenno", tokenno);
            cmd.Parameters.AddWithValue("@servicecall", servicecall);
            cmd.Parameters.AddWithValue("@APITitle", APITitle);
            // conc.Open();

            SqlParameter SqlParaErrCode = new SqlParameter("@message1", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = 0;

            SqlParameter SqlParaErrCode1 = new SqlParameter("@ErrCode1", SqlDbType.VarChar, 500);
            SqlParaErrCode1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode1).Value = 0;

            SqlParameter SqlParaflag = new SqlParameter("@flag", SqlDbType.VarChar, 500);
            SqlParaflag.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaflag).Value = 0;

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            message1 = (string)SqlParaErrCode.Value;
            ErrCode1 = (string)SqlParaErrCode1.Value;
            errflag = (string)SqlParaflag.Value;

            return value;

        }
        catch (Exception ex)
        {
            message1 = "0";
            ErrCode1 = ex.Message;
            errflag = "0";
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }

    #endregion

    #region LOGWebApi

    public int LOGWebApi(string CusID, string tokenno, string servicecall, string APITitle, out string errflag, out string message1, out string ErrCode1)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            SqlCommand cmd = new SqlCommand("LOGWebApi", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOGWebApi";
            cmd.Parameters.AddWithValue("@CusID", CusID);
            cmd.Parameters.AddWithValue("@tokenno", tokenno);
            cmd.Parameters.AddWithValue("@servicecall", servicecall);
            cmd.Parameters.AddWithValue("@APITitle", APITitle);
            // conc.Open();

            SqlParameter SqlParaErrCode = new SqlParameter("@message1", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = 0;

            SqlParameter SqlParaErrCode1 = new SqlParameter("@ErrCode1", SqlDbType.VarChar, 500);
            SqlParaErrCode1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode1).Value = 0;

            SqlParameter SqlParaflag = new SqlParameter("@flag", SqlDbType.VarChar, 500);
            SqlParaflag.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaflag).Value = 0;

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            message1 = (string)SqlParaErrCode.Value;
            ErrCode1 = (string)SqlParaErrCode1.Value;
            errflag = (string)SqlParaflag.Value;

            return value;

        }
        catch (Exception ex)
        {
            message1 = "0";
            ErrCode1 = ex.Message;
            errflag = "0";
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }

    #endregion

    #region InsertCustRecords

    public int InsertCustRecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, string Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, string _SkypeID, out string NewID, out string ErrCode)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_virthnext_onlyprereg]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
            cmd.Parameters.Add(new SqlParameter("@Email", Email));
            cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
            cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
            cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
            cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
            cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
            cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
            cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
            cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
            cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
            cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
            cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
            cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
            cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
            cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
            cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
            cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
            cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
            cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));
            cmd.Parameters.Add(new SqlParameter("@SkypeID", _SkypeID));
            cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
            cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
            cmd.Parameters.Add(new SqlParameter("@custusername", _custusername));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = "";

            SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = "";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int order = cmd.ExecuteNonQuery();
            NewID = (string)SqlParaNewID.Value;
            ErrCode = (string)SqlParaErrCode.Value;
            cmd.Dispose();
            conc.Close();
            return order;
        }
        catch (Exception ex)
        {
            NewID = "0";
            ErrCode = ex.Message.ToString();
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region verification_byregistration

    public int verification_byregistration(string cusid, out int NewID, out string ErrCode)
    {
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_kalmanserve_alinkver]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            NewID = (int)SqlParaNewID.Value;
            ErrCode = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            NewID = 0;
            ErrCode = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }

    #endregion

    #region GatewayFiftyFiftyC1

    public int GatewayFiftyFiftyC1(string CustID, string OrderID)
    {
	
        SqlConnection conc = new SqlConnection(ConnectionString1);
        try
        {
            // SqlCommand cmd = new SqlCommand("api_login", conc);
            // cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.AddWithValue("@CusId", CustID).ToString();
            cmd.Parameters.AddWithValue("@OrderID", OrderID).ToString();


            conc.Open();

            SqlParameter SqlParaErrCode = new SqlParameter("@message", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = 0;

            SqlParameter SqlParaErrCode1 = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrCode1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode1).Value = 0;



            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            message = (string)SqlParaErrCode.Value;
            ErrCode = (string)SqlParaErrCode1.Value;


            return value;

        }
        catch (Exception ex)
        {
            message = "0";
            ErrCode = ex.Message;

            return 0;
        }
        finally
        {
            conc.Close();
        }
    }
    #endregion


}