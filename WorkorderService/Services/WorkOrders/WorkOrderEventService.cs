using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Data;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Services.WorkOrders
{
    /// <summary>
    /// Class <c>SitesService</c> represents the Sites Service.
    /// <remarks>
    /// Implements the IWorkOrderEventsService <see cref="IWorkOrderEventService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderEventService : IWorkOrderEventService
    {
        /// <summary>
        /// Property <c>_externalSystemsService</c> represents the external system service for data access layer operations.
        /// <value>An interface representing the contract for the external system data access layer operations.</value>
        /// </summary>
        private readonly IExternalSystemService _externalSystemsService;

        /// <summary>
        /// Property <c>_workOrderStatusesService</c> represents the work order status service for data access layer operations.
        /// <value>An interface representing the contract for the work order status data access layer operations.</value>
        /// </summary>
        private readonly IWorkOrderStatusService _workOrderStatusService;

        /// <summary>
        /// Property <c>_sitesService</c> represents the sites service for data access layer operations.
        /// <value>An interface representing the contract for the sites data access layer operations.</value>
        /// </summary>
        private readonly ISiteService _sitesService;

        /// <summary>
        /// Property <c>_dbContext</c> represents the data access layer of the application.
        /// <value>A class containing the data access layer context.</value>
        /// </summary>
        private readonly WorkOrderWebServiceWebAPIDbContext _dbContext;

        /// <summary>
        /// Constructor <c>WorkOrderEventsService</c> is used to instantiate the work order events service.
        /// </summary>
        /// <param name="externalSystemsService">The interface representing the contract for the external system service.</param>
        /// <param name="workOrderStattusesService">The interface representing the contract for the work order status service.</param>
        /// <param name="sitesService">The interface representing the contract for the site service.</param>
        /// <param name="workOrdersService">The interface representing the contract for the work order service.</param>
        /// <param name="dbContext">The interface representing the contract for the data access layer context.</param>
        public WorkOrderEventService(IExternalSystemService externalSystemsService,
                                     IWorkOrderStatusService workOrderStattusesService,
                                     ISiteService sitesService,
                                     WorkOrderWebServiceWebAPIDbContext dbContext)
        {
            _externalSystemsService = externalSystemsService;
            _workOrderStatusService = workOrderStattusesService;
            _sitesService = sitesService;
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrderEvent? Create(WorkOrderEvent workOrderEvent)
        {
            if (workOrderEvent is null)
                return null;

            // Possibly perform a check for duplicate events.

            var newWorkOrderEvent = new WorkOrderEvent()
            {
                WorkOrderExternalId = workOrderEvent.WorkOrderExternalId,
                Details = workOrderEvent.Details,
            };

            // External System.
            if (!string.IsNullOrWhiteSpace(workOrderEvent.ExternalSystemId))
            {
                var externalSystem = _externalSystemsService.GetById(workOrderEvent.ExternalSystemId);
                if (externalSystem is not null)
                {
                    newWorkOrderEvent.ExternalSystem = externalSystem;
                    newWorkOrderEvent.ExternalSystemId = externalSystem.ExternalSystemId;
                }
            }

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrderEvent.WorkOrderStatusId))
                throw new Exception("The work order status has not been selected or is empty for this work order event.");


            var workOrderStatus = _workOrderStatusService.GetById(workOrderEvent.WorkOrderStatusId);
            if (workOrderStatus is null)
                throw new Exception("The work order status linked to the work order event does not exist.");

            newWorkOrderEvent.WorkOrderStatus = workOrderStatus;
            newWorkOrderEvent.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;

            // Sites.
            if (string.IsNullOrWhiteSpace(workOrderEvent.SiteId))
                throw new Exception("The site has not been selected or is empty for this work order event.");

            var site = _sitesService.GetById(workOrderEvent.SiteId);
            if (site is null)
                throw new Exception("The site does not exist for this work order event.");

            newWorkOrderEvent.Site = site;
            newWorkOrderEvent.SiteId = site.SiteId;

            // Work Order.
            if (string.IsNullOrWhiteSpace(workOrderEvent.WorkOrderId))
                throw new Exception("The work order has not been selected or is empty for this work order event.");

            var workOrder = _dbContext.WorkOrders.Find(workOrderEvent.WorkOrderId);
            if (workOrder is null)
                throw new Exception("he work order does not exist for this work order event.");

            newWorkOrderEvent.WorkOrder = workOrder;
            newWorkOrderEvent.WorkOrderId = workOrder.WorkOrderId;

            _dbContext.WorkOrderEvents.Add(newWorkOrderEvent);
            if (_dbContext.SaveChanges() > 1)
                return newWorkOrderEvent;

            return null;
        }

        // <inheritdoc />
        public async Task<WorkOrderEvent?> CreateAsync(WorkOrderEvent workOrderEvent)
        {
            if (workOrderEvent is null)
                return null;

            // Possibly perform a check for duplicate events.

            var newWorkOrderEvent = new WorkOrderEvent()
            {
                WorkOrderExternalId = workOrderEvent.WorkOrderExternalId,
                Details = workOrderEvent.Details,
            };

            // External System.
            if (!string.IsNullOrWhiteSpace(workOrderEvent.ExternalSystemId))
            {
                var externalSystem = await _externalSystemsService.GetByIdAsync(workOrderEvent.ExternalSystemId);
                if (externalSystem is not null)
                {
                    newWorkOrderEvent.ExternalSystem = externalSystem;
                    newWorkOrderEvent.ExternalSystemId = externalSystem.ExternalSystemId;
                }
            }

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrderEvent.WorkOrderStatusId))
                throw new Exception("The work order status has not been selected or is empty for this work order event.");

            var workOrderStatus = await _workOrderStatusService.GetByIdAsync(workOrderEvent.WorkOrderStatusId);
            if (workOrderStatus is null)
                throw new Exception("The work order status linked to the work order event does not exist.");

            newWorkOrderEvent.WorkOrderStatus = workOrderStatus;
            newWorkOrderEvent.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;

            // Sites.
            if (string.IsNullOrWhiteSpace(workOrderEvent.SiteId))
                throw new Exception("The site has not been selected or is empty for this work order event.");

            var site = await _sitesService.GetByIdAsync(workOrderEvent.SiteId);
            if (site is null)
                throw new Exception("The site does not exist for this work order event.");

            newWorkOrderEvent.Site = site;
            newWorkOrderEvent.SiteId = site.SiteId;

            // Work Order.
            if (string.IsNullOrWhiteSpace(workOrderEvent.WorkOrderId))
                throw new Exception("The work order has not been selected or is empty for this work order event.");

            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderEvent.WorkOrderId);
            if (workOrder is null)
                throw new Exception("he work order does not exist for this work order event.");

            newWorkOrderEvent.WorkOrder = workOrder;
            newWorkOrderEvent.WorkOrderId = workOrder.WorkOrderId;

            await _dbContext.WorkOrderEvents.AddAsync(newWorkOrderEvent);
            if (await _dbContext.SaveChangesAsync() > 1)
                return newWorkOrderEvent;

            return null;
        }

        // <inheritdoc />
        public IEnumerable<WorkOrderEvent> GetAll()
        {
            var workOrderEvents = _dbContext.WorkOrderEvents.AsEnumerable();
            if (!workOrderEvents.Any())
                return null;

            return workOrderEvents;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrderEvent>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrderEvents.AsEnumerable());

        public WorkOrderEvent? GetById(string workOrderEventId)
        {
            var workOrderEvent = _dbContext.WorkOrderEvents.Find(workOrderEventId);
            if (workOrderEvent is null)
                return null;

            return workOrderEvent;
        }

        // <inheritdoc />
        public async Task<WorkOrderEvent?> GetByIdAsync(string workOrderEventId)
        {
            var workOrderEvent = await _dbContext.WorkOrderEvents.FindAsync(workOrderEventId);
            if (workOrderEvent is null)
                return null;

            return workOrderEvent;
        }
    }
}
