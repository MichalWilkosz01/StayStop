﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Authorization;
using StayStop.BLL.DateTimeExtension;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.Exceptions;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.BLL_EF.Exceptions;
using StayStop.DAL.Context;
using StayStop.Model;
using StayStop.Model.Enums;
using System.Linq.Expressions;

namespace StayStop.BLL_EF.Service
{
    public class ReservationService : IReservationService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public ReservationService(StayStopDbContext context, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        private User GetUserById(int userId)
        {
            var user = _context.Users.Include(u=>u.UserReservations).FirstOrDefault(u => u.UserId == userId);
            if (user is null) throw new ContentNotFoundException($"User with id: {userId} was not found");

            return user;
        }
        private Reservation GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.Include(r => r.ReservationPositions).ThenInclude(rp => rp.Room).First(r => r.ReservationId == reservationId);
            if (reservation is null) throw new ContentNotFoundException($"Reservation with id: {reservationId} was not found");

            return reservation;
        }
        private decimal CalculatePrice(Reservation reservation)
        {
            var price = 0.0M;
            var nights = reservation.StartDate.NumberOfNights(reservation.EndDate);

            foreach (var position in reservation.ReservationPositions)
            {
                var room = _context.Rooms.FirstOrDefault(r => r.RoomId == position.RoomId);
                position.Price = (room.PriceForChild * position.NumberOfChildren + room.PriceForAdult * position.NumberOfAdults) * position.Amount;
                price += nights * position.Price;
            }

            return price;
        }
        public int Create(ReservationRequestDto reservationDto)
        {
            if (reservationDto.StartDate > reservationDto.EndDate) throw new InvalidReservationDate($"Reservation end date can't equal or before start date");
 
            var user = GetUserById(_userContextService.GetUserId ?? throw new InvalidDataException("User id was not found"));

            var reservation = _mapper.Map<Reservation>(reservationDto);
         
            reservation.Price = CalculatePrice(reservation);

            reservation.User = user;
            reservation.UserId = user.UserId;
            //foreach (var rp in reservation.ReservationPositions)
            //{
            //    rp.Room.IsAvailable = false;
            //}

            user.UserReservations.Add(reservation);

            _context.Add(reservation);

            _context.SaveChanges();

            return reservation.ReservationId;
            
        }

        public void DeleteById(int reservationId)
        {
            var reservation = GetReservationById(reservationId);
            foreach (var rp in reservation.ReservationPositions)
            {
                rp.Room.IsAvailable = true;
            }
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public PageResult<ReservationResponseDto> GetAll(ReservationPagination pagination)
        {
            var baseQuery = _context
           .Reservations
           .Include(r => r.User)
           .Include(r => r.ReservationPositions).AsQueryable();
  
        
           
            if (!string.IsNullOrEmpty(pagination.ReservationsSortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>()
                {
                    {nameof(Reservation.Price), r => r.Price },                 
                    {nameof(Reservation.StartDate), r => r.StartDate},                 
                    {nameof(Reservation.EndDate), r => r.EndDate },                 
                };

                var selected = columnsSelector[pagination.ReservationsSortBy];

                baseQuery = pagination.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selected)
                : baseQuery.OrderByDescending(selected);
            }


            var reservations = baseQuery
              .Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize)
            .ToList();

            var reservationsResults = _mapper.Map<List<ReservationResponseDto>>(reservations);

            var result = new PageResult<ReservationResponseDto>(reservationsResults, baseQuery.Count(), pagination.PageSize, pagination.PageNumber);

            return result;
        }

        public IEnumerable<ReservationResponseDto> GetUserReservationsById(int userId)
        {
            var user = GetUserById(userId);

            if (user.UserReservations is null)
            {
                throw new ContentNotFoundException($"User with id: {user.UserId} does not have reservations history");
            }
            var result = _mapper.Map<IEnumerable<ReservationResponseDto>>(user.UserReservations);

            return result;
        }

        public IEnumerable<ReservationResponseDto> GetReservationsHistory()
        {
            var user = GetUserById(_userContextService.GetUserId ?? throw new InvalidDataException("Provided Uuser id is wrong"));

            if (user.UserReservations?.Count == 0) throw new ContentNotFoundException($"User with id: {user.UserId} don't have reservation history");

            var results = _mapper.Map<IEnumerable<ReservationResponseDto>>(user.UserReservations);

            return results;

        }

        public void UpdateStatus(int reservationId, ReservationStatus reservationStatus)
        {
            var reservation = GetReservationById(reservationId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            if (reservation.ReservationStatus != reservationStatus &&
                reservation.ReservationStatus != ReservationStatus.Canceled)
            {
                reservation.ReservationStatus = reservationStatus;
            }

            _context.SaveChanges();
        }

        public ReservationResponseDto GetById(int reservationId)
        {
            var reservation = GetReservationById(reservationId);

            var result = _mapper.Map<ReservationResponseDto>(reservation);

            return result;
        }
    }
}
