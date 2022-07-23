using Company.Query.Domain.Providers.Responses;

namespace Company.Query.Infra.Data.Repositories.SQLs
{
    public static class PaymentSQL
    {
        //string modelo:
        public const string GET_PAYMENT_STATUS = @"
            SELECT
                TOP 1
                PT.[END_TO_END_ID] AS ID,
                PE.[COMPANY_STATUS_ID] AS STATUS,
                PT.[TRANSACTIONS_UUID] AS TRANSACTIONID,
                PE.[COMPANY_STATUS_ERROR_ID] AS STATUSERRORID
            FROM [COMPANY_EVENT] PE
            INNER JOIN [COMPANY_TRANSACTIONS] PT ON PE.[END_TO_END_ID] = PT.[END_TO_END_ID]
            WHERE PE.[END_TO_END_ID] = @EndToEnd
                AND PE.COMPANY_EVENT_NU = (SELECT MAX(X.COMPANY_EVENT_NU) FROM COMPANY_EVENT X WHERE X.END_TO_END_ID = @EndToEnd)
            ORDER BY PT.[COMPANY_EVENT_NU] DESC, PT.[CREATED_AT_DT] DESC;";

        public static string GET_COMPANY_TRANSACTIONS = $@"
            SELECT
                TRANSACTIONS.TRANSACTIONS_UUID AS {nameof(CompanyTransactionsDbResponse.TransactionUuid)},
                TRANSACTIONS.IS_SOURCE AS {nameof(CompanyTransactionsDbResponse.IsSource)},
                CASE
                    WHEN TRANSACTIONS.IS_SOURCE = 1
                        THEN
                            IIF(GETDATE() < (TRANSACTIONS.COMPANY_DT + 90), 1, 0)
                        ELSE
                            0
                END AS {nameof(CompanyTransactionsDbResponse.AllowRefund)},
                TRANSACTIONS.COMPANY_DT AS {nameof(CompanyTransactionsDbResponse.CompanyDate)}
            FROM (
                SELECT
                    TOP 1
                    TRANSACTIONS_UUID,
                    COMPANY_DT,
                    1 AS IS_SOURCE
                FROM
                    COMPANY_RECEIPT
                WHERE
                    END_TO_END_ID = @EndToEndId
                    AND
                    COMPANY_RECEIPT_TYPE_ID = 1

                UNION

                SELECT
                    L.TRANSACTIONS_UUID,
                    L.COMPANY_DT,
                    L.IS_SOURCE
                FROM (
                    SELECT
                        D.END_TO_END_ID,
                        D.TRANSACTIONS_UUID,
                        P.CREATED_AT_DT AS COMPANY_DT,
                        0 AS IS_SOURCE,
                        MAX(E.COMPANY_EVENT_NU) AS COMPANY_EVENT_NU
                    FROM
                        COMPANY_VOLUNTARY_PAYMENT_DETAIL D
                    LEFT JOIN
                        COMPANY P
                        ON D.END_TO_END_ID = P.END_TO_END_ID
                    LEFT JOIN
                        COMPANY_EVENT E
                        ON D.END_TO_END_ID = E.END_TO_END_ID
                    WHERE
                        D.RECEIPT_END_TO_END_ID = @EndToEndId
                    GROUP BY
                        D.END_TO_END_ID, D.TRANSACTIONS_UUID, P.CREATED_AT_DT
                ) AS L
                LEFT JOIN
                    COMPANY_EVENT E
                    ON L.END_TO_END_ID = E.END_TO_END_ID
                    AND L.COMPANY_EVENT_NU = E.COMPANY_EVENT_NU
                WHERE
                    E.COMPANY_STATUS_ID = 10
            ) AS TRANSACTIONS
            ORDER BY
                TRANSACTIONS.IS_SOURCE DESC,
                TRANSACTIONS.COMPANY_DT DESC";
    }
}
