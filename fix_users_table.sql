-- Fix Users table to add Email column
-- The CREATE TABLE statement is missing the Email column that's used in INSERT statements

ALTER TABLE Users ADD COLUMN Email VARCHAR(255) AFTER LastName;

-- Fix the INSERT statements to match the correct column order
-- Note: The original INSERT has inconsistent data - some rows have 3 values, some have 4
-- Here's the corrected version:

DELETE FROM Users; -- Clear existing data if any

INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role)
VALUES
-- Admin Accounts
('Adam', 'Doe', 'adamdoe@admin.umindanao.edu.ph', 'Adminadam@123', 'Admin'),
('Jane', 'Doe', 'janedoe@admin.umindanao.edu.ph', 'Adminjane@123', 'Admin'),

-- Librarian Accounts (Note: Role should be 'LibraryStaff' based on your schema, or add 'Librarian' to ENUM)
('Maria', 'Santos', 'msantos@library.umindanao.edu.ph', 'lib123', 'LibraryStaff'),
('John', 'Dela Cruz', 'jdelacruz@library.umindanao.edu.ph', 'lib456', 'LibraryStaff'),

-- Library Staff Accounts
('Ana', 'Robles', 'arobles@library.umindanao.edu.ph', 'staff123', 'LibraryStaff'),
('Kevin', 'Reyes', 'kreyes@library.umindanao.edu.ph', 'staff456', 'LibraryStaff'),

-- Member Accounts
('Erika', 'Mendoza', 'emendoza@student.umindanao.edu.ph', 'member123', 'Member'),
('Paul', 'Bautista', 'pbautista@student.umindanao.edu.ph', 'member456', 'Member'),
('Carla', 'Ramirez', 'cramirez@student.umindanao.edu.ph', 'member789', 'Member');

