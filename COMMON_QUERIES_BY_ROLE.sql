-- ============================================================================
-- PANCHO-TORAJA-UY Library Management System
-- COMMON QUERIES BY ROLE (Admin, Library Staff, Member)
-- ============================================================================
-- This file contains commonly used queries organized by user role
-- These queries are used throughout the application for various operations
-- ============================================================================

-- ============================================================================
-- ADMIN QUERIES
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Dashboard Statistics
-- ----------------------------------------------------------------------------

-- Total Books Count
SELECT COUNT(*) as TotalBooks FROM Books;

-- Available Books Count
SELECT COUNT(*) as AvailableBooks FROM Books WHERE Available > 0 AND Status = 'Available';

-- Books on Loan
SELECT COUNT(*) as BooksOnLoan FROM Transactions WHERE Status = 'Borrowed' AND ReturnDate IS NULL;

-- Total Members
SELECT COUNT(*) as TotalMembers FROM Members WHERE Status = 'Active';

-- Active Borrowings
SELECT COUNT(*) as ActiveBorrowings FROM Transactions WHERE Status = 'Borrowed';

-- Overdue Books
SELECT COUNT(*) as OverdueBooks 
FROM Transactions 
WHERE Status = 'Borrowed' 
AND DueDate < CURDATE() 
AND ReturnDate IS NULL;

-- Pending Fines Amount
SELECT COALESCE(SUM(Amount - Paid), 0) as PendingFines 
FROM Fines 
WHERE Status IN ('Pending', 'Partial');

-- Collected Fines Amount
SELECT COALESCE(SUM(Paid), 0) as CollectedFines 
FROM Fines 
WHERE Status = 'Paid' OR PaidDate IS NOT NULL;

-- Total Reservations
SELECT COUNT(*) as TotalReservations FROM Reservations WHERE Status IN ('Pending', 'Ready');

-- Books Added This Month
SELECT COUNT(*) as BooksAddedThisMonth 
FROM Books 
WHERE CreatedDate >= DATE_FORMAT(NOW(), '%Y-%m-01');

-- Members Added This Week
SELECT COUNT(*) as MembersAddedThisWeek 
FROM Members 
WHERE RegistrationDate >= DATE_SUB(NOW(), INTERVAL 7 DAY);

-- Books Borrowed Today
SELECT COUNT(*) as BooksBorrowedToday 
FROM Transactions 
WHERE DATE(BorrowDate) = CURDATE() 
AND TransactionType = 'Borrow';

-- Fines Collected Today
SELECT COALESCE(SUM(AmountPaid), 0) as FinesCollectedToday 
FROM FinePayments 
WHERE DATE(PaymentDate) = CURDATE();

-- ----------------------------------------------------------------------------
-- Collection Summary
-- ----------------------------------------------------------------------------

SELECT 
    (SELECT COUNT(*) FROM Books) as Total,
    (SELECT COUNT(*) FROM Books WHERE Status = 'Available' OR Status IS NULL) as Available,
    (SELECT COUNT(*) FROM Transactions WHERE Status = 'Borrowed' AND ReturnDate IS NULL) as OnLoan;

-- ----------------------------------------------------------------------------
-- Fine Summary
-- ----------------------------------------------------------------------------

SELECT 
    COALESCE(SUM(CASE WHEN Status = 'Paid' OR PaidDate IS NOT NULL THEN Amount ELSE 0 END), 0) as Collected,
    COALESCE(SUM(CASE WHEN Status IN ('Pending', 'Partial') AND PaidDate IS NULL THEN Amount - Paid ELSE 0 END), 0) as Pending,
    COALESCE(SUM(CASE WHEN Status = 'Waived' OR WaivedDate IS NOT NULL THEN Amount ELSE 0 END), 0) as Waived
FROM Fines;

-- ----------------------------------------------------------------------------
-- Library Usage Statistics
-- ----------------------------------------------------------------------------

SELECT
    (SELECT COUNT(DISTINCT DATE(BorrowDate)) FROM Transactions WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)) as DailyVisits,
    (SELECT COUNT(*) FROM Transactions) / GREATEST((SELECT COUNT(*) FROM Members), 1) as BooksPerMember,
    (SELECT AVG(DATEDIFF(COALESCE(ReturnDate, NOW()), BorrowDate)) FROM Transactions WHERE ReturnDate IS NOT NULL) as AvgPeriod,
    (SELECT (COUNT(*) * 100.0 / GREATEST((SELECT COUNT(*) FROM Books), 1)) FROM Transactions WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)) as Turnover;

