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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AccountModel accountModel)
        {
            try
            {
                var account = _mapper.Map<Account>(accountModel);

                _accountService.Insert(account);

                return Ok("Account registered successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to register account: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountModel accountModel)
        {
            var user = _accountService.Authenticate(accountModel.Name);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var account = _mapper.Map<AccountModel>(user);

            var accountJson = JsonSerializer.Serialize(account);

            return Ok(accountJson);
        }

        [HttpGet("accounts/{accountId}")]
        public IActionResult GetAllAccount(int accountId)
        {
            if (accountId != 0)
            {
                var adminAccount = _accountService.GetById(accountId);

                var adminAccountModel = _mapper.Map<AccountModel>(adminAccount);

                if (adminAccountModel.RoleEnum != Roles.RoleEnum.Administrator)
                {
                    return StatusCode(403, JsonSerializer.Serialize(new { Message = $"Access denied", Accounts = new List<AccountModel>()}));
                }

                List<Account> accounts = _accountService.GetAll().ToList();

                List<AccountModel> accountModels = new();

                foreach (var account in accounts)
                {
                    accountModels.Add(_mapper.Map<AccountModel>(account));
                }

                return Ok(JsonSerializer.Serialize(accountModels));
            }

            return StatusCode(500, JsonSerializer.Serialize(new { Message = $"User not present", Accounts = new List<AccountModel>() }));
        }

        [HttpPut("remove")]
        public IActionResult Remove([FromBody] ManageAccountDto manageAccountDto)
        {
            if (manageAccountDto.AdminAccount.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    var account = _mapper.Map<Account>(manageAccountDto.UserAccount);

                    _accountService.Delete(account);

                    return Ok(JsonSerializer.Serialize(new { Message = "User delete successfully", AccountModel = manageAccountDto.UserAccount }));
                }
                catch (Exception ex)
                {
                   return StatusCode(500, JsonSerializer.Serialize(new { Message = $"Failed user delete: {ex.Message}", AccountModel = manageAccountDto.UserAccount }));
                }
            }

            return StatusCode(403, JsonSerializer.Serialize(new { Message = "Access denied", AccountModel = manageAccountDto.UserAccount }));
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] ManageAccountDto manageAccountDto)
        {
            if (manageAccountDto.AdminAccount.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    var account = _mapper.Map<Account>(manageAccountDto.UserAccount);

                    _accountService.Update(account);

                    return Ok(JsonSerializer.Serialize(new { Message = "User update successfully", AccountModel = manageAccountDto.UserAccount }));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, JsonSerializer.Serialize(new { Message = $"Failed user update: {ex.Message}", AccountModel = manageAccountDto.UserAccount }));
                }
            }

            return StatusCode(403, JsonSerializer.Serialize(new { Message = "Access denied", AccountModel = manageAccountDto.UserAccount }));
        }
    }
}
