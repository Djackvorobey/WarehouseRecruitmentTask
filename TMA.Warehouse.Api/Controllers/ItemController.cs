using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Enums;
using TMA.Warehouse.Api.Dtos;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet("items/{accountId}")]
        public IActionResult GetAllItems(int accountId)
        {
           List<Item> items = _itemService.GetAll().ToList();

           string itemsJson = JsonSerializer.Serialize(items);

           return Ok(itemsJson);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           Item item = _itemService.GetById(id);

           return Ok(JsonSerializer.Serialize(item));
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] ItemAccountDto itemAccountDto)
        {
            if (itemAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Coordinator
                || itemAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    Item item = _mapper.Map<Item>(itemAccountDto.ItemModel);

                   Item dbItem = _itemService.Insert(item);

                    return Ok(JsonSerializer.Serialize(new { Message = "Item add successfully", ItemId = dbItem.Id }));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, JsonSerializer.Serialize(new { Message = $"Failed to add item: {ex.Message}", ItemId = 0 }));
                }
            }

            return StatusCode(403, JsonSerializer.Serialize(new { Message = $"Access denied", ItemId = 0 }));
        }

        [HttpPut]
        public IActionResult UpdateItem([FromBody] ItemAccountDto itemAccountDto)
        {
            if (itemAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Coordinator
                || itemAccountDto.AccountModel.RoleEnum == Roles.RoleEnum.Administrator)
            {
                try
                {
                    Item item = _mapper.Map<Item>(itemAccountDto.ItemModel);

                    _itemService.Update(item);

                    return Ok("Item update successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to update item: {ex.Message}");
                }
            }

            return StatusCode(403, "Access denied");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                Item item = _itemService.GetById(id);

                if (item == null)
                {
                    return NotFound($"Item with ID {id} not found");
                }

                _itemService.Delete(item);

                return Ok("Item deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete item: {ex.Message}");
            }
        }
    }
}
