using SAP_Integration.Core.Configurations;
using SAPbobsCOM;
using System;

namespace SAP_Integration.Core.Infrastructure
{
    public class SapConnector
    {
        private static Company _company;

        public static Company GetCompany()
        {
            // chưa connect thì mới chạy vô đây
            if (_company == null || !_company.Connected)
            {
                _company = new Company();
                _company.Server = AppSetting.SapServer;
                _company.LicenseServer = AppSetting.SapLicenseServer;
                _company.CompanyDB = AppSetting.SapCompanyDB;
                _company.UserName = AppSetting.SapUserName;
                _company.Password = AppSetting.SapPassword;
                _company.DbServerType = BoDataServerTypes.dst_HANADB;
                int connectionResult = _company.Connect();
                //_company.Connect() return interger  = 0 là connect successs còn != là code error
                if (connectionResult != 0)
                {

                    _company.GetLastError(out int errCode, out string errMsg);

                    throw new Exception($"SAP Connection Failed! Error Code: {errCode}. Message: {errMsg}");
                }
            }

            return _company;
        }
    }
}
