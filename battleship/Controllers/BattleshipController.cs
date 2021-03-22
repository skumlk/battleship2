using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Battleship.Data;
using Battleship.Dtos;
using Battleship.Models;
using Battleship.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Battleship;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleshipController : ControllerBase
    {
        private readonly IBattleshipRepository _repository;
        private readonly IConfiguration _configuration;

        public readonly IBattleshipService _battleshipService;

        public IAuthUtil _authUtil;

        private readonly IMapper _mapper;

        public BattleshipController(IBattleshipRepository repository,
            IConfiguration configuration, IMapper mapper,
            IBattleshipService battleshipService,
            IAuthUtil authUtil)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _battleshipService = battleshipService;
            _authUtil = authUtil;
        }

        [HttpPost("start")]
        public ActionResult<string> StartGame(WarshipsMinDto warshipsMinDto)
        {
            var boardAWarShips = _mapper.Map<IList<WarShip>>(warshipsMinDto.Ships);
            Game game = _battleshipService.createGame(boardAWarShips);
            var token = _authUtil.GenerateJSONWebToken(game.Id);
            return Ok(new ReadAuthDto { Token = token });
        }

        [HttpPost("mark/{x}/{y}")]
        public ActionResult<string> MarkCell(int x, int y)
        {
            

            var gameId = getGameId();
            var result = _battleshipService.markCell(gameId, x, y);
            var game = result.Item1;
            var gameDto = _mapper.Map<GameDto>(game);
            gameDto.BoardA.WarShips = null;
            gameDto.BoardA.successFire = result.Item2;
            gameDto.BoardB.successFire = result.Item3;
            return Ok(gameDto);
        }

        [HttpGet("status")]
        public ActionResult<string> GetStatus()
        {
            var gameId = getGameId();
            var result = _battleshipService.getGameStatus(gameId);

            var game = result.Item1;
            var gameDto = _mapper.Map<GameDto>(game);
            gameDto.BoardA.WarShips = null;
            gameDto.BoardA.successFire = result.Item2;
            gameDto.BoardB.successFire = result.Item3;

            return Ok(gameDto);
        }
        
        private int getGameId()
        {
            var token =  (string)HttpContext.Request.Headers["Authorization"];
            token = token.Split(" ")[1];
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var gameId = jwtToken.Payload["user"];
            return Int32.Parse(gameId.ToString());
        }
    }
}