-- ============================================================================
-- UPDATE ADMIN EMAILS TO CORRECT FORMAT
-- ============================================================================
-- This script updates existing admin user emails to match the required format
-- Required format: [name]@admin.umindanao.edu.ph
-- ============================================================================

-- Update Admin Emails
UPDATE Users 
SET Email = REPLACE(Email, '.admin@umindanao.edu.ph', '@admin.umindanao.edu.ph')
WHERE Role = 'Admin' 
AND Email LIKE '%.admin@umindanao.edu.ph';

-- Verify the updates
SELECT UserID, FirstName, LastName, Email, Role 
FROM Users 
WHERE Role = 'Admin'
ORDER BY Email;

-- ============================================================================
-- If you need to update specific admin emails manually:
-- ============================================================================
-- UPDATE Users SET Email = 'juan@admin.umindanao.edu.ph' WHERE Email = 'juan.admin@umindanao.edu.ph';
-- UPDATE Users SET Email = 'maria@admin.umindanao.edu.ph' WHERE Email = 'maria.admin@umindanao.edu.ph';
-- UPDATE Users SET Email = 'carlos@admin.umindanao.edu.ph' WHERE Email = 'carlos.admin@umindanao.edu.ph';
-- ============================================================================
