-- ============================================================================
-- SET WORKING PASSWORDS FOR LOGIN
-- ============================================================================
-- This script sets plain text passwords that will work with the application's
-- fallback mechanism. You can then use "Forgot Password" to properly hash them.
-- ============================================================================

-- Set passwords for Admin users
-- Password: "admin123" (you can change this)
UPDATE Users 
SET PasswordHash = 'admin123'
WHERE Role = 'Admin' 
AND Email LIKE '%@admin.umindanao.edu.ph';

-- Set passwords for Library Staff users
-- Password: "staff123" (you can change this)
UPDATE Users 
SET PasswordHash = 'staff123'
WHERE Role = 'LibraryStaff' 
AND Email LIKE '%@library.umindanao.edu.ph';

-- Set passwords for Member users
-- Password: "member123" (you can change this)
UPDATE Users 
SET PasswordHash = 'member123'
WHERE Role = 'Member' 
AND Email LIKE '%.member@umindanao.edu.ph';

-- ============================================================================
-- VERIFY PASSWORDS
-- ============================================================================
SELECT 
    UserID,
    FirstName,
    LastName,
    Email,
    Role,
    CASE 
        WHEN PasswordHash LIKE '%:%:%' THEN 'Hashed'
        ELSE 'Plain Text (Will Work)'
    END as PasswordType
FROM Users
ORDER BY Role, Email;

-- ============================================================================
-- LOGIN CREDENTIALS
-- ============================================================================
-- Admin Users:
--   Email: maria@admin.umindanao.edu.ph
--   Password: admin123
--
-- Library Staff:
--   Email: [name]@library.umindanao.edu.ph
--   Password: staff123
--
-- Members:
--   Email: [name].member@umindanao.edu.ph
--   Password: member123
-- ============================================================================
