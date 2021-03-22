using System;
using AutoMapper;
using Battleship.Dtos;
using Battleship.Models;

namespace Battleship.Profiles
{
    public class BattleshipProfile : Profile
    {

        public BattleshipProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Board, BoardDto>();

            CreateMap<WarshipDto, WarShip>();
            CreateMap<WarShip, WarshipDto>();
            CreateMap<WarshipMinDto, WarShip>()
                .ForMember(dest => dest.StartX, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.StartY, opt => opt.MapFrom(src => src.Y))
                .ForMember(dest => dest.EndX, opt => opt.MapFrom(src => calculateEndX(src)))
                .ForMember(dest => dest.EndY, opt => opt.MapFrom(src => src.Y));

        }

        private int calculateEndX(WarshipMinDto warshipMinDto){
            if(warshipMinDto.Type == WarshipType.BATTLESHIP) return Math.Min(warshipMinDto.X +  5, 10); 
            if(warshipMinDto.Type == WarshipType.DESTROYER) return Math.Min(warshipMinDto.X +  4, 10);
            return 0; 
        }
    }
}