-- ----------------------------------------------------------------------------
-- Most Borrowed Books (Top 10)
-- ----------------------------------------------------------------------------

SELECT
    b.BookID,
    b.Title,
    b.Author,
    COUNT(*) as TimesBorrowed
FROM Transactions t
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)
GROUP BY b.BookID, b.Title, b.Author
ORDER BY TimesBorrowed DESC
LIMIT 10;

-- ----------------------------------------------------------------------------
-- Member Activity by Type
-- ----------------------------------------------------------------------------

SELECT 
    COALESCE(m.Type, m.MemberType) as MemberType,
    COUNT(DISTINCT t.TransactionID) as ActivityCount
FROM Transactions t
INNER JOIN Members m ON t.MemberID = m.MemberID
WHERE t.BorrowDate >= DATE_SUB(NOW(), INTERVAL 30 DAY)
GROUP BY COALESCE(m.Type, m.MemberType);

-- ----------------------------------------------------------------------------
-- Category Distribution
-- ----------------------------------------------------------------------------

SELECT 
    Category,
    COUNT(*) as BookCount,
    SUM(TotalCopies) as TotalCopies,
    SUM(Available) as AvailableCopies
FROM Books
GROUP BY Category
ORDER BY BookCount DESC;

-- ----------------------------------------------------------------------------
-- Weekly Borrow Data
-- ----------------------------------------------------------------------------

SELECT 
    DAYNAME(BorrowDate) as DayName,
    COUNT(*) as BorrowCount
FROM Transactions
WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 7 DAY)
AND TransactionType = 'Borrow'
GROUP BY DAYNAME(BorrowDate)
ORDER BY MIN(BorrowDate);

-- ----------------------------------------------------------------------------
-- Monthly Borrow Data (Last 6 Months)
-- ----------------------------------------------------------------------------

SELECT 
    DATE_FORMAT(BorrowDate, '%Y-%m') as Month,
    COUNT(*) as BorrowCount
FROM Transactions
WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 6 MONTH)
AND TransactionType = 'Borrow'
GROUP BY DATE_FORMAT(BorrowDate, '%Y-%m')
ORDER BY Month;

-- ----------------------------------------------------------------------------
-- Recent Activities
-- ----------------------------------------------------------------------------

-- Recent Borrows
SELECT 
    'Book Borrowed' as ActivityType,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    b.Title as BookTitle,
    t.BorrowDate as ActivityDate
FROM Transactions t
INNER JOIN Members m ON t.MemberID = m.MemberID
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.Status = 'Borrowed'
ORDER BY t.BorrowDate DESC
LIMIT 10;

-- Recent Returns
SELECT 
    'Book Returned' as ActivityType,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    b.Title as BookTitle,
    t.ReturnDate as ActivityDate
FROM Transactions t
INNER JOIN Members m ON t.MemberID = m.MemberID
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.Status = 'Returned' AND t.ReturnDate IS NOT NULL
ORDER BY t.ReturnDate DESC
LIMIT 10;

-- Recent New Members
SELECT 
    'New Member' as ActivityType,
    CONCAT(FirstName, ' ', LastName) as MemberName,
    'Member Registration' as BookTitle,
    RegistrationDate as ActivityDate
FROM Members
ORDER BY RegistrationDate DESC
LIMIT 10;

-- ============================================================================
-- LIBRARY STAFF QUERIES
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Search Books
-- ----------------------------------------------------------------------------

-- Search by Title
SELECT * FROM Books 
WHERE Title LIKE '%search_term%' 
ORDER BY Title 
LIMIT 100;

-- Search by Author
SELECT * FROM Books 
WHERE Author LIKE '%search_term%' 
ORDER BY Author, Title 
LIMIT 100;

-- Search by ISBN
SELECT * FROM Books 
WHERE ISBN LIKE '%search_term%' 
ORDER BY ISBN 
LIMIT 100;

-- Search by Accession Number
SELECT * FROM Books 
WHERE AccessionNo LIKE '%search_term%' 
ORDER BY AccessionNo 
LIMIT 100;

-- Full Search (Title, Author, ISBN, AccessionNo)
SELECT * FROM Books
WHERE Title LIKE '%search_term%'
   OR Author LIKE '%search_term%'
   OR ISBN LIKE '%search_term%'
   OR AccessionNo LIKE '%search_term%'
