﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.Model;
using StayStop.Model.Constants;

namespace StayStop.API.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public ActionResult Create([FromBody] ReservationRequestDto reservationDto) 
        {
            var newReservationId = _reservationService.Create(reservationDto);

            return Created($"/api/reservation/{newReservationId}", null);
        }
        [HttpGet("/api/user/{userId}/reservations")]
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult<ReservationResponseDto> GetUserReservationsById([FromRoute] int userId)
        {
            var reservation = _reservationService.GetUserReservationsById(userId);

            return Ok(reservation);
        }
        [HttpGet("/api/user/reservations")]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetReservationsHistory()
        {
            var reservations = _reservationService.GetReservationsHistory();

            return Ok(reservations);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult<IEnumerable<ReservationResponseDto>> GetAll([FromQuery] ReservationPagination pagination)
        {
            var reservations = _reservationService.GetAll(pagination);

            return Ok(reservations);
        }
        [HttpDelete("{reservationId}")]
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult DeleteUserReservationById ([FromRoute] int reservationId)
        {
            _reservationService.DeleteById( reservationId);

            return NoContent();
        }
    }
}
