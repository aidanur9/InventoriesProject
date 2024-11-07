using InventoriesProject.Data;
using InventoriesProject.Models;
using InventoriesProject.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace InventoriesProject.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ILogger<ISupplierService> _logger; 
        private readonly ISupplierRepository _supplierRepository;
        
        public SupplierService(ILogger<SupplierService> logger, ISupplierRepository supplierRepository)
        {
            _logger = logger;
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierViewModel?> CreateSupplier(SupplierViewModel model)
        {
            try
            {
                var supplier = await _supplierRepository.CreateSupplier(new Supplier()
                {
                    Id = Guid.NewGuid(),
                    ContactInfo = model.ContactInfo,
                    SupplierName = model.SupplierName,
                    DateCreated = DateTime.Now
                });

                model.Id = supplier.Id;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating supplier. Error details: {0}", ex.Message);

                return null;
            }
        }

        public async Task DeleteSupplier(Guid id)
        {
            try
            {
                await _supplierRepository.DeleteSupplier(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting supplier. Error details: {0}", ex.Message);
            }
        }

        public async Task<SupplierViewModel?> DetailSupplier(Guid id)
        {
            try
            {
                var supplier = await _supplierRepository.GetSingleSupplier(id);
                if (supplier is null) return null;

                var supplierModel = new SupplierViewModel
                {
                    Id = supplier.Id,
                    ContactInfo = supplier.ContactInfo,
                    SupplierName = supplier.SupplierName,
                };

                return supplierModel;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while getting single supplier. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<SupplierViewModel?> EditSupplier(SupplierViewModel model)
        {
            try
            {
                var existingSupplier = await _supplierRepository.GetSingleSupplier(model.Id);
                if (existingSupplier is null) throw new InvalidOperationException($"There is no matching supplier with Id: {model.Id}");

                var editedSupplier = new Supplier
                {
                    Id = model.Id,
                    SupplierName = string.IsNullOrEmpty(model.SupplierName) ? existingSupplier.SupplierName : model.SupplierName,
                    ContactInfo = string.IsNullOrEmpty(model.ContactInfo) ? existingSupplier.ContactInfo : model.ContactInfo,
                    DateCreated = existingSupplier.DateCreated,
                    DateModified = DateTime.Now
                };

                await _supplierRepository.EditSupplier(editedSupplier);

                model.SupplierName = editedSupplier.SupplierName;
                model.ContactInfo = editedSupplier.ContactInfo;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to edit supplier. Error details: {0}", ex.Message);
                return null;
            }
        }

        public async Task<List<SupplierViewModel>?> GetAllSuppliers()
        {
            try
            {
                var list = await _supplierRepository.GetAllSuppliers();

                var modelList = list
                    .Select(i => new SupplierViewModel
                    {
                        Id = i.Id,
                        SupplierName = i.SupplierName,
                        ContactInfo = i.ContactInfo,
                    })
                    .ToList();

                return modelList;
            }
            catch (Exception ex)
            {
                _logger.LogError("There is error while trying to get all suppliers. Error details: {0}", ex.Message);
                return null;
            }
        }
    }
}
