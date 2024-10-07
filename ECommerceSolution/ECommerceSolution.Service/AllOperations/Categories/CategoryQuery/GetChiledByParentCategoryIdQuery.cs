using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using MediatR;
using System.Data;

namespace ECommerceSolution.Service.AllOperations.Categories.CategoryQuery
{
    public record class GetChildByParentCategoryIdQuery(int? ParentCategoryId) : IRequest<IList<CategoryModelDto>>;

    public class GetChildByParentCategoryIdQueryHandler : IRequestHandler<GetChildByParentCategoryIdQuery, IList<CategoryModelDto>>
    {
        public async Task<IList<CategoryModelDto>> Handle(GetChildByParentCategoryIdQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            List<CategoryModelDto> data = new List<CategoryModelDto>();

            parameters.Add("@ParentId", request.ParentCategoryId!, SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetCategoryByParentId";
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
