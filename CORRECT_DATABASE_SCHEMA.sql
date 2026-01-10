-- ============================================================================
-- PANCHO-TORAJA-UY Library Management System
-- CORRECT DATABASE SCHEMA - Matches Project Code Exactly
-- ============================================================================
-- This schema matches the actual queries and operations used in the project
-- Execute this script in SQLYog to create the database structure
-- ============================================================================

-- Drop existing tables if needed (uncomment if recreating)
-- DROP TABLE IF EXISTS FineAdjustments;
-- DROP TABLE IF EXISTS FinePayments;
-- DROP TABLE IF EXISTS Fines;
-- DROP TABLE IF EXISTS Reservations;
-- DROP TABLE IF EXISTS Transactions;
-- DROP TABLE IF EXISTS Inventory;
-- DROP TABLE IF EXISTS BookCopies;
-- DROP TABLE IF EXISTS Books;
-- DROP TABLE IF EXISTS Members;
-- DROP TABLE IF EXISTS Users;
-- DROP TABLE IF EXISTS Settings;

-- ============================================================================
-- 1. USERS TABLE (Login, Admin, Library Staff, Member Authentication)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastLoginDate DATETIME NULL,
    Status VARCHAR(50) DEFAULT 'Active'
);

-- ============================================================================
-- 2. MEMBERS TABLE (Member Information)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Members (
    MemberID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT UNIQUE NULL,
    FirstName VARCHAR(100) NULL,
    LastName VARCHAR(100) NULL,
    Email VARCHAR(150) NULL,
    Type VARCHAR(50) NULL,
    MemberType VARCHAR(50) NULL,
    RegistrationDate DATETIME NULL,
    ExpirationDate DATETIME NULL,
    Status VARCHAR(50) DEFAULT 'Active',
    Contact VARCHAR(50) NULL,
    Address VARCHAR(255) NULL,
    PhotoPath VARCHAR(255) NULL,
    ValidIDPath VARCHAR(255) NULL,
    MemberCardNumber VARCHAR(50) NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
);

-- ============================================================================
-- 3. BOOKS TABLE (Book Catalog - Main Table for Circulation)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Books (
    BookID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Subtitle VARCHAR(255) NULL,
    Author VARCHAR(255) NOT NULL,
    Editor VARCHAR(255) NULL,
    ISBN VARCHAR(20) NULL,
    Category VARCHAR(100) NULL,
    Publisher VARCHAR(255) NULL,
    PublicationYear INT NULL,
    Edition VARCHAR(50) NULL,
    Language VARCHAR(50) NULL,
    NumberOfPages INT NULL,
    PhysicalDescription TEXT NULL,
    TotalCopies INT DEFAULT 1,
    Available INT DEFAULT 1,
    Location VARCHAR(100) NULL,
    Status VARCHAR(50) DEFAULT 'Available',
    AccessionNo VARCHAR(100) NULL,
    CallNumber VARCHAR(50) NULL,
    BookType VARCHAR(50) DEFAULT 'Circulation',
    CoverImagePath VARCHAR(255) NULL,
    Barcode VARCHAR(100) NULL,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================================
-- 4. TRANSACTIONS TABLE (Borrow/Return Operations)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Transactions (
    TransactionID INT AUTO_INCREMENT PRIMARY KEY,
    MemberID INT NOT NULL,
    BookID INT NOT NULL,
    BorrowDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    Status VARCHAR(50) DEFAULT 'Borrowed',
    TransactionType VARCHAR(50) DEFAULT 'Borrow',
    Fine DECIMAL(10,2) DEFAULT 0.00,
    RenewalCount INT DEFAULT 0,
    RenewedCount INT DEFAULT 0,
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
);

-- ============================================================================
-- 5. RESERVATIONS TABLE (Book Reservations)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Reservations (
    ReservationID INT AUTO_INCREMENT PRIMARY KEY,
    MemberID INT NOT NULL,
    BookID INT NOT NULL,
    ReservationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PickupDate DATETIME NULL,
    ExpiryDate DATETIME NULL,
    Status VARCHAR(50) DEFAULT 'Pending',
    Priority INT DEFAULT 0,
    FulfilledDate DATETIME NULL,
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
);

-- ============================================================================
-- 6. FINES TABLE (Fine Management)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Fines (
    FineID INT AUTO_INCREMENT PRIMARY KEY,
    MemberID INT NOT NULL,
    BookID INT NULL,
    TransactionID INT NULL,
    FineType VARCHAR(50) DEFAULT 'Overdue',
    Amount DECIMAL(10,2) NOT NULL,
    Paid DECIMAL(10,2) DEFAULT 0.00,
    Status VARCHAR(50) DEFAULT 'Pending',
    DaysOverdue INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PaidDate DATETIME NULL,
    WaivedDate DATETIME NULL,
    Description VARCHAR(255) NULL,
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE SET NULL,
    FOREIGN KEY (TransactionID) REFERENCES Transactions(TransactionID) ON DELETE SET NULL
);

-- ============================================================================
-- 7. FINEPAYMENTS TABLE (Payment History)
-- ============================================================================
CREATE TABLE IF NOT EXISTS FinePayments (
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    TransactionID INT NULL,
    MemberID INT NOT NULL,
    AmountPaid DECIMAL(10,2) NOT NULL,
    PaymentMode VARCHAR(50) DEFAULT 'Cash',
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ReceiptNumber VARCHAR(50) NULL,
    ProcessedBy VARCHAR(100) NULL,
    Notes VARCHAR(255) NULL,
    IsWaived TINYINT(1) DEFAULT 0,
    WaiverReason VARCHAR(255) NULL,
    WaivedBy VARCHAR(100) NULL,
    WaiverDate DATETIME NULL,
    FOREIGN KEY (TransactionID) REFERENCES Transactions(TransactionID) ON DELETE SET NULL,
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE
);

-- ============================================================================
-- 8. FINEADJUSTMENTS TABLE (Fine Adjustments/Waivers)
-- ============================================================================
CREATE TABLE IF NOT EXISTS FineAdjustments (
    AdjustmentID INT AUTO_INCREMENT PRIMARY KEY,
    TransactionID INT NULL,
    MemberID INT NOT NULL,
    OriginalAmount DECIMAL(10,2) NOT NULL,
    AdjustedAmount DECIMAL(10,2) NOT NULL,
    AdjustmentAmount DECIMAL(10,2) NOT NULL,
    AdjustmentType VARCHAR(50) DEFAULT 'Waiver',
    Reason VARCHAR(255) NULL,
    AdjustedBy VARCHAR(100) NULL,
    AdjustmentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ApprovalRequired VARCHAR(10) DEFAULT 'No',
    ApprovedBy VARCHAR(100) NULL,
    ApprovalDate DATETIME NULL,
    FOREIGN KEY (TransactionID) REFERENCES Transactions(TransactionID) ON DELETE SET NULL,
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID) ON DELETE CASCADE
);

