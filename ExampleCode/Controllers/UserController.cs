using AutoMapper;
using ExampleCode.DataAccess.Data.Repository.IRepository;
using ExampleCode.Models;
using ExampleCode.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace ExampleCode.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger;

        public UserController(IUserRepository users, IMapper mapper)
        {
            _userService = users;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsertList()
        {
            //_logger.LogInformation("Ibtener todos los suarios");
            try
            {
                IEnumerable<User> usersList = await _userService.GetAllUserList();

                return Ok(_mapper.Map<IEnumerable<UserDto>>(usersList));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{id:int}", Name = "GetUsertById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUsertById([FromBody] int Id)
        {
            if (Id == 0)
                return BadRequest();

            try
            {
                var userId = await _userService.GetUserById(Id);

                if (userId == null)
                    return NotFound();

                return Ok(_mapper.Map<UserDto>(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<UserCreateDto>> Add([FromBody] UserCreateDto createDto)
        {
            if (createDto == null)
                return BadRequest(createDto);

            User model = _mapper.Map<User>(createDto);  

            var result = await _userService.AddUser(model);
            if (result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            // return Ok("Added Successfully");

            return CreatedAtRoute("GetUsertById", new { id = model.Id }, model);
        }

        [HttpPut]
        public async Task<ActionResult<UserUpdateDto>> Put(int id, [FromBody] UserUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            //User model = new()
            //{
            //    Name = modelDto.Name,
            //    LastName = modelDto.LastName
            //};
            User model = _mapper.Map<User>(updateDto);

            try
            {
                var result = await _userService.ModifyUser(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        public ActionResult<User> delete(User model)
        {
            var result = _userService.DeleteUser(model);
            if (result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Deleted Successfully");
        }
    }
}
