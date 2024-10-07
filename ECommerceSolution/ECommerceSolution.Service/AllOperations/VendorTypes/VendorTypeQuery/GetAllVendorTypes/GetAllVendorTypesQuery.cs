using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeQuery.GetAllVendorTypes
{
    public record GetAllVendorTypesQuery : IRequest<IList<VendorTypeModelDto>>;

    public class GetAllVendorTypesQueryHandler : IRequestHandler<GetAllVendorTypesQuery, IList<VendorTypeModelDto>>
    {
        public async Task<IList<VendorTypeModelDto>> Handle(GetAllVendorTypesQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            List<VendorTypeModelDto> data = new List<VendorTypeModelDto>();

            parameters.Add("@QueryExtension", "", SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetAllVendorTypes";
            try
            {
                ds = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        VendorTypeModelDto dataObj = new VendorTypeModelDto();
                        dataObj.Id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                        dataObj.TypeName = ds.Tables[0].Rows[i]["TypeName"].ToString();
                        dataObj.SerialNumber = int.Parse(ds.Tables[0].Rows[i]["SerialNumber"].ToString());
                        dataObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                        dataObj.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                        dataObj.CreatedAt =ds.Tables[0].Rows[i]["CreatedAt"].ToString();
                        dataObj.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"].ToString();
                        dataObj.IsActive =Convert.ToBoolean( ds.Tables[0].Rows[i]["IsActive"].ToString());
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
