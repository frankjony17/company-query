using Company.Query.Domain.Providers.Responses;

namespace Company.Query.Infra.Data.Repositories.SQLs
{
    internal static class DetailSql
    {
        public static string SelectCompany { get; } = $@"SELECT P.END_TO_END_ID AS {nameof(CompanyDetailResponse.EndToEndId)},
                                                            P.CHECKING_ACCOUNT_UUID AS {nameof(CompanyDetailResponse.CheckingAccountUuid)}
                                                       FROM COMPANY P WITH (NOLOCK)
                                                      WHERE P.END_TO_END_ID = @EndToEnd";

        public static string SelectPayment { get; } = $@"SELECT PP.KEY_TYPE_ID AS {nameof(CompanyDetailPaymentResponse.KeyTypeId)},
                                                                PP.BENEFICIARY_NM AS {nameof(CompanyDetailPaymentResponse.BeneficiaryName)},
                                                                PP.BENEFICIARY_KEY_ID AS {nameof(CompanyDetailPaymentResponse.BeneficiaryKeyId)},
                                                                PP.BENEFICIARY_BRANCH_NU AS {nameof(CompanyDetailPaymentResponse.BeneficiaryBranch)},
                                                                PP.BENEFICIARY_CHECKING_ACCOUNT_NU AS {nameof(CompanyDetailPaymentResponse.BeneficiaryCheckingAccount)},
                                                                PP.BENEFICIARY_CPF_CNPJ_NU AS {nameof(CompanyDetailPaymentResponse.BeneficiaryCpfCnpj)},
                                                                PP.BENEFICIARY_KEY_TYPE_ID AS {nameof(CompanyDetailPaymentResponse.BeneficiaryKeyTypeId)},
                                                                PP.SAME_OWNERSHIP_ST AS {nameof(CompanyDetailPaymentResponse.SameOwnership)},
                                                                PI.BANK_CD AS {nameof(CompanyDetailPaymentResponse.Bank)},
                                                                PI.PARTNER_ISPB_PRINCIPAL_NU AS {nameof(CompanyDetailPaymentResponse.IspbPrincipal)},
                                                                PI.PARTNER_ISPB_NU AS {nameof(CompanyDetailPaymentResponse.Ispb)},
                                                                PI.PARTNER_ISPB_NM AS {nameof(CompanyDetailPaymentResponse.IspnName)},
	                                                            KT.KEY_TYPE_ID AS {nameof(CompanyDetailPaymentResponse.KeyTypeId)},
                                                                KT.KEY_TYPE_DS AS {nameof(CompanyDetailPaymentResponse.KeyTypeDescription)},
	                                                            PPTYPE.COMPANY_TYPE_ID AS {nameof(CompanyDetailPaymentResponse.PaymentTypeId)},
                                                                PPTYPE.COMPANY_TYPE_DS AS {nameof(CompanyDetailPaymentResponse.PaymentTypeDescription)}
                                                           FROM COMPANY P WITH (NOLOCK)
                                                           INNER JOIN COMPANY PP WITH(NOLOCK)
                                                             ON P.END_TO_END_ID = PP.END_TO_END_ID
                                                           LEFT JOIN PARTNER_ISPB PI WITH(NOLOCK)
                                                             ON PP.PARTNER_ISPB_NU = PI.PARTNER_ISPB_NU
                                                           LEFT JOIN KEY_TYPE KT WITH(NOLOCK)
                                                             ON PP.KEY_TYPE_ID = KT.KEY_TYPE_ID
                                                           LEFT JOIN COMPANY_TYPE PPTYPE WITH(NOLOCK)
                                                             ON PP.COMPANY_TYPE_ID = PPTYPE.COMPANY_TYPE_ID
                                                         WHERE P.END_TO_END_ID = @EndToEnd";

