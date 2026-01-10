-- ============================================================================
-- PANCHO-TORAJA-UY Library Management System
-- SAMPLE DATA AND STORED PROCEDURES
-- ============================================================================
-- This script inserts sample data and creates stored procedures
-- Execute this after running CORRECT_DATABASE_SCHEMA.sql
-- ============================================================================
-- NOTE: Make sure you have selected the correct database in SQLYog
-- or uncomment the line below and adjust the database name if needed
-- ============================================================================

-- Create database if it doesn't exist (uncomment if needed)
-- CREATE DATABASE IF NOT EXISTS library_db;
-- USE library_db;

-- ============================================================================
-- SAMPLE USERS DATA
-- ============================================================================
-- NOTE: Default password for all users is "password123"
-- Password hashes are in format: iterations:base64salt:base64hash
-- Users should change their passwords after first login
-- 
-- EMAIL FORMAT REQUIREMENTS:
-- - Admin: [name]@admin.umindanao.edu.ph (e.g., juan@admin.umindanao.edu.ph)
-- - Library Staff: [name]@library.umindanao.edu.ph (e.g., msantos@library.umindanao.edu.ph)
-- - Members: [name].member@umindanao.edu.ph (e.g., john.member@umindanao.edu.ph)
-- ============================================================================

-- Admin Users (Email format: [name]@admin.umindanao.edu.ph)
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate, Status) VALUES
('Juan', 'Toraja', 'juan@admin.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Admin', NOW(), 'Active'),
('Maria', 'Pancho', 'maria@admin.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Admin', NOW(), 'Active'),
('Carlos', 'Santos', 'carlos@admin.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Admin', NOW(), 'Active');

