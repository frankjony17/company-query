using Company.Query.Domain.Providers.Responses;

namespace Company.Query.Infra.Data.Repositories.SQLs
{
    public static class RefundSQL
    {
        public static string GET_REFUND_STATUS = $@"SELECT
	            TOP 1
                 VPD.RETURNE_ID AS {nameof(RefundStatusResponse.ReturnId)}
                ,VPD.TRANSACTIONS_UUID AS {nameof(RefundStatusResponse.TransactionId)}
                ,PE.COMPANY_STATUS_ID AS {nameof(RefundStatusResponse.Status)}
            FROM
                COMPANY_VOLUNTARY_PAYMENT_DETAIL AS VPD WITH (NOLOCK)
            CROSS APPLY
            (
                SELECT TOP 1 
                    _PE.COMPANY_STATUS_ID
                FROM 
                    COMPANY_EVENT AS _PE WITH (NOLOCK)
                WHERE
                    VPD.END_TO_END_ID = _PE.END_TO_END_ID                    
                ORDER BY
                    _PE.COMPANY_EVENT_NU
                DESC
            ) AS PE
            WHERE
                VPD.RETURNE_ID = @ReturnId
            ORDER BY
	            VPD.CREATED_AT_DT DESC";
    }
}
