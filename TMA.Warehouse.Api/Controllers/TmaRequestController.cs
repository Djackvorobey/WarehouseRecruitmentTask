using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Enums;
using TMA.Warehouse.Api.Dtos;
using TMA.Warehouse.Api.Models;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmaRequestController : ControllerBase
    {
        private readonly ITmaRequestService _tmaRequestService;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public TmaRequestController(ITmaRequestService tmaRequestService, IItemService itemService, IMapper mapper, IAccountService accountService)
        {
            _tmaRequestService = tmaRequestService;
            _itemService = itemService;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAllTmaRequests(int accountId)
        {
            if (accountId != 0)
            {
                AccountModel accountModel = _mapper.Map<Account, AccountModel>(_accountService.GetById(accountId));

                if (accountModel.RoleEnum == Roles.RoleEnum.Coordinator
                    || accountModel.RoleEnum == Roles.RoleEnum.Administrator)
                {
                    List<TmaRequest> coordinatorTmaRequests = _tmaRequestService.GetAll().ToList();

                    return Ok(JsonSerializer.Serialize(coordinatorTmaRequests));
                }

                if (accountModel.RoleEnum == Roles.RoleEnum.Employee)
                {
                    List<TmaRequest> employeeTmaRequests = _tmaRequestService.GetAll().ToList().FindAll(x => x.EmployeeName == accountModel.Name);

                    return Ok(JsonSerializer.Serialize(employeeTmaRequests));
                }

            }
            List<TmaRequest> emptyTmaRequests = new();
            return StatusCode(500,JsonSerializer.Serialize(emptyTmaRequests));
        }

        [HttpPost]
        public IActionResult CreateTmaRequest([FromBody] TmaRequestModel tmaRequestModel)
        {
            try
            {
                TmaRequest tmaRequest = _mapper.Map<TmaRequest>(tmaRequestModel);

                _tmaRequestService.Insert(tmaRequest);

                return Ok("Order registered successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to create order: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdateTmaRequest([FromBody] TmaRequestAccountDto tmaRequestAccountDto)
        {
            if (tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Coordinator
                && tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    TmaRequest tmaRequest = _mapper.Map<TmaRequest>(tmaRequestAccountDto.TmaRequestModel);

                    _tmaRequestService.Update(tmaRequest);

                    return Ok("Tma Request Update successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to Update Tma Request: {ex.Message}");
                }
            }

            return StatusCode(403, "Access denied");
        }

        [HttpPut("confirm")]
        public IActionResult ConfirmTmaRequest([FromBody] TmaRequestAccountDto tmaRequestAccountDto)
        {
            if (tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Coordinator
                || tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    Item item = _itemService.GetById(tmaRequestAccountDto.TmaRequestModel.ItemId);

                    item.Quantity -= tmaRequestAccountDto.TmaRequestModel.Quantity;

                    TmaRequest tmaRequest = _tmaRequestService.GetById(tmaRequestAccountDto.TmaRequestModel.Id);

                    tmaRequest.Comment = tmaRequestAccountDto.TmaRequestModel.Comment;

                    tmaRequest.Status = Statuses.Status.Approved;

                    _tmaRequestService.Update(tmaRequest);

                    _itemService.Update(item);

                    TmaRequestModel tmaRequestModel = _mapper.Map<TmaRequest, TmaRequestModel>(tmaRequest);

                    return Ok(JsonSerializer.Serialize(new { Message = "Tma Request confirm successfully", TmaRequestModel = tmaRequestModel }));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, JsonSerializer.Serialize(new { Message = $"Failed to confirm Tma Request: {ex.Message}", TmaRequestModel = tmaRequestAccountDto.TmaRequestModel }));
                }
            }

            return StatusCode(403, JsonSerializer.Serialize(new { Message = "Access denied", TmaRequestModel = tmaRequestAccountDto.TmaRequestModel }));
        }

        [HttpPut("reject")]
        public IActionResult RejectTmaRequest([FromBody] TmaRequestAccountDto tmaRequestAccountDto)
        {
            if (tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Coordinator
                || tmaRequestAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    TmaRequest tmaRequest = _tmaRequestService.GetById(tmaRequestAccountDto.TmaRequestModel.Id);

                    tmaRequest.Comment = tmaRequestAccountDto.TmaRequestModel.Comment;

                    tmaRequest.Status = Statuses.Status.Rejected;

                    _tmaRequestService.Update(tmaRequest);

                    TmaRequestModel tmaRequestModel = _mapper.Map<TmaRequest, TmaRequestModel>(tmaRequest);

                    return Ok(JsonSerializer.Serialize(new { Message = "Tma Request confirm successfully", TmaRequestModel = tmaRequestModel }));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, JsonSerializer.Serialize(new { Message = $"Failed to confirm Tma Request: {ex.Message}", TmaRequestModel = tmaRequestAccountDto.TmaRequestModel }));
                }
            }

            return StatusCode(403, JsonSerializer.Serialize(new { Message = "Access denied", TmaRequestModel = tmaRequestAccountDto.TmaRequestModel }));
        }
    }
}