-- Library Staff Users (Email format: [name]@library.umindanao.edu.ph)
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate, Status) VALUES
('Ana', 'Garcia', 'agarcia@library.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'LibraryStaff', NOW(), 'Active'),
('Roberto', 'Lopez', 'rlopez@library.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'LibraryStaff', NOW(), 'Active'),
('Liza', 'Martinez', 'lmartinez@library.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'LibraryStaff', NOW(), 'Active'),
('Michael', 'Tan', 'mtan@library.umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'LibraryStaff', NOW(), 'Active');

-- Member Users
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate, Status) VALUES
('John', 'Doe', 'john.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Jane', 'Smith', 'jane.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Pedro', 'Cruz', 'pedro.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Sofia', 'Reyes', 'sofia.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Miguel', 'Villanueva', 'miguel.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Isabella', 'Fernandez', 'isabella.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Jose', 'Ramirez', 'jose.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Carmen', 'Torres', 'carmen.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Ricardo', 'Mendoza', 'ricardo.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active'),
('Elena', 'Gonzalez', 'elena.member@umindanao.edu.ph', '10000:VGhpcyBpcyBhIHNhbHQ=:dGVzdA==', 'Member', NOW(), 'Active');

-- ============================================================================
-- SAMPLE MEMBERS DATA
-- ============================================================================

INSERT INTO Members (UserID, FirstName, LastName, Email, Type, MemberType, RegistrationDate, ExpirationDate, Status, Contact, Address, MemberCardNumber) VALUES
((SELECT UserID FROM Users WHERE Email = 'john.member@umindanao.edu.ph'), 'John', 'Doe', 'john.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 6 MONTH), DATE_ADD(NOW(), INTERVAL 6 MONTH), 'Active', '09123456789', '123 Main Street, Davao City', 'MEM-202401-0001'),
((SELECT UserID FROM Users WHERE Email = 'jane.member@umindanao.edu.ph'), 'Jane', 'Smith', 'jane.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 1 YEAR), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Active', '09123456790', '456 University Avenue, Davao City', 'MEM-202401-0002'),
((SELECT UserID FROM Users WHERE Email = 'pedro.member@umindanao.edu.ph'), 'Pedro', 'Cruz', 'pedro.member@umindanao.edu.ph', 'Faculty', 'Faculty', DATE_SUB(NOW(), INTERVAL 2 YEAR), DATE_ADD(NOW(), INTERVAL 2 YEAR), 'Active', '09123456791', '789 Faculty Lane, Davao City', 'MEM-202402-0003'),
((SELECT UserID FROM Users WHERE Email = 'sofia.member@umindanao.edu.ph'), 'Sofia', 'Reyes', 'sofia.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 3 MONTH), DATE_ADD(NOW(), INTERVAL 9 MONTH), 'Active', '09123456792', '321 Student Road, Davao City', 'MEM-202402-0004'),
((SELECT UserID FROM Users WHERE Email = 'miguel.member@umindanao.edu.ph'), 'Miguel', 'Villanueva', 'miguel.member@umindanao.edu.ph', 'Staff', 'Staff', DATE_SUB(NOW(), INTERVAL 1 YEAR), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Active', '09123456793', '654 Staff Street, Davao City', 'MEM-202403-0005'),
((SELECT UserID FROM Users WHERE Email = 'isabella.member@umindanao.edu.ph'), 'Isabella', 'Fernandez', 'isabella.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 4 MONTH), DATE_ADD(NOW(), INTERVAL 8 MONTH), 'Active', '09123456794', '987 Campus Drive, Davao City', 'MEM-202403-0006'),
((SELECT UserID FROM Users WHERE Email = 'jose.member@umindanao.edu.ph'), 'Jose', 'Ramirez', 'jose.member@umindanao.edu.ph', 'Faculty', 'Faculty', DATE_SUB(NOW(), INTERVAL 3 YEAR), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Active', '09123456795', '147 Academic Way, Davao City', 'MEM-202404-0007'),
((SELECT UserID FROM Users WHERE Email = 'carmen.member@umindanao.edu.ph'), 'Carmen', 'Torres', 'carmen.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 2 MONTH), DATE_ADD(NOW(), INTERVAL 10 MONTH), 'Active', '09123456796', '258 Library Boulevard, Davao City', 'MEM-202404-0008'),
((SELECT UserID FROM Users WHERE Email = 'ricardo.member@umindanao.edu.ph'), 'Ricardo', 'Mendoza', 'ricardo.member@umindanao.edu.ph', 'Staff', 'Staff', DATE_SUB(NOW(), INTERVAL 6 MONTH), DATE_ADD(NOW(), INTERVAL 6 MONTH), 'Active', '09123456797', '369 Office Park, Davao City', 'MEM-202405-0009'),
((SELECT UserID FROM Users WHERE Email = 'elena.member@umindanao.edu.ph'), 'Elena', 'Gonzalez', 'elena.member@umindanao.edu.ph', 'Student', 'Student', DATE_SUB(NOW(), INTERVAL 5 MONTH), DATE_ADD(NOW(), INTERVAL 7 MONTH), 'Active', '09123456798', '741 Education Center, Davao City', 'MEM-202405-0010');

-- ============================================================================
-- SAMPLE BOOKS DATA
-- ============================================================================

INSERT INTO Books (Title, Subtitle, Author, Editor, ISBN, Category, Publisher, PublicationYear, Edition, Language, NumberOfPages, PhysicalDescription, TotalCopies, Available, Location, Status, AccessionNo, CallNumber, BookType, DateAdded, CreatedDate) VALUES
('Introduction to Computer Science', 'Fundamentals and Applications', 'John Smith', NULL, '978-0-123456-78-9', 'Computer Science', 'Tech Publishers', 2023, '5th Edition', 'English', 650, 'Hardcover, 25cm x 20cm', 5, 5, 'Section A, Shelf 1', 'Available', 'ACC-2024-0001', 'QA76.123.S65', 'Circulation', NOW(), NOW()),
('Database Systems', 'Design and Implementation', 'Maria Garcia', NULL, '978-0-234567-89-0', 'Computer Science', 'Academic Press', 2022, '4th Edition', 'English', 720, 'Hardcover, 26cm x 21cm', 3, 3, 'Section A, Shelf 2', 'Available', 'ACC-2024-0002', 'QA76.9.D3.G37', 'Circulation', NOW(), NOW()),
('Data Structures and Algorithms', NULL, 'Robert Johnson', NULL, '978-0-345678-90-1', 'Computer Science', 'Programming Books Inc.', 2024, '3rd Edition', 'English', 580, 'Paperback, 24cm x 19cm', 4, 4, 'Section A, Shelf 3', 'Available', 'ACC-2024-0003', 'QA76.9.D35.J64', 'Circulation', NOW(), NOW()),
('Software Engineering Principles', 'A Practical Approach', 'Sarah Williams', NULL, '978-0-456789-01-2', 'Computer Science', 'Software Publishing', 2023, '2nd Edition', 'English', 690, 'Hardcover, 25cm x 20cm', 3, 2, 'Section A, Shelf 4', 'Available', 'ACC-2024-0004', 'QA76.758.W55', 'Circulation', NOW(), NOW()),
('Network Security Fundamentals', NULL, 'David Brown', NULL, '978-0-567890-12-3', 'Computer Science', 'Security Books', 2024, '1st Edition', 'English', 540, 'Hardcover, 26cm x 21cm', 2, 2, 'Section A, Shelf 5', 'Available', 'ACC-2024-0005', 'TK5105.59.B76', 'Circulation', NOW(), NOW()),
('The History of the Philippines', 'From Pre-Colonial to Modern Times', 'Jose Rizal', NULL, '978-0-678901-23-4', 'History', 'Philippine Historical Society', 2022, 'Revised Edition', 'English', 850, 'Hardcover, 27cm x 22cm', 6, 6, 'Section B, Shelf 1', 'Available', 'ACC-2024-0006', 'DS655.R59', 'Circulation', NOW(), NOW()),
('Filipino Literature', 'A Comprehensive Anthology', 'Lualhati Bautista', NULL, '978-0-789012-34-5', 'Literature', 'Literary Press', 2023, '1st Edition', 'Filipino', 920, 'Hardcover, 28cm x 23cm', 4, 4, 'Section B, Shelf 2', 'Available', 'ACC-2024-0007', 'PL5531.B38', 'Circulation', NOW(), NOW()),
('Mathematics for Engineers', 'Calculus and Linear Algebra', 'Michael Chen', NULL, '978-0-890123-45-6', 'Mathematics', 'Engineering Books', 2023, '6th Edition', 'English', 780, 'Hardcover, 25cm x 20cm', 5, 5, 'Section C, Shelf 1', 'Available', 'ACC-2024-0008', 'TA330.C44', 'Circulation', NOW(), NOW()),
('Physics Fundamentals', 'Mechanics and Thermodynamics', 'Lisa Anderson', NULL, '978-0-901234-56-7', 'Physics', 'Science Publishers', 2024, '5th Edition', 'English', 650, 'Hardcover, 26cm x 21cm', 4, 4, 'Section C, Shelf 2', 'Available', 'ACC-2024-0009', 'QC21.A53', 'Circulation', NOW(), NOW()),
('Chemistry Principles', 'Organic and Inorganic Chemistry', 'James Wilson', NULL, '978-0-012345-67-8', 'Chemistry', 'Chemistry Books', 2023, '4th Edition', 'English', 720, 'Hardcover, 25cm x 20cm', 3, 3, 'Section C, Shelf 3', 'Available', 'ACC-2024-0010', 'QD31.2.W55', 'Circulation', NOW(), NOW()),
('Business Management', 'Strategic Planning and Operations', 'Patricia Martinez', NULL, '978-0-123456-78-0', 'Business', 'Business Press', 2023, '3rd Edition', 'English', 680, 'Hardcover, 26cm x 21cm', 4, 4, 'Section D, Shelf 1', 'Available', 'ACC-2024-0011', 'HD31.M37', 'Circulation', NOW(), NOW()),
('Marketing Strategies', 'Digital and Traditional Approaches', 'Richard Lee', NULL, '978-0-234567-89-1', 'Business', 'Marketing Books', 2024, '2nd Edition', 'English', 590, 'Paperback, 24cm x 19cm', 3, 3, 'Section D, Shelf 2', 'Available', 'ACC-2024-0012', 'HF5415.L44', 'Circulation', NOW(), NOW()),
('Psychology Today', 'Understanding Human Behavior', 'Jennifer Taylor', NULL, '978-0-345678-90-2', 'Psychology', 'Psychology Press', 2023, '7th Edition', 'English', 750, 'Hardcover, 27cm x 22cm', 5, 5, 'Section E, Shelf 1', 'Available', 'ACC-2024-0013', 'BF121.T39', 'Circulation', NOW(), NOW()),
('Sociology in the Modern World', NULL, 'Christopher Moore', NULL, '978-0-456789-01-3', 'Sociology', 'Social Science Books', 2022, '5th Edition', 'English', 640, 'Hardcover, 25cm x 20cm', 4, 4, 'Section E, Shelf 2', 'Available', 'ACC-2024-0014', 'HM51.M66', 'Circulation', NOW(), NOW()),
('Environmental Science', 'Climate Change and Sustainability', 'Amanda Green', NULL, '978-0-567890-12-4', 'Environmental Science', 'Eco Publishers', 2024, '1st Edition', 'English', 710, 'Hardcover, 26cm x 21cm', 3, 3, 'Section F, Shelf 1', 'Available', 'ACC-2024-0015', 'GE105.G74', 'Circulation', NOW(), NOW()),
('Research Methods', 'A Guide for Students', 'Daniel White', NULL, '978-0-678901-23-5', 'Research', 'Academic Research Press', 2023, '4th Edition', 'English', 560, 'Paperback, 24cm x 19cm', 4, 4, 'Section F, Shelf 2', 'Available', 'ACC-2024-0016', 'H62.W48', 'Circulation', NOW(), NOW()),
('English Grammar', 'Complete Reference Guide', 'Susan Black', NULL, '978-0-789012-34-6', 'Language', 'Language Books', 2022, '8th Edition', 'English', 480, 'Hardcover, 25cm x 20cm', 6, 6, 'Section G, Shelf 1', 'Available', 'ACC-2024-0017', 'PE1112.B53', 'Circulation', NOW(), NOW()),
('Creative Writing', 'Fiction and Non-Fiction', 'Thomas Gray', NULL, '978-0-890123-45-7', 'Writing', 'Creative Press', 2024, '2nd Edition', 'English', 520, 'Paperback, 24cm x 19cm', 3, 3, 'Section G, Shelf 2', 'Available', 'ACC-2024-0018', 'PN3355.G73', 'Circulation', NOW(), NOW()),
('Art and Design', 'Principles and Practice', 'Emily Davis', NULL, '978-0-901234-56-8', 'Arts', 'Art Publishers', 2023, '3rd Edition', 'English', 600, 'Hardcover, 28cm x 23cm', 4, 4, 'Section H, Shelf 1', 'Available', 'ACC-2024-0019', 'N7430.D38', 'Circulation', NOW(), NOW()),
('Music Theory', 'From Basics to Advanced', 'Kevin Harris', NULL, '978-0-012345-67-9', 'Music', 'Music Books', 2022, '6th Edition', 'English', 550, 'Hardcover, 25cm x 20cm', 3, 3, 'Section H, Shelf 2', 'Available', 'ACC-2024-0020', 'MT6.H37', 'Circulation', NOW(), NOW());

-- ============================================================================
-- SAMPLE TRANSACTIONS DATA (Some borrowed books)
-- ============================================================================

INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, ReturnDate, Status, TransactionType, Fine, RenewalCount, RenewedCount) VALUES
((SELECT MemberID FROM Members WHERE Email = 'john.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0001'), 
 DATE_SUB(NOW(), INTERVAL 5 DAY), DATE_ADD(NOW(), INTERVAL 9 DAY), NULL, 'Borrowed', 'Borrow', 0.00, 0, 0),
((SELECT MemberID FROM Members WHERE Email = 'jane.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0004'), 
 DATE_SUB(NOW(), INTERVAL 3 DAY), DATE_ADD(NOW(), INTERVAL 11 DAY), NULL, 'Borrowed', 'Borrow', 0.00, 0, 0),
((SELECT MemberID FROM Members WHERE Email = 'pedro.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0006'), 
 DATE_SUB(NOW(), INTERVAL 10 DAY), DATE_SUB(NOW(), INTERVAL 2 DAY), DATE_SUB(NOW(), INTERVAL 1 DAY), 'Returned', 'Return', 2.00, 0, 0),
((SELECT MemberID FROM Members WHERE Email = 'sofia.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0008'), 
 DATE_SUB(NOW(), INTERVAL 7 DAY), DATE_ADD(NOW(), INTERVAL 7 DAY), NULL, 'Borrowed', 'Borrow', 0.00, 0, 0);

-- Update Available count for borrowed books
UPDATE Books SET Available = Available - 1 WHERE BookID IN (
    SELECT BookID FROM Transactions WHERE Status = 'Borrowed'
);

-- ============================================================================
-- SAMPLE RESERVATIONS DATA
-- ============================================================================

INSERT INTO Reservations (MemberID, BookID, ReservationDate, PickupDate, ExpiryDate, Status, Priority, FulfilledDate) VALUES
((SELECT MemberID FROM Members WHERE Email = 'miguel.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0002'), 
 DATE_SUB(NOW(), INTERVAL 2 DAY), NULL, DATE_ADD(NOW(), INTERVAL 3 DAY), 'Pending', 1, NULL),
((SELECT MemberID FROM Members WHERE Email = 'isabella.member@umindanao.edu.ph'), 
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0005'), 
 DATE_SUB(NOW(), INTERVAL 1 DAY), NULL, DATE_ADD(NOW(), INTERVAL 4 DAY), 'Pending', 1, NULL);

-- ============================================================================
-- SAMPLE FINES DATA
-- ============================================================================

INSERT INTO Fines (MemberID, BookID, TransactionID, FineType, Amount, Paid, Status, DaysOverdue, CreatedDate, PaidDate, Description) VALUES
((SELECT MemberID FROM Members WHERE Email = 'pedro.member@umindanao.edu.ph'),
 (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0006'),
 (SELECT TransactionID FROM Transactions WHERE MemberID = (SELECT MemberID FROM Members WHERE Email = 'pedro.member@umindanao.edu.ph') AND BookID = (SELECT BookID FROM Books WHERE AccessionNo = 'ACC-2024-0006') LIMIT 1),
 'Overdue', 2.00, 0.00, 'Pending', 1, DATE_SUB(NOW(), INTERVAL 1 DAY), NULL, 'Late return fine');

-- ============================================================================
-- STORED PROCEDURES
-- ============================================================================

DELIMITER //

-- ============================================================================
-- SP_BORROWBOOK - Borrow a book
-- ============================================================================
DROP PROCEDURE IF EXISTS sp_BorrowBook//
CREATE PROCEDURE sp_BorrowBook(
    IN p_MemberID INT,
    IN p_BookID INT,
    IN p_BorrowDate DATETIME,
    IN p_DueDate DATETIME,
    OUT p_TransactionID INT,
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_Available INT DEFAULT 0;
    DECLARE v_BookStatus VARCHAR(50);
    DECLARE v_MemberStatus VARCHAR(50);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_Message = 'Error occurred during book borrowing';
    END;
    
    START TRANSACTION;
    
    SELECT Available, Status INTO v_Available, v_BookStatus
    FROM Books WHERE BookID = p_BookID;
    
    SELECT Status INTO v_MemberStatus
    FROM Members WHERE MemberID = p_MemberID;
    
    IF v_BookStatus IS NULL THEN
        SET p_Success = 0;
        SET p_Message = 'Book not found';
        ROLLBACK;
    ELSEIF v_Available <= 0 THEN
        SET p_Success = 0;
        SET p_Message = 'Book is not available';
        ROLLBACK;
    ELSEIF v_BookStatus != 'Available' THEN
        SET p_Success = 0;
        SET p_Message = 'Book status is not available for borrowing';
        ROLLBACK;
    ELSEIF v_MemberStatus != 'Active' THEN
        SET p_Success = 0;
        SET p_Message = 'Member is not active';
        ROLLBACK;
    ELSE
        INSERT INTO Transactions (MemberID, BookID, BorrowDate, DueDate, Status, TransactionType, Fine, RenewalCount, RenewedCount)
        VALUES (p_MemberID, p_BookID, p_BorrowDate, p_DueDate, 'Borrowed', 'Borrow', 0.00, 0, 0);
        
        SET p_TransactionID = LAST_INSERT_ID();
        
        UPDATE Books SET Available = Available - 1 WHERE BookID = p_BookID;
        
        SET p_Success = 1;
        SET p_Message = 'Book borrowed successfully';
        COMMIT;
    END IF;
END//

-- ============================================================================
-- SP_RETURNBOOK - Return a book
-- ============================================================================
DROP PROCEDURE IF EXISTS sp_ReturnBook//
CREATE PROCEDURE sp_ReturnBook(
    IN p_TransactionID INT,
    IN p_ReturnDate DATETIME,
    IN p_FineRatePerDay DECIMAL(10,2),
    IN p_MaxFineCap DECIMAL(10,2),
    OUT p_Success BIT,
    OUT p_FineAmount DECIMAL(10,2),
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_BookID INT;
    DECLARE v_DueDate DATETIME;
    DECLARE v_DaysOverdue INT DEFAULT 0;
    DECLARE v_CalculatedFine DECIMAL(10,2) DEFAULT 0.00;
    DECLARE v_Status VARCHAR(50);
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_FineAmount = 0.00;
        SET p_Message = 'Error occurred during book return';
    END;
    
    START TRANSACTION;
    
    SELECT BookID, DueDate, Status INTO v_BookID, v_DueDate, v_Status
    FROM Transactions
    WHERE TransactionID = p_TransactionID;
    
    IF v_BookID IS NULL THEN
        SET p_Success = 0;
        SET p_FineAmount = 0.00;
        SET p_Message = 'Transaction not found';
        ROLLBACK;
    ELSEIF v_Status = 'Returned' THEN
        SET p_Success = 0;
        SET p_FineAmount = 0.00;
        SET p_Message = 'Book already returned';
        ROLLBACK;
    ELSE
        IF p_ReturnDate > v_DueDate THEN
            SET v_DaysOverdue = DATEDIFF(p_ReturnDate, v_DueDate);
            SET v_CalculatedFine = v_DaysOverdue * p_FineRatePerDay;
            IF v_CalculatedFine > p_MaxFineCap THEN
                SET v_CalculatedFine = p_MaxFineCap;
            END IF;
        END IF;
        
        UPDATE Transactions
        SET ReturnDate = p_ReturnDate,
            Status = 'Returned',
            TransactionType = 'Return',
            Fine = v_CalculatedFine
        WHERE TransactionID = p_TransactionID;
        
        UPDATE Books SET Available = Available + 1 WHERE BookID = v_BookID;
        
        IF v_CalculatedFine > 0 THEN
            INSERT INTO Fines (MemberID, BookID, TransactionID, FineType, Amount, Paid, Status, DaysOverdue, CreatedDate, Description)
            SELECT MemberID, BookID, TransactionID, 'Overdue', v_CalculatedFine, 0.00, 'Pending', v_DaysOverdue, NOW(), 'Late return fine'
            FROM Transactions WHERE TransactionID = p_TransactionID;
        END IF;
        
        SET p_Success = 1;
        SET p_FineAmount = v_CalculatedFine;
        SET p_Message = CONCAT('Book returned successfully. Fine: ', v_CalculatedFine);
        COMMIT;
    END IF;
END//

-- ============================================================================
-- SP_PROCESSPAYMENT - Process a fine payment
-- ============================================================================
DROP PROCEDURE IF EXISTS sp_ProcessPayment//
CREATE PROCEDURE sp_ProcessPayment(
    IN p_TransactionID INT,
    IN p_MemberID INT,
    IN p_AmountPaid DECIMAL(10,2),
    IN p_PaymentMode VARCHAR(50),
    IN p_ProcessedBy VARCHAR(100),
    OUT p_ReceiptNumber VARCHAR(50),
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_ReceiptCounter INT DEFAULT 0;
    DECLARE v_DatePrefix VARCHAR(20);
    DECLARE v_FineID INT;
    DECLARE v_FineAmount DECIMAL(10,2);
    DECLARE v_PaidAmount DECIMAL(10,2);
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_ReceiptNumber = '';
        SET p_Message = 'Error occurred during payment processing';
    END;
    
    START TRANSACTION;
    
    SET v_DatePrefix = DATE_FORMAT(NOW(), '%Y%m%d');
    
    SELECT COUNT(*) + 1 INTO v_ReceiptCounter
    FROM FinePayments
    WHERE DATE(PaymentDate) = CURDATE();
    
    SET p_ReceiptNumber = CONCAT('RCP-', v_DatePrefix, '-', LPAD(v_ReceiptCounter, 6, '0'));
    
    SELECT FineID, Amount, Paid INTO v_FineID, v_FineAmount, v_PaidAmount
    FROM Fines
    WHERE TransactionID = p_TransactionID AND MemberID = p_MemberID
    ORDER BY CreatedDate DESC LIMIT 1;
    
    IF v_FineID IS NULL THEN
        SET p_Success = 0;
        SET p_ReceiptNumber = '';
        SET p_Message = 'Fine record not found';
        ROLLBACK;
    ELSE
        INSERT INTO FinePayments (TransactionID, MemberID, AmountPaid, PaymentMode, PaymentDate, ReceiptNumber, ProcessedBy)
        VALUES (p_TransactionID, p_MemberID, p_AmountPaid, p_PaymentMode, NOW(), p_ReceiptNumber, p_ProcessedBy);
        
        SET v_PaidAmount = v_PaidAmount + p_AmountPaid;
        
        UPDATE Fines
        SET Paid = v_PaidAmount,
            Status = CASE
                WHEN v_PaidAmount >= v_FineAmount THEN 'Paid'
                WHEN v_PaidAmount > 0 THEN 'Partial'
                ELSE 'Pending'
            END,
            PaidDate = CASE
                WHEN v_PaidAmount >= v_FineAmount THEN NOW()
                ELSE PaidDate
            END
        WHERE FineID = v_FineID;
        
        SET p_Success = 1;
        SET p_Message = 'Payment processed successfully';
        COMMIT;
    END IF;
END//

-- ============================================================================
-- SP_RENEWBOOK - Renew a borrowed book
-- ============================================================================
DROP PROCEDURE IF EXISTS sp_RenewBook//
CREATE PROCEDURE sp_RenewBook(
    IN p_TransactionID INT,
    IN p_NewDueDate DATETIME,
    IN p_MaxRenewals INT,
    OUT p_Success BIT,
    OUT p_Message VARCHAR(255)
)
BEGIN
    DECLARE v_CurrentRenewals INT DEFAULT 0;
    DECLARE v_Status VARCHAR(50);
    DECLARE v_BookID INT;
    DECLARE v_MemberID INT;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Success = 0;
        SET p_Message = 'Error occurred during book renewal';
    END;
    
    START TRANSACTION;
    
    SELECT RenewalCount, Status, BookID, MemberID INTO v_CurrentRenewals, v_Status, v_BookID, v_MemberID
    FROM Transactions
    WHERE TransactionID = p_TransactionID;
    
    IF v_BookID IS NULL THEN
        SET p_Success = 0;
        SET p_Message = 'Transaction not found';
        ROLLBACK;
    ELSEIF v_Status != 'Borrowed' THEN
        SET p_Success = 0;
        SET p_Message = 'Book is not currently borrowed';
        ROLLBACK;
    ELSEIF v_CurrentRenewals >= p_MaxRenewals THEN
        SET p_Success = 0;
        SET p_Message = CONCAT('Maximum renewals (', p_MaxRenewals, ') reached');
        ROLLBACK;
    ELSE
        UPDATE Transactions
        SET DueDate = p_NewDueDate,
            TransactionType = 'Renew',
            RenewalCount = RenewalCount + 1,
            RenewedCount = RenewedCount + 1
        WHERE TransactionID = p_TransactionID;
        
        SET p_Success = 1;
        SET p_Message = CONCAT('Book renewed successfully. Renewal count: ', v_CurrentRenewals + 1);
        COMMIT;
    END IF;
END//

DELIMITER ;

-- ============================================================================
-- PASSWORD SETUP (Optional - Use application's password reset feature)
-- ============================================================================
-- NOTE: The password hashes above are placeholders.
-- To set actual passwords, use the application's "Forgot Password" feature
-- or register new users through the application interface.
-- 
-- Alternatively, you can manually update passwords using the application's
-- PasswordHasher class, which generates hashes in format: iterations:salt:hash
-- ============================================================================

-- ============================================================================
-- VERIFICATION QUERIES
-- ============================================================================

-- Check users
-- SELECT Role, COUNT(*) as Count FROM Users GROUP BY Role;

-- Check members
-- SELECT Type, COUNT(*) as Count FROM Members GROUP BY Type;

-- Check books
-- SELECT Category, COUNT(*) as Count, SUM(TotalCopies) as TotalCopies, SUM(Available) as Available FROM Books GROUP BY Category;

-- Check transactions
-- SELECT Status, COUNT(*) as Count FROM Transactions GROUP BY Status;

-- Check reservations
-- SELECT Status, COUNT(*) as Count FROM Reservations GROUP BY Status;

-- Check fines
-- SELECT Status, COUNT(*) as Count, SUM(Amount) as TotalAmount, SUM(Paid) as TotalPaid FROM Fines GROUP BY Status;

-- ============================================================================
-- END OF SAMPLE DATA AND STORED PROCEDURES
-- ============================================================================
