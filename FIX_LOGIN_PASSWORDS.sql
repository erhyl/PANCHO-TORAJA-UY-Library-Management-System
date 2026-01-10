-- ============================================================================
-- FIX LOGIN PASSWORDS - Set Working Plain Text Passwords
-- ============================================================================
-- The application has a fallback that accepts plain text passwords
-- if the hash format is invalid. This script sets simple passwords that work.
-- ============================================================================
-- IMPORTANT: After login, use "Forgot Password" to properly hash passwords
-- ============================================================================

-- Update Maria's password (Admin)
UPDATE Users 
SET PasswordHash = 'admin123'
WHERE Email = 'maria@admin.umindanao.edu.ph';

-- Update all Admin passwords
UPDATE Users 
SET PasswordHash = 'admin123'
WHERE Role = 'Admin';

-- Update all Library Staff passwords  
UPDATE Users 
SET PasswordHash = 'staff123'
WHERE Role = 'LibraryStaff';

-- Update all Member passwords
UPDATE Users 
SET PasswordHash = 'member123'
WHERE Role = 'Member';

-- ============================================================================
-- LOGIN CREDENTIALS (Use these to login)
-- ============================================================================
-- Admin:
--   Email: maria@admin.umindanao.edu.ph
--   Password: admin123
--
-- Other Admins:
--   Email: juan@admin.umindanao.edu.ph
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

-- Verify updates
SELECT 
    Email,
    Role,
    PasswordHash as CurrentPassword,
    CASE 
        WHEN PasswordHash LIKE '%:%:%' THEN 'Hashed Format'
        ELSE 'Plain Text (Will Work)'
    END as Status
FROM Users
ORDER BY Role, Email;
