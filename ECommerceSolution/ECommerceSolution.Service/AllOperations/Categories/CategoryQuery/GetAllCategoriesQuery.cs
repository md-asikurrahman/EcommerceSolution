using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryQuery
{
    public record class GetAllCategoriesQuery : IRequest<IList<CategoryModelDto>>;

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery,IList<CategoryModelDto>>
    {
        public async Task<IList<CategoryModelDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            List<CategoryModelDto> data = new List<CategoryModelDto>();

            parameters.Add("@QueryExtension", "", SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetAllCategories";
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
                        data.Add(dataObj);
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
