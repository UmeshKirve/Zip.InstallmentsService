using System;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory
    {
        private readonly Clock _clock;
        private readonly GuidProvider _guidProvider;
        public PaymentPlanFactory()
        { 
           _clock = new Clock();
            _guidProvider = new GuidProvider();
        }
        
        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        public PaymentPlan CreatePaymentPlan(decimal purchaseAmount)
        {
            var wholePurchaseAmount = Math.Truncate(purchaseAmount);
            var floatingAmount = purchaseAmount - wholePurchaseAmount;
            var totalNoInstallments = 4;
            var amountToBePaidPerInstallment = wholePurchaseAmount / totalNoInstallments;

            var installementsDetails = new Installment[totalNoInstallments];

            //I have explicty set purchasedate to currentUtc time
            var purchaseDate = _clock.UtcNow();
            var frequencyOfInstallment = 14;
            installementsDetails[0] = new Installment()
            {
                Id = Guid.NewGuid(),
                DueDate = purchaseDate.Date,
                Amount = amountToBePaidPerInstallment
            };

            var nextInstallmentDate = purchaseDate.Date;

            for (int i = 1; i < totalNoInstallments; i++)
            {
                nextInstallmentDate = nextInstallmentDate.AddDays(frequencyOfInstallment);
                installementsDetails[i] = new Installment()
                {
                    Id = _guidProvider.NewGuid(),
                    DueDate = nextInstallmentDate,
                    Amount = (i + 1 == totalNoInstallments) ? amountToBePaidPerInstallment + floatingAmount : amountToBePaidPerInstallment
                };
            }
            return new PaymentPlan()
            {
                Id = _guidProvider.NewGuid(),
                PurchaseAmount = purchaseAmount,
                Installments = installementsDetails
            };
        }
    }
}