ORDER BY Title
LIMIT 100;

-- ----------------------------------------------------------------------------
-- Search Members
-- ----------------------------------------------------------------------------

-- Search by Name
SELECT * FROM Members 
WHERE FirstName LIKE '%search_term%' 
   OR LastName LIKE '%search_term%'
ORDER BY LastName, FirstName 
LIMIT 100;

-- Search by Email
SELECT * FROM Members 
WHERE Email LIKE '%search_term%' 
ORDER BY Email 
LIMIT 100;

-- Search by Member ID
-- NOTE: Replace @search_term with actual value or use parameter
SELECT * FROM Members 
WHERE MemberID = @search_term 
   OR CAST(MemberID AS CHAR) LIKE CONCAT('%', @search_term, '%')
ORDER BY MemberID 
LIMIT 100;

-- Full Member Search
SELECT * FROM Members
WHERE FirstName LIKE '%search_term%'
   OR LastName LIKE '%search_term%'
   OR Email LIKE '%search_term%'
   OR CAST(MemberID AS CHAR) LIKE '%search_term%'
ORDER BY LastName, FirstName
LIMIT 100;

-- ----------------------------------------------------------------------------
-- Member Borrowing History
-- ----------------------------------------------------------------------------

SELECT 
    t.TransactionID,
    b.Title,
    b.Author,
    t.BorrowDate,
    t.DueDate,
    t.ReturnDate,
    t.Status,
    t.Fine
FROM Transactions t
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.MemberID = @MemberID
ORDER BY t.BorrowDate DESC;

-- ----------------------------------------------------------------------------
-- Currently Borrowed by Member
-- ----------------------------------------------------------------------------

SELECT 
    t.TransactionID,
    b.Title,
    b.Author,
    t.BorrowDate,
    t.DueDate,
    DATEDIFF(CURDATE(), t.DueDate) as DaysOverdue,
    t.RenewalCount
FROM Transactions t
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.MemberID = @MemberID
AND (t.Status = 'Borrowed' OR t.ReturnDate IS NULL)
ORDER BY t.DueDate ASC;

-- ----------------------------------------------------------------------------
-- Overdue Books List
-- ----------------------------------------------------------------------------

SELECT 
    t.TransactionID,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    m.Email as MemberEmail,
    b.Title,
    b.Author,
    t.BorrowDate,
    t.DueDate,
    DATEDIFF(CURDATE(), t.DueDate) as DaysOverdue
FROM Transactions t
INNER JOIN Members m ON t.MemberID = m.MemberID
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.Status = 'Borrowed'
AND t.DueDate < CURDATE()
AND t.ReturnDate IS NULL
ORDER BY t.DueDate ASC;

-- ----------------------------------------------------------------------------
-- Pending Reservations
-- ----------------------------------------------------------------------------

SELECT 
    r.ReservationID,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    b.Title,
    b.Author,
    r.ReservationDate,
    r.ExpiryDate,
    r.Priority,
    r.Status
FROM Reservations r
INNER JOIN Members m ON r.MemberID = m.MemberID
INNER JOIN Books b ON r.BookID = b.BookID
WHERE r.Status = 'Pending'
ORDER BY r.Priority DESC, r.ReservationDate ASC;

-- ----------------------------------------------------------------------------
-- Book Availability Check
-- ----------------------------------------------------------------------------

SELECT 
    BookID,
    Title,
    Author,
    TotalCopies,
    Available,
    (TotalCopies - Available) as CheckedOut,
    Status
FROM Books
WHERE BookID = @BookID;

-- ----------------------------------------------------------------------------
-- Member Fine Summary
-- ----------------------------------------------------------------------------

SELECT 
    f.FineID,
    f.Amount,
    f.Paid,
    (f.Amount - f.Paid) as Remaining,
    f.Status,
    f.CreatedDate,
    f.PaidDate,
    b.Title as BookTitle
FROM Fines f
LEFT JOIN Books b ON f.BookID = b.BookID
WHERE f.MemberID = @MemberID
ORDER BY f.CreatedDate DESC;

-- ----------------------------------------------------------------------------
-- Payment History for Member
-- ----------------------------------------------------------------------------

SELECT 
    PaymentID,
    ReceiptNumber,
    AmountPaid,
    PaymentMode,
    PaymentDate,
    ProcessedBy
