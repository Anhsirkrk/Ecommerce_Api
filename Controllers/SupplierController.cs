using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository isr;
        private readonly EcommercedemoContext context;

        public SupplierController(ISupplierRepository _isr, EcommercedemoContext _context)
        {
            context = _context;
            isr = _isr;
        }

        [HttpPost]
        [Route("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier(SupplierViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await isr.AddSupplier(model);

                // Create a custom response
                var customResponse = new
                {
                    Message = "Supplier created successfully",
                    Supplier = result // Include the newly created supplier data
                };

                return Ok(customResponse);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetSupplier")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            try
            {
                var supplier =await isr.GetSupplierById(id);

                if (supplier == null)
                {
                    return null;
                }
                var supplierViewModel = MapSupplierToViewModel(supplier);
                return Ok(supplierViewModel);
            }
            catch (Exception ex)
            {
               throw;
            }
        }
        [NonAction]
        public SupplierViewModel MapSupplierToViewModel(Supplier supplier)
        {
            // Map properties from Supplier to SupplierViewModel
            var supplierViewModel = new SupplierViewModel
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Email = supplier.Email,
                Mobile = supplier.Mobile,
                JoinDate = supplier.JoinDate ?? DateTime.MinValue,
                RegistrationAmountPaid = supplier.RegistrationAmountPaid ?? 0m,
                ExpiryDate = supplier.ExpiryDate ?? DateTime.MinValue,
                StatusOfRegistration = supplier.StatusOfRegistration,
                PanCard = supplier.PanCard,
                Licenceno = supplier.Licenceno,
                // Map other properties here
            };

            return supplierViewModel;
        }

        [HttpGet]
        [Route("GetAllSuppliers")]
        public IActionResult GetAllSuppliers()
        {
            try
            {
                var suppliers = isr.GetAllSuppliers();

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
