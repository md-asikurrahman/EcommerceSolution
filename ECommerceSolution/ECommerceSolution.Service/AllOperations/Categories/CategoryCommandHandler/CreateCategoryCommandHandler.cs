using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using ECommerceSolution.Service.CommonServices;
using ECommerceSolution.Service.AllOperations.Categories.CategoryCommand;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryCommandHandler
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IList<string>>
    {
        public async Task<IList<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            NotificationModel nModel = new NotificationModel();
            DataSet dataSet = new DataSet();
            ParameterList parameterList = new ParameterList();
            string storedProcedureName = "SP_Create_Category";
            parameterList.Add("@CategoryName", request.CategoryName!, SqlDbType.NVarChar);
            parameterList.Add("@ParentCategoryId", Convert.ToString(request.ParentCategoryId)!, SqlDbType.NVarChar);
            parameterList.Add("@CategoryDescription", request.CategoryDescription!, SqlDbType.NVarChar);
            parameterList.Add("@IsActive", request.IsActive, SqlDbType.Bit);
            parameterList.Add("@ImageUrl", request.ImageUrl!, SqlDbType.NVarChar);
            parameterList.Add("@SerialNumber", request.SerialNumber, SqlDbType.NVarChar);
            parameterList.Add("@CreatedBy", request.CreatedBy!, SqlDbType.NVarChar);

            var resultList = new List<string>();
            try
            {
                dataSet = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameterList);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        NotificationModel model = new NotificationModel();
                        model.ErrorCode = dataSet.Tables[0].Rows[i][0].ToString()!;
                        model.StrErrorMessage = dataSet.Tables[0].Rows[i][1].ToString()!;
                        nModel = model;
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
