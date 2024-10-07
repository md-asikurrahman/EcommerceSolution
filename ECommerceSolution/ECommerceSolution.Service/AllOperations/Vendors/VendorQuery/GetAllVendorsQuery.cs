using MediatR;
using System.Data;
using ECommerceSolution.Service.Extensions;
using ECommerceSolution.Service.StaticModels;

namespace ECommerceSolution.Service.AllOperations.Vendors.VendorQuery
{
    public record GetAllVendorsQuery : IRequest<IList<VendorModelDto>>;

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, IList<VendorModelDto>>
    {
        public async Task<IList<VendorModelDto>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            DataSet ds = new DataSet();
            ParameterList parameters = new ParameterList();
            List<VendorModelDto> data = new List<VendorModelDto>();

            parameters.Add("@QueryExtension", "", SqlDbType.NVarChar);

            string storedProcedureName = "SP_Select_GetAllVendors";
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
                        dataObj.SerialNumber = int.Parse(ds.Tables[0].Rows[i]["SerialNumber"].ToString());
                        dataObj.MobileNumber = ds.Tables[0].Rows[i]["MobileNumber"].ToString();
                        dataObj.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                        dataObj.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                        dataObj.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                        dataObj.CreatedAt = ds.Tables[0].Rows[i]["CreatedAt"].ToString();
                        dataObj.UpdatedAt = ds.Tables[0].Rows[i]["UpdatedAt"].ToString();
                        dataObj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        dataObj.VendorTypeName = ds.Tables[0].Rows[i]["VendorTypeName"].ToString();
                        dataObj.VendorTypeId = int.Parse(ds.Tables[0].Rows[i]["VendorTypeId"].ToString());
                        data.Add(dataObj);
                    }
                }

                return data;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
