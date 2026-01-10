-- ============================================================================
-- FIX USER CREDENTIALS - Password Hashes and Email Formats
-- ============================================================================
-- This script fixes:
-- 1. Password hashes (currently plain text, needs proper hashing)
-- 2. Library Staff email format to match database (@library.umindanao.edu.ph)
-- ============================================================================
-- NOTE: Password hashing must be done through the application's PasswordHasher
-- This script provides the structure, but actual hashes need to be generated
-- by the C# application using PasswordHasher.HashPassword()
-- ============================================================================

-- ============================================================================
-- STEP 1: Update Library Staff Email Format
-- ============================================================================
-- Change from: [name].lstaff@umindanao.edu.ph
-- Change to: [name]@library.umindanao.edu.ph (to match your database)

UPDATE Users 
SET Email = REPLACE(Email, '.lstaff@umindanao.edu.ph', '@library.umindanao.edu.ph')
WHERE Role = 'LibraryStaff' 
AND Email LIKE '%.lstaff@umindanao.edu.ph';

-- Or update specific users to match your database:
-- UPDATE Users SET Email = 'msantos@library.umindanao.edu.ph' WHERE FirstName = 'Maria' AND LastName = 'Santos';
-- UPDATE Users SET Email = 'jdelacruz@library.umindanao.edu.ph' WHERE FirstName = 'John' AND LastName = 'Dela Cruz';
-- UPDATE Users SET Email = 'arobles@library.umindanao.edu.ph' WHERE FirstName = 'Ana' AND LastName = 'Robles';
-- UPDATE Users SET Email = 'kreyes@library.umindanao.edu.ph' WHERE FirstName = 'Kevin' AND LastName = 'Reyes';

-- ============================================================================
-- STEP 2: Password Hash Information
-- ============================================================================
-- IMPORTANT: The PasswordHasher uses PBKDF2 with format: iterations:salt:hash
-- Format: "10000:base64salt:base64hash"
-- 
-- The application's PasswordHasher.Verify() has a fallback that allows
-- plain text passwords if the hash format is invalid, BUT this is not secure.
--
-- To properly hash passwords:
-- 1. Use the application's "Forgot Password" feature, OR
-- 2. Create a temporary C# console app to hash passwords, OR
-- 3. Use the application's user creation form which hashes passwords automatically
-- ============================================================================

-- ============================================================================
-- STEP 3: Temporary Fix - Keep Plain Text (NOT RECOMMENDED FOR PRODUCTION)
-- ============================================================================
-- If you need immediate access, the application will accept plain text passwords
-- due to the fallback in PasswordHasher.Verify(), but this is INSECURE.
--
-- Current passwords in your database (from the image):
-- Admin users: Adminadam@123, Adminjane@123
-- Library Staff: staffmsantos@123, staffjdelacruz@456, staffarobles@123, staffkreyes@456
-- ============================================================================

-- Verify current users and their email formats
SELECT 
    UserID,
    FirstName,
    LastName,
    Email,
    Role,
    CASE 
        WHEN PasswordHash LIKE '%:%:%' THEN 'Hashed (Proper Format)'
        ELSE 'Plain Text (Needs Hashing)'
    END as PasswordStatus,
    LENGTH(PasswordHash) as PasswordLength
FROM Users
ORDER BY Role, Email;

-- ============================================================================
-- STEP 4: Recommended Approach - Use Application to Hash Passwords
-- ============================================================================
-- The best way to fix passwords is to:
-- 1. Use the application's "Forgot Password" feature for each user
-- 2. Or create new users through the application (which hashes automatically)
-- 3. Or create a simple C# script to hash passwords:
--
-- using Project5LMS.Helpers;
-- string hashed = PasswordHasher.HashPassword("Adminadam@123");
-- Console.WriteLine(hashed);
-- ============================================================================

-- ============================================================================
-- STEP 5: Update Sample Data Format (for future use)
-- ============================================================================
-- If you want to update the sample data file to match your database format:
-- Library Staff emails should be: [name]@library.umindanao.edu.ph
-- Example: msantos@library.umindanao.edu.ph (not msantos.lstaff@umindanao.edu.ph)
-- ============================================================================

-- ============================================================================
-- VERIFICATION QUERIES
-- ============================================================================

-- Check Admin emails (should end with @admin.umindanao.edu.ph)
SELECT UserID, FirstName, LastName, Email, Role 
FROM Users 
WHERE Role = 'Admin'
ORDER BY Email;

-- Check Library Staff emails (should end with @library.umindanao.edu.ph)
SELECT UserID, FirstName, LastName, Email, Role 
FROM Users 
WHERE Role = 'LibraryStaff'
ORDER BY Email;

-- Check password hash format
SELECT 
    UserID,
    Email,
    Role,
    LEFT(PasswordHash, 50) as PasswordHashPreview,
    CASE 
        WHEN PasswordHash LIKE '10000:%:%' THEN 'Properly Hashed'
        WHEN PasswordHash LIKE '%:%:%' THEN 'Hashed (Different Format)'
        ELSE 'Plain Text (INSECURE)'
    END as HashStatus
FROM Users
ORDER BY Role, Email;

-- ============================================================================
-- END OF FIX SCRIPT
-- ============================================================================
