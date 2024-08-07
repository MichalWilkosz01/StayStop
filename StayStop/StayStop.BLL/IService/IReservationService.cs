﻿using StayStop.BLL.Dtos.Reservation;
using StayStop.BLL.Pagination;
using StayStop.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IReservationService
    {
        int Create(ReservationRequestDto reservationDto);
        IEnumerable<ReservationResponseDto> GetUserReservationsById(int userId);
        IEnumerable<ReservationResponseDto> GetReservationsHistory();
        PageResult<ReservationResponseDto> GetAll(ReservationPagination pagination);
        void DeleteById(int reservationId);
        void UpdateStatus(int reservationId, ReservationStatus reservationStatus);
        ReservationResponseDto GetById(int reservationId);
    }
}
