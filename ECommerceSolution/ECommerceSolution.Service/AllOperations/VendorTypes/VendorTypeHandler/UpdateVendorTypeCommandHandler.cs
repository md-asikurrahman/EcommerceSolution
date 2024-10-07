using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using ECommerceSolution.Service.CommonServices;
using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeHandler
{
    public class UpdateVendorTypeCommandHandler : IRequestHandler<UpdateVendorTypeCommand, List<string>>
    {
        public async Task<List<string>> Handle(UpdateVendorTypeCommand request, CancellationToken cancellationToken)
        {
            NotificationModel nModel = new NotificationModel();
            DataSet ds = new DataSet();
            ParameterList parameterList = new ParameterList();
            string storedProcedureName = "SP_Update_VendorType";

            parameterList.Add("@Id", request.Id, SqlDbType.BigInt);
            parameterList.Add("@TypeName", request.TypeName, SqlDbType.NVarChar);
            parameterList.Add("@CreatedAt", request.CreatedAt.Trim(), SqlDbType.NVarChar);
            parameterList.Add("@CreatedBy", request.CreatedBy, SqlDbType.NVarChar);
            parameterList.Add("@UpdatedBy", request.UpdatedBy, SqlDbType.NVarChar);
            parameterList.Add("@IsActive", request.IsActive, SqlDbType.Bit);
            parameterList.Add("@SerialNumber", request.SerialNumber, SqlDbType.NVarChar);
            var resultList = new List<string>();

            try
            {
                ds = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameterList);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        NotificationModel dataObj = new NotificationModel();
                        dataObj.ErrorCode = ds.Tables[0].Rows[i][0].ToString();
                        dataObj.StrErrorMessage = ds.Tables[0].Rows[i][1].ToString();
                        nModel= dataObj;
                    }
                }
                resultList.Add($"Error Code: {nModel.ErrorCode}, Error Message: {nModel.StrErrorMessage}");

                return resultList;
            }
            catch (Exception ex)
            {
                resultList.Add(ex.Message);
                return resultList;
            }
        }
    }
}
