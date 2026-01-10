using System.Collections.Generic;
using Project5LMS.Models;

namespace Project5LMS.Interfaces
{
    public interface IReservationService
    {
        /// <summary>
        /// Create a new reservation
        /// </summary>
        bool CreateReservation(int memberId, int bookId);

        /// <summary>
        /// Cancel a reservation
        /// </summary>
        bool CancelReservation(int reservationId, string reason = null);

        /// <summary>
        /// Fulfill a reservation (book is ready for pickup)
        /// </summary>
        bool FulfillReservation(int reservationId);

        /// <summary>
        /// Mark reservation as picked up
        /// </summary>
        bool MarkAsPickedUp(int reservationId);

        /// <summary>
        /// Get all reservations for a member
        /// </summary>
        IEnumerable<Reservation> GetMemberReservations(int memberId);

        /// <summary>
        /// Get all reservations for a book
        /// </summary>
        IEnumerable<Reservation> GetBookReservations(int bookId);

        /// <summary>
        /// Get pending reservations
        /// </summary>
        IEnumerable<Reservation> GetPendingReservations();

        /// <summary>
        /// Get ready reservations (available for pickup)
        /// </summary>
        IEnumerable<Reservation> GetReadyReservations();

        /// <summary>
        /// Check if member can reserve (within limits)
        /// </summary>
        bool CanReserve(int memberId, int bookId);

        /// <summary>
        /// Get reservation by ID
        /// </summary>
        Reservation GetReservation(int reservationId);

        /// <summary>
        /// Check if book has active reservations
        /// </summary>
        bool HasActiveReservations(int bookId);
    }
}

