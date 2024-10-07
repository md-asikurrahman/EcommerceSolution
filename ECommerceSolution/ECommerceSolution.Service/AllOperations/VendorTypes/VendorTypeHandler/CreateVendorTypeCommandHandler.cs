using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using ECommerceSolution.Service.CommonServices;
using ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeCommand;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeHandler
{
    public class CreateVendorTypeCommandHandler : IRequestHandler<CreateVendorTypeCommand, List<string>>
    {
        public async Task<List<string>> Handle(CreateVendorTypeCommand request, CancellationToken cancellationToken)
        {
            NotificationModel nModel = new NotificationModel();
            DataSet dataSet = new DataSet();
            ParameterList parameterList = new ParameterList();
            string storedProcedureName = "SP_Create_VendorType";
         
            parameterList.Add("@VendorTypeName", request.TypeName, SqlDbType.NVarChar);          
            parameterList.Add("@IsActive", request.IsActive, SqlDbType.Bit);
            parameterList.Add("@SerialNumber", request.SerialNumber, SqlDbType.NVarChar);
            parameterList.Add("@CreatedBy", request.CreatedBy, SqlDbType.NVarChar);

            var resultList = new List<string>();

            try
            {
                dataSet = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameterList);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        NotificationModel model = new NotificationModel();
                        model.ErrorCode = dataSet.Tables[0].Rows[i][0].ToString();
                        model.StrErrorMessage = dataSet.Tables[0].Rows[i][1].ToString();
                        nModel = model;
                    }
                }

                resultList.Add($"Error Code: {nModel.ErrorCode}, Error Message: {nModel.StrErrorMessage}");

                return resultList;
            }
            catch
            {
                return null;
            }
        }
    }
}
