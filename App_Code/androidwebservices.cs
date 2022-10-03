using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

/// <summary>
/// Summary description for yours4organic
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class androidwebservices : System.Web.Services.WebService
{
    Connectioncls ConCls = new Connectioncls();
    Utility ud = new Utility();
    public androidwebservices()
    {
    }
    string ErrCode = "";
    private readonly string RESPONSESTATUS;

    [WebMethod]
    public void Notification(string CusID, string token)
    {
        try
        {
            if (CusID == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please enter Cust ID";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (CusID.All(char.IsDigit) == false)
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter Cust ID is only Number";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }

            string message1 = "";
            string ErrCode1 = "";
            string tokenno = token;
            string statusflag = "0";
            string serviceaction = "Notification";
            string APITitle = "Notification-API";
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                ConCls.Notification(CusID, tokenno, serviceaction, APITitle, out statusflag, out message1, out ErrCode1);

                if (statusflag == "1")
                {
                    using (SqlCommand cmd = new SqlCommand(" select slno, timestamp, subject, message , active, status  from  popup_personal_message where cusid = " + CusID + " and status != 'deleted' and active =1  order by slno DESC"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                dt.TableName = "popup_personal_message";
                                sda.Fill(dt);
                                var json = JsonConvert.SerializeObject(dt);
                                dynamic jsonResponse = JsonConvert.DeserializeObject(json);
                                JArray jarray = new JArray(jsonResponse);

                                ShowdataNew show = new ShowdataNew();
                                show.result = "SUCCESS";
                                show.Message = "Notification Get Successfully";
                                show.Statuscode = "200";
                                show.Data = jarray;
                                string json1 = JsonConvert.SerializeObject(show);
                                this.Context.Response.Write(json1);
                            }
                        }
                    }
                }
                else
                {
                    Service_Stop service = new Service_Stop();
                    service.result = "FAILED";
                    service.message = ErrCode1;
                    string json = JsonConvert.SerializeObject(service);
                    this.Context.Response.Write(json);
                }

            }
        }
        catch (Exception ex)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = ex.Message; ;
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }
    }

    [WebMethod]
    public void CheckLogin(string CusID, string Password, string token)
    {
        try
        {
            if (CusID == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please enter Cust ID";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (CusID.All(char.IsDigit) == false)
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter Cust ID is only Number";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            string message1 = "";
            string ErrCode1 = "";
            string tokenno = token;
            string statusflag = "0";
            string serviceaction = "CheckLogin";
            string APITitle = "CheckLogin-API";
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                ConCls.LOGWebApi(CusID, tokenno, serviceaction, APITitle, out statusflag, out message1, out ErrCode1);

                if (statusflag == "1")
                {
                    using (SqlCommand cmd = new SqlCommand(" select cast(cusid AS varchar(20)) AS UserID,Cust_Name AS Name,Cust_UserName AS UserName  from  Custrecords where cusid = " + CusID + " AND Cust_Password='" + Password + "'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                dt.TableName = "CheckLogin";
                                sda.Fill(dt);
                                var json = JsonConvert.SerializeObject(dt);
                                dynamic jsonResponse = JsonConvert.DeserializeObject(json);
                                JArray jarray = new JArray(jsonResponse);
                                ShowdataNew show = new ShowdataNew();
                                if (dt.Rows.Count == 0)
                                {
                                    show.result = "FAILED";
                                    show.Message = "Invalid User Name And Password";
                                    show.Statuscode = "404";
                                    show.Data = jarray;
                                }
                                else
                                {
                                    show.result = "SUCCESS";
                                    show.Message = "Login Successfully";
                                    show.Statuscode = "200";
                                    show.Data = jarray;
                                }

                                string json1 = JsonConvert.SerializeObject(show);
                                this.Context.Response.Write(json1);
                            }
                        }
                    }
                }
                else
                {
                    Service_Stop service = new Service_Stop();
                    service.result = "FAILED";
                    service.message = ErrCode1;
                    string json = JsonConvert.SerializeObject(service);
                    this.Context.Response.Write(json);
                }

            }
        }
        catch (Exception ex)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = ex.Message;
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }
    }

    [WebMethod]
    public void CheckSponserID(string CusID, string token)
    {
        try
        {
            if (CusID == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please enter Cust ID";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (CusID.All(char.IsDigit) == false)
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter Cust ID is only Number";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            string message1 = "";
            string ErrCode1 = "";
            string tokenno = token;
            string statusflag = "0";
            string serviceaction = "CheckSponserID";
            string APITitle = "CheckSponserID-API";
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                ConCls.LOGWebApi(CusID, tokenno, serviceaction, APITitle, out statusflag, out message1, out ErrCode1);

                if (statusflag == "1")
                {
                    using (SqlCommand cmd = new SqlCommand(" select cast(cusid AS varchar(20)),Cust_Name  from  Custrecords where cusid = " + CusID + ""))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                dt.TableName = "CheckSponserID";
                                sda.Fill(dt);
                                var json = JsonConvert.SerializeObject(dt);
                                dynamic jsonResponse = JsonConvert.DeserializeObject(json);
                                JArray jarray = new JArray(jsonResponse);
                                ShowdataNew show = new ShowdataNew();
                                if (dt.Rows.Count == 0)
                                {
                                    show.result = "FAILED";
                                    show.Message = "Affiliate ID not found";
                                    show.Statuscode = "404";
                                    show.Data = jarray;
                                }
                                else
                                {
                                    show.result = "SUCCESS";
                                    show.Message = "Sponser Name Get Successfully";
                                    show.Statuscode = "200";
                                    show.Data = jarray;
                                }
                                string json1 = JsonConvert.SerializeObject(show);
                                this.Context.Response.Write(json1);
                            }
                        }
                    }
                }
                else
                {
                    Service_Stop service = new Service_Stop();
                    service.result = "FAILED";
                    service.message = ErrCode1;
                    string json = JsonConvert.SerializeObject(service);
                    this.Context.Response.Write(json);
                }

            }
        }
        catch (Exception ex)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = ex.Message; ;
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }
    }

    [WebMethod]
    public void RegistrastionPre(string token, string CusID, string SponserID, string LeftRight, string Name, string Email, string Country, string CountryCode, string MobileNo, string Password, string TxPassword)
    {
        try
        {
            if (CusID == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please enter Cust ID";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (CusID.All(char.IsDigit) == false)
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter Cust ID is only Number";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (Name.Trim().ToString() == "" || Email.Trim().ToString() == "" || Password.Trim().ToString() == "" || TxPassword.Trim().ToString() == "" || SponserID.Trim().ToString() == "" || MobileNo.Trim() == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter All Values";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            string message1 = "";
            string ErrCode1 = "";
            string tokenno = token;
            string statusflag = "0";
            string serviceaction = "RegistrastionPre";
            string APITitle = "RegistrastionPre-API";
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                ConCls.LOGWebApi(SponserID, tokenno, serviceaction, APITitle, out statusflag, out message1, out ErrCode1);

                if (statusflag == "1")
                {
                    string Newid = "0";
                    string Errocode = "";
                    ConCls.inserrtnewcustrecords(SponserID, Email, "", "", "", "", "", Country, Password, "", Name, "", "", "", "", MobileNo, "", "", "", "", "", 0, "", "", "", "", "", "", TxPassword, out Newid, out Errocode);
                    if (Newid.Length > 4)
                    {
                        string rotp = "";
                        rotp = Newid.ToString();
                        ud.SendSMS(Newid, Name, Email, "", rotp);
                        var result = char.ConvertFromUtf32(34);
                        var json = "[{" + result + "CustID:" + result + ":" + result + Newid + result + "," + result + "OTP:" + result + ":" + result + rotp + result + "," + result + "SponserID:" + result + ":" + result + SponserID + result + "}]";
                        dynamic jsonResponse = JsonConvert.DeserializeObject(json);
                        JArray jarray = new JArray(jsonResponse);
                        ShowdataNew show = new ShowdataNew();
                        show.result = "SUCCESS";
                        show.Message = "Send OTP Successfully";
                        show.Statuscode = "200";
                        show.Data = jarray;
                        string json1 = JsonConvert.SerializeObject(show);
                        this.Context.Response.Write(json1);
                    }
                }
                else
                {
                    Service_Stop service = new Service_Stop();
                    service.result = "FAILED";
                    service.message = ErrCode1;
                    string json = JsonConvert.SerializeObject(service);
                    this.Context.Response.Write(json);
                }

            }
        }
        catch (Exception ex)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = ex.Message;
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }
    }

    [WebMethod]
    public void Registrastion(string token, string CusID, string SponserID, string OTP)
    {
        try
        {
            if (CusID == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please enter Cust ID";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (CusID.All(char.IsDigit) == false)
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter Cust ID is only Number";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            if (token.Trim().ToString() == "" || CusID.Trim().ToString() == "" || OTP.Trim().ToString() == "" || SponserID.Trim().ToString() == "")
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Please Enter All Values";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
            string message1 = "";
            string ErrCode1 = "";
            string tokenno = token;
            string statusflag = "0";
            string serviceaction = "Registrastion";
            string APITitle = "Registrastion-API";
            string constr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                ConCls.LOGWebApi(SponserID, tokenno, serviceaction, APITitle, out statusflag, out message1, out ErrCode1);

                if (statusflag == "1")
                {
                    int flag = 0; string errMsg = "0";
                    ConCls.verification_byregistration(OTP, out flag, out errMsg);
                    string NewID = "";
                    NewID = flag.ToString();
                    if (NewID.Length > 4)
                    {
                        string xxx_xx = "";
                        xxx_xx = errMsg.ToString().Trim();
                        if (xxx_xx.ToString() == "SUCCESS")
                        {
                            string _Cust_Name = "";
                            string _email = "";
                            string _pass = "";
                            string txpws = "";
                            SqlDataReader sdr1 = ConCls.GetdataReader("SELECT * from CustRecords where cusid = " + NewID);
                            if (sdr1.HasRows)
                            {
                                if (sdr1.Read())
                                {
                                    _email = sdr1["Email"].ToString();
                                    _Cust_Name = sdr1["cust_name"].ToString();
                                    _pass = sdr1["Cust_Password"].ToString();
                                    txpws = sdr1["Cust_Title"].ToString();
                                }
                            }
                            ud.WelcomeSendSMS(NewID, _Cust_Name, _email, _pass, txpws);
                            var result = char.ConvertFromUtf32(34);
                            var json = "[{" + result + "CustID:" + result + ":" + result + NewID + result + "," + result + "cust_name:" + result + ":" + result + _Cust_Name + result + "," + result + "Password:" + result + ":" + result + _pass + result + "}]";
                            dynamic jsonResponse = JsonConvert.DeserializeObject(json);
                            JArray jarray = new JArray(jsonResponse);
                            ShowdataNew show = new ShowdataNew();
                            show.result = "SUCCESS";
                            show.Message = "Registrastion Successfully";
                            show.Statuscode = "200";
                            show.Data = jarray;
                            string json1 = JsonConvert.SerializeObject(show);
                            this.Context.Response.Write(json1);
                        }
                    }
                }
                else
                {
                    Service_Stop service = new Service_Stop();
                    service.result = "FAILED";
                    service.message = ErrCode1;
                    string json = JsonConvert.SerializeObject(service);
                    this.Context.Response.Write(json);
                }

            }
        }
        catch (Exception ex)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = ex.Message;
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }
    }

    [WebMethod]
    public void geteway5050dapp(string OrderID, string Security_Key)
    {
        if (OrderID == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Order ID";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID.All(char.IsDigit) == false)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Enter OrderID is only Number";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (ConCls.getScalerValue("Select ISNULL(COUNT(*),0) From gatewayfiftyfityRequest where OrderID =" + OrderID.ToString()) == "0")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Order ID does  not Exist..!";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (Security_Key == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Security_Key";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        string orderid1 = OrderID;
        string security_key = Security_Key;
        string err = "0";
        string fullQuery = "";
        string fullresult = "";

        if (security_key == "123456")
        {
            fullQuery = "select custid,OrderID,TxDatetime,Total_Amt,Pay_Mode,Coin1,Coin2,Coin1_Amt,Coin2_Amt,Coin1_Rate,Coin2_Rate,Coin1_Tx,Coin2_Tx ";
            fullQuery = fullQuery + ", Coin1_Status,Coin2_Status,Final_Status,Final_TxDatetime,Coin1_APres,Coin2_APres,Final_APres ";
            fullQuery = fullQuery + " from gatewayfiftyfityRequest where OrderID =" + orderid1.ToString();
            var result = char.ConvertFromUtf32(34);
            SqlDataReader sdr_privkey = ConCls.GetdataReader(fullQuery);
            if (sdr_privkey.HasRows)
            {
                if (sdr_privkey.Read())
                {
                    var json = "[{" + result + "customerid" + result + ":" + result + sdr_privkey["custid"].ToString() + result + "," +
                        "" + result + "OrderID" + result + ":" + result + sdr_privkey["OrderID"].ToString() + result + "," +
                        "" + result + "TxDatetime" + result + ":" + result + sdr_privkey["TxDatetime"].ToString() + result + "," +
                        "" + result + "Total_Amt" + result + ":" + result + sdr_privkey["Total_Amt"].ToString() + result + "," +
                        "" + result + "Pay_Mode" + result + ":" + result + sdr_privkey["Pay_Mode"].ToString() + result + "," +
                        "" + result + "Coin1" + result + ":" + result + sdr_privkey["Coin1"].ToString() + result + "," +
                        "" + result + "Coin2" + result + ":" + result + sdr_privkey["Coin2"].ToString() + result + "," +
                        "" + result + "Coin1_Amt" + result + ":" + result + sdr_privkey["Coin1_Amt"].ToString() + result + "," +
                        "" + result + "Coin2_Amt" + result + ":" + result + sdr_privkey["Coin2_Amt"].ToString() + result + "," +
                        "" + result + "Coin1_Rate" + result + ":" + result + sdr_privkey["Coin1_Rate"].ToString() + result + "," +
                        "" + result + "Coin2_Rate" + result + ":" + result + sdr_privkey["Coin2_Rate"].ToString() + result + "," +
                        "" + result + "Coin1_Tx" + result + ":" + result + sdr_privkey["Coin1_Tx"].ToString() + result + "," +
                        "" + result + "Coin2_Tx" + result + ":" + result + sdr_privkey["Coin2_Tx"].ToString() + result + "," +
                        "" + result + "CoinA_Status" + result + ":" + result + sdr_privkey["Coin1_Status"].ToString() + result + "," +
                        "" + result + "CoinB_Status" + result + ":" + result + sdr_privkey["Coin2_Status"].ToString() + result + "," +
                        "" + result + "Final_Status" + result + ":" + result + sdr_privkey["Final_Status"].ToString() + result + "," +
                        "" + result + "Final_TxDatetime" + result + ":" + result + sdr_privkey["Final_TxDatetime"].ToString() + result + "," +
                        "" + result + "Coin1_APres" + result + ":" + result + sdr_privkey["Coin1_APres"].ToString() + result + "," +
                        "" + result + "Coin2_APres" + result + ":" + result + sdr_privkey["Coin2_APres"].ToString() + result + "," +
                        "" + result + "Final_APres" + result + ":" + result + sdr_privkey["Final_APres"].ToString() + result + "}]";
                    fullresult = json;
                }

            }
            sdr_privkey.Close();
            sdr_privkey.Dispose();

            dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(fullresult);
            Newtonsoft.Json.Linq.JArray jarray = new Newtonsoft.Json.Linq.JArray(jsonResponse);
            ShowdataNew show = new ShowdataNew();
            show.result = "SUCCESS";
            show.Message = "GET Data  Successfully";
            show.Statuscode = "200";
            show.Data = jsonResponse;
            string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(show);
            this.Context.Response.Write(json1.ToString());
        }
        else
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Wrong Security Key ";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
    }

    [WebMethod]
    public void geteway5050dappCoin1(string OrderID, string CustID, string Security_Key, string CoinName1, string Coin1_Amt, string Coin1_Rate, string Coin1_Tx, string Coin1_Status)
    {
        if (OrderID == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Order ID";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID.All(char.IsDigit) == false)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Enter OrderID is only Number";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (ConCls.getScalerValue("Select ISNULL(COUNT(*),0) From gatewayfiftyfityRequest where OrderID =" + OrderID.ToString()) == "0")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Order ID does  not Exist..!";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (Security_Key == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Security_Key";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID == "" || CustID == "" || CoinName1 == "" || Coin1_Amt == "" || Coin1_Rate == "" || Coin1_Tx == "" || Coin1_Status == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Send All Field";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        string orderid1 = OrderID;
        string security_key = Security_Key;
        string err = "0";
        string fullQuery = "";
        string fullresult = "";

        if (security_key == "123456")
        {
            string Coin1_APres = "FAILED";
            if (Coin1_Status == "1")
            {
                Coin1_APres = "SUCCESS";
            }
            var result = char.ConvertFromUtf32(34);
            fullQuery = "Update gatewayfiftyfityRequest SET Coin1_Amt=" + Coin1_Amt + ",Coin1_Rate=" + Coin1_Rate + ",Coin1_Tx='" + Coin1_Tx + "',Coin1_Status=" + Coin1_Status + ",Coin1_APres='" + Coin1_APres + "' where OrderID=" + OrderID + "";
            if (ConCls.ExecuteSqlnonQuery(fullQuery) > 0)
            {
                var json = "[{" + result + "customerid" + result + ":" + result + CustID.ToString() + result + "," +
                            "" + result + "OrderID" + result + ":" + result + OrderID.ToString() + result + "," +
                            "" + result + "Coin1_APres" + result + ":" + result + Coin1_APres.ToString() + result + "}]";
                fullresult = json;
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(fullresult);
                Newtonsoft.Json.Linq.JArray jarray = new Newtonsoft.Json.Linq.JArray(jsonResponse);
                ShowdataNew show = new ShowdataNew();
                show.result = "SUCCESS";
                show.Message = "Coin 1 Data Save Successfully";
                show.Statuscode = "200";
                show.Data = jsonResponse;
                string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(show);
                this.Context.Response.Write(json1.ToString());
            }
            else
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Data Coin 1 Updation FAILED ";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
        }
        else
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Wrong Security Key ";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
    }

    [WebMethod]
    public void geteway5050dappCoin2(string OrderID, string CustID, string Security_Key, string CoinName2, string Coin2_Amt, string Coin2_Rate, string Coin2_Tx, string Coin2_Status)
    {
        if (OrderID == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Order ID";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID.All(char.IsDigit) == false)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Enter OrderID is only Number";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (ConCls.getScalerValue("Select ISNULL(COUNT(*),0) From gatewayfiftyfityRequest where OrderID =" + OrderID.ToString()) == "0")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Order ID does  not Exist..!";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (Security_Key == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Security_Key";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID == "" || CustID == "" || CoinName2 == "" || Coin2_Amt == "" || Coin2_Rate == "" || Coin2_Tx == "" || Coin2_Status == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Send All Field";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        string orderid1 = OrderID;
        string security_key = Security_Key;
        string err = "0";
        string fullQuery = "";
        string fullresult = "";

        if (security_key == "123456")
        {
            string Coin2_APres = "FAILED";
            if (Coin2_Status == "1")
            {
                Coin2_APres = "SUCCESS";
            }
            var result = char.ConvertFromUtf32(34);
            fullQuery = "Update gatewayfiftyfityRequest SET Coin2_Amt=" + Coin2_Amt + ",Coin2_Rate=" + Coin2_Rate + ",Coin2_Tx='" + Coin2_Tx + "',Coin2_Status=" + Coin2_Status + ",Coin2_APres='" + Coin2_APres + "' where OrderID=" + OrderID + "";
            if (ConCls.ExecuteSqlnonQuery(fullQuery) > 0)
            {
                var json = "[{" + result + "customerid" + result + ":" + result + CustID.ToString() + result + "," +
                            "" + result + "OrderID" + result + ":" + result + OrderID.ToString() + result + "," +
                            "" + result + "Coin2_APres" + result + ":" + result + Coin2_APres.ToString() + result + "}]";
                fullresult = json;
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(fullresult);
                Newtonsoft.Json.Linq.JArray jarray = new Newtonsoft.Json.Linq.JArray(jsonResponse);
                ShowdataNew show = new ShowdataNew();
                show.result = "SUCCESS";
                show.Message = "Coin 2 Data Save Successfully";
                show.Statuscode = "200";
                show.Data = jsonResponse;
                string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(show);
                this.Context.Response.Write(json1.ToString());
            }
            else
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Data Coin 2 Updation FAILED ";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
        }
        else
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Wrong Security Key ";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
    }

    [WebMethod]
    public void geteway5050dappFinal(string OrderID, string CustID, string Security_Key, string Final_Status)
    {
        if (OrderID == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Order ID";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID.All(char.IsDigit) == false)
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Enter OrderID is only Number";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (ConCls.getScalerValue("Select ISNULL(COUNT(*),0) From gatewayfiftyfityRequest where OrderID =" + OrderID.ToString()) == "0")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Order ID does  not Exist..!";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (Security_Key == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please enter Security_Key";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        if (OrderID == "" || CustID == "" || Final_Status == "")
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Please Send All Field";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
        string orderid1 = OrderID;
        string security_key = Security_Key;
        string err = "0";
        string fullQuery = "";
        string fullresult = "";

        if (security_key == "123456")
        {
            string Final_APres = "FAILED";
            if (Final_Status == "1")
            {
                Final_APres = "SUCCESS";
            }
            var result = char.ConvertFromUtf32(34);
            fullQuery = "Update gatewayfiftyfityRequest SET Final_Status=" + Final_Status + ",Final_APres='" + Final_APres + "',Final_TxDatetime=GETDATE() where OrderID=" + OrderID + "";
            if (ConCls.ExecuteSqlnonQuery(fullQuery) > 0)
            {
                var json = "[{" + result + "customerid" + result + ":" + result + CustID.ToString() + result + "," +
                            "" + result + "OrderID" + result + ":" + result + OrderID.ToString() + result + "," +
                            "" + result + "Final_APres" + result + ":" + result + Final_APres.ToString() + result + "}]";
                fullresult = json;
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(fullresult);
                Newtonsoft.Json.Linq.JArray jarray = new Newtonsoft.Json.Linq.JArray(jsonResponse);
                ShowdataNew show = new ShowdataNew();
                show.result = "SUCCESS";
                show.Message = "Final Data Save Successfully";
                show.Statuscode = "200";
                show.Data = jsonResponse;
                string json1 = Newtonsoft.Json.JsonConvert.SerializeObject(show);
                this.Context.Response.Write(json1.ToString());
            }
            else
            {
                Service_Stop service = new Service_Stop();
                service.result = "FAILED";
                service.message = "Data Final Updation FAILED ";
                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
                return;
            }
        }
        else
        {
            Service_Stop service = new Service_Stop();
            service.result = "FAILED";
            service.message = "Wrong Security Key ";
            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
            return;
        }
    }




    #region gatewayfiftyfiftyC1
    [WebMethod]
    public void GatewayFiftyFiftyC1(string CustID, string OrderID)
    {
        string Coin1 = "Matic";
        string Coin2 = "Tbac";
        string message1 = "";
        string ErrCode1 = "";
        ////string tokenno = "";
        //string serviceaction = "gatewayfiftyfiftyC1";
        //string statusflag = "0";
        //ConCls.tokencheck(CusID, tokenno, serviceaction, out statusflag, out message1, out ErrCode1);

        NameValueCollection Parameterslist = System.Web.HttpUtility.ParseQueryString(string.Empty);

        Parameterslist["CustID"] = CustID.ToString();
        Parameterslist["OrderID"] = OrderID.ToString();
        Parameterslist["TxDatetime"] = DateTime.Now.ToString();



        WebClient c_euro = new WebClient();


        ////NEED TO EDIT LINK
        //string link = "http://staging.mrupay.in//Api/smartdigital.asmx/gatewayfiftyfiftyC1?" + Parameterslist.ToString();
        //string data_euro = c_euro.DownloadString("http://staging.mrupay.in//Api/smartdigital.asmx/gatewayfiftyfiftyC1?" + Parameterslist.ToString());

        //XDocument xml = XDocument.Parse(data_euro);

        //List<string> list = xml.Descendants("RESPONSESTATUS").Select(e => e.Value).ToList();

        //string RESPONSESTATUS = string.Join(Environment.NewLine, list.ToArray());

        //List<string> list1 = xml.Descendants("MESSAGE").Select(e => e.Value).ToList();

        //string MESSAGE = string.Join(Environment.NewLine, list1.ToArray());


        if (RESPONSESTATUS == "0")
        {
            gatewayfiftyfiftyC1 service = new gatewayfiftyfiftyC1();
            service.MESSAGE = "ERROR!!";

            string json = JsonConvert.SerializeObject(service);
            this.Context.Response.Write(json);
        }

        else if (RESPONSESTATUS == "1")
        {
            if (Coin1.Equals("Matic"))
            {

                ConCls.ExecuteSqlnonQuery("update gatewayfiftyfityRequest set Coin1_Rate=@Coin1_Rate, Coin1_Tx=@Coin1_Tx, Coin1_Status=@Coin1_Status, Coin1_APres=@Coin1_APres where custid=" + CustID.ToString() + "and OrderID" + OrderID.ToString());
                gatewayfiftyfiftyC1 service = new gatewayfiftyfiftyC1();
                service.MESSAGE = "Updated C1 Coin  Successfully !!";

                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
            }
            else
            {
                gatewayfiftyfiftyC1 service = new gatewayfiftyfiftyC1();
                service.MESSAGE = "FAILED -1 !!";

                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
            }

        }
        {
            if (Coin2.Equals("Tbac"))
            {

                ConCls.ExecuteSqlnonQuery("update gatewayfiftyfityRequest set Coin2_Rate=@Coin2_Rate, Coin2_Tx=@Coin2_Tx, Coin2_Status=@Coin2_Status, Coin2_APres=@Coin2_APres where custid=" + CustID.ToString() + "and OrderID" + OrderID.ToString());


                gatewayfiftyfiftyC1 service = new gatewayfiftyfiftyC1();
                service.MESSAGE = "Updated C2 Coin  Successfully !!";

                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
            }
            else
            {
                gatewayfiftyfiftyC1 service = new gatewayfiftyfiftyC1();
                service.MESSAGE = "FAILED - 2 !!";

                string json = JsonConvert.SerializeObject(service);
                this.Context.Response.Write(json);
            }

        }
    }
    #endregion


}
