using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using MediatR;
using System.Data;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryQuery
{
    public record class GetCategoryByIdQuery(int Id):IRequest<CategoryModelDto>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryModelDto>
    {
        public async Task<CategoryModelDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            CategoryModelDto data = new CategoryModelDto();

            parameters.Add("@Id", request.Id, SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetCategoryById";
            try
            {
                ds = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CategoryModelDto dataObj = new CategoryModelDto();
                        dataObj.Id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                        dataObj.ParentCategoryId = int.Parse(ds.Tables[0].Rows[i]["ParentCategoryId"].ToString());
                        dataObj.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                        dataObj.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"].ToString();
                        dataObj.CategoryDescription = ds.Tables[0].Rows[i]["CategoryDescription"].ToString();
                        dataObj.SerialNumber = int.Parse(ds.Tables[0].Rows[i]["SerialNumber"].ToString());
                        dataObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                        dataObj.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                        dataObj.CreatedAt = ds.Tables[0].Rows[i]["CreatedAt"].ToString();
                        dataObj.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"].ToString();
                        dataObj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        data = dataObj;
                    }
                }

                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