        public static string SelectPaymentEvents { get; } = $@" SELECT PPE.CREATED_AT_DT AS {nameof(CompanyDetailPaymentEventResponse.CreatedAt)},
                                                                       PS.COMPANY_STATUS_ID AS {nameof(CompanyDetailPaymentEventResponse.Status)},
                                                                       PS.COMPANY_STATUS_DS AS {nameof(CompanyDetailPaymentEventResponse.StatusDescription)},
                                                                       PSE.COMPANY_STATUS_ERROR_ID AS {nameof(CompanyDetailPaymentEventResponse.StatusError)},
                                                                       PSE.COMPANY_STATUS_ERROR_DS AS {nameof(CompanyDetailPaymentEventResponse.StatusErrorDescription)},
                                                                       PPT.COMPANY_EVENT_NU AS {nameof(CompanyDetailPaymentEventResponse.PaymentEventNu)},
                                                                       PPT.TRANSACTIONS_TP AS {nameof(CompanyDetailPaymentEventResponse.TranscationsType)},
                                                                       PPT.TRANSACTIONS_UUID AS {nameof(CompanyDetailPaymentEventResponse.TransactionsUuid)},
                                                                       PPT.TRANSACTIONS_VL AS {nameof(CompanyDetailPaymentEventResponse.Value)},
                                                                       PPT.TRANSACTIONS_DT AS {nameof(CompanyDetailPaymentEventResponse.TransactionsDate)},
                                                                       PPT.BROKER_ID AS {nameof(CompanyDetailPaymentEventResponse.BrokerId)}
                                                                  FROM COMPANY P WITH (NOLOCK)
                                                                  LEFT JOIN COMPANY PP WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PP.END_TO_END_ID
                                                                  LEFT JOIN PARTNER_ISPB PI WITH(NOLOCK)
                                                                    ON PP.PARTNER_ISPB_NU = PI.PARTNER_ISPB_NU
                                                                  LEFT JOIN COMPANY_EVENT PPE WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PPE.END_TO_END_ID
                                                                  LEFT JOIN COMPANY_STATUS_ERROR PSE WITH(NOLOCK)
                                                                    ON PPE.COMPANY_STATUS_ERROR_ID = PSE.COMPANY_STATUS_ERROR_ID
                                                                  LEFT JOIN COMPANY_TRANSACTIONS PPT WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PPT.END_TO_END_ID AND PPE.COMPANY_EVENT_NU = PPT.COMPANY_EVENT_NU
                                                                  LEFT JOIN KEY_TYPE KT WITH(NOLOCK)
                                                                    ON PP.KEY_TYPE_ID = KT.KEY_TYPE_ID
                                                                  LEFT JOIN COMPANY_TYPE PPTYPE WITH(NOLOCK)
                                                                    ON PP.COMPANY_TYPE_ID = PPTYPE.COMPANY_TYPE_ID
                                                                  LEFT JOIN COMPANY_STATUS PS WITH(NOLOCK)
                                                                    ON PPE.COMPANY_STATUS_ID = PS.COMPANY_STATUS_ID
                                                                WHERE PP.END_TO_END_ID = @EndToEnd
                                                                ORDER BY PP.END_TO_END_ID, PPE.COMPANY_EVENT_NU";
        public static string SelectRefundEvents { get; } = $@" SELECT PPE.END_TO_END_ID AS {nameof(CompanyDetailRefundEventResponse.EndToEndId)},
                                                                      PPE.CREATED_AT_DT AS {nameof(CompanyDetailRefundEventResponse.CreatedAt)},
                                                                      PS.COMPANY_STATUS_ID AS {nameof(CompanyDetailRefundEventResponse.Status)},
                                                                      PS.COMPANY_STATUS_DS AS {nameof(CompanyDetailRefundEventResponse.StatusDescription)},
                                                                      PSE.COMPANY_STATUS_ERROR_ID AS {nameof(CompanyDetailRefundEventResponse.StatusError)},
                                                                      PSE.COMPANY_STATUS_ERROR_DS AS {nameof(CompanyDetailRefundEventResponse.StatusErrorDescription)},
                                                                      PPT.COMPANY_EVENT_NU AS {nameof(CompanyDetailRefundEventResponse.PaymentEventNu)},
                                                                      PPT.TRANSACTIONS_TP AS {nameof(CompanyDetailRefundEventResponse.TransactionsType)},
                                                                      PPT.TRANSACTIONS_UUID AS {nameof(CompanyDetailRefundEventResponse.TransactionsUuid)},
                                                                      PPT.TRANSACTIONS_VL AS {nameof(CompanyDetailRefundEventResponse.Value)},
                                                                      PPT.TRANSACTIONS_DT AS {nameof(CompanyDetailRefundEventResponse.TransactionsDate)},
                                                                      PPT.BROKER_ID AS {nameof(CompanyDetailRefundEventResponse.BrokerId)}
                                                                  FROM COMPANY P WITH (NOLOCK)
                                                                  LEFT JOIN COMPANY PP WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PP.END_TO_END_ID
                                                                  LEFT JOIN PARTNER_ISPB PI WITH(NOLOCK)
                                                                    ON PP.PARTNER_ISPB_NU = PI.PARTNER_ISPB_NU
                                                                  LEFT JOIN COMPANY_EVENT PPE WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PPE.END_TO_END_ID
                                                                  LEFT JOIN COMPANY_STATUS_ERROR PSE WITH(NOLOCK)
                                                                    ON PPE.COMPANY_STATUS_ERROR_ID = PSE.COMPANY_STATUS_ERROR_ID
                                                                  LEFT JOIN COMPANY_TRANSACTIONS PPT WITH(NOLOCK)
                                                                    ON P.END_TO_END_ID = PPT.END_TO_END_ID AND PPE.COMPANY_EVENT_NU = PPT.COMPANY_EVENT_NU
                                                                  LEFT JOIN KEY_TYPE KT WITH(NOLOCK)
                                                                    ON PP.KEY_TYPE_ID = KT.KEY_TYPE_ID
                                                                  LEFT JOIN COMPANY_TYPE PPTYPE WITH(NOLOCK)
                                                                    ON PP.COMPANY_TYPE_ID = PPTYPE.COMPANY_TYPE_ID
                                                                  LEFT JOIN COMPANY_STATUS PS WITH(NOLOCK)
                                                                    ON PPE.COMPANY_STATUS_ID = PS.COMPANY_STATUS_ID
                                                                WHERE PP.RECEIPT_END_TO_END_ID = @EndToEnd
                                                                ORDER BY PP.END_TO_END_ID, PPE.COMPANY_EVENT_NU ";