FROM FinePayments
WHERE MemberID = @MemberID
ORDER BY PaymentDate DESC;

-- ============================================================================
-- MEMBER QUERIES
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Member's Currently Borrowed Books
-- ----------------------------------------------------------------------------

SELECT 
    t.TransactionID,
    b.BookID,
    b.Title,
    b.Author,
    t.BorrowDate,
    t.DueDate,
    DATEDIFF(t.DueDate, CURDATE()) as DaysRemaining,
    t.RenewalCount,
    CASE 
        WHEN t.DueDate < CURDATE() THEN 'Overdue'
        WHEN DATEDIFF(t.DueDate, CURDATE()) <= 3 THEN 'Due Soon'
        ELSE 'On Time'
    END as Status
FROM Transactions t
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.MemberID = @MemberID
AND (t.Status = 'Borrowed' OR t.ReturnDate IS NULL)
ORDER BY t.DueDate ASC;

-- ----------------------------------------------------------------------------
-- Member's Borrowing History
-- ----------------------------------------------------------------------------

SELECT 
    b.Title,
    b.Author,
    t.BorrowDate,
    t.ReturnDate,
    DATEDIFF(COALESCE(t.ReturnDate, NOW()), t.BorrowDate) as DaysBorrowed
FROM Transactions t
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.MemberID = @MemberID
AND t.ReturnDate IS NOT NULL
ORDER BY t.ReturnDate DESC
LIMIT 50;

-- ----------------------------------------------------------------------------
-- Member's Reservations
-- ----------------------------------------------------------------------------

SELECT 
    r.ReservationID,
    b.Title,
    b.Author,
    r.ReservationDate,
    r.PickupDate,
    r.ExpiryDate,
    r.Status,
    CASE 
        WHEN r.Status = 'Ready' THEN 'Ready for Pickup'
        WHEN r.Status = 'Pending' AND r.ExpiryDate < NOW() THEN 'Expired'
        WHEN r.Status = 'Pending' THEN 'Waiting'
        ELSE r.Status
    END as StatusDescription
FROM Reservations r
INNER JOIN Books b ON r.BookID = b.BookID
WHERE r.MemberID = @MemberID
ORDER BY r.ReservationDate DESC;

-- ----------------------------------------------------------------------------
-- Member's Fines
-- ----------------------------------------------------------------------------

SELECT 
    f.FineID,
    b.Title as BookTitle,
    f.Amount,
    f.Paid,
    (f.Amount - f.Paid) as Remaining,
    f.Status,
    f.CreatedDate,
    f.DaysOverdue,
    f.Description
FROM Fines f
LEFT JOIN Books b ON f.BookID = b.BookID
WHERE f.MemberID = @MemberID
AND f.Status IN ('Pending', 'Partial')
ORDER BY f.CreatedDate DESC;

-- ----------------------------------------------------------------------------
-- Member's Payment History
-- ----------------------------------------------------------------------------

SELECT 
    ReceiptNumber,
    AmountPaid,
    PaymentMode,
    PaymentDate,
    ProcessedBy
FROM FinePayments
WHERE MemberID = @MemberID
ORDER BY PaymentDate DESC;

-- ----------------------------------------------------------------------------
-- Member's Account Statistics
-- ----------------------------------------------------------------------------

SELECT 
    (SELECT COUNT(*) FROM Transactions WHERE MemberID = @MemberID) as TotalBorrowed,
    (SELECT COUNT(*) FROM Transactions WHERE MemberID = @MemberID AND (Status = 'Borrowed' OR ReturnDate IS NULL)) as CurrentlyBorrowed,
    (SELECT COUNT(*) FROM Reservations WHERE MemberID = @MemberID AND Status = 'Pending') as PendingReservations,
    (SELECT COALESCE(SUM(Amount - Paid), 0) FROM Fines WHERE MemberID = @MemberID AND Status IN ('Pending', 'Partial')) as PendingFines;

-- ----------------------------------------------------------------------------
-- Search Books (Member View)
-- ----------------------------------------------------------------------------

SELECT 
    BookID,
    Title,
    Author,
    Category,
    ISBN,
    TotalCopies,
    Available,
    Status,
    CASE 
        WHEN Available > 0 THEN 'Available'
        ELSE 'Not Available'
    END as Availability
