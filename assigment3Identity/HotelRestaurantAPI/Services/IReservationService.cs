using HotelRestaurantAPI.Data;
using HotelRestaurantAPI.Models;

namespace HotelRestaurantAPI.Services;

public interface IReservationService
{
    Task<bool> AddExpected(HotelDataContext context, int day, int month, int adults, int children);
    Task<bool> AddCheckedIn(HotelDataContext context, int day, int month, int roomNumber, int adults, int children);
}