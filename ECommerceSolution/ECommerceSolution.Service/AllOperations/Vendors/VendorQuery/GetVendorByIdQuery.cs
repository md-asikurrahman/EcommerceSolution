using ECommerceSolution.Service.AllOperations.VendorTypes;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;
using MediatR;
using System.Data;

namespace ECommerceSolution.Service.AllOperations.Vendors.VendorQuery
{
    public record class GetVendorByIdQuery(int Id):IRequest<VendorModelDto>;

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorModelDto>
    {
        public async Task<VendorModelDto> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            VendorModelDto data = new VendorModelDto();

            parameters.Add("@Id", request.Id, SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetVendorById";
            try
            {
                ds = SqlHelper.ExecuteDataSet(MySettings.DbConnection, storedProcedureName, CommandType.StoredProcedure, parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        VendorModelDto dataObj = new VendorModelDto();
                        dataObj.Id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString());
                        dataObj.VendorName = ds.Tables[0].Rows[i]["VendorName"].ToString();
                        dataObj.MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"].ToString();
                        dataObj.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                        dataObj.SerialNumber = int.Parse(ds.Tables[0].Rows[i]["SerialNumber"].ToString());
                        dataObj.IsActive = bool.Parse(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        dataObj.CreatedAt = ds.Tables[0].Rows[i]["CreatedAt"].ToString();
                        dataObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                        dataObj.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"].ToString();
                        dataObj.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                        dataObj.VendorTypeName = ds.Tables[0].Rows[i]["VendorTypeName"].ToString();
                        dataObj.VendorTypeId = int.Parse(ds.Tables[0].Rows[i]["VendorTypeId"].ToString());


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
