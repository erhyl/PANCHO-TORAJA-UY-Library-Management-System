using System.Collections.Generic;
using Project5LMS.Models;
namespace Project5LMS.Interfaces
{
    public interface IReservationService
    {
        bool CreateReservation(int memberId, int bookId);
        bool CancelReservation(int reservationId, string reason = null);
        bool FulfillReservation(int reservationId);
        bool MarkAsPickedUp(int reservationId);
        IEnumerable<Reservation> GetMemberReservations(int memberId);
        IEnumerable<Reservation> GetBookReservations(int bookId);
        IEnumerable<Reservation> GetPendingReservations();
        IEnumerable<Reservation> GetReadyReservations();
        bool CanReserve(int memberId, int bookId);
        Reservation GetReservation(int reservationId);
        bool HasActiveReservations(int bookId);
    }
}