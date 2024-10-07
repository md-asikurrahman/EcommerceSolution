using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;

namespace ECommerceSolution.Service.AllOperations.VendorTypes.VendorTypeQuery
{
    public record GetVendorTypeByIdQuery(int Id) : IRequest<VendorTypeModelDto>;

    public class GetVendorTypeByIdQueryHandler : IRequestHandler<GetVendorTypeByIdQuery, VendorTypeModelDto>
    {
        public async Task<VendorTypeModelDto> Handle(GetVendorTypeByIdQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            VendorTypeModelDto data = new VendorTypeModelDto();

            parameters.Add("@Id", request.Id, SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetVendorTypeById";
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
                        dataObj.SerialNumber =int.Parse(ds.Tables[0].Rows[i]["SerialNumber"].ToString());
                        dataObj.IsActive =bool.Parse(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        dataObj.CreatedAt = ds.Tables[0].Rows[i]["CreatedAt"].ToString();
                        dataObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                        dataObj.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"].ToString();
                        dataObj.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                        
                        data=dataObj;
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
