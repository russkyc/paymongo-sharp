// MIT License
// 
// Copyright (c) 2025 Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Text.Json.Serialization;

namespace Paymongo.Sharp.Features.CardInstallments.Contracts
{
    public class Plan
    {
        [JsonPropertyName("issuer_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string IssuerId { get; set; } = null!;
        
        [JsonPropertyName("tenure")]
        public int Tenure { get; set; }

        [JsonPropertyName("auth_amount")]
        public int AuthAmount { get; set; }

        [JsonPropertyName("bank_interest_rate")]
        public decimal BankInterestRate { get; set; }

        [JsonPropertyName("distribution_type")]
        public string DistributionType { get; set; } = null!;

        [JsonPropertyName("geo_scope")]
        public string GeoScope { get; set; } = null!;

        [JsonPropertyName("installment_config_id")]
        public string? InstallmentConfigId { get; set; }

        [JsonPropertyName("interest_amount_charged")]
        public int InterestAmountCharged { get; set; }

        [JsonPropertyName("issuer_cashback_value")]
        public int IssuerCashbackValue { get; set; }

        [JsonPropertyName("issuer_name")]
        public string IssuerName { get; set; } = null!;

        [JsonPropertyName("loan_amount")]
        public int LoanAmount { get; set; }

        [JsonPropertyName("monthly_installment")]
        public int MonthlyInstallment { get; set; }

        [JsonPropertyName("processing_fee_percent")]
        public decimal ProcessingFeePercent { get; set; }

        [JsonPropertyName("processing_fee_type")]
        public string ProcessingFeeType { get; set; } = null!;

        [JsonPropertyName("processing_fee_value")]
        public int ProcessingFeeValue { get; set; }

        [JsonPropertyName("program_type")]
        public string ProgramType { get; set; } = null!;

        [JsonPropertyName("scheme_description")]
        public string SchemeDescription { get; set; } = null!;

        [JsonPropertyName("scheme_id")]
        public string SchemeId { get; set; } = null!;

        [JsonPropertyName("subvention_amount")]
        public int SubventionAmount { get; set; }

        [JsonPropertyName("subvention_type")]
        public string SubventionType { get; set; } = null!;

        [JsonPropertyName("terms_and_conditions")]
        public string TermsAndConditions { get; set; } = null!;

        [JsonPropertyName("terms_and_conditions_version")]
        public string? TermsAndConditionsVersion { get; set; }
    }
}