        public static string SelectReceipt { get; } = $@"SELECT PR.COMPANY_RECEIPT_VERSION_NU AS {nameof(CompanyDetailReceiptResponse.Version)},
                                                                PR.SENDER_NM AS {nameof(CompanyDetailReceiptResponse.SenderName)},
                                                                PR.SENDER_PARTNER_ISPB_NU AS {nameof(CompanyDetailReceiptResponse.SenderPartnerISPBNu)},
                                                                PR.SENDER_KEY_BRANCH_NU AS {nameof(CompanyDetailReceiptResponse.SenderKeyBranchNu)},
                                                                PR.SENDER_KEY_CHECKING_ACCOUNT_NU AS {nameof(CompanyDetailReceiptResponse.SenderCpfCnpj)},
                                                                PR.COMPANY_VL AS {nameof(CompanyDetailReceiptResponse.Value)},
                                                                PR.COMPANY_DT AS {nameof(CompanyDetailReceiptResponse.Date)},
                                                                PR.COMPANY_RECEIPT_CM AS {nameof(CompanyDetailReceiptResponse.Commentary)},
                                                                PR.TRANSACTIONS_UUID AS {nameof(CompanyDetailReceiptResponse.TransactionsUuid)},
                                                                PR.RETURNE_ID AS {nameof(CompanyDetailReceiptResponse.ReturneId)},
                                                                PR.CREATED_AT_DT AS {nameof(CompanyDetailReceiptResponse.CreatedAt)}
                                                           FROM COMPANY_RECEIPT PR WITH (NOLOCK) 
                                                          WHERE END_TO_END_ID = @EndToEnd
                                                          ORDER BY PR.COMPANY_RECEIPT_VERSION_NU";

    }
}
