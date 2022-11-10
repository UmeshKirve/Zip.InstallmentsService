using Moq;
using Shouldly;
using System;
using Xunit;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        private readonly Mock<Clock> _clockMock;
        private readonly Mock<GuidProvider> _guidProvider;
        private static readonly DateTime _currentDateTime = DateTime.UtcNow.Date;
        private readonly Guid _newGuid = Guid.NewGuid();
        private decimal purchaseAmount = 123.45M;

        public PaymentPlanFactoryTests()
        {
            _clockMock = new Mock<Clock>();
            _guidProvider = new Mock<GuidProvider>();
            _clockMock.Setup(x => x.UtcNow()).Returns(_currentDateTime); 
            _guidProvider.Setup(x => x.NewGuid()).Returns(_newGuid);
           
        }

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {

            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan = paymentPlanFactory.CreatePaymentPlan(purchaseAmount);


            var response = new PaymentPlan()
            {

                Id = _newGuid,
                PurchaseAmount = purchaseAmount,
                Installments = new Installment[]
             {
                new Installment(){
                     Id = _newGuid,
                     DueDate = _currentDateTime,
                     Amount = 30.75M,

                 },
                 new Installment(){
                     Id = _newGuid,
                     DueDate = _currentDateTime.AddDays(14),
                     Amount = 30.75M,

                 },

                new Installment(){

                      Id = _newGuid,
                     DueDate = _currentDateTime.AddDays(28),
                     Amount = 30.75M,
                 },

                new Installment(){
                      Id = _newGuid,
                     DueDate = _currentDateTime.AddDays(42),
                     Amount = 31.20M,

                 }, 
              }

            };

            // Assert
            paymentPlan.ShouldBe(response);
        }
    }
}
