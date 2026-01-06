-- ============================================
-- Database Indexes for Library Management System
-- ============================================
-- These indexes optimize search performance to meet the <2 seconds target
-- ============================================

-- Books table indexes for search performance
CREATE INDEX IF NOT EXISTS idx_books_title ON Books(Title);
CREATE INDEX IF NOT EXISTS idx_books_author ON Books(Author);
CREATE INDEX IF NOT EXISTS idx_books_isbn ON Books(ISBN);
CREATE INDEX IF NOT EXISTS idx_books_accessionno ON Books(AccessionNo);
CREATE INDEX IF NOT EXISTS idx_books_category ON Books(Category);
CREATE INDEX IF NOT EXISTS idx_books_status ON Books(Status);

-- Members table indexes
-- Optimizes member searches by name, email, and type
CREATE INDEX IF NOT EXISTS idx_members_email ON Members(Email);
CREATE INDEX IF NOT EXISTS idx_members_firstname ON Members(FirstName);
CREATE INDEX IF NOT EXISTS idx_members_lastname ON Members(LastName);
CREATE INDEX IF NOT EXISTS idx_members_type ON Members(Type);

-- Transactions table indexes
-- Critical for circulation operations (<10 seconds target)
CREATE INDEX IF NOT EXISTS idx_transactions_memberid ON Transactions(MemberID);
CREATE INDEX IF NOT EXISTS idx_transactions_bookid ON Transactions(BookID);
CREATE INDEX IF NOT EXISTS idx_transactions_status ON Transactions(Status);
CREATE INDEX IF NOT EXISTS idx_transactions_duedate ON Transactions(DueDate);
CREATE INDEX IF NOT EXISTS idx_transactions_borrowdate ON Transactions(BorrowDate);

-- Reservations table indexes
CREATE INDEX IF NOT EXISTS idx_reservations_memberid ON Reservations(MemberID);
CREATE INDEX IF NOT EXISTS idx_reservations_bookid ON Reservations(BookID);
CREATE INDEX IF NOT EXISTS idx_reservations_status ON Reservations(Status);

-- Users table indexes
-- Optimizes authentication and user management
CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);
CREATE INDEX IF NOT EXISTS idx_users_role ON Users(Role);

