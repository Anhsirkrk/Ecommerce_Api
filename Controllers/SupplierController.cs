using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Identity.Client;

namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository isr;
        private readonly EcommerceDailyPickContext context;

        public SupplierController(ISupplierRepository _isr, EcommerceDailyPickContext _context)
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
                var supplier = await isr.GetSupplierById(id);

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
                Password= supplier.Password
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
                var suppliers = isr.GetSuppliersWithPinCodes();

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("SupplierOrderCreation")]
        public async Task<IActionResult> SupplierOrderCreation(SupplierOrderViewModel sovm)
        {
            try
            {
                if (sovm == null)
                {
                    return BadRequest("Invalid data");
                }
                var result = await isr.SupplierOrderCreation(sovm);

                // Create a custom response
                var SupplierOrderResponse = new
                {
                    Message = "Supplier Order Created  successfully",
                    Supplier = result // Include the newly created supplier data
                };

                return Ok(SupplierOrderResponse);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetSupplierOrderDetailsBySupplierId")]
        public async Task<IActionResult> GetSupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1 = null, string filterStatus2 = null, string filterStatus3 = null, string filterStatus4 = null, string filterStatus5 = null, string filterStatus6 = null, string filterStatus7 = null, string filterStatus8 = null)
        {
            try
            {
                var items = await isr.GetSupplierOrderDetailsBySupplierId(supplierId,filterStatus1,filterStatus2,filterStatus3,filterStatus4,filterStatus5,filterStatus6,filterStatus7,filterStatus8);

                if (items == null)
                {
                    return null;
                }
              var supplierOrderViewModel = items.Select(item => MapSupplierOrderDetailsToViewModel(item)).ToList();

                return Ok(supplierOrderViewModel);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [NonAction]
        public SupplierOrderDetailsViewModel MapSupplierOrderDetailsToViewModel(SupplierOrderDetailsViewModel supplierorderdetailsViewModel)
        {
            // Map properties from SupplierOrderDetailsViewModel to SupplierViewModel
            var supplierOrderDetails = new SupplierOrderDetailsViewModel
            {   
                OrderID = supplierorderdetailsViewModel.OrderID,
                ProductName = supplierorderdetailsViewModel.ProductName,
      ImageUrl=supplierorderdetailsViewModel.ImageUrl,

                DeliveryAddress= supplierorderdetailsViewModel.DeliveryAddress,
                Name = supplierorderdetailsViewModel.Name,
                ContactNo = supplierorderdetailsViewModel.ContactNo,
                SubscriptionTypes = supplierorderdetailsViewModel.SubscriptionTypes,
                Amount = supplierorderdetailsViewModel.Amount,
                StartDate=supplierorderdetailsViewModel.StartDate,
                EndDate=supplierorderdetailsViewModel.EndDate,
                PaymentStatus = supplierorderdetailsViewModel.PaymentStatus,
                OrderStatus = supplierorderdetailsViewModel.OrderStatus,


            };

            return supplierOrderDetails;
        }

        [HttpPost]
        [Route("UpdatetheOrderStatusBySupplier")]
        public async Task<IActionResult> UpdatetheOrderStatusBySupplier(UpdateOrderStatusSupplierViewModel supplierOrderTable)
        {

            if (supplierOrderTable == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await isr.UpdatetheOrderStatusBySupplier(supplierOrderTable);

               
                var customResponse = new
                {
                    Message = "Supplier Chaged Order Status Successfully",
                    Supplier = result 
                };

                return Ok(customResponse);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("SupplierLogin")]
        public async Task<IActionResult> SupplierLogin(SupplierLoginViewModel supplierLoginViewModel)
        {
            if (supplierLoginViewModel == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await isr.SupplierLogin(supplierLoginViewModel);


                var customResponse = new
                {
                    Message = "Supplier Sucesssfully Login",
                    Supplier = result
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
        [Route("GetTodaySupplierOrderDetailsBySupplierId")]
        public async Task<IActionResult> GetTodaySupplierOrderDetailsBySupplierId(int supplierId, string filterStatus1 = null, string filterStatus2 = null, string filterStatus3 = null, string filterStatus4 = null, string filterStatus5 = null, string filterStatus6 = null, string filterStatus7 = null, string filterStatus8 = null)
        {
            try
            {
                var items = await isr.GetTodaySupplierOrderDetailsBySupplierId(supplierId, filterStatus1, filterStatus2, filterStatus3, filterStatus4, filterStatus5, filterStatus6, filterStatus7, filterStatus8);

                if (items == null)
                {
                    return null;
                }
                var supplierOrderViewModel = items.Select(item => MapSupplierOrderDetailsToViewModel(item)).ToList();

                return Ok(supplierOrderViewModel);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetSupplierApprovalRejectlist")]
        public async Task<List<SupplierApprovalRejectViewModal>> GetSupplierApprovalRejectlist(int supplierId)
        {
            try
            {
                if (supplierId != null)
                {
                    return await isr.GetSupplierApprovalRejectList(supplierId);
                }
                return null;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}