-- ============================================================================
-- 9. INVENTORY TABLE (Optional - Detailed Copy Tracking)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Inventory (
    InventoryID INT AUTO_INCREMENT PRIMARY KEY,
    BookID INT NOT NULL,
    CopyNumber INT NOT NULL,
    Location VARCHAR(50) NULL,
    `Condition` VARCHAR(50) DEFAULT 'Good',
    Status VARCHAR(50) DEFAULT 'Available',
    LastVerified DATETIME NULL,
    Notes VARCHAR(255) NULL,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
);

-- ============================================================================
-- 10. BOOKCOPIES TABLE (Optional - Alternative Copy Tracking)
-- ============================================================================
CREATE TABLE IF NOT EXISTS BookCopies (
    CopyID INT AUTO_INCREMENT PRIMARY KEY,
    BookID INT NOT NULL,
    AccessionNumber VARCHAR(100) NULL,
    Barcode VARCHAR(100) NULL,
    CopyStatus VARCHAR(50) DEFAULT 'Available',
    Location VARCHAR(100) NULL,
    Notes VARCHAR(255) NULL,
    LastCheckedOut DATETIME NULL,
    LastReturned DATETIME NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedDate DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
);

-- ============================================================================
-- 11. SETTINGS TABLE (System Settings)
-- ============================================================================
CREATE TABLE IF NOT EXISTS Settings (
    SettingKey VARCHAR(100) PRIMARY KEY,
    SettingValue TEXT NULL,
    Category VARCHAR(50) NULL,
    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- ============================================================================
-- INDEXES FOR PERFORMANCE OPTIMIZATION
-- ============================================================================
-- Note: MySQL doesn't support "IF NOT EXISTS" for CREATE INDEX.
-- If an index already exists, you'll get an error which can be safely ignored.
-- To avoid errors, you can check if index exists first or just ignore duplicate errors.
-- ============================================================================

-- Users table indexes
-- Note: MySQL doesn't support IF NOT EXISTS for indexes. If index exists, error can be ignored.
CREATE INDEX idx_users_email ON Users(Email);
CREATE INDEX idx_users_role ON Users(Role);
CREATE INDEX idx_users_status ON Users(Status);

-- Members table indexes
CREATE INDEX idx_members_email ON Members(Email);
CREATE INDEX idx_members_firstname ON Members(FirstName);
CREATE INDEX idx_members_lastname ON Members(LastName);
CREATE INDEX idx_members_type ON Members(Type);
CREATE INDEX idx_members_membertype ON Members(MemberType);
CREATE INDEX idx_members_status ON Members(Status);
CREATE INDEX idx_members_userid ON Members(UserID);

-- Books table indexes
CREATE INDEX idx_books_title ON Books(Title);
CREATE INDEX idx_books_author ON Books(Author);
CREATE INDEX idx_books_isbn ON Books(ISBN);
CREATE INDEX idx_books_accessionno ON Books(AccessionNo);
CREATE INDEX idx_books_category ON Books(Category);
CREATE INDEX idx_books_status ON Books(Status);
CREATE INDEX idx_books_barcode ON Books(Barcode);

-- Transactions table indexes
CREATE INDEX idx_transactions_memberid ON Transactions(MemberID);
CREATE INDEX idx_transactions_bookid ON Transactions(BookID);
CREATE INDEX idx_transactions_status ON Transactions(Status);
CREATE INDEX idx_transactions_borrowdate ON Transactions(BorrowDate);
CREATE INDEX idx_transactions_duedate ON Transactions(DueDate);
CREATE INDEX idx_transactions_returndate ON Transactions(ReturnDate);

-- Reservations table indexes
CREATE INDEX idx_reservations_memberid ON Reservations(MemberID);
CREATE INDEX idx_reservations_bookid ON Reservations(BookID);
CREATE INDEX idx_reservations_status ON Reservations(Status);
CREATE INDEX idx_reservations_reservationdate ON Reservations(ReservationDate);

-- Fines table indexes
CREATE INDEX idx_fines_memberid ON Fines(MemberID);
CREATE INDEX idx_fines_bookid ON Fines(BookID);
CREATE INDEX idx_fines_transactionid ON Fines(TransactionID);
CREATE INDEX idx_fines_status ON Fines(Status);
CREATE INDEX idx_fines_createddate ON Fines(CreatedDate);

-- FinePayments table indexes
CREATE INDEX idx_finepayments_memberid ON FinePayments(MemberID);
CREATE INDEX idx_finepayments_transactionid ON FinePayments(TransactionID);
CREATE INDEX idx_finepayments_paymentdate ON FinePayments(PaymentDate);
CREATE INDEX idx_finepayments_receiptnumber ON FinePayments(ReceiptNumber);

-- FineAdjustments table indexes
CREATE INDEX idx_fineadjustments_memberid ON FineAdjustments(MemberID);
CREATE INDEX idx_fineadjustments_transactionid ON FineAdjustments(TransactionID);
CREATE INDEX idx_fineadjustments_adjustmentdate ON FineAdjustments(AdjustmentDate);

-- Inventory table indexes
CREATE INDEX idx_inventory_bookid ON Inventory(BookID);
CREATE INDEX idx_inventory_status ON Inventory(Status);
CREATE INDEX idx_inventory_condition ON Inventory(`Condition`);

-- BookCopies table indexes
CREATE INDEX idx_bookcopies_bookid ON BookCopies(BookID);
CREATE INDEX idx_bookcopies_copystatus ON BookCopies(CopyStatus);
CREATE INDEX idx_bookcopies_accessionnumber ON BookCopies(AccessionNumber);
CREATE INDEX idx_bookcopies_barcode ON BookCopies(Barcode);

-- ============================================================================
-- FULL-TEXT SEARCH INDEX FOR BOOKS (Optional but Recommended)
-- ============================================================================
-- Uncomment if you want full-text search capabilities
-- ALTER TABLE Books ADD FULLTEXT INDEX ft_books_search (Title, Author, ISBN, Category, Publisher);

-- ============================================================================
-- SAMPLE DATA (Optional - Uncomment to add test data)
-- ============================================================================
/*
-- Sample Admin User
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate)
VALUES ('Admin', 'User', 'admin@library.com', '$2a$10$...', 'Admin', NOW());

-- Sample Library Staff User
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate)
VALUES ('Staff', 'User', 'staff@library.com', '$2a$10$...', 'LibraryStaff', NOW());

-- Sample Member User
INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, CreatedDate)
VALUES ('Member', 'User', 'member@library.com', '$2a$10$...', 'Member', NOW());
*/

-- ============================================================================
-- VERIFICATION QUERIES (Run these to verify schema)
-- ============================================================================
-- SELECT COUNT(*) as TableCount FROM INFORMATION_SCHEMA.TABLES 
-- WHERE TABLE_SCHEMA = DATABASE() 
-- AND TABLE_NAME IN ('Users', 'Members', 'Books', 'Transactions', 'Reservations', 'Fines', 'FinePayments', 'FineAdjustments', 'Inventory', 'BookCopies', 'Settings');

-- SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
-- FROM INFORMATION_SCHEMA.COLUMNS 
-- WHERE TABLE_SCHEMA = DATABASE() 
-- ORDER BY TABLE_NAME, ORDINAL_POSITION;

-- ============================================================================
-- END OF SCHEMA
-- ============================================================================
