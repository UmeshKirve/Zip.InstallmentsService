using Microsoft.AspNetCore.Mvc;

namespace Zip.InstallmentsService.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/installmentsservice")]
    public class InstallmentsServiceController : ControllerBase
    {
        private readonly PaymentPlanFactory _paymentPlanFactory;

        private readonly ILogger<InstallmentsServiceController> _logger;
        
        public InstallmentsServiceController(ILogger<InstallmentsServiceController> logger)
        {
            _logger = logger;
            _paymentPlanFactory = new PaymentPlanFactory();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaymentPlan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("installmentdetails/{purchaseAmount:decimal}")]
        public ActionResult<PaymentPlan> Get(decimal purchaseAmount)
        {
            if (purchaseAmount <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid Purchase amount");

            }
            var paymentPlan = _paymentPlanFactory.CreatePaymentPlan(purchaseAmount);

            return Ok(paymentPlan);
        }
    }
}