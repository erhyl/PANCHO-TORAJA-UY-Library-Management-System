using Project5LMS.Helpers;
namespace Project5LMS.Helpers
{
    public static class AccessControlHelper
    {
        public static bool HasRole(string requiredRole)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Role) || string.IsNullOrWhiteSpace(requiredRole))
                return false;
            return CurrentUser.Role.Equals(requiredRole, System.StringComparison.OrdinalIgnoreCase);
        }
        public static bool HasAnyRole(params string[] requiredRoles)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Role) || requiredRoles == null || requiredRoles.Length == 0)
                return false;
            foreach (string role in requiredRoles)
            {
                if (CurrentUser.Role.Equals(role, System.StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        public static bool IsAdmin()
        {
            return HasRole("Admin");
        }
        public static bool IsLibraryStaff()
        {
            return HasAnyRole("LibraryStaff", "Admin");
        }
        public static bool IsMember()
        {
            return HasRole("Member");
        }
        public static void RequireRole(string requiredRole)
        {
            if (!HasRole(requiredRole))
            {
                throw new System.UnauthorizedAccessException($"Access denied. Required role: {requiredRole}");
            }
        }
        public static void RequireAnyRole(params string[] requiredRoles)
        {
            if (!HasAnyRole(requiredRoles))
            {
                throw new System.UnauthorizedAccessException($"Access denied. Required one of the following roles: {string.Join(", ", requiredRoles)}");
            }
        }
    }
}