FROM Books
WHERE (Title LIKE '%search_term%'
   OR Author LIKE '%search_term%'
   OR ISBN LIKE '%search_term%'
   OR Category LIKE '%search_term%')
AND Status = 'Available'
ORDER BY Title
LIMIT 100;

-- ============================================================================
-- UTILITY QUERIES (All Roles)
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Check Book Availability
-- ----------------------------------------------------------------------------

SELECT 
    BookID,
    Title,
    Available,
    TotalCopies,
    (TotalCopies - Available) as CheckedOut,
    CASE 
        WHEN Available > 0 THEN 'Available'
        ELSE 'Not Available'
    END as Status
FROM Books
WHERE BookID = @BookID;

-- ----------------------------------------------------------------------------
-- Get Book by Accession Number
-- ----------------------------------------------------------------------------

SELECT * FROM Books WHERE AccessionNo = @AccessionNo LIMIT 1;

-- ----------------------------------------------------------------------------
-- Get Member by Email
-- ----------------------------------------------------------------------------

SELECT * FROM Members WHERE Email = @Email LIMIT 1;

-- ----------------------------------------------------------------------------
-- Get Transaction Details
-- ----------------------------------------------------------------------------

SELECT 
    t.*,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    b.Title as BookTitle,
    b.Author as BookAuthor
FROM Transactions t
INNER JOIN Members m ON t.MemberID = m.MemberID
INNER JOIN Books b ON t.BookID = b.BookID
WHERE t.TransactionID = @TransactionID;

-- ----------------------------------------------------------------------------
-- Get Fine Details
-- ----------------------------------------------------------------------------

SELECT 
    f.*,
    CONCAT(m.FirstName, ' ', m.LastName) as MemberName,
    b.Title as BookTitle
FROM Fines f
INNER JOIN Members m ON f.MemberID = m.MemberID
LEFT JOIN Books b ON f.BookID = b.BookID
WHERE f.FineID = @FineID;

-- ============================================================================
-- REPORT QUERIES
-- ============================================================================

-- ----------------------------------------------------------------------------
-- Books by Category Report
-- ----------------------------------------------------------------------------

SELECT 
    Category,
    COUNT(*) as BookCount,
    SUM(TotalCopies) as TotalCopies,
    SUM(Available) as AvailableCopies,
    SUM(TotalCopies - Available) as CheckedOutCopies
FROM Books
GROUP BY Category
ORDER BY BookCount DESC;

-- ----------------------------------------------------------------------------
-- Member Type Distribution
-- ----------------------------------------------------------------------------

SELECT 
    COALESCE(Type, MemberType) as MemberType,
    COUNT(*) as MemberCount,
    SUM(CASE WHEN Status = 'Active' THEN 1 ELSE 0 END) as ActiveCount,
    SUM(CASE WHEN Status = 'Expired' THEN 1 ELSE 0 END) as ExpiredCount
FROM Members
GROUP BY COALESCE(Type, MemberType);

-- ----------------------------------------------------------------------------
-- Transaction Summary by Month
-- ----------------------------------------------------------------------------

SELECT 
    DATE_FORMAT(BorrowDate, '%Y-%m') as Month,
    COUNT(*) as TotalTransactions,
    SUM(CASE WHEN TransactionType = 'Borrow' THEN 1 ELSE 0 END) as Borrows,
    SUM(CASE WHEN TransactionType = 'Return' THEN 1 ELSE 0 END) as Returns,
    SUM(CASE WHEN TransactionType = 'Renew' THEN 1 ELSE 0 END) as Renewals
FROM Transactions
WHERE BorrowDate >= DATE_SUB(NOW(), INTERVAL 12 MONTH)
GROUP BY DATE_FORMAT(BorrowDate, '%Y-%m')
ORDER BY Month DESC;

-- ----------------------------------------------------------------------------
-- Fine Collection Report
-- ----------------------------------------------------------------------------

SELECT 
    DATE_FORMAT(PaymentDate, '%Y-%m') as Month,
    COUNT(*) as PaymentCount,
    SUM(AmountPaid) as TotalCollected,
    AVG(AmountPaid) as AveragePayment
FROM FinePayments
WHERE PaymentDate >= DATE_SUB(NOW(), INTERVAL 12 MONTH)
GROUP BY DATE_FORMAT(PaymentDate, '%Y-%m')
ORDER BY Month DESC;

-- ============================================================================
-- END OF COMMON QUERIES
-- ============================================================================
