using AutoMapper;
using ExampleCode.DataAccess.Data.Repository.IRepository;
using ExampleCode.Models;
using ExampleCode.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Net;


namespace ExampleCode.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger;
        protected APIResponse _response;

        public UserController(IUserRepository users, IMapper mapper)
        {
            _userService = users;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<APIResponse>> GetUsertList()
        {
            //_logger.LogInformation("Ibtener todos los suarios");
            try
            {
                IEnumerable<UserModel> usersList = await _userService.GetAllUserList();

                _response.result = _mapper.Map<IEnumerable<UserDto>>(usersList);
                _response.statusCode = HttpStatusCode.OK;
                _response.isExitoso = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);

                _response.isExitoso = false;
                _response.errorMessage = new List<string>() { ex.ToString() };

                return _response;
            }
        }

        [HttpGet("{id:int}", Name = "GetUsertById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetUsertById(int Id)
        {
            if (Id == 0)
                //_response.statusCode= HttpStatusCode.BadRequest;
                return BadRequest();

            try
            {
                UserModel userId = await _userService.GetUserById(Id);

                if (userId == null)
                    // _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound();

                _response.result = _mapper.Map<UserDto>(userId);
                _response.statusCode = HttpStatusCode.OK;
                _response.isExitoso = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult<APIResponse>> Add([FromBody] UserCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                    return BadRequest(createDto);

                UserModel model = _mapper.Map<UserModel>(createDto);

                var result = await _userService.AddUser(model);
                if (result == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                _response.statusCode = HttpStatusCode.Created;
                _response.result = model;
                _response.isExitoso = true;

                return CreatedAtRoute("GetUsertById", new { id = model.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.errorMessage = new List<string>() { ex.ToString() };

                return _response;

            }
        }

        [HttpPut]
        [Route("PutUser")]
        public async Task<ActionResult<APIResponse>> Put(int id,  UserUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest();
                }

                UserModel model = _mapper.Map<UserModel>(updateDto);
                var result = await _userService.ModifyUser(model);

                _response.result = _mapper.Map<UserDto>(result);
                _response.statusCode = HttpStatusCode.OK;
                _response.isExitoso = true;

                return Ok(result);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete]
        public ActionResult<UserModel> delete(UserModel model)
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
